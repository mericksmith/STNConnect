using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class IdentificationTypeFromXmlAssembler : ObjectFromXmlAssembler<IdentificationType>
    {
        public IdentificationTypeFromXmlAssembler(XElement element): base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // IdentificationTypeCode element
            this.ObjectToAssemble.Value = this.Element.Value;

            // IdentificationTypeId element
            var identificationTypeIdElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IdentificationTypeId"), namespaceManager);
            if (identificationTypeIdElement != null)
            {
                int identificationTypeId = 0;
                if (int.TryParse(identificationTypeIdElement.Value, out identificationTypeId))
                {
                    this.ObjectToAssemble.IdentificationTypeId = identificationTypeId;
                }
            }

            // IdentificationTypeDescription element
            var identificationTypeDescriptionElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IdentificationTypeDescription"), namespaceManager);
            if (identificationTypeDescriptionElement != null)
            {
                this.ObjectToAssemble.IdentificationTypeDescription = identificationTypeDescriptionElement.Value;
            }
        }
    }
}