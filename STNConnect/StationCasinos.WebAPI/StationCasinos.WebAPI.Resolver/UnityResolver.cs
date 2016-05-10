using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using StationCasinos.WebAPI.Service.Pms;
using StationCasinos.WebAPI.Service.Interface;
using StationCasinos.Repository.Interface.Patron;
using StationCasinos.Repository.Patron;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.Repository.Ratings;
using StationCasinos.Repository.Interface.Lookup;
using StationCasinos.Repository.Lookup;
using StationCasinos.WebAPI.Utility.Interface;
using StationCasinos.WebAPI.Utility.Logging;

namespace StationCasinos.WebAPI.Resolver
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver()
        {
            container = new UnityContainer();
            container.RegisterType<IPatronService, PatronPmsService>()
                .RegisterType<IPatronRepository, PatronRepository>()
                .RegisterType<IRatingsRepository, RatingsRepository>()
                .RegisterType<IPlayerTracking, SpinPlayerTracking>()
                .RegisterType<ILookupRepository, LookupRepository>()
                .RegisterType<IOleDbConnection, MyOleDbConnection>()
                .RegisterType<IOleDbCommand, MyOleDbCommand>()
                .RegisterType<ILogging, Logging>();
        }

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}