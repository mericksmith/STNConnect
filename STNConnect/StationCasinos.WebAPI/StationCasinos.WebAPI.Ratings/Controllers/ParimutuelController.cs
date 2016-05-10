using StationCasinos.EnterpriseObjects.Ratings;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.WebAPI.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StationCasinos.WebAPI.Utility.Interface;

namespace StationCasinos.WebAPI.Ratings.Controllers
{
    [Route("api/ratings/parimutuel")]
    public class ParimutuelController : ApiController
    {

        IRatingsRepository _ratingsRepository;
        ILogging _logging;

        public ParimutuelController(IRatingsRepository ratingsRepository, ILogging logging)
        {
            _ratingsRepository = ratingsRepository;
            _logging = logging;
        }
        
        // POST api/<controller>/parimutuel
        public HttpResponseMessage Post(EventRatingParimutuel rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "Parimutuel.POST");
                rating.Validate(ratingStatus.Open);

                var returnObject = _ratingsRepository.CreateRating<EventRatingParimutuel>(rating, GameCodeType.Parimutuel);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Parimutuel.POST");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Parimutuel.POST");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Parimutuel.POST");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to update rating: {0}", ex.Message));
            }

        }
        
        // PUT api/<controller>/parimutuel
        public HttpResponseMessage Put(EventRatingParimutuel rating)
        {
            try
            {
                if (rating?.Rating?.ratingStatus == ratingStatus.Void)
                {
                    return Delete(rating);
                }
                else
                {
                    _logging.Write(rating.ToLog(), "Parimutuel.PUT");
                    rating.Validate(ratingStatus.Update);

                    var returnObject = _ratingsRepository.UpdateRating<EventRatingParimutuel>(rating, GameCodeType.Parimutuel);
                    return Request.CreateResponse(HttpStatusCode.Created, returnObject);
                }
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Parimutuel.PUT");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Parimutuel.PUT");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Parimutuel.PUT");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to create rating: {0}", ex.Message));
            }

        }
        
        // DELETE api/<controller>/parimutuel
        private HttpResponseMessage Delete(EventRatingParimutuel rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "Parimutuel.DELETE");
                rating.Validate(ratingStatus.Void);

                var returnObject = _ratingsRepository.VoidRating<EventRatingParimutuel>(rating, GameCodeType.Parimutuel);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "Parimutuel.DELETE");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "Parimutuel.DELETE");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "Parimutuel.DELETE");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to void rating: {0}", ex.Message));
            }
        }
    }
}