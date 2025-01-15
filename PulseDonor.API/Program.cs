using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using PulseDonor.Application.Authentication.Interfaces;
using PulseDonor.Application.Authentication.Services;
using PulseDonor.Application.City.Interfaces;
using PulseDonor.Application.City.Services;
using PulseDonor.Core;
using PulseDonor.Infrastructure.Models;
using PulseDonor.Application.City.Commands;
using PulseDonor.Application.User.Interfaces;
using PulseDonor.Application.User.Services;
using PulseDonor.Application.CurrentUser.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PulseDonor.Application.User.Commands;
using PulseDonor.Infrastructure.Authentication.Database.Models;
using PulseDonor.Infrastructure.Authentication.Database;
using PulseDonor.Application.Data.Interfaces;
using PulseDonor.Application.Data.Services;
using PulseDonor.Application.Account.Interfaces;
using PulseDonor.Application.Account.Services;
using PulseDonor.Application.Account.Commands;
using PulseDonor.Application.BloodDonationPoint.Services;
using PulseDonor.Application.BloodDonationPoint.Interfaces;
using PulseDonor.Application.Hospitals.Interfaces;
using PulseDonor.Application.Hospitals.Services;
using PulseDonor.Application.BloodRequest.Interfaces;
using PulseDonor.Application.BloodRequest.Services;
using PulseDonor.Application.HallOfFame.Interfaces;
using PulseDonor.Application.HallOfFame.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DevPulsedonorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDataProtection();

// Configure Identity
//builder.Services.AddIdentityCore<ApplicationUser>()
//    .AddEntityFrameworkStores<DevPulsedonorContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<ApplicationUser>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
	//TODO password requirements to fit your needs
	options.SignIn.RequireConfirmedAccount = false;
	//options.Lockout.MaxFailedAccessAttempts = 5;
	//options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
	options.Password.RequiredLength = 6;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.User.RequireUniqueEmail = true;
})
		   .AddRoles<ApplicationRole>()
		   .AddEntityFrameworkStores<ApplicationDbContext>()
		   .AddDefaultTokenProviders();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Remove the "Bearer " prefix if it exists
            
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception);
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7269",
        ValidAudience = "https://localhost:7269",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("QFIHUFQIUWHHIUGY$$##352763256gJDGJGHJAD"))
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddUserAPICommand>();
builder.Services.AddValidatorsFromAssemblyContaining<EditUserAPICommand>();
builder.Services.AddValidatorsFromAssemblyContaining<EditAccountOverviewCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<AddBloodRequestCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<EditAccountBloodRequestCommand>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICityAPIService, CityAPIService>();
builder.Services.AddScoped<IUserAPIService, UserAPIService>();
builder.Services.AddScoped<IDataAPIService, DataAPIService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBloodDonationPointsService, BloodDonationPointsService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IBloodRequestService, BloodRequestService>();
builder.Services.AddScoped<IHallOfFameService, HallOfFameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ensure the middleware order
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
