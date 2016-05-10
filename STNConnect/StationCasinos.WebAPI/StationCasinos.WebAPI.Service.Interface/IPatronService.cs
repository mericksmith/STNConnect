using StationCasinos.WebAPI.Service.Models;
namespace StationCasinos.WebAPI.Service.Interface
{
    public interface IPatronService
    {
        Patron GetPatronByMagStrip(string magStripe);
        Patron GetPatronByPatronId(string patronId);
    }
}
