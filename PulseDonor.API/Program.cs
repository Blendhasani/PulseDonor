using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.Authentication.Interfaces;
using PulseDonor.Application.Authentication.Services;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Application.City.Services;
using PulseDonor.Core;
using PulseDonor.Infrastructure.Models;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.User.Interfaces;
using PulseDonor.Application.User.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DevPulsedonorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataProtection();  // Ensure DataProtection is registered

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<DevPulsedonorContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<User, IdentityRole<int>>()
//	.AddEntityFrameworkStores<DevPulsedonorContext>()
//	.AddDefaultTokenProviders();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICityAPIService, CityAPIService>();
builder.Services.AddScoped<IUserAPIService, UserAPIService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
