using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.WebAPI.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StationCasinos.WebAPI.Utility.Interface;
using StationCasinos.EnterpriseObjects.Ratings;

namespace StationCasinos.WebAPI.Ratings.Controllers
{
    [Route("api/ratings/sports")]
    public class SportsController : ApiController
    {
        IRatingsRepository _ratingsRepository;
        ILogging _logging;

        public SportsController(IRatingsRepository ratingsRepository, ILogging logging)
        {
            _ratingsRepository = ratingsRepository;
            _logging = logging;
        }


        // POST api/<controller>/Inhouse
        public HttpResponseMessage Post(EventRatingInhouse rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "Sports.POST");
                rating.Validate(ratingStatus.Open);

                var returnObject = _ratingsRepository.CreateRating<EventRatingInhouse>(rating, GameCodeType.Sports);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Sports.POST");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Sports.POST");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Sports.POST");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to create rating: {0}", ex.Message));
            }

        }


        public HttpResponseMessage Put(EventRatingInhouse rating)
        {
            try
            {
                if (rating?.Rating?.ratingStatus == ratingStatus.Void)
                {
                    return Delete(rating);
                }
                else
                {
                    _logging.Write(rating.ToLog(), "Sports.PUT");
                    rating.Validate(ratingStatus.Update);
                    var returnObject = _ratingsRepository.UpdateRating<EventRatingInhouse>(rating, GameCodeType.Sports);
                    return Request.CreateResponse(HttpStatusCode.Created, returnObject);
                }
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Sports.PUT");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Sports.PUT");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Sports.PUT");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to update rating: {0}", ex.Message));
            }
        }

        private HttpResponseMessage Delete(EventRatingInhouse rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "Sports.DELETE");
                rating.Validate(ratingStatus.Void);
                var returnObject = _ratingsRepository.VoidRating<EventRatingInhouse>(rating, GameCodeType.Sports);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Sports.DELETE");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Sports.DELETE");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Sports.DELETE");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to void rating: {0}", ex.Message));
            }
        }
    }
}