using System;
namespace StationCasinos.WebAPI.Service.Models
{
    public class PatronIdentity
    {
        public string IdNumber
        {
            get;
            set;
        }

        public IdentificationType IdType
        {
            get;
            set;
        }

        public DateTime IdExpiration
        {
            get;
            set;
        }

        public StateProvince StateProvince
        {
            get;
            set;
        }

        public Country Country
        {
            get;
            set;
        }

        public DateTime VerifyDate
        {
            get;
            set;
        }

        public int VerifiedBy
        {
            get;
            set;
        }

        public int PatronIdentityId
        {
            get;
            set;
        }
    }
}
