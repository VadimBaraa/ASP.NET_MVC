using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC.Controllers;

public class UsersController : Controller
{
    private readonly IBlogRepository _repo;
    private readonly BlogContext _context; 

    public UsersController(IBlogRepository repo, BlogContext context) 
        {
            _repo = repo;
            _context = context; 
        }
    public async Task <IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            if (authors.Length > 0)
            {
                return View(authors.Take(1).ToArray());
            }
            return View(authors);
        }

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