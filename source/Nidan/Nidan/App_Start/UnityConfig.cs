using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Nidan.Business;
using Nidan.Business.Interfaces;
using Nidan.Data;
using Nidan.Data.Interfaces;
using Nidan.Data.Models;
using Nidan.Interfaces;
using Nidan.Document.Interfaces;
using Nidan.Document;

namespace Nidan
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<INidanDatabaseFactory, NidanDatabaseFactory>(new InjectionConstructor(
                    new InjectionParameter<string>(ConfigHelper.DefaultConnection)
                 ));

            //container.RegisterType<IDocumentServiceRestClient, DocumentServiceRestClient>(
            //    new InjectionConstructor(
            //        new InjectionParameter<Uri>(new Uri(ConfigurationManager.AppSettings["DocumentRESTApiAddress"])),
            //        new InjectionParameter<string>(ConfigurationManager.AppSettings["Username"]),
            //        new InjectionParameter<string>(ConfigurationManager.AppSettings["Password"])
            //    ));

            // let's enforce a singleton on CacheProvider, even though it accesses a static MemoryCache.Default
            container.RegisterType<ICacheProvider, MemoryCacheProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<INidanDataService, NidanDataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<INidanBusinessService, NidanBusinessService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITenantOrganisationService, NidanBusinessService>(new ContainerControlledLifetimeManager());
           // container.RegisterType<ITemplateService, TemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new InjectionParameter<string>(ConfigHelper.OverrideEmailAddresses)
                )
            );

            container.RegisterType<IDocumentService, DocumentService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITenantsService, TenantsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPdfService, PdfService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRazorService, RazorService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITemplateService, TemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISMSService, SMSService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager());
        }
    }
}
