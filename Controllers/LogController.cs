using ASP.NET_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NET_MVC.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            var logs = _logService.GetLogs();
            return View(logs);
        }
    }
}