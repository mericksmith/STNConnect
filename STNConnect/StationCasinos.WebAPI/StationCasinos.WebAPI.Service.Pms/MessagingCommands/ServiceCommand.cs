using System;
using System.Xml.Linq;
using StationCasinos.EnterpriseMessaging;
using StationCasinos.EnterpriseMessaging.Assemblers;



namespace StationCasinos.WebAPI.Service.Pms.MessagingCommands
{
    public abstract class ServiceCommand<TResult, TResultAssembler>
        where TResult : class, new()
        where TResultAssembler : ObjectFromXmlAssembler<TResult>
    {
        private string domain;
        private Guid sessionId = Guid.NewGuid();
        private XNamespace namespaceField;

        /// <summary>
        /// Initializes a new instance of the ServiceCommand class using the specified domain name
        /// </summary>
        /// <param name="domain">The domain attribute value of the messagingServiceInfo in enterpriseMessagingAppSettings configuration element</param>
        /// <param name="ns">The XNamespace that will be used to construct the XElement</param>
        protected ServiceCommand(string domain, XNamespace ns)
        {
            this.domain = domain;
            this.namespaceField = ns;
        }

        /// <summary>
        /// Gets the Namespace used to construct the XElement
        /// </summary>
        protected XNamespace Namespace
        {
            get
            {
                return this.namespaceField;
            }
        }

        /// <summary>
        /// Gets the XElement object that will be the body of the request message
        /// </summary>
        protected abstract XElement RequestElement
        {
            get;
        }

        /// <summary>
        /// Creates a new request message and sends it to the enterprise services for further processing
        /// </summary>
        /// <returns>The T type result from the response element returned by the enterprise service</returns>
        public TResult ProcessRequest()
        {
            using (var command = CommandFactory.Create(this.domain, this.sessionId))
            {
                var scrubbedElement = this.RequestElement;
                this.ScrubNamespaceAttributes(scrubbedElement);
                using (var request = MessageFactory.CreateRequest(command.Binding.MessageVersion, scrubbedElement))
                {
                    command.ProcessMessage(request);
                    var objectAssembler = (TResultAssembler)Activator.CreateInstance(typeof(TResultAssembler), new object[] { command.ResponseElement });
                    //// objectAssembler.Assemble(); -- We can't do this explicitly because the Activator pings the attributes of the class, and the getter of the AssembledObject, if null, will assemble it then.
                    return objectAssembler.AssembledObject;
                }
            }
        }
        public TResult ProcessRequestWithTransaction()
        {
            using (var command = CommandFactory.Create(this.domain, this.sessionId))
            {

                command.UseTransaction = true;
                var scrubbedElement = this.RequestElement;
                this.ScrubNamespaceAttributes(scrubbedElement);
                using (var request = MessageFactory.CreateRequestWithTransaction(command.Binding.MessageVersion, scrubbedElement))
                {
                    command.ProcessMessage(request);
                    var objectAssembler = (TResultAssembler)Activator.CreateInstance(typeof(TResultAssembler), new object[] { command.ResponseElement });
                    //// objectAssembler.Assemble(); -- We can't do this explicitly because the Activator pings the attributes of the class, and the getter of the AssembledObject, if null, will assemble it then.
                    return objectAssembler.AssembledObject;
                }
            }
        }

        /// <summary>
        /// This method is used to scrub duplicate xml namespaces from child elements as a result from nesting assemblers objects to assemble the entire message.
        /// </summary>
        /// <param name="parent">Parent element to be scrubbed.</param>
        private void ScrubNamespaceAttributes(XElement parent)
        {
            // Only scrub the children.  Do not scrub if this is the root element where the namespace belongs.
            if (parent.Parent != null)
            {
                foreach (XAttribute attribute in parent.Attributes())
                {
                    if (attribute.IsNamespaceDeclaration)
                    {
                        attribute.Remove();
                        break;
                    }
                }
            }

            // Call this method recursively for each child of the current element.
            if (parent.HasElements)
            {
                foreach (XElement child in parent.Elements())
                {
                    this.ScrubNamespaceAttributes(child);
                }
            }
        }
    }
}
