using GestionTicket.Context;

namespace GestionTicket.Repositories.Role
{
   public class RoleRepository : IRoleRepository
   {
      private TicketManageContext _ticketManageContext;
      public RoleRepository(TicketManageContext ticketManageContext)
      {
         _ticketManageContext = ticketManageContext;
      }
      public string getTypeRole(int? idUser)
      {
         if (idUser == null) return null;
         return _ticketManageContext.Roles.FirstOrDefault(x => x.UserId == idUser).Types;
      }
   }
}
