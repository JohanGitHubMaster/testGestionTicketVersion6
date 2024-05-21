using GestionTicket.Models.Ticket;
using GestionTicket.Models.User;
using Microsoft.EntityFrameworkCore;

namespace GestionTicket.Context;

using GestionTicket.Models.Role;
using Models.User;
public class TicketManageContext : DbContext
{
   public TicketManageContext(DbContextOptions<TicketManageContext> options) : base(options)
   {
   }
   public DbSet<User> Users { get; set; }
   public DbSet<Ticket> Tickets { get; set; }
   public DbSet<Role> Roles { get; set; }
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<User>().HasKey(e => e.Id);
      modelBuilder.Entity<User>()
                     .Property(e => e.Id)
                     .ValueGeneratedOnAdd();
      modelBuilder.Entity<User>().
         Property(e => e.UserName)
         .HasMaxLength(800);
      modelBuilder.Entity<User>().
        Property(e => e.Email)
        .HasMaxLength(400);

      modelBuilder.Entity<Ticket>().HasKey(e => e.Id);
      modelBuilder.Entity<Ticket>()
                     .Property(e => e.Id)
                     .ValueGeneratedOnAdd();
      modelBuilder.Entity<Ticket>().
       Property(e => e.Status)
       .HasMaxLength(40);
      modelBuilder.Entity<Ticket>().
       Property(e => e.Description)
       .HasMaxLength(4000);
      modelBuilder.Entity<Ticket>().
       Property(e => e.Title)
       .HasMaxLength(400);

      modelBuilder.Entity<Role>().HasKey(e => e.Id);
      modelBuilder.Entity<Role>()
                     .Property(e => e.Id)
                     .ValueGeneratedOnAdd();
      modelBuilder.Entity<Role>().
         Property(e => e.Types)
         .HasMaxLength(20);
   }
}
