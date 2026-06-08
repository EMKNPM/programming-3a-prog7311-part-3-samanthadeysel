
using TechMove_Logistics.Models;
using TechMove_Logistics.State;

namespace Logistics_Test
{
    public class StateTest
    {
        [Fact]
        public void Contract_DefaultsToDraft()
        {
            var contract = new Contract();

            Assert.Equal("Draft", contract.Status);
        }

        [Fact]
        public void SetState_ChangesStatusToActive()
        {
            var contract = new Contract();

            contract.SetState(new ActiveState());

            Assert.Equal("Active", contract.Status);
        }

        [Fact]
        public void SetState_ChangesStatusToExpired()
        {
            var contract = new Contract();

            contract.SetState(new ExpiredState());

            Assert.Equal("Expired", contract.Status);
        }
    }
}
