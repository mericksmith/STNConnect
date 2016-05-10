using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.Repository.Interface.Ratings
{
    public interface IRatingsRepository
    {
        T CreateRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;

        T UpdateRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;

        T VoidRating<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating;


        
    }
}
