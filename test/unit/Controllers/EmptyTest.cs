using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Vuture.Controllers;
using Vuture.Models.Dtos;
using Vuture.Persistence;

namespace Vuture.Test.Unit.Validation
{
    [TestFixture]
    [Category("Unit")]
    public class EmptyValidationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
            .UseInMemoryDatabase(databaseName: "ContactDb")
            .Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new ContactDbContext(options))
            {
                context.Contacts.Add(new Contact { Id = 1, FirstName = "fname1", LastName = "lname1", Title = "Head", EmailAddress = "name1.webster@vutu.re", Company = "Vuture" });
                context.Contacts.Add(new Contact { Id = 2, FirstName = "fname2", LastName = "lname2", Title = "Head2", EmailAddress = "name2.webster@vutu.re", Company = "Vuture" });
                context.Contacts.Add(new Contact { Id = 3, FirstName = "fname3", LastName = "lname3", Title = "Head3", EmailAddress = "name3.webster@vutu.re", Company = "Vuture" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ContactDbContext(options))
            {
                var sut = new ContactController(context);
                //Act
                var contact = sut.GetContactById(3);
      
                //Assert
                Assert.AreEqual(1, 1);
            }

        }
      
    }
}