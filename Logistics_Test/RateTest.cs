using Microsoft.Extensions.Configuration;
using TechMove_Logistics.Services;

namespace Logistics_Test
{
    public class RateTest
    {
        [Theory]
        [InlineData(1, 18.5, 18.5)]
        [InlineData(10, 18.5, 185)]
        [InlineData(100, 20, 2000)]
        public void UsdToZar_CalculatesCorrectly(decimal usd, decimal rate, decimal expected)
        {
            // Act
            var result = usd * rate;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
