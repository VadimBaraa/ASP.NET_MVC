using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC.Models.Db;
using ASP.NET_MVC.Services;
using ASP.NET_MVC.Middleware;

public class Startup
{
    public IConfiguration Configuration { get; }

    // Конструктор, который принимает IConfiguration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
   
   public void ConfigureServices(IServiceCollection services)
   {
       services.AddDbContext<BlogContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
     
       services.AddScoped<IBlogRepository, BlogRepository>();
       services.AddScoped<IRequestRepository, RequestRepository>();
       services.AddSingleton<ILogService, LogService>();
       services.AddControllersWithViews();
       services.AddLogging();
   }
 
   // Метод вызывается средой ASP.NET.
   
   public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
   {
       Console.WriteLine($"Launching project from: {env.ContentRootPath}");

       if (env.IsDevelopment())
       {
           app.UseDeveloperExceptionPage();
       }
       else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
       
 
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseLoggingMiddleware();

        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute(
            name: "log",
            pattern: "Log/{action=Index}/{id?}",
            defaults: new { controller = "Log" }
            );
            
            
        });
        
        // Все прочие страницы имеют отдельные обработчики
        app.Map("/about", appBuilder => About(appBuilder, env));

        app.Map("/config", appBuilder => Config(appBuilder, env));
        
       // Обработчик для ошибки "страница не найдена"
       
   }
    private static void About(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project");
        });
    }
        
        /// <summary>
        ///  Обработчик для главной страницы
        /// </summary>
    private static void Config(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}");
        });
    }

    

}