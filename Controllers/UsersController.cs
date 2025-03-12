using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC.Controllers;

public class UsersController : Controller
{
    private readonly IBlogRepository _repo;
    
    public UsersController(IBlogRepository repo) 
        {
            _repo = repo;
             
        }
    public async Task <IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Отображаем форму регистрации (Register.cshtml)
    }    
    [HttpPost]
       public async Task <IActionResult> Register (User newUser)
       {
           await _repo.AddUser(newUser);
           return View(newUser);
       }
}       