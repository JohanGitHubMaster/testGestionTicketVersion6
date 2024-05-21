using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GetionTicket.Web.DAO.Ticket;

using GetionTicket.Web.Models.ConnectAPI;
using Models.Ticket;
using Models.Token;
using System.Text.Json.Nodes;
using System.Text;

public class TicketDAO:ITicketDAO
{
   private readonly ConnectAPI _connectionToken;
   public TicketDAO(ConnectAPI connectionToken)
   {
      _connectionToken = connectionToken;
   }
   public List<Ticket> getAllTickets(string username)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = "/Tickets";
      HttpResponseMessage response = _connectionToken.client.GetAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         string jsonString = response.Content.ReadAsStringAsync().Result;
         List<Ticket> ticket = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
         return ticket;
      }
      return null;
   }
   public bool AddTicket(string username,Ticket ticket)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = "/Tickets";
      string jsonBody = JsonConvert.SerializeObject(ticket);
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

   public bool DeleteTicket(string username, int idTicket)
   {
      string accessToken = _connectionToken.getToken(username).token;
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = $"/Tickets/{idTicket}";
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

   public bool AssignTicket(string username,int idTicket, int userId)
   {
      string accessToken = _connectionToken.getToken(username).token;
      HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = $"/{idTicket}/assign/{userId}";
      HttpResponseMessage response = _connectionToken.client.PutAsync(path,content).Result;
      if (response.IsSuccessStatusCode)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public List<Ticket> GetTicketById(string username, int userId)
   {
      string accessToken = _connectionToken.getToken(username).token;
      HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
      _connectionToken.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = $"/{userId}/ticket";
      HttpResponseMessage response = _connectionToken.client.GetAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         string jsonString = response.Content.ReadAsStringAsync().Result;
         List<Ticket> ticket = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
         return ticket;
      }
      return null;
   }
}
