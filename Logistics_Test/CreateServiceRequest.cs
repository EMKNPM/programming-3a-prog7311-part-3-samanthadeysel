using Microsoft.EntityFrameworkCore;
using TechMoves_WebAPI.Data;
using TechMoves_WebAPI.Models;
using Xunit;

namespace Logistics_Test
{
    public class CreateServiceRequest
    {
        private TechMoves_WebAPIContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TechMoves_WebAPIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TechMoves_WebAPIContext(options);
        }

        [Fact]
        public async Task CreateServiceRequest_SavesSuccessfully()
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
                SignedAgreementPath = "agreement.pdf",
                Status = "Draft"
            };

            context.Contracts.Add(contract);
            await context.SaveChangesAsync();

            var serviceRequest = new ServiceRequest
            {
                ContractId = contract.ContractId, 
                Description = "Urgent delivery",
                RequestDate = DateTime.Now,
                Status = "Open",
                RequestType = "Delivery"
            };

            context.ServiceRequests.Add(serviceRequest);
            await context.SaveChangesAsync();

            var saved = await context.ServiceRequests.FirstAsync();
            Assert.Equal("Urgent delivery", saved.Description);
            Assert.Equal("Open", saved.Status);
            Assert.Equal(contract.ContractId, saved.ContractId);
            Assert.True(saved.ServiceRequestId > 0); 
        }
    }
}
