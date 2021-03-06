﻿using LowFlightFare.BusinessLogic;
using LowFlightFare.DAL;
using LowFlightFare.DbContexts;
using LowFlightFare.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LowFlightFare
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection()
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

        private void ConfigureServices(IServiceCollection services)
        {
            ///// Add SearchFlights to DI - Start /////
            services.AddTransient(typeof(LowFlightFareDbContext));
            services.AddTransient(typeof(CurrencyDAL));
            services.AddTransient(typeof(Airport_IATA_CodesDAL));
            services.AddTransient(typeof(SearchParametersDAL));
            services.AddTransient(typeof(SearchResultsDAL));
            services.AddTransient(typeof(LowFlightFareHub));
            services.AddTransient(typeof(SearchFlightsLogic));
            ///// Add SearchFlights to DI - End /////

            ///// Add Settings to DI - Start /////
            services.AddTransient(typeof(LocaleDAL));
            services.AddTransient(typeof(SettingsDAL));
            services.AddTransient(typeof(SettingsLogic));
            ///// Add Settings to DI - End /////

            // Add Controllers to DI(Dependency Injection)
            AddControllersAsServices(services, Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)));
        }

        private void AddControllersAsServices(IServiceCollection services, IEnumerable<Type> controllerTypes)
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