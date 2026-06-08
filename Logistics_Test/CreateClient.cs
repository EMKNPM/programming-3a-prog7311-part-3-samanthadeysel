using Microsoft.EntityFrameworkCore;
using TechMove_Logistics.Data;
using TechMove_Logistics.Models;

namespace Logistics_Test;

public class CreateClient
{
    private TechMove_LogisticsContext GetContext()
    {
        var options = new DbContextOptionsBuilder<TechMove_LogisticsContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TechMove_LogisticsContext(options);
    }

    [Fact]
    public async Task CreateClient_SavesSuccessfully()
    {
        using var context = GetContext();
        var client = new Client
        {
            ClientId = 1,
            Name = "Test Client",
            ContactNumber = "0645005000",
            Region = "KwaZulu-Natal"
        };

        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var saved = await context.Clients.FirstAsync();
        Assert.Equal("Test Client", saved.Name);
    }
}
