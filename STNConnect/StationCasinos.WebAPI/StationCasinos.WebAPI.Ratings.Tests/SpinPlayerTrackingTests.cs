using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StationCasinos.EnterpriseObjects.Ratings;
using StationCasinos.Repository.Interface.Lookup;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.Repository.Ratings;
using StationCasinos.WebAPI.Utility.Interface;

namespace StationCasinos.WebAPI.Ratings.Tests
{
    [TestClass]
    public class SpinPlayerTrackingTests
    {

        [TestMethod]
        public void Create_ValidInhouseSports()
        {
            // Arrange
            Mock<ILookupRepository> mockLookup = new Mock<ILookupRepository>();
            IOleDbCommand mockDbCommand = new MockOleDbCommand();
            IOleDbConnection mockDbConnection = new MockOleDbConnection();
            Mock<ILogging>  mockLogging = new Mock<ILogging>();

            IPlayerTracking playerTracking = new SpinPlayerTracking(mockLookup.Object, mockDbConnection,  mockDbCommand, mockLogging.Object);
            
            EventRatingInhouse openRating = TestUtility.GenerateRatingInhouse(ratingStatus.Open);
            openRating.Rating.ratingHostId = "";
            
            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            openRating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            mockLookup.Setup(x => x.GetPropertyCode("3")).Returns("GV");

            GameCodeInfo gameCodeInfo = TestUtility.GetGameCodeInfo("1", GameCodeType.Sports, "1");

            mockLookup.Setup(x => x.GetGameCodeInfo("1", GameCodeType.Sports, "1")).Returns(gameCodeInfo);

            // Act
            var response = playerTracking.Create<EventRatingInhouse>(openRating, GameCodeType.Sports);

            response.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            // Assert
            Assert.IsTrue(ratingObject.ratingHostId != "");

            // Assert
            Assert.AreEqual(ratingObject.ratingHostId.Substring(11, 3), "000");
        }

        [TestMethod]
        public void Update_ValidInhouseSports()
        {
            // Arrange
            Mock<ILookupRepository> mockLookup = new Mock<ILookupRepository>();
            IOleDbCommand mockDbCommand = new MockOleDbCommand();
            IOleDbConnection mockDbConnection = new MockOleDbConnection();
            Mock<ILogging> mockLogging = new Mock<ILogging>();

            IPlayerTracking playerTracking = new SpinPlayerTracking(mockLookup.Object, mockDbConnection, mockDbCommand, mockLogging.Object);

            EventRatingInhouse updateRating = TestUtility.GenerateRatingInhouse(ratingStatus.Update);
            updateRating.Rating.ratingHostId = "12345678901000";

            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            updateRating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            mockLookup.Setup(x => x.GetPropertyCode("3")).Returns("GV");

            GameCodeInfo gameCodeInfo = TestUtility.GetGameCodeInfo("1", GameCodeType.Sports, "1");

            mockLookup.Setup(x => x.GetGameCodeInfo("1", GameCodeType.Sports, "1")).Returns(gameCodeInfo);

            // Act
            var response = playerTracking.Update<EventRatingInhouse>(updateRating, GameCodeType.Sports);

            response.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            // Assert
            Assert.IsTrue(ratingObject.ratingHostId == "12345678901001");
            
        }
    }
}
