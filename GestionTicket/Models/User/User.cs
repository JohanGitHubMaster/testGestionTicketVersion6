
namespace GestionTicket.Models.User;
using GestionTicket.Models.Ticket;
using NSwag.Annotations;

public class User
{

   public int Id { get; set; }
   public string? UserName { get; set; }
   public string? Email { get; set; }
   [SwaggerIgnore]
   public List<Ticket>? Tickets { get; set; }
}
