
using Blinko_5_minute.darkStore;
using Blinko_5_minute.manager;

namespace Blinko_5_minute
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            var app = builder.Build();
            app.UseCors("AllowVueApp");

            builder.Services.AddTransient<DarkStore>();

            builder.Services.AddSingleton<DarkStoreManager>(sp =>
            {
                var darkStore = sp.GetRequiredService<DarkStore>();
                var appsetting =  sp.GetRequiredService<IConfiguration>();
                var name = appsetting["DarkStoreName"] ?? "Default Dark Store";
                return new DarkStoreManager(darkStore, name);
            }
                );
            builder.Services.AddSingleton<OrderManager>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
