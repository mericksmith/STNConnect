using System;
using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
        public sealed class PatronProfileFromXmlAssembler : ObjectFromXmlAssembler<PatronProfile>
        {
            public PatronProfileFromXmlAssembler(XElement element)
                : base(element)
            {
            }

            public override void Assemble()
            {
                // Get a namespace manager for XPath queries.
                var namespaceManager = this.Element.GetNamespaceManager();

                // Title element
                var titleElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Title"), namespaceManager);
                if (titleElement != null)
                {
                    this.ObjectToAssemble.Title = titleElement.Value;
                }

                // FirstName element
                var firstNameElement = this.Element.XPathSelectElement(this.FormatXPathExpression("FirstName"), namespaceManager);
                if (firstNameElement != null)
                {
                    this.ObjectToAssemble.FirstName = firstNameElement.Value;
                }

                // FirstName element
                var middleInitialElement = this.Element.XPathSelectElement(this.FormatXPathExpression("MiddleInitial"), namespaceManager);
                if (middleInitialElement != null)
                {
                    this.ObjectToAssemble.MiddleInitial = middleInitialElement.Value;
                }

                // LastName element
                var lastNameElement = this.Element.XPathSelectElement(this.FormatXPathExpression("LastName"), namespaceManager);
                if (lastNameElement != null)
                {
                    this.ObjectToAssemble.LastName = lastNameElement.Value;
                }

                // Suffix element
                var suffixElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Suffix"), namespaceManager);
                if (suffixElement != null)
                {
                    this.ObjectToAssemble.Suffix = suffixElement.Value;
                }

                // NickName element
                var nickNameElement = this.Element.XPathSelectElement(this.FormatXPathExpression("NickName"), namespaceManager);
                if (nickNameElement != null)
                {
                    this.ObjectToAssemble.Nickname = nickNameElement.Value;
                }

                // MaidenName element
                var maidenNameElement = this.Element.XPathSelectElement(this.FormatXPathExpression("MaidenName"), namespaceManager);
                if (maidenNameElement != null)
                {
                    this.ObjectToAssemble.Title = maidenNameElement.Value;
                }

                // SSN element
                var ssnElement = this.Element.XPathSelectElement(this.FormatXPathExpression("SSN"), namespaceManager);
                if (ssnElement != null)
                {
                    this.ObjectToAssemble.SSNumber = ssnElement.Value;
                }

                // Dob element
                var dobElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Dob"), namespaceManager);
                if (dobElement != null)
                {
                    DateTimeOffsetFromXmlAssembler dateTimeOffsetFromXmlAssembler = new DateTimeOffsetFromXmlAssembler(dobElement.ToString());
                    this.ObjectToAssemble.Dob = dateTimeOffsetFromXmlAssembler.GetAssembledDateTimeOffset().Date;
                }

                // Gender element
                var genderElement = this.Element.XPathSelectElement(this.FormatXPathExpression("Gender"), namespaceManager);
                if (genderElement != null)
                {
                    GenderType gender = GenderType.Unknown;
                    if (Enum.TryParse<GenderType>(genderElement.Value, out gender))
                    {
                        this.ObjectToAssemble.Gender = gender;
                    }
                }

                // MaritalStatus element
                var maritalStatusElement = this.Element.XPathSelectElement(this.FormatXPathExpression("MaritalStatus"), namespaceManager);
                if (maritalStatusElement != null)
                {
                    MaritalStatusType maritalStatus = MaritalStatusType.Unknown;
                    if (Enum.TryParse<MaritalStatusType>(genderElement.Value, out maritalStatus))
                    {
                        this.ObjectToAssemble.MaritalStatus = maritalStatus;
                    }
                }

                // IsVip element
                var isVipElement = this.Element.XPathSelectElement(this.FormatXPathExpression("IsVip"), namespaceManager);
                if (isVipElement != null)
                {
                    bool isVip = false;
                    if (bool.TryParse(isVipElement.Value, out isVip))
                    {
                        this.ObjectToAssemble.IsVip = isVip;
                    }
                }
            }
        }
    }