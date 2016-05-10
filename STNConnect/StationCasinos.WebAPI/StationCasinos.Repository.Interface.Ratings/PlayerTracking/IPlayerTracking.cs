using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.Repository.Interface.Ratings
{
    public interface IPlayerTracking
    {
        EventRating Create<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;


        EventRating Update<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;


        EventRating Void<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;
        
    }
}
