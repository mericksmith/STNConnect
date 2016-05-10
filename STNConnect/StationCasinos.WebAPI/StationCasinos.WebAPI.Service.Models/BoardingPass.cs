using System;
namespace StationCasinos.WebAPI.Service.Models
{

    public class BoardingPass
    {
        public string CardNumber
        {
            get;
            set;
        }

        public string Ocr
        {
            get;
            set;
        }

        public DateTime Expiration
        {
            get;
            set;
        }

        public bool IsLost
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public int BoardingPassId
        {
            get;
            set;
        }

        public string MagStripe
        {
            get;
            set;
        }

    }
}
