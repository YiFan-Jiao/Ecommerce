using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Data;
using Ecommerce.SeedData;
using Ecommerce.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext") ?? throw new InvalidOperationException("Connection string 'EcommerceContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<Products>), typeof(ProductsRepo));
builder.Services.AddScoped(typeof(IRepository<Country>), typeof(CountryRepo));
builder.Services.AddScoped(typeof(IRepository<Cart>), typeof(CartRepo));
builder.Services.AddScoped(typeof(IRepository<Order>), typeof(OrderRepo));


var app = builder.Build();

//seedData
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedData.Initialize(services);
}

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
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
