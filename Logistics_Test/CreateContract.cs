using Microsoft.EntityFrameworkCore;
using TechMove_Logistics.Data;
using TechMove_Logistics.Models;

namespace Logistics_Test
{
    public class CreateContract
    {
        private TechMove_LogisticsContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TechMove_LogisticsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TechMove_LogisticsContext(options);
        }

        [Fact]
        public async Task CreateContract_DefaultsToDraftStatus()
        {
            using var context = GetContext();
            var contract = new Contract
            {
                ClientId = 1,
                ContractName = "Delivery Contract",
                ContractDescription = "Delivery from AUS",
                ContractType = "Delivery",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                ServiceLevel = "Standard",
                SignedAgreementPath = "agreement.pdf"
            };

            context.Contracts.Add(contract);
            await context.SaveChangesAsync();

            var saved = await context.Contracts.FirstAsync();
            Assert.Equal("Draft", saved.Status);
        }
    }
}
