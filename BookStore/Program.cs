using BookStore.Data.Context;
using BookStore.Data.Entities;
using BookStore.Data.Extensions;
using BookStore.Service.Extensions;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions
{
    PositionClass = ToastPositions.TopRight,
    TimeOut = 5000,
});

// Mevcut extension metotlarýnýz
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();

// Burada HttpClientFactory'i ekliyoruz
builder.Services.AddHttpClient();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    // test asamasindaki zorlugu engellemek icin bazi ayarlar. daha sonradan degistirilmeli.
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
})
.AddRoleManager<RoleManager<AppRole>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rotasyon iþlemleri
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
