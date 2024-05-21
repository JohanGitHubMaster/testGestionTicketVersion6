using GestionTicket.Context;
using GestionTicket.Models.Ticket;
using GestionTicket.Models.User;
using GestionTicket.Repositories.Ticket;
using GestionTicket.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace GestionTicket.Test
{

   [Parallelizable(ParallelScope.Self)]
   [TestFixture]
   public class Tests : PageTest
   {
      private TicketManageContext _dbContext;
      private TicketRepository _ticketRepository;
      private UserRepository _userRepository;

      [SetUp]
      public void Setup()
      {
         // Configurer une instance de base de données en mémoire
         var options = new DbContextOptionsBuilder<TicketManageContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;

         _dbContext = new TicketManageContext(options);
         _ticketRepository = new TicketRepository(_dbContext);
         _userRepository = new UserRepository(_dbContext);

         
      }

      public void addData()
      {
         // Clear existing tickets and add new ones
         _dbContext.Users.RemoveRange(_dbContext.Users);
         _dbContext.Tickets.RemoveRange(_dbContext.Tickets); 
         _dbContext.SaveChanges(); 
         // Ajouter des données de test à la base de données en mémoire
         _dbContext.Tickets.AddRange(new List<Ticket>
            {
                new Ticket { Id = 1, Title = "Ticket 1", Description = "Description du ticket 1",Status = "en cours" ,UserId = 1 },
                new Ticket { Id = 2, Title = "Ticket 2", Description = "Description du ticket 2",Status = "en cours",UserId = 2 },
                new Ticket { Id = 3, Title = "Ticket 3", Description = "Description du ticket 3",Status = "en cours",UserId = 2 }
            });

         _dbContext.Users.AddRange(new List<User>
            {
                new User { Id = 1, UserName = "Jean", Email = "Jean@email.com" },
                new User { Id = 2, UserName = "Bernard", Email = "Bernard@email.com" },
                new User { Id = 3, UserName = "Emilie", Email = "Emilie@email.com" },
            });

         _dbContext.SaveChanges();
      }

      [TearDown]
      public void TearDown()
      {
         // Disposer les ressources après chaque test
         _dbContext.Dispose();
      }
      [Test]
      public void GetAllTickets_ShouldReturnAllTickets()
      {
         addData();
         // Act (Action)
         var result = _ticketRepository.GetAllTickets();

         // Assert (Assertion)
         Assert.AreEqual(3, result.Count); // Vérifier si le nombre de tickets retournés est correct
         Assert.IsTrue(result.Any(t => t.Id == 1)); // Vérifier si un ticket spécifique est retourné
      }

      [Test]
      public void GetTickets_ShouldReturnTicketsOfUserById()
      {
         addData();
         // Act (Action)
         var result = _ticketRepository.GetTicketsById(2);

         // Assert (Assertion)
         Assert.AreEqual(result.Title, "Ticket 2"); // Vérifier si le titre du ticket est correct
      }

      [Test]
      public void GetCreateTickets_ShouldCreateTicket()
      {
         addData();
         // Act (Action)
         Ticket ticketToAdd = new Ticket { Title = "Ticket 4", Description = "Description du ticket 4", Status = "en cours 4" };
         _ticketRepository.CreateTicket(ticketToAdd);

         // Assert (Assertion)
         Assert.AreEqual(_dbContext.Tickets.Count(), 4); // Vérifier si il est inserer dans le DBContext
      }

      [Test]
      public void GetModifyTickets_ShouldModifyTicket()
      {
         addData();
         // Act (Action)
         _ticketRepository.ModifyTicketOfUser(2,2);

         // Assert (Assertion)
         Assert.AreEqual(_dbContext.Tickets.FirstOrDefault(x=>x.Id == 2).UserId, 2); // Vérifier si il est modifié dans le DBContext
      }

      [Test]
      public void GetDeleteTicketsById_ShouldDeleteTicketsById()
      {
         addData();
         // Act (Action)
         _ticketRepository.DeleteTicketById(2);

         // Assert (Assertion)
         Assert.AreEqual(_dbContext.Tickets.FirstOrDefault(x => x.Id == 2), null); // Vérifier si il est modifié dans le DBContext
      }

      [Test]
      public void GetTicketOfUsersById_ShouldTicketOfUsersById()
      {
         addData();
         // Act (Action)
         List<Ticket> ticketsByUser = _ticketRepository.GetTicketOfUsersById(2);

         // Assert (Assertion)
         Assert.AreEqual(ticketsByUser.Count, 2); // Vérifier si il est modifié dans le DBContext
      }

      [Test]
      public void GetAllUsers_ShouldAllUsers()
      {
         addData();
         // Act (Action)
         List<User> Users = _userRepository.GetAllUsers();

         // Assert (Assertion)
         Assert.AreEqual(Users.Count, 3); // Vérifier le nombre des utilisateur dans le DBContext
      }

      [Test]
      public void GetCreateUser_ShouldCreateUser()
      {
         addData();
         //Arrange
         User user = new User { UserName = "Bernard", Email = "Bernard@email.com" };
         // Act (Action)
         _userRepository.CreateUser(user);

         // Assert (Assertion)
         Assert.AreEqual(_dbContext.Users.Count(), 4); // Vérifier le nombre des utilisateur dans le DBContext
      }

      [Test]
      public void GetRemoveUser_ShouldRemoveUser()
      {
         addData();
         // Act (Action)
         _userRepository.RemoveUser(1);

         // Assert (Assertion)
         Assert.AreEqual(_dbContext.Users.Count(), 2); // Vérifier le nombre des utilisateur dans le DBContext
      }

      

   }
}
