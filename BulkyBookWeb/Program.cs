using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// this tells the application to use the ApplicationDbContext class for creation of the database.
// first install EntityFrameworkCore Sqlserver... GetConnectionString method only works for the ConnectionStrings in appsettings
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))) ;


// Singleton vs Scoped vs transient 
// Transient objects are always different; a new instance is provided to every controller and every service.
// Scoped objects are the same within a request, but different across different requests.
// Singleton objects are the same for every object and every request.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

/*
 when we want to register anything with our dependeny injection container  we will be doing it right here
lets say if we want to register our database or email or anything else,
we will have to do that between the builder and before we call build on that builder object
right now we have got AddControllersWithViews services added by default.
if you were to use razor pages service would be different
 */

var app = builder.Build();



// order of the pipeline is extremely important, this is how the request will be passed.
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

//app.UseAuthentication  middleware should always come before user authentication, 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
