using ASP.NET_MVC.Models.Db;
using ASP.NET_MVC.Services;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET_MVC.Middleware;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
   private readonly RequestDelegate _next = next;
   
   /// <summary>
   ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
   /// </summary>
     
   /// <summary>
   ///  Необходимо реализовать метод Invoke  или InvokeAsync
   /// </summary>
   public async Task InvokeAsync(HttpContext context)
    {
        var requestRepository = context.RequestServices.GetRequiredService<IRequestRepository>();

        // Создание объекта Request
        var request = new Request()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Url = context.Request.Path.Value
        };

        // Логирование запроса
        await requestRepository.AddRequest(request);

        logger.LogInformation("Здесь логирование с параметром {@request}", request);
        var message = $"User visited page: {context.Request.Path.Value} at {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}";

        // Вызов следующего middleware в конвейере
        await _next(context);
    }

    
}


