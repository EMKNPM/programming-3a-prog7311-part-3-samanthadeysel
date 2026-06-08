using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;
using Xunit;

namespace Logistics_Test
{
    public class CreateContract
    {
        private TechMoves_WebAPIContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TechMoves_WebAPIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TechMoves_WebAPIContext(options);
        }

        [Fact]
        public async Task CreateContract_DefaultsToDraftStatus()
        {
            using var context = GetContext();
            var contract = new Contract
            {
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
            Assert.True(saved.ContractId > 0);
        }

        [Fact]
        public async Task CreateContract_CanUpdateStatusToActive()
        {
            using var context = GetContext();
            var contract = new Contract
            {
                ContractName = "Service Contract",
                ContractDescription = "Testing status change",
                ContractType = "Service",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(15),
                ServiceLevel = "Premium",
                SignedAgreementPath = "agreement.pdf"
            };

            context.Contracts.Add(contract);
            await context.SaveChangesAsync();

            contract.Status = "Active";
            context.Contracts.Update(contract);
            await context.SaveChangesAsync();

            var saved = await context.Contracts.FirstAsync();
            Assert.Equal("Active", saved.Status);
        }
    }
}
