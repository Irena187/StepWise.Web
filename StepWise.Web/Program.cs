using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Data.Repository;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core;
using StepWise.Services.Core.Interfaces;
using StepWise.Services.Mapping;
using StepWise.Web.ViewModels;
using StepWise.Web.Infrastructure.Extensions;
using System.Drawing.Text;
using StepWise.Data.Seeding.Interfaces;
using StepWise.Data.Seeding;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services
    .AddDbContext<StepWiseDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;

        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<StepWiseDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddUserManager<UserManager<ApplicationUser>>();

builder.Services.AddRepositories(typeof(ICareerPathRepository).Assembly);
builder.Services.AddUserDefinedServices(typeof(ICareerPathService).Assembly);
builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.SeedDefaultIdentity();

app.UseAuthentication();
app.UseAuthorization();

app.UseAdminRedirection();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();





