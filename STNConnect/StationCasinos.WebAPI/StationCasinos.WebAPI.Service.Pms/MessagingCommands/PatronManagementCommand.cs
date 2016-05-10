using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using StationCasinos.EnterpriseMessaging.Assemblers;

namespace StationCasinos.WebAPI.Service.Pms.MessagingCommands
{
    public abstract class PatronManagementCommand<TResult, TResultAssembler> : ServiceCommand<TResult, TResultAssembler>
        where TResult : class, new()
        where TResultAssembler : ObjectFromXmlAssembler<TResult>
    {
        protected PatronManagementCommand(XNamespace ns)
            : base("PatronManagement", ns)
        {
        }
    }
}
