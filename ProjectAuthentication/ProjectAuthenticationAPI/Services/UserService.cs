using BCrypt.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using ProjectAuthenticationAPI.Data;
using ProjectAuthenticationAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectAuthenticationAPI.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailOrUsername(string email, string username = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = email;
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.UserName == username);
        }

        public async Task SignUp(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> SignIn(string EmailOrUsername, string Password)
        {
            User user = await GetUserByEmailOrUsername(EmailOrUsername);
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(Password, user.Password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("jiEOLH25uxLKhZHEALSutJITQimUYoUO");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public async Task<User> GetUserDetails(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }
    }
}
