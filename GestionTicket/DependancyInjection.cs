using GestionTicket.Repositories.Role;
using GestionTicket.Repositories.Ticket;
using GestionTicket.Repositories.Token;
using GestionTicket.Repositories.User;

namespace GestionTicket
{
   public static class DependancyInjection
   {
      public static void AddInfrastructure(this IServiceCollection services)
      {
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<ITicketRepository, TicketRepository>();
         services.AddScoped<IRoleRepository, RoleRepository>();
         services.AddScoped<Token>();
      }
   }
}
