using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(15);
            options.Cookie.IsEssential = true;
        });
        //connection db
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectDb"]);
        });
            var app = builder.Build();
           app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
        app.MapControllerRoute(
               name: "Areas",
               pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

        app.UseAuthorization();
        app.MapControllerRoute(
               name: "category",
               pattern: "category/{Slug?}",
        defaults: new {controller="Brand",action="Index"});

        app.UseAuthorization();
        app.MapControllerRoute(
               name: "brand",
               pattern: "brand/{Slug?}",
        defaults: new { controller = "Category", action = "Index" });

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
		
		//seeding data
		var context =app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
        SeedData.seedingData(context);

            app.Run();
            
        }
}
