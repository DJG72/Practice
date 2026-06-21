using Moq;
using NUnit.Framework;
using PlayersManagerLib;
using System;

namespace PlayerManager.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        Mock<IPlayerMapper> mockMapper;

        [OneTimeSetUp]
        public void Init()
        {
            mockMapper =
                new Mock<IPlayerMapper>();
        }

        [TestCase]
        public void RegisterNewPlayer_ReturnsPlayer()
        {
            mockMapper
                .Setup(x =>
                    x.IsPlayerNameExistsInDb(
                        It.IsAny<string>()))
                .Returns(false);

            Player player =
                Player.RegisterNewPlayer(
                    "Virat",
                    mockMapper.Object);

            Assert.That(
                player.Name,
                Is.EqualTo("Virat"));

            Assert.That(
                player.Age,
                Is.EqualTo(23));

            Assert.That(
                player.Country,
                Is.EqualTo("India"));

            Assert.That(
                player.NoOfMatches,
                Is.EqualTo(30));
        }

        [TestCase]
        public void RegisterNewPlayer_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Player.RegisterNewPlayer(
                        "",
                        mockMapper.Object);
                });
        }
    }
}