using MediatR;
using Moq;
using WotBlitzStatisticsPro.WargamingApi.Services;

namespace WotBlitzStatisticsPro.WargamingApi.Tests
{
    public class PlayersServiceTests
    {
        private Mock<IMediator> _mediatorMock;
        private PlayersService _service;

        [SetUp]
        protected void Setup()
        {
            _mediatorMock = new();
        }

                [Test]
        public void ShouldShowAppropriatePlayerInfo()
        {
            Assert.Fail("Implement PlayersService tests, ClansService tests, WargamingClient tests (with caching), ");
        }
    }
}