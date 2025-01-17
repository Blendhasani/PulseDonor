﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PulseDonor.Application.Authentication.Commands;
using PulseDonor.Application.Authentication.DTO;
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
using PulseDonor.Application.Enums;
namespace PulseDonor.Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly DevPulsedonorContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;


		//private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
		private readonly IConfiguration _configuration;

		//public AuthService(DevPulsedonorContext context, IPasswordHasher<ApplicationUser> passwordHasher, IConfiguration configuration)
		public AuthService(DevPulsedonorContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
			//_passwordHasher = passwordHasher;
			_configuration = configuration;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<string> SignupAsync(SignupCommand command)
        {
            var dto = command.SignupDto;
            //var user = new PulseDonor.Infrastructure.Models.User
            var user = new ApplicationUser
			{
                UserName = dto.Email.Trim(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                GenderId = dto.GenderId,
                EmailConfirmed = true,
                BloodTypeId = dto.BloodTypeId,
                InsertedDate = DateTime.UtcNow,
                IsActive = true
                //PasswordHash = _passwordHasher.HashPassword(null!, dto.Password)
            };
			var result = await _userManager.CreateAsync(user, dto.Password);

			if (!result.Succeeded)
			{
				return "Request Failed";
			}

			await _userManager.AddToRoleAsync(user, ApplicationRoles.User.ToString());

			return "Signup successful";
        }

        public async Task<string> LoginAsync(LoginCommand command)
        {


			var user = await _userManager.Users.Where(x => x.Email.Equals(command.Email))
													 .FirstOrDefaultAsync();
			if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if(user.IsBlocked == true)
            {
				throw new UnauthorizedAccessException("You are blocked.");
			}

			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
			if (!signInResult.Succeeded)
			{
				throw new UnauthorizedAccessException("Invalid email or password.");

			}

			return "Bearer " + GenerateJwtToken(user);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("QFIHUFQIUWHHIUGY$$##352763256gJDGJGHJAD");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("BloodTypeId", user.BloodTypeId.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = "https://localhost:7269",
                Audience = "https://localhost:7269", 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

		public async Task<bool> LogoutAsync()
		{
			await _signInManager.SignOutAsync();
            return true;
		}
	}
}
