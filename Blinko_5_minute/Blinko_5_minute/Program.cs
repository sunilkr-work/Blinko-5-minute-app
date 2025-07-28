
using Blinko_5_minute.context;
using Blinko_5_minute.darkStore;
using Blinko_5_minute.inventoryStore;
using Blinko_5_minute.manager;
using Blinko_5_minute.middleware;
using Blinko_5_minute.model;
using Blinko_5_minute.replineshStrategy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blinko_5_minute
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddDbContext<BlinkoDBContext>(OptionsBuilderConfigurationExtensions =>
            //{
            //    OptionsBuilderConfigurationExtensions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            //});
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey))


                    };


                });


            builder.Services.AddDbContext<BlinkoDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),

                    sqlOption => sqlOption.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null


                        )


                    );
                Options.EnableSensitiveDataLogging();
                Options.LogTo(Console.WriteLine, LogLevel.Information);
                Options.UseQueryTrackingBehavior(queryTrackingBehavior: QueryTrackingBehavior.NoTracking);
            }

            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy => policy.WithOrigins("http://localhost:5173")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            builder.Services.AddScoped<Cart>();
            builder.Services.AddScoped<Product>();

            builder.Services.AddTransient<IInventoryStore, DbInventoryStore>(sp =>
            {
                var dbContext = sp.GetRequiredService<BlinkoDBContext>();
                return new DbInventoryStore(dbContext);
            });

            builder.Services.AddScoped<InventoryManager>( sp =>
            {
                var inventoryStore = sp.GetRequiredService<IInventoryStore>();
                return new InventoryManager(inventoryStore);
            });
            builder.Services.AddTransient<IReplenishStrategy, ThresholdReplenishStrategy>( sp =>
            {
                int threshold = int.Parse(sp.GetRequiredService<IConfiguration>()["ReplenishThreshold:default"]?? "10");
                var inventoryManager = sp.GetRequiredService<InventoryManager>();
                return new ThresholdReplenishStrategy(threshold, inventoryManager);
            });

            builder.Services.AddScoped<DeliveryManager>(sp =>
            {
                var dbContext = sp.GetRequiredService<BlinkoDBContext>();
                return new DeliveryManager(dbContext);
            });
            builder.Services.AddSingleton<DarkStoreManager>(sp =>
            {
                var replenish = sp.GetRequiredService<IReplenishStrategy>();
                var appsetting =  sp.GetRequiredService<IConfiguration>();
                var dbContext = sp.GetRequiredService<BlinkoDBContext>();
                var deliveryManager = sp.GetRequiredService<DeliveryManager>();
                var name = appsetting["ClassName:DarkStoreName"] ?? "Default Dark Store";
                return new DarkStoreManager(name, replenish,deliveryManager, dbContext);
            });

            builder.Services.AddSingleton<OrderManager>(sp =>
            {
                var deliveryManager = sp.GetRequiredService<DeliveryManager>();
                var dbContext = sp.GetRequiredService<BlinkoDBContext>();
                var darkStoreManager = sp.GetRequiredService<DarkStoreManager>();
                return new OrderManager(deliveryManager, dbContext, darkStoreManager);
            });

            var app = builder.Build();
            app.UseCors("AllowVueApp");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var rateLimiter = app.Services.GetRequiredService<IOptions<RequestCounter>>().Value;

            TimeSpan timeSpan = TimeSpan.FromSeconds(rateLimiter.RetryTimeInSec);
            app.UseHttpsRedirection();
            app.UseMiddleware<RateLimiterMiddleware>(timeSpan, rateLimiter.Count);
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            /////////
            Routes.MapRoute(
                name: "sk",
                


                );

            app.Run();
        }
    }
}
