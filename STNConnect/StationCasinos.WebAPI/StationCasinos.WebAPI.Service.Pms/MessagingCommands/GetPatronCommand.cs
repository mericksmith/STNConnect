using System.Xml.Linq;
using StationCasinos.WebAPI.Service.Models;
using StationCasinos.WebAPI.Service.Pms.Assemblers;

namespace StationCasinos.WebAPI.Service.Pms.MessagingCommands
{
    public class GetPatronCommand : PatronManagementCommand<GetPatronResponse, GetPatronResponseFromXmlAssembler>
    {
        private GetPatronRequest request;

        public GetPatronCommand(XNamespace ns, GetPatronRequest request)
            : base(ns)
        {
            this.request = request;
        }

        protected override XElement RequestElement
        {
            get
            {
                var assembler = new GetPatronRequestXElementAssembler(this.request, this.Namespace);
                assembler.Assemble();
                return assembler.AssembledElement;
            }
        }
    }
}
