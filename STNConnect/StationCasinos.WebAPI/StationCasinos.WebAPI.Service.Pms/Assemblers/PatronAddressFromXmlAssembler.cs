using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{


    public sealed class PatronAddressFromXmlAssembler : ObjectFromXmlAssembler<PatronAddress>
    {
        public PatronAddressFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // PatronAddressId attribute
            var patronAddressIdAttribute = this.Element.Attribute(this.Namespace + "PatronAddressId");
            if (patronAddressIdAttribute != null)
            {
                int patronAddressId = 0;
                if (int.TryParse(patronAddressIdAttribute.Value, out patronAddressId))
                {
                    this.ObjectToAssemble.PatronAddressId = patronAddressId;
                }
            }

            // AddressLine element
            var addressLineElements = this.Element.XPathSelectElements(this.FormatXPathExpression("AddressLine"), namespaceManager);
            if (addressLineElements != null)
            {
                foreach (var element in addressLineElements)
                {
                    if (element != null)
                    {
                        this.ObjectToAssemble.AddressLineList.Add(element.Value);
                    }
                }
            }

            // City element
            var cityElement = this.Element.XPathSelectElement(this.FormatXPathExpression("City"), namespaceManager);
            if (cityElement != null)
            {
                this.ObjectToAssemble.City = cityElement.Value;
            }

            // StateProvince element
            var stateProvinceElement = this.Element.XPathSelectElement(this.FormatXPathExpression("StateProvince"), namespaceManager);
            if (stateProvinceElement != null)
            {
                var assembler = new StateProvinceFromXmlAssembler(stateProvinceElement);
                assembler.Assemble();
                this.ObjectToAssemble.StateProvince = assembler.AssembledObject;
            }

            // PostalCode element
            var postalCodeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PostalCode"), namespaceManager);
            if (postalCodeElement != null)
            {
                this.ObjectToAssemble.PostalCode = postalCodeElement.Value;
            }

            // Country element
            var countryElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Country"), namespaceManager);
            if (countryElement != null)
            {
                var assembler = new CountryFromXmlAssembler(countryElement);
                assembler.Assemble();
                this.ObjectToAssemble.Country = assembler.AssembledObject;
            }

            // IsPrimary element
            var isPrimaryElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsPrimary"), namespaceManager);
            if (isPrimaryElement != null)
            {
                bool isPrimary = false;
                if (bool.TryParse(isPrimaryElement.Value, out isPrimary))
                {
                    this.ObjectToAssemble.IsPrimary = isPrimary;
                }
            }

            // IsMail element
            var isMailElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsMail"), namespaceManager);
            if (isMailElement != null)
            {
                bool isMail = false;
                if (bool.TryParse(isMailElement.Value, out isMail))
                {
                    this.ObjectToAssemble.IsMail = isMail;
                }
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