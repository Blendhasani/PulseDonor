using FluentValidation.AspNetCore;
using FluentValidation;
using PulseDonor.MVC.City.Interfaces;
using PulseDonor.MVC.City.Services;
using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.Helper.Services;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.User.Interfaces;
using PulseDonor.MVC.User.Services;
using PulseDonor.MVC.Auth.Interfaces;
using PulseDonor.MVC.Auth.Services;
using PulseDonor.MVC.User.Commands;
using PulseDonor.MVC.Auth.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddControllersWithViews()
	.AddRazorRuntimeCompilation()
	.AddCookieTempDataProvider()
	.AddMvcOptions(options =>
	{
		options.ModelMetadataDetailsProviders.Clear(); // Clear DataAnnotations fallback
	});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddCityCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<AddUserCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<EditUserCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginCommand>();

#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IApiClientHelper, ApiClientHelper>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

builder.Services.AddAuthentication("Cookies")
	.AddCookie("Cookies", options =>
	{
		options.LoginPath = "/Auth/Login";
		options.LogoutPath = "/Auth/Logout";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
