using GestionTicket.Models.User;
using GestionTicket.Repositories.Role;
using GestionTicket.Repositories.Ticket;
using GestionTicket.Repositories.Token;
using GestionTicket.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionTicket.Controllers
{  
   [ApiController]
   [Route("[controller]")]
   public class UsersController : ControllerBase
   {
      private IUserRepository _userRepository;
      private ITicketRepository _ticketRepository;
      private IRoleRepository _rolerepository;
      private Token _token;
      public UsersController(IUserRepository userRepository,ITicketRepository ticketRepository, Token token, IRoleRepository rolerepository)
      {
         _userRepository = userRepository;
         _ticketRepository = ticketRepository;
         _token = token;
         _rolerepository = rolerepository;
      }
      [HttpGet("")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult GetUsers()
      {
         return Ok(_userRepository.GetAllUsers());
      }
      [HttpGet("/{id}/ticket")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult GetUsers(int id)
      {
         return Ok(_ticketRepository.GetTicketOfUsersById(id));
      }

      [HttpPost("")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult CreateUsers(User user)
      {
         user.Tickets = null;
         _userRepository.CreateUser(user);
         return Ok();
      }

      [HttpPut("/{id}")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult ModifyUser(int id,User user)
      {
         user.Id = id;
         _userRepository.ModifyUser(id,user);
         return Ok();
      }

      [HttpDelete("/User/{id}")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult DeleteUser(int id)
      {
         _userRepository.RemoveUser(id);
         return Ok();
      }

      [HttpGet("/Role/{id}")]
      public IActionResult UserRole(int id)
      {
         return Ok(_rolerepository.getTypeRole(id));
      }

      [HttpGet("GetToken/{user}")]
      [Produces("application/json")]
      public IActionResult GenerateToken(string user)
      {
         try
         {
            int? idUser = _userRepository.GetIdUserByName(user);
            string role = _rolerepository.getTypeRole(idUser);
            string token = _token.GenerateTokenAsync(role);
            var response = new
            {
               Token = token,
               idUser = idUser
            };
            return Ok(response);
         }
         catch
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Une erreur s'est produite lors de la génération du jeton.");
         }

      }

   }
}
