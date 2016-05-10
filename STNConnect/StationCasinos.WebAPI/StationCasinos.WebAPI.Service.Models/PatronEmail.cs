namespace StationCasinos.WebAPI.Service.Models
{
        public class PatronEmail
        {
            public string EmailAddress
            {
                get;
                set;
            }

            public EmailType EmailType
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

            public bool CanSendTo
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

            public int PatronEmailId
            {
                get;
                set;
            }
        }
    }
