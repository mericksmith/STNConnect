using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StationCasinos.Repository.Interface.Patron;
using Enterprise = StationCasinos.EnterpriseObjects.Patron;
using StationCasinos.WebAPI.Patron.Controllers;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using StationCasinos.WebAPI.Utility.Interface;

namespace Patron.WebAPI.Tests
{
    [TestClass]
    public class PatronControllerTest
    {
        [TestMethod]
        public void GetPatronByMagStripe_TestMethod()
        {
            Mock<IPatronRepository> mockRepository = new Mock<IPatronRepository>();

            Enterprise.Patron patron = new Enterprise.Patron();

            var json = @"{""PatronId"": ""3012508"",""PatronProfile"": {""FirstName"": ""Jody"",""LastName"": ""Capasso"",""Dob"": {""DT"": ""1973-11-10T00:00:00"",""UtcOffsetMinutes"": ""0""}},""BoardingPass"": [{""MagStripe"": ""013042151503012508"",""CardNumber"": ""3012508""}]}";
            patron = JsonConvert.DeserializeObject<Enterprise.Patron>(json);
            mockRepository.Setup(x => x.GetPatronByMagStripe("013042151503012508")).Returns(patron);

            Mock<ILogging> mockLogging = new Mock<ILogging>();

            // Arrange
            PatronsController controller = new PatronsController(mockRepository.Object, mockLogging.Object);
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/patrons")
            };

            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            

            // Act
            var response = controller.Get("013042151503012508");

            // Assert
            Enterprise.Patron returnedPatron;
            Assert.IsTrue(response.TryGetContentValue(out returnedPatron));
            
            // Assert
            Assert.AreEqual(patron, returnedPatron);
        }
    }
}
