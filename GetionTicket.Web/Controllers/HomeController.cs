using GetionTicket.Web.Models;
using GetionTicket.Web.Models.ConnectAPI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GetionTicket.Web.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private readonly ConnectAPI _connectAPI;

      public HomeController(ILogger<HomeController> logger, ConnectAPI connectAPI)
      {
         _logger = logger;
         _connectAPI = connectAPI;
      }

      public IActionResult Index()
      {
         _connectAPI.getAllTickets("rhemshall2");
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
}
