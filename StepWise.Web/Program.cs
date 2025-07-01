using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services
    .AddDbContext<StepWiseDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity with Guid keys to match your DbContext 
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
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
.AddEntityFrameworkStores<StepWiseDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();