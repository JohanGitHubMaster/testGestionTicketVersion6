namespace GestionTicket.Repositories.Ticket;
using Models.Ticket;
 public interface ITicketRepository
 {
   List<Ticket> GetTicketOfUsersById(int Userid);

   List<Ticket> GetAllTickets();
   Ticket GetTicketsById(int id);
   void CreateTicket(Ticket ticket);
   void ModifyTicket(int id, Ticket ticket);
   void ModifyTicketOfUser(int id, int userId);
   void DeleteTicketById(int id);
 }
