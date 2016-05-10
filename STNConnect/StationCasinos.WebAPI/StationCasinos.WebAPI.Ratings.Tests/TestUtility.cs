using StationCasinos.Repository.Interface.Lookup;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.WebAPI.Ratings.Controllers;
using StationCasinos.WebAPI.Utility.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StationCasinos.WebAPI.Ratings.Tests
{
    public static class TestUtility
    {
        #region "Controller Mocks"


        public static InhouseRaceController GetInhouseRaceController(IRatingsRepository repo,ILogging logging)
        {
            InhouseRaceController controller = new InhouseRaceController(repo, logging);

            return (InhouseRaceController)ConfigureControllerInternal(controller);
        }

        public static SportsController GetSportsController(IRatingsRepository repo, ILogging logging)
        {
            SportsController controller = new SportsController(repo, logging);

            return (SportsController)ConfigureControllerInternal(controller);
        }

        public static ParimutuelController GetParimuetuelController(IRatingsRepository repo, ILogging logging)
        {
            ParimutuelController controller = new ParimutuelController(repo, logging);

            return (ParimutuelController)ConfigureControllerInternal(controller);
        }

        private static ApiController ConfigureControllerInternal(ApiController controller)
        {
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/ratings/inhouserace") //This URL doesn't seem to matter really for test purposes unless it's absent.
            };

            controller.Configuration = new HttpConfiguration();

            return controller;
        }

        #endregion

        #region "Property"
        public static string GetPropertyCodeFromStaticList(string propertyId)
        {
            return GetPropertyCodeList()[propertyId];
        }

        public static IDictionary<string, string> GetPropertyCodeList()
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            properties.Add("2", "BA"); //properties.Add("2", "Barleys");
            properties.Add("3", "BS"); //properties.Add("3", "Boulder Station");
            properties.Add("4", "FR"); //properties.Add("4", "Fiesta Rancho");
            properties.Add("5", "GR"); //properties.Add("5", "Wildfire Sunset");
            properties.Add("6", "GV"); //properties.Add("6", "Green Valley Ranch");
            properties.Add("7", "LM"); //properties.Add("7", "Wildfire Lake Mead");
            properties.Add("8", "MS"); //properties.Add("8", "Wildfire Boulder");
            properties.Add("9", "PS"); //properties.Add("9", "Palace Station");
            properties.Add("10", "RE"); //properties.Add("10", "Fiesta Henderson");
            properties.Add("11", "RN"); //properties.Add("11", "Wildfire Lanes");
            properties.Add("12", "RR"); //properties.Add("12", "Red Rock Station");
            properties.Add("13", "SF"); //properties.Add("13", "Santa Fe Station");
            properties.Add("14", "SS"); //properties.Add("14", "Sunset Station");
            properties.Add("15", "TG"); //properties.Add("15", "The Greens");            //TODO ??
            properties.Add("16", "TS"); //properties.Add("16", "Texas Station");
            properties.Add("17", "WF"); //properties.Add("17", "Wildfire Rancho");       //TODO ??
            properties.Add("18", "WW"); //properties.Add("18", "Wild Wild West");
            properties.Add("19", "NA"); //properties.Add("19", "");
            properties.Add("20", "NA"); //properties.Add("20", "");
            properties.Add("21", "NA"); //properties.Add("21", "");
            properties.Add("22", "NA"); //properties.Add("22", "");
            properties.Add("23", "WA"); //properties.Add("23", "Wildfire Anthem");       //TODO ??
            properties.Add("24", "WV"); //properties.Add("24", "Wildfire Valley View");  //TODO ??
            properties.Add("25", "R1"); //properties.Add("25", "Baldini Reno");

            return properties;
        }

        #endregion

        #region "GameCode"

        public static GameCodeInfo GetGameCodeInfo(string gameId, GameCodeType gameCodeType, string locationId)
        {
            if (gameCodeType == GameCodeType.Sports)
                return GetSportsGameCodeInfo(gameId, locationId);
            else
                return GetRaceGameCodeInfo(gameId, gameCodeType, locationId);
        }

        private static GameCodeInfo GetRaceGameCodeInfo(string gameId, GameCodeType gameCodeType, string locationId)
        {
            //Race can have separate codes and theo for parimutuel and inhouse race wagers

            List<GameCodeInfo> infoList = new List<GameCodeInfo>();

            infoList.Add(new GameCodeInfo() { GameCode = "IH", TheoPct = .10M, locationId = "0", wagerTypeId = "1", GameCodeType = GameCodeType.InhouseRace });
            infoList.Add(new GameCodeInfo() { GameCode = "PE", TheoPct = .22M, locationId = "0", wagerTypeId = "1", GameCodeType = GameCodeType.Parimutuel });

            var results = infoList.Where(x => x.wagerTypeId == gameId && x.locationId == locationId && x.GameCodeType == gameCodeType)?.ToList();

            if(results.Count != 0)
                return results[0];
            else
                return new GameCodeInfo() { GameCode = "IH", TheoPct = .10M, locationId = "0", wagerTypeId = "1", GameCodeType = GameCodeType.InhouseRace };
        }

        private static GameCodeInfo GetSportsGameCodeInfo(string gameId, string locationId)
        {
            
            List<GameCodeInfo> infoList = new List<GameCodeInfo>();

            infoList.Add(new GameCodeInfo() { GameCode = "SG", TheoPct = .04M, locationId = "0", wagerTypeId = "1", GameCodeType = GameCodeType.Sports });

            var results = infoList.Where(x => x.wagerTypeId == gameId && x.locationId == locationId)?.ToList();

            if (results.Count != 0)
                return results[0];
            else
                return new GameCodeInfo() { GameCode = "IH", TheoPct = .10M, locationId = "0", wagerTypeId = "1" };

        }

        #endregion

        #region "GenerateObjects"

        public static EventRatingParimutuel GenerateRatingParimutuel(ratingStatus status)
        {
            EObjectRatingParimutuel ratingInhouse = new EObjectRatingParimutuel();
            ratingInhouse.Session = new EObjectRatingSessionParimutuel();
            ratingInhouse.Session.gameId = "1";
            ratingInhouse.Session.locationId = "1";

            ratingInhouse.Action = new EObjectRatingActionParimutuel();
            ratingInhouse.Action.ratedBy = "sourceReference";
            ratingInhouse.Action.actualWin = 0;//req
            ratingInhouse.Action.markerBuyIn = 0; //req
            ratingInhouse.Action.cpvBuyIn = 10; //req
            ratingInhouse.Action.watIn = 5;
            ratingInhouse.Action.otherBuyIn = 0;
            ratingInhouse.Action.cashBuyIn = 0;
            ratingInhouse.Action.totalBuyIn = 15;

            ratingInhouse.patronId = "5109926";
            ratingInhouse.pointsEarned = 100;
            if (status == ratingStatus.Open)
            {
                ratingInhouse.ratingHostId = GenerateNumber(11) + "000";
            }
            else
            {
                ratingInhouse.ratingHostId = GenerateNumber(11) + "001";
            }
            
            ratingInhouse.ratingStatus = status;
            ratingInhouse.ratingSourceId = "sourceId";

            var blah = new EventRatingParimutuel();
            blah.PropertyCode = "3";
            blah.Rating = ratingInhouse;
            blah.transDateTime = DateTime.Now;
            return blah;
        }

        public static EventRatingInhouse GenerateRatingInhouse(ratingStatus status)
        {

            EObjectRatingInhouse ratingInhouse = new EObjectRatingInhouse();
            ratingInhouse.Session = new EObjectRatingSessionInhouse();
            ratingInhouse.Session.gameId = "1";
            ratingInhouse.Session.locationId = "1";

            ratingInhouse.Action = new EObjectRatingActionInhouse();
            ratingInhouse.Action.ratedBy = "sourceReference";
            ratingInhouse.Action.actualWin = 0;//req
            ratingInhouse.Action.markerBuyIn = 0; //req
            ratingInhouse.Action.cpvBuyIn = 10; //req
            ratingInhouse.Action.watIn = 5;
            ratingInhouse.Action.otherBuyIn = 0;
            ratingInhouse.Action.cashBuyIn = 0;
            ratingInhouse.Action.totalBuyIn = 15;

            ratingInhouse.patronId = "5109926";
            ratingInhouse.pointsEarned = 100;
            if (status == ratingStatus.Open)
            {
                ratingInhouse.ratingHostId = GenerateNumber(11) + "000";
            }
            else
            {
                ratingInhouse.ratingHostId = GenerateNumber(11) + "001";
            }
            ratingInhouse.ratingStatus = status;
            ratingInhouse.ratingSourceId = "sourceId";

            var blah = new EventRatingInhouse();
            blah.PropertyCode = "3";
            blah.Rating = ratingInhouse;
            blah.transDateTime = DateTime.Now;
            return blah;

        }

        public static string GenerateNumber(int length)
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i <= length; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }

        #endregion
    }
}
