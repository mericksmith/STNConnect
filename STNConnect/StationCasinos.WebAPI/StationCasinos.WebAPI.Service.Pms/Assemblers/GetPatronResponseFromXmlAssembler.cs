using System.Xml.Linq;
using System.Xml.XPath;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.Extensions;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{
    public class GetPatronResponseFromXmlAssembler : ObjectFromXmlAssembler<GetPatronResponse>
    {
        public GetPatronResponseFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            var namespaceManager = this.Element.GetNamespaceManager();
            var patron = this.Element.XPathSelectElement(this.FormatXPathExpression("Patron"), namespaceManager);
            var assembler = new PatronFromXmlAssembler(patron);
            assembler.Assemble();
            this.ObjectToAssemble.Patron = assembler.AssembledObject;
        }
    }
}
