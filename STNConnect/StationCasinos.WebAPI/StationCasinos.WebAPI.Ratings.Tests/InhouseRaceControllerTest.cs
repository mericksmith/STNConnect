using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.WebAPI.Ratings.Controllers;
using StationCasinos.WebAPI.Utility;
using StationCasinos.WebAPI.Utility.Interface;
using System;
using System.Net.Http;
using System.Web.Http;

namespace StationCasinos.WebAPI.Ratings.Tests
{
    [TestClass]
    public class InhouseRaceControllerTest
    {
        [TestMethod]
        public void Post_InhouseController()
        {
            // Arrange
            Mock<IRatingsRepository> mockRepository = new Mock<IRatingsRepository>();
            Mock<ILogging> mockLogging = new Mock<ILogging>();

            EventRatingInhouse openRating = TestUtility.GenerateRatingInhouse(ratingStatus.Open);

            EventRatingInhouse openRatingWithHostId = openRating;
            openRatingWithHostId.Rating.ratingHostId = "12345678912000"; //Sequence number is always 000 for new record.
            
            mockRepository.Setup(x => x.CreateRating<EventRatingInhouse>(openRating, GameCodeType.InhouseRace)).Returns(openRatingWithHostId);

            InhouseRaceController controller = TestUtility.GetInhouseRaceController(mockRepository.Object, mockLogging.Object);
            
            // Act
            var response = controller.Post(openRating);
            
            // Assert
            Assert.IsTrue(response.TryGetContentValue(out openRating));

            // Assert
            Assert.AreEqual(openRating, openRatingWithHostId);
        }

        [TestMethod]
        public void Post_IncorrectRatingStatusRaisesError()
        {
            Mock<IRatingsRepository> mockRepository = new Mock<IRatingsRepository>();
            Mock<ILogging> mockLogging = new Mock<ILogging>();

            EventRatingInhouse voidRating = TestUtility.GenerateRatingInhouse(ratingStatus.Void);
            EventRatingInhouse updateRating = TestUtility.GenerateRatingInhouse(ratingStatus.Update);

            InhouseRaceController controller = TestUtility.GetInhouseRaceController(mockRepository.Object, mockLogging.Object);

            var postResponse = controller.Post(voidRating);
            var updateResponse = controller.Post(updateRating);

            Assert.IsFalse(postResponse.IsSuccessStatusCode);
            Assert.IsFalse(updateResponse.IsSuccessStatusCode);

        }
    }
}
