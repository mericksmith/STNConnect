using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{


    public sealed class PatronFromXmlAssembler : ObjectFromXmlAssembler<Patron>
    {
        public PatronFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // Get a namespace manager for XPath queries.
            var namespaceManager = this.Element.GetNamespaceManager();

            this.Assemble1(namespaceManager);
            this.Assemble2(namespaceManager);
        }

        private void Assemble1(XmlNamespaceManager namespaceManager)
        {
            // PatronId attribute
            var patronIdAttribute = this.Element.Attribute(this.Namespace + "PatronId");
            if (patronIdAttribute != null)
            {
                this.ObjectToAssemble.PatronId = patronIdAttribute.Value;
            }

            // PatronProfile element
            var patronProfileElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PatronProfile"), namespaceManager);
            if (patronProfileElement != null)
            {
                var assembler = new PatronProfileFromXmlAssembler(patronProfileElement);
                assembler.Assemble();
                this.ObjectToAssemble.PatronProfile = assembler.AssembledObject;
            }

            // PatronAddress element
            var patronAddressElements = this.Element.XPathSelectElements(this.FormatXPathExpression("PatronAddress"), namespaceManager);
            if (patronAddressElements != null)
            {
                foreach (var element in patronAddressElements)
                {
                    if (element != null)
                    {
                        var assembler = new PatronAddressFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.PatronAddressList.Add(assembler.AssembledObject);
                    }
                }
            }

            // PatronIdentity element
            var patronIdentityElements = this.Element.XPathSelectElements(this.FormatXPathExpression("PatronIdentity"), namespaceManager);
            if (patronIdentityElements != null)
            {
                foreach (var element in patronIdentityElements)
                {
                    if (element != null)
                    {
                        var assembler = new PatronIdentityFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.PatronIdentityList.Add(assembler.AssembledObject);
                    }
                }
            }
        }

        private void Assemble2(XmlNamespaceManager namespaceManager)
        {
            // PatronEmail element
            var patronEmailElements = this.Element.XPathSelectElements(this.FormatXPathExpression("PatronEmail"), namespaceManager);
            if (patronEmailElements != null)
            {
                foreach (var element in patronEmailElements)
                {
                    if (element != null)
                    {
                        var assembler = new PatronEmailFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.PatronEmailList.Add(assembler.AssembledObject);
                    }
                }
            }

            // PatronPhone element
            var patronPhoneElements = this.Element.XPathSelectElements(this.FormatXPathExpression("PatronPhone"), namespaceManager);
            if (patronPhoneElements != null)
            {
                foreach (var element in patronPhoneElements)
                {
                    if (element != null)
                    {
                        var assembler = new PatronPhoneFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.PatronPhoneList.Add(assembler.AssembledObject);
                    }
                }
            }

            // BoardingPass element
            var boardingPassElements = this.Element.XPathSelectElements(this.FormatXPathExpression("BoardingPass"), namespaceManager);
            if (boardingPassElements != null)
            {
                foreach (var element in boardingPassElements)
                {
                    if (element != null)
                    {
                        var assembler = new BoardingPassFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.BoardingPassList.Add(assembler.AssembledObject);
                    }
                }
            }

            // PreferredProperty element
            var preferredPropertyElements = this.Element.XPathSelectElements(this.FormatXPathExpression("PreferredProperty"), namespaceManager);
            if (preferredPropertyElements != null)
            {
                foreach (var element in preferredPropertyElements)
                {
                    if (element != null)
                    {
                        var assembler = new PropertyDetailFromXmlAssembler(element);
                        assembler.Assemble();
                        this.ObjectToAssemble.PreferredPropertyList.Add(assembler.AssembledObject);
                    }
                }
            }

            // PatronPin element
            var patronPinElement = this.Element.XPathSelectElement(this.FormatXPathExpression("PatronPin"), namespaceManager);
            if (patronPinElement != null)
            {
                var assembler = new PatronPinFromXmlAssembler(patronPinElement);
                assembler.Assemble();
                this.ObjectToAssemble.PatronPin = assembler.AssembledObject;
            }
        }
    }
}