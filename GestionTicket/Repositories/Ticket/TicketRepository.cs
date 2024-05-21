using GestionTicket.Context;

namespace GestionTicket.Repositories.Ticket;

using Microsoft.EntityFrameworkCore;
using Models.Ticket;
public class TicketRepository: ITicketRepository
{
   private TicketManageContext _ticketManageContext;
   public TicketRepository(TicketManageContext ticketManageContext)
   {
      _ticketManageContext = ticketManageContext;
   }
   public List<Ticket> GetTicketOfUsersById(int Userid)
   {
      return _ticketManageContext
               .Tickets
               .Where(x => x.UserId == Userid)             
               .AsNoTracking()
               .ToList();
   }

   public List<Ticket> GetAllTickets()
   {
      return _ticketManageContext
               .Tickets               
               .AsNoTracking()
               .ToList();
   }

   public Ticket GetTicketsById(int id)
   {
      return _ticketManageContext
               .Tickets
               .FirstOrDefault(x=>x.Id==id);
   }

   public void  CreateTicket(Ticket ticket)
   {
      _ticketManageContext
               .Tickets
               .Add(ticket);
      _ticketManageContext.SaveChanges();
   }

   public void ModifyTicket(int id,Ticket ticket)
   {
      ticket.Id = id;
      _ticketManageContext
               .Tickets
               .Update(ticket);
      _ticketManageContext.SaveChanges();
   }

   public void ModifyTicketOfUser(int id, int userId)
   {
      Ticket ticket = _ticketManageContext
               .Tickets
               .FirstOrDefault(x => x.Id == id);
      ticket.UserId = userId;
      _ticketManageContext
               .Tickets
               .Update(ticket);
      _ticketManageContext.SaveChanges();
   }

   public void DeleteTicketById(int id)
   {
      Ticket ticket = _ticketManageContext
               .Tickets
               .FirstOrDefault(x => x.Id == id);
     
      _ticketManageContext
               .Tickets
               .Remove(ticket);
      _ticketManageContext.SaveChanges();
   }
}
