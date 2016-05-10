using System.Collections.Generic;



namespace StationCasinos.WebAPI.Service.Models
{
    public class Patron
    {
        private List<PatronAddress> patronAddressList = new List<PatronAddress>();
        private List<PatronIdentity> patronIdentityList = new List<PatronIdentity>();
        private List<PatronEmail> patronEmailList = new List<PatronEmail>();
        private List<PatronPhone> patronPhoneList = new List<PatronPhone>();
        private List<BoardingPass> boardingPassList = new List<BoardingPass>();
        private List<PropertyDetail> preferredPropertyList = new List<PropertyDetail>();

        public PatronProfile PatronProfile
        {
            get;
            set;
        }

        public List<PatronAddress> PatronAddressList
        {
            get
            {
                if (this.patronAddressList == null)
                {
                    this.patronAddressList = new List<PatronAddress>();
                }

                return this.patronAddressList;
            }
        }

        public List<PatronIdentity> PatronIdentityList
        {
            get
            {
                if (this.patronIdentityList == null)
                {
                    this.patronIdentityList = new List<PatronIdentity>();
                }

                return this.patronIdentityList;
            }
        }

        public List<PatronEmail> PatronEmailList
        {
            get
            {
                if (this.patronEmailList == null)
                {
                    this.patronEmailList = new List<PatronEmail>();
                }

                return this.patronEmailList;
            }
        }

        public List<PatronPhone> PatronPhoneList
        {
            get
            {
                if (this.patronPhoneList == null)
                {
                    this.patronPhoneList = new List<PatronPhone>();
                }

                return this.patronPhoneList;
            }
        }

        public List<BoardingPass> BoardingPassList
        {
            get
            {
                if (this.boardingPassList == null)
                {
                    this.boardingPassList = new List<BoardingPass>();
                }

                return this.boardingPassList;
            }
        }

        public List<PropertyDetail> PreferredPropertyList
        {
            get
            {
                if (this.preferredPropertyList == null)
                {
                    this.preferredPropertyList = new List<PropertyDetail>();
                }

                return this.preferredPropertyList;
            }
        }

        public PatronPin PatronPin
        {
            get;
            set;
        }

        public string PatronId
        {
            get;
            set;
        }
    }
}
