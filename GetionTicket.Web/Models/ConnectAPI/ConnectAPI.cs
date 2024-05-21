using System.IO;

namespace GetionTicket.Web.Models.ConnectAPI;
using Models.Ticket;
using Models.Token;
using Newtonsoft.Json;
using System.Net.Http.Headers;

public class ConnectAPI
{
   public HttpClient client = new HttpClient();
   public Token getToken(string username)
   {
      try
      {
         if (client.BaseAddress == null)
            client.BaseAddress = new Uri("https://localhost:7275");

         string path = $"/Users/GetToken/{username}";
         HttpResponseMessage response = client.GetAsync(path).Result;
         if (response.IsSuccessStatusCode)
         {
            string jsonString = response.Content.ReadAsStringAsync().Result;
            Token token = JsonConvert.DeserializeObject<Token>(jsonString);
            return token;
         }
      }
      catch
      {
         //ivalide user
         return null;
      }    
      return null;
   }
   public List<Ticket> getAllTickets(string username)
   {
      string accessToken = getToken(username).token;
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      string path = "/Tickets";
      HttpResponseMessage response = client.GetAsync(path).Result;
      if (response.IsSuccessStatusCode)
      {
         string jsonString = response.Content.ReadAsStringAsync().Result;
         List<Ticket> ticket = JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
         return ticket;        
      }
      return null;
   } 
   
}
