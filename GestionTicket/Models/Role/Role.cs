using NSwag.Annotations;

namespace GestionTicket.Models.Role;
using Models.User;
public class Role
{
   public int Id { get; set; }
   public string? Types { get; set; }
   public int? UserId { get; set; }
   [SwaggerIgnore]
   public User User { get; set; }
}
