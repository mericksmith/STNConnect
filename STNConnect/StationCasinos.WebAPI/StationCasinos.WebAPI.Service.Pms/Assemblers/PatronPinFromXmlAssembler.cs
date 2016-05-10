using System;
using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class PatronPinFromXmlAssembler : ObjectFromXmlAssembler<PatronPin>
    {
        public PatronPinFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // PinNumber element
            var pinNumberElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PinNumber"), namespaceManager);
            if (pinNumberElement != null)
            {
                this.ObjectToAssemble.PinNumber = pinNumberElement.Value;
            }

            // HashType element
            var hashTypeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("HashType"), namespaceManager);
            if (hashTypeElement != null)
            {
                PinHashType pinHash = PinHashType.None;
                if (Enum.TryParse<PinHashType>(hashTypeElement.Value, out pinHash))
                {
                    this.ObjectToAssemble.HashType = pinHash;
                }
            }
        }
    }
}