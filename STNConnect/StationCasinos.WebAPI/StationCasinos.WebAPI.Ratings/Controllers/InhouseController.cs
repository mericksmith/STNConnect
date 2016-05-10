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
    [Route("api/ratings/inhouserace")]
    public class InhouseRaceController : ApiController
    {
        IRatingsRepository _ratingsRepository;
        ILogging _logging;

        public InhouseRaceController(IRatingsRepository ratingsRepository, ILogging logging)
        {
            _ratingsRepository = ratingsRepository;
            _logging = logging;
        }


        // POST api/<controller>/Inhouse
        public HttpResponseMessage Post(EventRatingInhouse rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "InhouseRace.POST");
                rating.Validate(ratingStatus.Open);

                var returnObject = _ratingsRepository.CreateRating<EventRatingInhouse>(rating, GameCodeType.InhouseRace);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "InhouseRace.POST");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "InhouseRace.POST");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "InhouseRace.POST");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to create rating: {0}", ex.Message));
            }

        }


        // PUT api/<controller>/Inhouse
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
                    _logging.Write(rating.ToLog(), "InhouseRace.PUT");
                    rating.Validate(ratingStatus.Update);

                    var returnObject = _ratingsRepository.UpdateRating<EventRatingInhouse>(rating, GameCodeType.InhouseRace);
                    return Request.CreateResponse(HttpStatusCode.Created, returnObject);
                }
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "InhouseRace.PUT");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "InhouseRace.PUT");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "InhouseRace.PUT");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to update rating: {0}", ex.Message));
            }
        }


        // DELETE api/<controller>/Inhouse
        private HttpResponseMessage Delete(EventRatingInhouse rating)
        {
            try
            {
                _logging.Write(rating.ToLog(), "InhouseRace.DELETE");
                rating.Validate(ratingStatus.Void);

                var returnObject = _ratingsRepository.VoidRating<EventRatingInhouse>(rating, GameCodeType.InhouseRace);
                return Request.CreateResponse(HttpStatusCode.Created, returnObject);
            }
            catch (WebAPIValidationException validEx)
            {
                _logging.Write(validEx.ToErrorMessage(), "InhouseRace.DELETE");
                return Request.CreateErrorResponse(validEx.StatusCode, validEx.ToErrorMessage());
            }
            catch (WebAPIException webEx)
            {
                _logging.Write(webEx, "InhouseRace.DELETE");
                return Request.CreateErrorResponse(webEx.StatusCode, webEx.Message);
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "InhouseRace.DELETE");
                return Request.CreateResponse<string>(HttpStatusCode.Ambiguous, string.Format("Failed to void rating: {0}", ex.Message));
            }
        }
    }
}