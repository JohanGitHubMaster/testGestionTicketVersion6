namespace GestionTicket.Repositories.User;
using Models.User;
public interface IUserRepository
 {
   List<User> GetAllUsers();
   void CreateUser(User user);
   void ModifyUser(int userId,User user);
   void RemoveUser(int userId);
   int? GetIdUserByName(string username);
}
