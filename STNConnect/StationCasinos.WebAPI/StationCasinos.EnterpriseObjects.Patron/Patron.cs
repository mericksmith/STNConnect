using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.EnterpriseObjects.Patron
{
    public class Patron
    {
        public string PatronId
        {
            get;
            set;
        }

        public PatronProfile PatronProfile
        {
            get;
            set;
        }

        public List<BoardingPass> BoardingPass
        {
            get;
            set;
        }
    }
}
