using System.Xml.Linq;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class CountryFromXmlAssembler : ObjectFromXmlAssembler<Country>
    {
        public CountryFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // CountryId attribute
            var countryIdElement = this.Element.Attribute(this.Namespace + "CountryId");
            if (countryIdElement != null)
            {
                int countryId = 0;
                if (int.TryParse(countryIdElement.Value, out countryId))
                {
                    this.ObjectToAssemble.CountryId = countryId;
                }
            }

            // CountryDescription attribute
            var countryDescriptionElement = this.Element.Attribute(this.Namespace + "CountryDescription");
            if (countryDescriptionElement != null)
            {
                this.ObjectToAssemble.CountryDescription = countryDescriptionElement.Value;
            }

            // Country code element
            var countryCode = this.Element.Value;
            if (countryCode != null)
            {
                this.ObjectToAssemble.Value = countryCode;
            }
        }
    }
}