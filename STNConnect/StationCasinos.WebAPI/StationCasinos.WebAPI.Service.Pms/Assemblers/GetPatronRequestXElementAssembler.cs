using System;
using System.Xml.Linq;
using StationCasinos.EnterpriseMessaging.Assemblers;
using StationCasinos.WebAPI.Service.Models;

namespace StationCasinos.WebAPI.Service.Pms.Assemblers
{

    /// <summary>
    /// This class encapsulates the ability to assemble a GetPatronRequest message from a GetPatronRequest object.
    /// </summary>
    public class GetPatronRequestXElementAssembler : XElementFromObjectAssembler<GetPatronRequest>
    {
        /// <summary>
        /// Initializes a new instance of the GetPatronRequestXElementAssembler class.
        /// </summary>
        /// <param name="obj">Object to assemble from.</param>
        /// <param name="ns">Namespace object.</param>
        public GetPatronRequestXElementAssembler(GetPatronRequest obj, XNamespace ns)
            : base(ns + "GetPatronRequest", obj, ns)
        {
        }

        public override void Assemble()
        {
            if (!String.IsNullOrEmpty(this.ObjectToUse.PatronId))
            {
                this.AssembledElement.Add(new XElement(this.Namespace + "PatronId", this.ObjectToUse.PatronId));
            }

            if (!String.IsNullOrEmpty(this.ObjectToUse.Magstripe))
            {
                this.AssembledElement.Add(new XElement(this.Namespace + "Magstripe", this.ObjectToUse.Magstripe));
            }

            if (!String.IsNullOrEmpty(this.ObjectToUse.PreferredProperty))
            {
                this.AssembledElement.Add(new XElement(this.Namespace + "PreferredProperty", this.ObjectToUse.PreferredProperty));
            }
        }
    }
}
