using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class PropertyDetailFromXmlAssembler : ObjectFromXmlAssembler<PropertyDetail>
    {
        public PropertyDetailFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // PropertyId element
            var propertyIdElement = this.Element.Attribute(this.Namespace + "PropertyId");
            if (propertyIdElement != null)
            {
                int propertyId = 0;
                if (int.TryParse(propertyIdElement.Value, out propertyId))
                {
                    this.ObjectToAssemble.PropertyId = propertyId;
                }
            }

            // PropertyDescription element
            var propertyDescriptionElement = this.Element.Attribute(this.Namespace + "PropertyDescription");
            if (propertyDescriptionElement != null)
            {
                this.ObjectToAssemble.PropertyDescription = propertyDescriptionElement.Value;
            }

            // Property code element
            var propertyCodeElement = this.Element.Value;
            if (propertyCodeElement != null)
            {
                this.ObjectToAssemble.Value = propertyCodeElement;
            }
        }
    }
}