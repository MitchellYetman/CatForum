using System.Diagnostics;
using CatForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
       
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();        
        }

      
    }
}
