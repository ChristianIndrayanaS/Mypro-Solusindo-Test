using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Digimatika.Data;
using Digimatika.Areas.Identity.Data;
using Digimatika.Repository.Interface.Interface;
using Digimatika.Repository.Repository;
using Digimatika.Services.Interface.Interface;
using Digimatika.Services.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DigimatikaDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DigimatikaDbContextConnection' not found.");

builder.Services.AddDbContext<DigimatikaDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DigimatikaUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DigimatikaDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();

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
