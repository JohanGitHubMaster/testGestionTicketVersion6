namespace GestionTicket.Repositories.User;

using GestionTicket.Context;
using GestionTicket.Models.Role;
using Microsoft.EntityFrameworkCore;
using Models.User;
using Models.Ticket;
public class UserRepository : IUserRepository
{
   private TicketManageContext _ticketManageContext;
   public UserRepository(TicketManageContext ticketManageContext)
   {
      _ticketManageContext = ticketManageContext;
   }
   public List<User> GetAllUsers()
   {
      return _ticketManageContext
               .Users
               .AsNoTracking()
               .ToList();
   }
   public void CreateUser(User user)
   {
       _ticketManageContext
               .Users       
               .Add(user);             
      _ticketManageContext.SaveChanges();
      _ticketManageContext.Roles.Add(new Role() { UserId = user.Id, Types = "User" });
      _ticketManageContext.SaveChanges();
   }

   public void ModifyUser(int userId,User user)
   {
       user.Id = userId;
      _ticketManageContext
              .Users
              .Update(user);
      _ticketManageContext.SaveChanges();
   }

   public void RemoveUser(int userId)
   {
      User usertodelete = _ticketManageContext.Users.FirstOrDefault(x => x.Id == userId);
      Role role = _ticketManageContext.Roles.FirstOrDefault(x => x.UserId == userId);
      if(role!=null)
      _ticketManageContext.Roles.Remove(role);
         List<Ticket> ticket = _ticketManageContext.Tickets.Where(x => x.UserId == userId).ToList();
      if (ticket != null)
      _ticketManageContext.Tickets.RemoveRange(ticket);

      _ticketManageContext
              .Users
              .Remove(usertodelete);
      
      _ticketManageContext.SaveChanges();
   }

   public int? GetIdUserByName(string username)
   {
      User user = _ticketManageContext.Users.FirstOrDefault(x => x.UserName == username);
      if (user != null)
         return _ticketManageContext.Users.FirstOrDefault(x => x.UserName == username).Id;
      else
         return null;
   }
}
