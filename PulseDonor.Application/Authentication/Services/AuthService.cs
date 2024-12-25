using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PulseDonor.Application.Authentication.Commands;
using PulseDonor.Application.Authentication.Interfaces;
using PulseDonor.Core;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly DevPulsedonorContext _context;
        private readonly IPasswordHasher<PulseDonor.Infrastructure.Models.User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(DevPulsedonorContext context, IPasswordHasher<PulseDonor.Infrastructure.Models.User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string> SignupAsync(SignupCommand command)
        {
            var dto = command.SignupDto;
            var user = new PulseDonor.Infrastructure.Models.User
			{
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                GenderId = dto.GenderId,
                //EmailConfirmed = true,
                BloodTypeId = dto.BloodTypeId,
                InsertedDate = DateTime.UtcNow,
                IsActive = true,
                PasswordHash = _passwordHasher.HashPassword(null!, dto.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return "Signup successful";
        }

        public async Task<string> LoginAsync(LoginCommand command)
        {
            var dto = command.LoginDto;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !_passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, dto.Password).Equals(PasswordVerificationResult.Success))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(PulseDonor.Infrastructure.Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("QFIHUFQIUWHHIUGY$$##352763256gJDGJGHJAD");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "https://localhost:7269",
                Audience = "https://localhost:7269", 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
