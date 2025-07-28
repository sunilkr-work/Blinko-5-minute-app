using Blinko_5_minute.context;
using Blinko_5_minute.model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blinko_5_minute.service
{
    public class AuthenticationService
    {
        private JwtSettings _jwtSettings;
        private BlinkoDBContext _dbContext;

        public AuthenticationService(IOptions<JwtSettings> jwtSettings, BlinkoDBContext blinkoDBContext)
        {
            _jwtSettings = jwtSettings.Value;
            _dbContext = blinkoDBContext;
        }

        public async Task<string> GenerateJwtToken(User user)
        {

            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims : claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Expire)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task RegisterUser(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> TryToGetUser(int id)
        {
            User user = await _dbContext.Users.FindAsync(id);
            return user;

        }

    }
}
