using GestionTicket.Models.Ticket;
using GestionTicket.Repositories.Ticket;
using GestionTicket.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;

namespace GestionTicket.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class TicketsController : ControllerBase
   {
      private ITicketRepository _ticketRepository;
      public TicketsController(ITicketRepository ticketRepository)
      {
         _ticketRepository = ticketRepository;
      }

      

      [HttpGet("")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult GetTicket()
      {
         return Ok(_ticketRepository.GetAllTickets());
      }

      [HttpGet("/{id}")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult GetTicket(int id)
      {
         return Ok(_ticketRepository.GetTicketsById(id));
      }

      [HttpPost("")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult CreateTicket(Ticket ticket)
      {
         _ticketRepository.CreateTicket(ticket);
         return Ok();
      }

      [HttpPut("/{id}/assign/{userId}")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      public IActionResult ModifyTicket(int id,int userId)
      {
         _ticketRepository.ModifyTicketOfUser(id,userId);
         return Ok();
      }

      [HttpDelete("{id}")]
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
      public IActionResult DeleteTicket(int id)
      {
         _ticketRepository.DeleteTicketById(id);
         return Ok();
      }
   }
}
