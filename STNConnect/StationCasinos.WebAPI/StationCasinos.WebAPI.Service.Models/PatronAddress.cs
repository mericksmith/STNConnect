using System;
using System.Collections.Generic;

namespace StationCasinos.WebAPI.Service.Models
{
    public class PatronAddress
    {
        private List<string> addressLineList = new List<string>();

        public List<string> AddressLineList
        {
            get
            {
                if (this.addressLineList == null)
                {
                    this.addressLineList = new List<string>();
                }

                return this.addressLineList;
            }
        }

        public string City
        {
            get;
            set;
        }

        public StateProvince StateProvince
        {
            get;
            set;
        }

        public string PostalCode
        {
            get;
            set;
        }

        public Country Country
        {
            get;
            set;
        }

        public bool IsPrimary
        {
            get;
            set;
        }

        public bool IsMail
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

        public int PatronAddressId
        {
            get;
            set;
        }
    }
}
