using System;
namespace StationCasinos.WebAPI.Service.Models
{
        public class PatronProfile
        {
            public string Title
            {
                get;
                set;
            }

            public string FirstName
            {
                get;
                set;
            }

            public string MiddleInitial
            {
                get;
                set;
            }

            public string LastName
            {
                get;
                set;
            }

            public string Suffix
            {
                get;
                set;
            }

            public string Nickname
            {
                get;
                set;
            }

            public string MaidenName
            {
                get;
                set;
            }

            public string SSNumber
            {
                get;
                set;
            }

            public DateTime Dob
            {
                get;
                set;
            }

            public GenderType Gender
            {
                get;
                set;
            }

            public MaritalStatusType MaritalStatus
            {
                get;
                set;
            }

            public bool IsVip
            {
                get;
                set;
            }
        }
    }
