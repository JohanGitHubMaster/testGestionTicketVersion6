using GetionTicket.Web.Models.ConnectAPI;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GetionTicket.Web.DAO.User;
using Models.User;
using System.Text;

 public class UserDAO: IUserDAO
{
   private readonly ConnectAPI _connectionToken;
   public UserDAO(ConnectAPI connectionToken)
   {
      _connectionToken = connectionToken;
   }
   public List<User> getAllUsers(string username)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = "/Users";
      HttpResponseMessage response = _connectionToken.client.GetAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         string jsonString = response.Content.ReadAsStringAsync().Result;
         List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonString);
         return users;
      }
      return null;
   }
   public bool AddUser(string username, User user)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = "/Users";
      string jsonBody = JsonConvert.SerializeObject(user);
      HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
      HttpResponseMessage response = _connectionToken.client.PostAsync(path, content).Result;
      if (response.IsSuccessStatusCode)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool DeleteUser(string username, int idUser)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = $"/User/{idUser}";
      HttpResponseMessage response = _connectionToken.client.DeleteAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool GetTicketUserById(string username, int idUser)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = $"/User/{idUser}";
      HttpResponseMessage response = _connectionToken.client.DeleteAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public string RoleUser(int idUser)
   {
      string path = $"/Role/{idUser}";
      HttpResponseMessage response = _connectionToken.client.GetAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         string jsonString = response.Content.ReadAsStringAsync().Result;
         return jsonString;
      }
      else
      {
         return null;
      }
   }
}
