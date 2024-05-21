namespace GestionTicket.Models.Ticket
{
   using GestionTicket.Models.User;
   using NSwag.Annotations;

   public class Ticket
   {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string Status { get; set; }
      public int? UserId { get; set; }

      [SwaggerIgnore]
      public User? User { get; set; }
   }
}
