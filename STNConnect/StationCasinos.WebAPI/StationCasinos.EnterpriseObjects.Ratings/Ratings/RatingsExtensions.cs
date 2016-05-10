using StationCasinos.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.EnterpriseObjects.Ratings
{
    public static class RatingExtensions
    {
        public static void ExtractRatingObjects(this EventRating rating, out EObjectRatingAction ratingAction, out EObjectRatingSession ratingSession, out EObjectRating ratingObject)
        {
            if (rating.GetType() == typeof(EventRatingInhouse))
            {
                ratingObject = ((EventRatingInhouse)rating).Rating;
                ratingAction = ((EventRatingInhouse)rating).Rating.Action;
                ratingSession = ((EventRatingInhouse)rating).Rating.Session;
            }
            else
            {
                ratingObject = ((EventRatingParimutuel)rating).Rating;
                ratingAction = ((EventRatingParimutuel)rating).Rating.Action;
                ratingSession = ((EventRatingParimutuel)rating).Rating.Session;
            }
        }

        /// <summary>
        /// Includes special validation for EventRating type objects alongside annotated validation.
        /// </summary>
        /// <param name="rating"></param>
        /// <param name="ratingStatus">Action for rating being validated.</param>
        public static void Validate(this EventRating rating, ratingStatus ratingStatus)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (rating == null)
            {
                results.Add(new ValidationResult("EventRating object null or not correctly resolved from request body."));
                throw new WebAPIValidationException(results);
            }
                
            //First do the date annotation exceptions.
            try
            {
                rating.Validate();
            }
            catch (WebAPIValidationException validEx)
            {
                results.AddRange(validEx.Errors);
            }
            
            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);
            
            //ratingHostId
            if (ratingStatus != ratingStatus.Open)
            {
                //Validate ratingHostId for non Open Action
                if (string.IsNullOrWhiteSpace(ratingObject.ratingHostId) || ratingObject.ratingHostId.Length != 14)
                {
                    results.Add(new ValidationResult("ratingHostId field must be 14 characters in length."));
                }
                else
                {
                    //Check sequence ID
                    var seq = ratingObject?.ratingHostId?.Substring(11, 3);
                    int test = 0;
                    if (!int.TryParse(seq, out test))
                    {
                        results.Add(new ValidationResult("ratingHostId field final 3 digits must be numeric."));
                    }
                }
            }

            if (rating.transDateTime == DateTime.MinValue)
            {
                results.Add(new ValidationResult("transDateTime must be provided."));
            }

            //totalBuyIn
            if (ratingAction.totalBuyIn != ratingAction.cpvBuyIn + ratingAction.cashBuyIn + ratingAction.markerBuyIn + ratingAction.otherBuyIn + ratingAction.watIn)
            {
                results.Add(new ValidationResult("totalBuyIn not equal to the sum of individual buy in types"));
            }

            //Must provide non zero totalBuyIn value.
            if (ratingAction.totalBuyIn == 0)
            {
                results.Add(new ValidationResult("totalBuyIn value cannot be zero."));
            }

            //ratingStatus
            if (ratingStatus != ratingObject.ratingStatus)
            {
                results.Add(new ValidationResult(string.Format("ratingStatus ({0}) does not match WebAPI verb implication ({1}).", ratingStatus.ToString(), ratingObject.ratingStatus.ToString())));
            }

            if (results.Count > 0)
                throw new WebAPIValidationException(results);
        }

        public static string ToLog(this EventRating rating)
        {
            if (rating == null)
                return "EventRating null";

            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            return string.Format("EventRating message - propertyCode: {0}  locationId: {1}  patronId: {2}  pointsEarned: {3}  transDateTime: {4} " +
                          " ratedBy: {5}  ratingHostId: {6}  cpvBuyIn: {7}  otherBuyIn: {8}  cashBuyIn: {9}  markerBuyIn: {10}  watIn: {11} " +
                          " totalBuyIn: {12}  actualWin: {13}  gameId: {14}  ratingStatus: {15} ratingSourceId: {16}",
                            rating?.PropertyCode,
                            ratingSession?.locationId,
                            ratingObject?.patronId,
                            ratingObject?.pointsEarned,
                            rating?.transDateTime,
                            ratingAction?.ratedBy,
                            ratingObject?.ratingHostId,
                            ratingAction?.cpvBuyIn,
                            ratingAction?.otherBuyIn,
                            ratingAction?.cashBuyIn,
                            ratingAction?.markerBuyIn,
                            ratingAction?.watIn,
                            ratingAction?.totalBuyIn,
                            ratingAction?.actualWin,
                            ratingSession?.gameId,
                            ratingObject?.ratingStatus,
                            ratingObject?.ratingSourceId
                            );
            
        }
    }
}
