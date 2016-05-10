using StationCasinos.Repository.Interface.Ratings;

namespace StationCasinos.Repository.Interface.Lookup
{
    public interface ILookupRepository
    {
        string GetPropertyCode(string propertyId);

        GameCodeInfo GetGameCodeInfo(string gameId, GameCodeType gameCodeType, string locationId);
    }
}
