using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LowFlightFare
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection()
        {
            // We will use Dependency Injection for all controllers and other classes, so we'll need a service collection
            ServiceCollection services = new ServiceCollection();

            // configure all of the services required for DI
            ConfigureServices(services);

            // Create a new resolver from our own default implementation
            CustomDependencyResolver resolver = new CustomDependencyResolver(services.BuildServiceProvider());

            // Set the application resolver to our default resolver. This comes from "System.Web.Mvc"
            //Other services may be added elsewhere through time
            DependencyResolver.SetResolver(resolver);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///// Add ONTO to DI - Start
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //services.AddTransient(typeof(OntoDbContext));
            //services.AddTransient(typeof(UserSettingsDAL));
            //services.AddTransient(typeof(LocaleDAL));
            //services.AddTransient(typeof(UserSettingsLogic));
            //services.AddTransient(typeof(LocaleLogic));
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///// Add ONTO to DI - Start - End
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Add Controllers to DI(Dependency Injection)
            AddControllersAsServices(services, Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)));
        }

        private static void AddControllersAsServices(IServiceCollection services, IEnumerable<Type> controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }
        }
    }

    internal class CustomDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider _serviceProvider;

        public CustomDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return this._serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._serviceProvider.GetServices(serviceType);
        }
    }
}