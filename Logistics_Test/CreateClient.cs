using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;
using Xunit;

namespace Logistics_Test
{
    public class CreateClient
    {
        private TechMoves_WebAPIContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TechMoves_WebAPIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TechMoves_WebAPIContext(options);
        }

        [Fact]
        public async Task CreateClient_SavesSuccessfully()
        {
            using var context = GetContext();
            var client = new Client
            {
                Name = "Test Client",
                ContactNumber = "0645005000",
                Region = "KwaZulu-Natal"
            };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var saved = await context.Clients.FirstAsync();
            Assert.Equal("Test Client", saved.Name);
            Assert.Equal("0645005000", saved.ContactNumber);
            Assert.Equal("KwaZulu-Natal", saved.Region);
        }

        [Fact]
        public void CreateClient_FailsWithoutName()
        {
            using var context = GetContext();
            var client = new Client
            {
                ContactNumber = "0645005000",
                Region = "KwaZulu-Natal"
            };

            var validationContext = new ValidationContext(client, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, validationContext, validationResults, true);

            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains("Name"));
        }
    }
}
