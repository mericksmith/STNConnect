using StationCasinos.Repository.Interface.Lookup;
using StationCasinos.Repository.Interface.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationCasinos.Repository.Lookup.SCEnterpriseService;
namespace StationCasinos.Repository.Lookup
{
    public class LookupRepository : ILookupRepository
    {
        public GameCodeInfo GetGameCodeInfo(string gameId, GameCodeType gameCodeType, string locationId)
        {
            SCEnterpriseService.MetadataResourceClient client = new MetadataResourceClient();
            GameCodeInfo gameCodeInfo = new GameCodeInfo();

            if (gameCodeType == GameCodeType.Sports)
            {
                SportsGameCodeData data = client.GetSportsGameCodeDataById(int.Parse(gameId), int.Parse(locationId));
                if (data != null)
                {
                    gameCodeInfo.locationId = data.LocationID.ToString();
                    gameCodeInfo.TheoPct = data.TheoPct;
                    gameCodeInfo.wagerTypeId = data.SportsBetTypeID.ToString();
                    gameCodeInfo.GameCode = data.GameCode;
                    gameCodeInfo.GameCodeType = gameCodeType;
                }
            }
            else
            {
                RaceGameCodeData data = null;
                if (gameCodeType == GameCodeType.InhouseRace)
                {
                    data = client.GetRaceGameCodeDataById(int.Parse(gameId), int.Parse(locationId), false);
                }
                else
                {
                    data = client.GetRaceGameCodeDataById(int.Parse(gameId), int.Parse(locationId), true);
                }

                if(data != null)
                {
                    gameCodeInfo.locationId = data.LocationID.ToString();
                    gameCodeInfo.TheoPct = data.TheoPct;
                    gameCodeInfo.wagerTypeId = data.RaceBetTypeID.ToString();
                    gameCodeInfo.GameCode = data.GameCode;
                    gameCodeInfo.GameCodeType = gameCodeType;
                }
            }
            
            return gameCodeInfo;
        }

        public string GetPropertyCode(string propertyId)
        {   
            SCEnterpriseService.MetadataResourceClient client = new MetadataResourceClient();
            PropertyData property = client.RetrieveProperty(int.Parse(propertyId));
            return property.Abbreviation;
        }

    }
}
