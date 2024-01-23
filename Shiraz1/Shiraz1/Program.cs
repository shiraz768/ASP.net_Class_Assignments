using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Shiraz1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
     .AddRoleManager<RoleManager<IdentityRole>>()
        .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddAuthentication();
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=website}/{action=Index}/{id?}");

app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await rolemanager.RoleExistsAsync(role))
        {
            await rolemanager.CreateAsync(new IdentityRole(role));
        }
    }


}
using (var scope= app.Services.CreateScope())
{
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var email = "admin@admin.com";
    var password = "Test1234,";
    var adminUser = await usermanager.FindByEmailAsync(email);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        await usermanager.CreateAsync(adminUser, password);
        await usermanager.AddToRoleAsync(adminUser, "Admin");
    }
}
app.Run();
