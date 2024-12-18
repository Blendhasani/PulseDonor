using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PulseDonor.Application.Authentication.Interfaces;
using PulseDonor.Application.Authentication.Services;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Application.City.Services;
using PulseDonor.Core;
using PulseDonor.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DevPulsedonorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataProtection();  // Ensure DataProtection is registered

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<DevPulsedonorContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<ICityService, CityService>();

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
