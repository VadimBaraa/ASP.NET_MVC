using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_MVC.Models;
using ASP.NET_MVC.Services;

namespace ASP.NET_MVC.Controllers;

public class HomeController : Controller
{
    private readonly IBlogRepository _repo;
    private readonly ILogger<HomeController> _logger;
    private readonly ILogService _logService;


    public HomeController(ILogger<HomeController> logger, IBlogRepository repo, ILogService logService)
    {
        _logger = logger;
        _repo = repo;
        _logService = logService;
    }

    public async Task <IActionResult> Index()
       {
           // Добавим создание нового пользователя
           var newUser = new User()
           {
               Id = Guid.NewGuid(),
               FirstName = "Andrey",
               LastName = "Petrov",
               JoinDate = DateTime.Now
           };
 
           // Добавим в базу
           await _repo.AddUser(newUser);
          
           return View();
       }

    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
