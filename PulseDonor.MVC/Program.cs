using FluentValidation.AspNetCore;
using FluentValidation;
using PulseDonor.MVC.City.Interfaces;
using PulseDonor.MVC.City.Services;
using PulseDonor.MVC.Helper.Interfaces;
using PulseDonor.MVC.Helper.Services;
using PulseDonor.MVC.City.Commands;
using PulseDonor.MVC.User.Interfaces;
using PulseDonor.MVC.User.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
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

#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IApiClientHelper, ApiClientHelper>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
