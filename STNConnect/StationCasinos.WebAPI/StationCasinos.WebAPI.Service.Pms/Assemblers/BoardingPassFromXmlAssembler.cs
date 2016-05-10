using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class BoardingPassFromXmlAssembler : ObjectFromXmlAssembler<BoardingPass>
    {
        public BoardingPassFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // BoardingPassId attribute
            var boardingPassIdAttribute = this.Element.Attribute(this.Namespace + "BoardingPassId");
            if (boardingPassIdAttribute != null)
            {
                int boardingPassId = 0;
                if (int.TryParse(boardingPassIdAttribute.Value, out boardingPassId))
                {
                    this.ObjectToAssemble.BoardingPassId = boardingPassId;
                }
            }

            // CardNumber element
            var cardNumberElement = this.Element.XPathSelectElement(this.FormatXPathExpression("CardNumber"), namespaceManager);
            if (cardNumberElement != null)
            {
                this.ObjectToAssemble.CardNumber = cardNumberElement.Value;
            }

            // OCR element
            var ocrElement = this.Element.XPathSelectElement(this.FormatXPathExpression("OCR"), namespaceManager);
            if (ocrElement != null)
            {
                this.ObjectToAssemble.Ocr = ocrElement.Value;
            }

            // Expiration element
            var expirationElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Expiration"), namespaceManager);
            if (expirationElement != null)
            {
                DateTimeOffsetFromXmlAssembler dateTimeOffsetFromXmlAssembler = new DateTimeOffsetFromXmlAssembler(expirationElement.ToString());
                this.ObjectToAssemble.Expiration = dateTimeOffsetFromXmlAssembler.GetAssembledDateTimeOffset().Date;
            }

            // IsLost element
            var isLostElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsLost"), namespaceManager);
            if (isLostElement != null)
            {
                bool isLost = false;
                if (bool.TryParse(isLostElement.Value, out isLost))
                {
                    this.ObjectToAssemble.IsLost = isLost;
                }
            }

            // IsActive element
            var isActiveElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsActive"), namespaceManager);
            if (isActiveElement != null)
            {
                bool isActive = false;
                if (bool.TryParse(isActiveElement.Value, out isActive))
                {
                    this.ObjectToAssemble.IsLost = isActive;
                }
            }

            // MagStripe element
            var magStripeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("MagStripe"), namespaceManager);
            if (magStripeElement != null)
            {
                this.ObjectToAssemble.MagStripe = magStripeElement.Value;
            }

            



        }
    }
}