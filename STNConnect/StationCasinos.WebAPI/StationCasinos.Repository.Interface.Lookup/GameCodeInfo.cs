using StationCasinos.Repository.Interface.Ratings;

namespace StationCasinos.Repository.Interface.Lookup
{
    public class GameCodeInfo
    {
        public GameCodeType GameCodeType { get; set; }

        public string GameCode { get; set; }

        public decimal TheoPct { get; set; }

        public string locationId { get; set; }

        public string wagerTypeId { get; set; }
    }
}
