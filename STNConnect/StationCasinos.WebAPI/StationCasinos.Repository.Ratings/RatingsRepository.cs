using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.Repository.Interface.Ratings.PlayerTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.Repository.Ratings
{
    public class RatingsRepository : IRatingsRepository
    {

        private IPlayerTracking _playerTracking;

        public RatingsRepository(IPlayerTracking playerTracking)
        {
            _playerTracking = playerTracking;
        }

        public T CreateRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            return (T)_playerTracking.Create<T>(rating, gameCodeType);
        }

        public T UpdateRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            return (T)_playerTracking.Update<T>(rating, gameCodeType);

        }        

        public T VoidRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            return (T)_playerTracking.Void<T>(rating, gameCodeType);
        }
        
        private JurisdictionType GetJurisdiction(EventRating rating)
        {
            //TODO, no magic string here.
            if (rating.PropertyCode == "21" || rating.PropertyCode == "22")
                return JurisdictionType.ECExternal;
            else
                return JurisdictionType.Indeterminate;
        }
    }
}
