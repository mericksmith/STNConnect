using System.Web.Http;
using System.Net.Http;
using StationCasinos.Repository.Interface.Patron;
using Enterprise = StationCasinos.EnterpriseObjects.Patron;
using System.Net;
using System;
using StationCasinos.WebAPI.Utility.Interface;

namespace StationCasinos.WebAPI.Patron.Controllers
{
    public class PatronsController : ApiController
    {
        IPatronRepository _repository;
        ILogging _logging;

        private const int VALID_PATRON_ID_LENGTH = 7;
        private const int VALID_MIN_PATRON_ID_VALUE = 3000000;

        public PatronsController(IPatronRepository repository, ILogging logging)
        {
            _repository = repository;
            _logging = logging;
        }

        public HttpResponseMessage Get(string id)
        {
            _logging.Write(string.Format("GetPatron request received for {0}", id), "Patrons.Get");

            try
            {
                Enterprise.Patron patron;

                //Check if passed string is a valid PatronId
                //Use it for retrieving patronId
                //else use Magstripe method to retrieve
                if (IsValidPatronId(id))
                {
                    patron = _repository.GetPatronByPatronId(id);
                }
                else
                {
                    patron = _repository.GetPatronByMagStripe(id);
                }

                if (patron != null)
                {
                    _logging.Write(string.Format("Patron found for {0}", id), "Patrons.Get");
                    return Request.CreateResponse<Enterprise.Patron>(HttpStatusCode.OK, patron);
                }
                else
                {
                    _logging.Write(string.Format("Patron not found for {0}", id), "Patrons.Get");
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch(Exception ex)
            {
                _logging.Write(ex, "Patrons.Get");
                return Request.CreateResponse<string>(HttpStatusCode.InternalServerError, "Internal Error occured. Please try again.");
            }
        }

        private bool IsValidPatronId(string patronIdentifier)
        {
            if (patronIdentifier.Length == VALID_PATRON_ID_LENGTH)
            {
                Int32 patronId;
                if (Int32.TryParse(patronIdentifier, out patronId))
                {
                    if (patronId > VALID_MIN_PATRON_ID_VALUE)
                        return true;
                }
            }
            return false;
        }
    }
}
