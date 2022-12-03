using Microsoft.EntityFrameworkCore;
using ProductApp.Extensions;
using Repositories.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.ConfigureDbContext();

builder.Services.AddDbContext<RepositoryContext>(options => 
options.UseSqlServer(builder.Configuration
.GetConnectionString("sqlconnection"),
    prj => prj.MigrationsAssembly("ProductApp")
));

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

app.UseEndpoints(endpoints=>
{
    endpoints.MapAreaControllerRoute(
        name:"Admin",
        areaName:"Admin",
        pattern:"Admin/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
        name:"default",
        pattern:"{controller=Home}/{action=Index}/{id?}"
        );
});

app.Run();