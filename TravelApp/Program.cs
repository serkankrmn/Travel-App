using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using TravelApp.ConfigurationOptions;
using TravelApp.Data;
using TravelApp.HelperService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(ops => {
    ops.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"));
});

//// Session ayarlarý ESKÝ
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});


//Cookie/Session new
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddSession();


// dbcontext'ý DI Container'a ekledik.
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IProductImageService, ProductFileImageService>();
builder.Services.AddScoped<IMailSender, SmtpMailSender>();
builder.Services.AddScoped<CustomerServiceHelper>();

builder.Services.Configure<MailOptions>(builder.Configuration.GetSection(MailOptions.MailSettings));

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

app.UseSession(); // Bu satýrý ekleyin

app.UseAuthentication();
app.UseAuthorization();

// Admin tarafýnýn routing kuralý.
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


