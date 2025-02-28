using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_MVC.Models;

namespace ASP.NET_MVC.Controllers;

public class FeedbackController : Controller
{
   /// <summary>
   ///  Метод, возвращающий страницу с отзывами
   /// </summary>
   [HttpGet]
   public IActionResult Add()
   {
       return View();
   }
  
   /// <summary>
   /// Метод для Ajax-запросов
   /// </summary>
   [HttpPost]
    public IActionResult Add(Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            return PartialView("_FeedbackThanks", feedback);
        }
        return View(feedback);
    }

 
   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error()
   {
       return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
   }
}