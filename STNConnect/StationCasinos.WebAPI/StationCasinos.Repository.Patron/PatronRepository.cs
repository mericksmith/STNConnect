using System;
using System.Collections.Generic;
using Enterprise = StationCasinos.EnterpriseObjects.Patron;
using StationCasinos.Repository.Interface.Patron;
using StationCasinos.WebAPI.Service.Interface;
using ServiceModels = StationCasinos.WebAPI.Service.Models;
using StationCasinos.WebAPI.Utility.Interface;

namespace StationCasinos.Repository.Patron
{
    public class PatronRepository : IPatronRepository
    {
        IPatronService _patronService;
        ILogging _logging;

        public PatronRepository(IPatronService patronService, ILogging logging)
        {
            _patronService = patronService;
            _logging = logging;
        }

        /// <summary>
        /// returns the Patron data based on magstripe provided
        /// </summary>
        /// <param name="magStripe">18 character magstripe string</param>
        /// <returns></returns>
        public Enterprise.Patron GetPatronByMagStripe(string magStripe)
        {
            Enterprise.Patron patron = new Enterprise.Patron();
            try
            {
                ServiceModels.Patron enterprisePatron = _patronService.GetPatronByMagStrip(magStripe);
                if (enterprisePatron != null)
                {
                    patron = ConvertPatron(enterprisePatron);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "PatronRepository.GetPatronByMagStripe");
                return null;
            }
            return patron;
        }

        public Enterprise.Patron GetPatronByPatronId(string patronId)
        {
            Enterprise.Patron patron = new Enterprise.Patron();
            try
            {
                var enterprisePatron = _patronService.GetPatronByPatronId(patronId);
                if (enterprisePatron != null)
                {
                    //For now populating only fields which need to be returned for Stadium.
                    patron = ConvertPatron(enterprisePatron);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "PatronRepository.GetPatronByPatronId");
                return null;
            }
            return patron;
        }

        private Enterprise.Patron ConvertPatron(ServiceModels.Patron enterprisePatron)
        {
            try
            {
                //For now populating only fields which need to be returned for Stadium.
                Enterprise.Patron patron = new Enterprise.Patron()
                {
                    PatronId = enterprisePatron.PatronId,
                    BoardingPass = new List<EnterpriseObjects.Patron.BoardingPass>(),
                    PatronProfile = new Enterprise.PatronProfile()
                    {
                        Dob = new Enterprise.DateTime()
                        {
                            DT = enterprisePatron.PatronProfile.Dob,
                            UtcOffsetMinutes = 0
                        },
                        FirstName = enterprisePatron.PatronProfile.FirstName,
                        LastName = enterprisePatron.PatronProfile.LastName
                    }
                };

                foreach (var boardingPass in enterprisePatron.BoardingPassList)
                {
                    patron.BoardingPass.Add(new Enterprise.BoardingPass()
                    {
                        CardNumber = boardingPass.CardNumber,
                        MagStripe = boardingPass.MagStripe
                    });
                }

                return patron;
            }
            catch (Exception ex)
            {
                _logging.Write(ex, "PatronRepository.ConvertPatron");
                return null;
            }
        }
    }
}
