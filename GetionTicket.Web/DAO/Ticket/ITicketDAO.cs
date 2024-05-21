namespace GetionTicket.Web.DAO.Ticket;
using Models.Ticket;

public interface ITicketDAO
{
   List<Ticket> getAllTickets(string username);
   bool AddTicket(string username, Ticket ticket);
   bool DeleteTicket(string username, int idTicket);
   bool AssignTicket(string username, int idTicket, int userId);
   List<Ticket> GetTicketById(string username, int userId);
}
