using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechMove_Logistics.Data;
using TechMove_Logistics.Models;

namespace Logistics_Test
{
    public class CreateServiceRequest
    {
        private TechMove_LogisticsContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TechMove_LogisticsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TechMove_LogisticsContext(options);
        }

        [Fact]
        public async Task CreateServiceRequest_SavesSuccessfully()
        {
            using var context = GetContext();
            var serviceRequest = new ServiceRequest
            {
                ServiceRequestId = 1,
                ContractId = 1,
                Description = "Urgent delivery",
                RequestDate = DateTime.Now,
                Status = "Open"
            };

            context.ServiceRequests.Add(serviceRequest);
            await context.SaveChangesAsync();

            var saved = await context.ServiceRequests.FirstAsync();
            Assert.Equal("Urgent delivery", saved.Description);
        }
    }
}
