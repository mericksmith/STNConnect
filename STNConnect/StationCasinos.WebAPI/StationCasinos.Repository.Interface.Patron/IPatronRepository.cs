using Enterprise = StationCasinos.EnterpriseObjects.Patron;

namespace StationCasinos.Repository.Interface.Patron
{
    public interface IPatronRepository
    {
        Enterprise.Patron GetPatronByMagStripe(string magStrip);

        Enterprise.Patron GetPatronByPatronId(string patonId);
    }
}
