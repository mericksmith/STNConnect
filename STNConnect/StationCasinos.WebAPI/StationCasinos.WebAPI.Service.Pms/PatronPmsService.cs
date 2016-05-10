using StationCasinos.WebAPI.Service.Models;
using StationCasinos.WebAPI.Service.Pms.MessagingCommands;
using System.Xml.Linq;
using StationCasinos.WebAPI.Service.Interface;
using StationCasinos.WebAPI.Utility.Interface;
using System;

namespace StationCasinos.WebAPI.Service.Pms
{
    public class PatronPmsService:IPatronService
    {
        private static XNamespace ns = "http://StationCasinos.net/EnterpriseMessaging/V1";
        ILogging _logging;

        
        public PatronPmsService(ILogging logging)
        {
            _logging = logging;
        }

        public Patron GetPatronByMagStrip(string magStripe)
        {
            try
            {
                var patronRequest = new GetPatronRequest { Magstripe = magStripe };
                var patronCommand = new GetPatronCommand(ns, patronRequest);
                var patronResponse = patronCommand.ProcessRequest();
                var enterprisePatron = patronResponse.Patron;
                return enterprisePatron;
            }
            catch(Exception ex)
            {
                _logging.Write(ex, "PatronPmsService.GetPatronByMagStrip");
                return null;
            }
        }
        public Patron GetPatronByPatronId(string patronId)
        {
            try
            {
                var patronRequest = new GetPatronRequest { PatronId = patronId };
                var patronCommand = new GetPatronCommand(ns, patronRequest);
                var patronResponse = patronCommand.ProcessRequest();
                var enterprisePatron = patronResponse.Patron;
                return enterprisePatron;
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "PatronPmsService.GetPatronByPatronId");
                return null;
            }
        }
    }
}
