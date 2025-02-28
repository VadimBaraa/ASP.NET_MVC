using ASP.NET_MVC.Models.Db;
using ASP.NET_MVC.Services;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


public class LoggingMiddleware
{
   private readonly RequestDelegate _next;
   private readonly ILogService _logService;

  
   /// <summary>
   ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
   /// </summary>
   public LoggingMiddleware(RequestDelegate next, ILogService logService)
   {
       _next = next;
       _logService = logService;
       
    
   }
  
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

        Console.SetOut(new LogWriter(Console.Out, _logService));

        var message = $"User visited page: {context.Request.Path.Value} at {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}";
        _logService.Log(message);

        // Вызов следующего middleware в конвейере
        await _next(context);
    }

    private class LogWriter : TextWriter
    {
        private readonly TextWriter _originalWriter;
        private readonly ILogService _logService;

        public LogWriter(TextWriter originalWriter, ILogService logService)
        {
            _originalWriter = originalWriter;
            _logService = logService;
        }

        public override Encoding Encoding => _originalWriter.Encoding;

        public override void Write(char value)
        {
            _originalWriter.Write(value);
        }
        public override void WriteLine(string? value)
        {
            _originalWriter.WriteLine(value);
            _logService.Log(value);
        }
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
