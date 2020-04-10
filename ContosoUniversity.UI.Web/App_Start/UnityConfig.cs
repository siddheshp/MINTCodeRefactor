using ContosoUniversity.Interfaces;
using ContosoUniversity.Logging;
using ContosoUniversity.Models;
using ContosoUniversity.Persistence.SQL;
using System;
using System.Configuration;
using Unity;
using Unity.Injection;

namespace ContosoUniversity.UI.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString;

            container.RegisterType<IRepository<Student>, Repository<Student>>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<SchoolContext>(
                new InjectionConstructor(connectionString));
        }
    }
}