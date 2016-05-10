using System;
using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class PatronPhoneFromXmlAssembler : ObjectFromXmlAssembler<PatronPhone>
    {
        public PatronPhoneFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // PatronPhoneId attribute
            var patronPhoneIdAttribute = this.Element.Attribute(this.Namespace + "PatronPhoneId");
            if (patronPhoneIdAttribute != null)
            {
                int patronPhoneId = 0;
                if (int.TryParse(patronPhoneIdAttribute.Value, out patronPhoneId))
                {
                    this.ObjectToAssemble.PatronPhoneId = patronPhoneId;
                }
            }

            // PhoneType element
            var phoneTypeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PhoneType"), namespaceManager);
            if (phoneTypeElement != null)
            {
                PhoneType phone = PhoneType.None;
                if (Enum.TryParse<PhoneType>(phoneTypeElement.Value, out phone))
                {
                    this.ObjectToAssemble.PhoneType = phone;
                }
            }

            // CountryCode element
            var countryCodeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("CountryCode"), namespaceManager);
            if (countryCodeElement != null)
            {
                this.ObjectToAssemble.CountryCode = countryCodeElement.Value;
            }

            // AreaCode element
            var areaCodeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("AreaCode"), namespaceManager);
            if (areaCodeElement != null)
            {
                this.ObjectToAssemble.AreaCode = areaCodeElement.Value;
            }

            // PhoneNumber element
            var phoneNumberElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PhoneNumber"), namespaceManager);
            if (phoneNumberElement != null)
            {
                this.ObjectToAssemble.PhoneNumber = phoneNumberElement.Value;
            }

            // Extension element
            var extensionElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Extension"), namespaceManager);
            if (extensionElement != null)
            {
                this.ObjectToAssemble.Extension = extensionElement.Value;
            }

            // Comment element
            var commentElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Comment"), namespaceManager);
            if (commentElement != null)
            {
                this.ObjectToAssemble.Comment = commentElement.Value;
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

            // CanCall element
            var canCallElement = this.Element.XPathSelectElement(this.FormatXPathExpression("CanCall"), namespaceManager);
            if (canCallElement != null)
            {
                bool canCall = false;
                if (bool.TryParse(canCallElement.Value, out canCall))
                {
                    this.ObjectToAssemble.CanCall = canCall;
                }
            }

            // ExcludeFromLists element
            var excludeFromListsElement = this.Element.XPathSelectElement(this.FormatXPathExpression("ExcludeFromLists"), namespaceManager);
            if (excludeFromListsElement != null)
            {
                bool excludeFromLists = false;
                if (bool.TryParse(excludeFromListsElement.Value, out excludeFromLists))
                {
                    this.ObjectToAssemble.ExcludeFromLists = excludeFromLists;
                }
            }

            // IsActive element
            var isActiveElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsActive"), namespaceManager);
            if (isActiveElement != null)
            {
                bool isActive = false;
                if (bool.TryParse(isActiveElement.Value, out isActive))
                {
                    this.ObjectToAssemble.IsActive = isActive;
                }
            }
        }
    }
}