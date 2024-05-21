namespace GetionTicket.Web.DAO.User;
using Models.User;

public interface IUserDAO
{
   List<User> getAllUsers(string username);
   bool AddUser(string username, User user);
   bool DeleteUser(string username, int idUser);
   string RoleUser(int idUser);
}
