using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using StationCasinos.EnterpriseObjects.Ratings;
using StationCasinos.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.WebAPI.Ratings.Tests
{
    [TestClass]
    public class RatingsExtensionsTests
    {
        [TestMethod]
        public void ToLog_FormatsRatingFields()
        {

            //Arrange
            var rating = TestUtility.GenerateRatingInhouse(ratingStatus.Open);

            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            //Act
            var logString = rating.ToLog();

            //Assert
            Assert.IsTrue(logString.IndexOf(rating.PropertyCode) != -1);
            Assert.IsTrue(logString.IndexOf(ratingSession.locationId) != -1);
            Assert.IsTrue(logString.IndexOf(ratingObject.patronId) != -1);
            Assert.IsTrue(logString.IndexOf(ratingObject.pointsEarned.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(rating.transDateTime.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.ratedBy) != -1);
            Assert.IsTrue(logString.IndexOf(ratingObject.ratingHostId) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.cpvBuyIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.otherBuyIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.cashBuyIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.markerBuyIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.watIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.totalBuyIn.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingAction.actualWin.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingSession.gameId) != -1);
            Assert.IsTrue(logString.IndexOf(ratingObject.ratingStatus.ToString()) != -1);
            Assert.IsTrue(logString.IndexOf(ratingObject.ratingSourceId) != -1);

        }

        [TestMethod]
        public void ExtractRatingObjects_ExtractsCorrectObjects()
        {
            //Arrange
            var rating = TestUtility.GenerateRatingInhouse(ratingStatus.Open);

            EObjectRatingAction controlRatingAction = ((EventRatingInhouse)rating).Rating.Action;
            EObjectRatingSession controlRatingSession = ((EventRatingInhouse)rating).Rating.Session;
            EObjectRating controlRatingObject = ((EventRatingInhouse)rating).Rating;

            //Act
            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            //Assert
            Assert.AreEqual<EObjectRatingAction>(controlRatingAction, ratingAction);
            Assert.AreEqual<EObjectRatingSession>(controlRatingSession, ratingSession);
            Assert.AreEqual<EObjectRating>(controlRatingObject, ratingObject);
        }


        [TestMethod]
        public void Validate_ValidatesRatingInhouse()
        {
            //Arrange
            var rating = TestUtility.GenerateRatingInhouse(ratingStatus.Void);

            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            rating.PropertyCode = "";
            ratingSession.locationId = "ff";
            ratingObject.patronId = "12345678";
            ratingObject.pointsEarned = -1;
            rating.transDateTime = DateTime.MinValue;
            ratingAction.ratedBy = "";
            ratingObject.ratingHostId = "abcdefghijklmop";
            ratingAction.cpvBuyIn = -1;
            ratingAction.otherBuyIn = -1;
            ratingAction.cashBuyIn = -1;
            ratingAction.markerBuyIn = -1;
            ratingAction.watIn = -1;
            ratingAction.totalBuyIn = -1;
            ratingAction.actualWin = -1;
            ratingSession.gameId = "";
            ratingObject.ratingStatus = ratingStatus.Void;
            ratingObject.ratingSourceId = "";

            //Act
            try
            {
                rating.Validate(ratingStatus.Update);
                //Assert
                Assert.Fail("Validate did not throw Exception on erroneous rating data.");
            }
            catch (WebAPIValidationException ex)
            {
                //Assert
                Assert.IsTrue(ex.Errors.Count != 0);
                var errorMessage = ex.ToErrorMessage();

                //Should be an error for every field
                Assert.IsTrue(errorMessage.IndexOf("PropertyCode") != -1);
                Assert.IsTrue(errorMessage.IndexOf("locationId") != -1);
                Assert.IsTrue(errorMessage.IndexOf("patronId") != -1);
                Assert.IsTrue(errorMessage.IndexOf("pointsEarned") != -1);
                Assert.IsTrue(errorMessage.IndexOf("transDateTime") != -1);
                Assert.IsTrue(errorMessage.IndexOf("ratedBy") != -1);
                Assert.IsTrue(errorMessage.IndexOf("ratingHostId") != -1);
                Assert.IsTrue(errorMessage.IndexOf("cpvBuyIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("otherBuyIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("cashBuyIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("markerBuyIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("watIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("totalBuyIn") != -1);
                Assert.IsTrue(errorMessage.IndexOf("actualWin") != -1);
                Assert.IsTrue(errorMessage.IndexOf("gameId") != -1);
                Assert.IsTrue(errorMessage.IndexOf("ratingStatus") != -1);
                Assert.IsTrue(errorMessage.IndexOf("ratingSourceId") != -1);
            }
            catch (Exception)
            {
                Assert.Fail("Incorrect exception type thrown.");
            }
        }
    }
}
