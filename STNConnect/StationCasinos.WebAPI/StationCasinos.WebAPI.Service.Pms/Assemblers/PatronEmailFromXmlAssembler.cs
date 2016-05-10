using System;
using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public sealed class PatronEmailFromXmlAssembler : ObjectFromXmlAssembler<PatronEmail>
    {
        public PatronEmailFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            // PatronEmailId Attribute
            var patronEmailIdAttribute = this.Element.Attribute(this.Namespace + "PatronEmailId");
            if (patronEmailIdAttribute != null)
            {
                int patronEmailId = 0;
                if (int.TryParse(patronEmailIdAttribute.Value, out patronEmailId))
                {
                    this.ObjectToAssemble.PatronEmailId = patronEmailId;
                }
            }

            // EmailAddress element
            var emailAddressElement = this.Element.XPathSelectElement(this.FormatXPathExpression("EmailAddress"), namespaceManager);
            if (emailAddressElement != null)
            {
                this.ObjectToAssemble.EmailAddress = emailAddressElement.Value;
            }

            // EmailType element
            var emailTypeElement = this.Element.XPathSelectElement(this.FormatXPathExpression("EmailType"), namespaceManager);
            if (emailTypeElement != null)
            {
                EmailType email = EmailType.None;
                if (Enum.TryParse<EmailType>(emailTypeElement.Value, out email))
                {
                    this.ObjectToAssemble.EmailType = email;
                }
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

            // CanSendTo element
            var canSendToElement = this.Element.XPathSelectElement(this.FormatXPathExpression("CanSendTo"), namespaceManager);
            if (canSendToElement != null)
            {
                bool canSendTo = false;
                if (bool.TryParse(canSendToElement.Value, out canSendTo))
                {
                    this.ObjectToAssemble.CanSendTo = canSendTo;
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
