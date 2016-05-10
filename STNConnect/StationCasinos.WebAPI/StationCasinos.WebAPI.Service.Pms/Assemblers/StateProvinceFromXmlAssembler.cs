using System.Xml.Linq;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{

    public sealed class StateProvinceFromXmlAssembler : ObjectFromXmlAssembler<StateProvince>
    {
        public StateProvinceFromXmlAssembler(XElement element)
            : base(element)
        {
        }

        public override void Assemble()
        {
            // StateProvinceId element
            var stateProvinceIdElement = this.Element.Attribute(this.Namespace + "StateProvinceId");
            if (stateProvinceIdElement != null)
            {
                int stateProvinceId = 0;
                if (int.TryParse(stateProvinceIdElement.Value, out stateProvinceId))
                {
                    this.ObjectToAssemble.StateProvinceId = stateProvinceId;
                }
            }

            // StateProvinceDescription element
            var stateProvinceDescriptionElement = this.Element.Attribute(this.Namespace + "StateProvinceDescription");
            if (stateProvinceDescriptionElement != null)
            {
                this.ObjectToAssemble.StateProvinceDescription = stateProvinceDescriptionElement.Value;
            }

            // Code element
            var codeElement = this.Element.Value;
            if (codeElement != null)
            {
                this.ObjectToAssemble.Value = codeElement;
            }
        }
    }
}