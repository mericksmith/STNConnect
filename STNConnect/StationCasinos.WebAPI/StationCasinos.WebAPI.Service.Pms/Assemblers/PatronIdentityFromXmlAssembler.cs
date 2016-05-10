using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class PatronIdentityFromXmlAssembler : ObjectFromXmlAssembler<PatronIdentity>
    {
        public PatronIdentityFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // PatronIdentityId Attribute
            var patronIdentityIdAttribute = this.Element.Attribute(this.Namespace + "PatronIdentityId");
            if (patronIdentityIdAttribute != null)
            {
                int patronIdentityId = 0;
                if (int.TryParse(patronIdentityIdAttribute.Value, out patronIdentityId))
                {
                    this.ObjectToAssemble.PatronIdentityId = patronIdentityId;
                }
            }

            // IdNumber element
            var idNumberElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IdNumber"), namespaceManager);
            if (idNumberElement != null)
            {
                this.ObjectToAssemble.IdNumber = idNumberElement.Value;
            }

            // IdType element
            var idTypeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IdType"), namespaceManager);
            if (idTypeElement != null)
            {
                var assembler = new IdentificationTypeFromXmlAssembler(idTypeElement);
                assembler.Assemble();
                this.ObjectToAssemble.IdType = assembler.AssembledObject;
            }

            // IdExpiration element
            var idExpirationElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IdExpiration"), namespaceManager);
            if (idExpirationElement != null)
            {
                DateTimeOffsetFromXmlAssembler dateTimeOffsetFromXmlAssembler = new DateTimeOffsetFromXmlAssembler(idExpirationElement.ToString());
                this.ObjectToAssemble.IdExpiration = dateTimeOffsetFromXmlAssembler.GetAssembledDateTimeOffset().Date;
            }

            // StateProvince element
            var stateProvinceElement = this.Element.XPathSelectElement(this.FormatXPathExpression("StateProvince"), namespaceManager);
            if (stateProvinceElement != null)
            {
                var assembler = new StateProvinceFromXmlAssembler(stateProvinceElement);
                assembler.Assemble();
                this.ObjectToAssemble.StateProvince = assembler.AssembledObject;
            }

            // Country element
            var countryElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Country"), namespaceManager);
            if (countryElement != null)
            {
                var assembler = new CountryFromXmlAssembler(countryElement);
                assembler.Assemble();
                this.ObjectToAssemble.Country = assembler.AssembledObject;
            }

            // VerifyDate element
            var verifyDateElement = this.Element.XPathSelectElement(this.FormatXPathExpression("VerifyDate"), namespaceManager);
            if (verifyDateElement != null)
            {
                DateTimeOffsetFromXmlAssembler dateTimeOffsetFromXmlAssembler = new DateTimeOffsetFromXmlAssembler(verifyDateElement.ToString());
                this.ObjectToAssemble.VerifyDate = dateTimeOffsetFromXmlAssembler.GetAssembledDateTimeOffset().Date;
            }

            // VerifiedBy element
            var verifiedByElement = this.Element.XPathSelectElement(this.FormatXPathExpression("VerifiedBy"), namespaceManager);
            if (verifiedByElement != null)
            {
                int verifiedBy = 0;
                if (int.TryParse(verifiedByElement.Value, out verifiedBy))
                {
                    this.ObjectToAssemble.VerifiedBy = verifiedBy;
                }
            }
        }
    }
}