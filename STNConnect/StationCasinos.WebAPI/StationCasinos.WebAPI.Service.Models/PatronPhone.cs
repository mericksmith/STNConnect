namespace StationCasinos.WebAPI.Service.Models
{
    public class PatronPhone
    {
        public PhoneType PhoneType
        {
            get;
            set;
        }

        public string CountryCode
        {
            get;
            set;
        }

        public string AreaCode
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public bool IsPrimary
        {
            get;
            set;
        }

        public bool CanCall
        {
            get;
            set;
        }

        public bool ExcludeFromLists
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public int PatronPhoneId
        {
            get;
            set;
        }
    }
}
