using PicturyMarket.DAL;
using PicturyMarket.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using PicturyMarket.DAL.Repositories;
using PicturyMarket.Service.Interfaces;
using PicturyMarket.Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PicturyMarketDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPicturyRepository, PicturyRepository>();
builder.Services.AddScoped<IPicturyService, PicturyService>();

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
    pattern: "{controller=Home}/{action=Index}/{Id?}");

app.Run();
