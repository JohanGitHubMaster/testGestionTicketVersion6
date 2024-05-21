using GetionTicket.Web.DAO.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace GetionTicket.Web.Controllers;

using GetionTicket.Web.DAO.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Models.Ticket;
using Models.User;
public class TicketController : Controller
{
   ITicketDAO _ticketDAO;
   IUserDAO _userDAO;
   public TicketController(ITicketDAO ticketDAO, IUserDAO userDAO)
   {
      _ticketDAO = ticketDAO;
      _userDAO =  userDAO;
   }
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
   public IActionResult Index()
   {
      List<Ticket> tickets = _ticketDAO.getAllTickets(HttpContext.Session.GetString("UserName"));
      ViewBag.Users = _userDAO.getAllUsers(HttpContext.Session.GetString("UserName"));
      return View(tickets);
   }
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
   public IActionResult AddTicket()
   {
      return View();
   }
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
   [HttpPost]
   public IActionResult AddTicket(Ticket ticket)
   {
      bool isInsert = _ticketDAO.AddTicket(HttpContext.Session.GetString("UserName"), ticket);
      if (isInsert)
      {
         return RedirectToAction("Index", "Ticket");
      }
      else
      {
         return View();
      }
      
   }
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
   [HttpPost]
   public IActionResult DeleteTicket(int idTicket)
   {
      _ticketDAO.DeleteTicket(HttpContext.Session.GetString("UserName"), idTicket);
      return RedirectToAction("Index", "Ticket");
   }
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
   [HttpPost]
   public IActionResult AssignTicket(int idTicket)
   {
      int userId = int.Parse(Request.Form["userId_" + idTicket]);
      _ticketDAO.AssignTicket(HttpContext.Session.GetString("UserName"), idTicket, userId);
      return RedirectToAction("Index", "Ticket");
   }

}
