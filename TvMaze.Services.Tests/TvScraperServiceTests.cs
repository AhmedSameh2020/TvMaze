using AutoMoq;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TvMaze.Services;

namespace TvMaze.Services.Tests
{
    [TestFixture]
    public class TvScraperServiceTests
    {
        [Test]
        public async Task GetShowCast_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new AutoMoqer();
            var service = mocker.Create<TvScraperService>();
            int showId = 1;

            // Act
            var result = await service.GetShowCast(
                showId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetShows_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new AutoMoqer();
            var service = mocker.Create<TvScraperService>();

            // Act
            var result = await service.GetShows();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetShowsByPage_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new AutoMoqer();
            var service = mocker.Create<TvScraperService>();
            int page = 1;

            // Act
            var result = await service.GetShowsByPage(
                page);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
