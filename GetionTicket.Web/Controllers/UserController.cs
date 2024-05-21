using GetionTicket.Web.DAO.User;
using GetionTicket.Web.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GetionTicket.Web.Models.ConnectAPI;
using GetionTicket.Web.Models.Token;
using Microsoft.AspNetCore.Authorization;
using GetionTicket.Web.DAO.Ticket;
using GetionTicket.Web.Models.Ticket;

namespace GetionTicket.Web.Controllers
{
   [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
   public class UserController : Controller
   {
      IUserDAO _userDAO;
      ITicketDAO _ticketDAO;
      ConnectAPI _connectAPI;
      public UserController(IUserDAO userDAO, ITicketDAO ticketDAO, ConnectAPI connectAPI)
      {
         _userDAO = userDAO;
         _connectAPI = connectAPI;
         _ticketDAO = ticketDAO;
      }
      [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult Index()
      {
         List<User> users = _userDAO.getAllUsers(HttpContext.Session.GetString("UserName"));
         return View(users);
      }
      [AllowAnonymous]
      public IActionResult Login()
      {
         return View();
      }
      [AllowAnonymous]
      [HttpPost]
      public IActionResult Login(User user)
      {
         Token token = _connectAPI.getToken(user.UserName);
         if (token != null)
         {
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetInt32("idUser", token.idUser);
            Claim claim;
            if(_userDAO.RoleUser(token.idUser) == "Admin")
            {
               claim = new Claim(ClaimTypes.Role, "Admin");
            }            
            else
            {
               claim = new Claim(ClaimTypes.Role, "User");
            }
            List<Claim> claims = new List<Claim>()
            {
              claim,
            };
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new()
            {
               AllowRefresh = true,
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
            if (_userDAO.RoleUser(token.idUser) == "Admin")
            {
               return RedirectToAction("Index", "Ticket");
            }
            else
            {
               return RedirectToAction("GetTicketUser", "User");
            }
            
         }        
         ViewData["ValidateMessage"] = "User not found";
         return View();
      }

      public async Task<IActionResult> LogOut()
      {
         HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         HttpContext.Session.Remove("UserName");
         HttpContext.Session.Remove("idUser");
         return RedirectToAction("Login", "User");
      }
      [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult AddUser()
      {
         return View();
      }
      [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
      [HttpPost]
      public IActionResult AddUser(User user)
      {
         bool isInsert = _userDAO.AddUser(HttpContext.Session.GetString("UserName"), user);
         if (isInsert)
         {
            return RedirectToAction("Index", "User");
         }
         else
         {
            return View();
         }
      }

      public IActionResult GetTicketUser()
      {
         ViewBag.name = HttpContext.Session.GetString("UserName");
         List<Ticket> tickets = _ticketDAO.GetTicketById(HttpContext.Session.GetString("UserName"), (int)HttpContext.Session.GetInt32("idUser"));
         return View(tickets);
      }
      [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
      [HttpPost]
      public IActionResult DeleteUser(int idUser)
      {
         _userDAO.DeleteUser(HttpContext.Session.GetString("UserName"), idUser);
         return RedirectToAction("Index", "User");
      }
   }
}
