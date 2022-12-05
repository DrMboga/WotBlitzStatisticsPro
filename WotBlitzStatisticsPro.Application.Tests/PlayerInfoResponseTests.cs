using MediatR;
using Moq;
using WotBlitzStatisticsPro.Application.Services;

namespace WotBlitzStatisticsPro.Application.Tests
{
    public class PlayerInfoResponseTests
    {
        private Mock<IMediator> _mediatorMock;
        private PlayerRequestsHandler _service;

        [SetUp]
        protected void Setup()
        {
            _mediatorMock = new();
        }

        [Test]
        public void ShouldCallFindPlayersOnMediatREvent()
        {
            Assert.Fail("Implement tests for PlayerRequestsHandler, PlayerInfoService and FindPlayersService. Last two should call appropriate WG API mediatR requests");
        }
    }
}