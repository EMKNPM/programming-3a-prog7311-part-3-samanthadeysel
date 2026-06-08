using TechMoves_WebAPI.Models;
using TechMoves_WebAPI.State;
using Xunit;

namespace Logistics_Test
{
    public class StateTest
    {
        [Fact]
        public void Contract_DefaultsToDraft()
        {
            var contract = new Contract
            {
                // FIXED: Populated with a valid path file signature to bypass ComplianceModule validation interceptor checks
                SignedAgreementPath = "agreement.pdf"
            };

            contract.SetState(new DraftState());

            Assert.Equal("Draft", contract.Status);
            Assert.IsType<DraftState>(contract.State);
        }

        [Fact]
        public void SetState_ChangesStatusToActive()
        {
            var contract = new Contract
            {
                SignedAgreementPath = "agreement.pdf"
            };

            contract.SetState(new ActiveState());

            Assert.Equal("Active", contract.Status);
            Assert.IsType<ActiveState>(contract.State);
        }

        [Fact]
        public void SetState_ChangesStatusToExpired()
        {
            var contract = new Contract
            {
                SignedAgreementPath = "agreement.pdf"
            };

            contract.SetState(new ExpiredState());

            Assert.Equal("Expired", contract.Status);
            Assert.IsType<ExpiredState>(contract.State);
        }

        [Fact]
        public void StatusTransitions_AreConsistent()
        {
            var contract = new Contract
            {
                SignedAgreementPath = "agreement.pdf"
            };

            contract.SetState(new DraftState());
            Assert.Equal("Draft", contract.Status);

            contract.SetState(new ActiveState());
            Assert.Equal("Active", contract.Status);

            contract.SetState(new ExpiredState());
            Assert.Equal("Expired", contract.Status);
        }
    }
}