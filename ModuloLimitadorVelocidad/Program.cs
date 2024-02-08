using Microsoft.EntityFrameworkCore;
using ModuloLimitadorVelocidad.Data;
using Microsoft.AspNetCore.Identity;
using ModuloLimitadorVelocidad.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ModuloLimitadorVelocidadDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ModuloLimitadorVelocidadDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddDbContext<ModuloLimitadorVelocidadDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MVC_DbContext>(options=>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("Mvc_DbConnectionString")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ModuloLimitadorVelocidadDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options => 
{
    options.Password.RequireUppercase = false;
});

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
