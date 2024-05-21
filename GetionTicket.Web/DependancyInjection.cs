using GetionTicket.Web.DAO.Ticket;
using GetionTicket.Web.DAO.User;
using GetionTicket.Web.Models.ConnectAPI;

namespace GetionTicket.Web
{
   public static class DependancyInjection
   {
      public static void AddInfrastructure(this IServiceCollection services)
      {
         services.AddScoped<ConnectAPI>();
         services.AddScoped<ITicketDAO,TicketDAO>();
         services.AddScoped<IUserDAO, UserDAO>();
      }
   }
}
