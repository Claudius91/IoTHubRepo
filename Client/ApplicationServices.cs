

using SharedLib;

namespace Client
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The application service class. Sets dependency injection up.
    /// </summary>
    public class ApplicationServices
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationServices"/> class from being created.
        /// </summary>
        private ApplicationServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<UrlService>();
            services.AddLogging(coll =>
            {
                coll.AddConsole();
                coll.AddDebug();
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// An instance of this class.
        /// </summary>
        private static ApplicationServices _instance;

        /// <summary>
        /// The instance lock.
        /// </summary>
        private static object _instanceLock = new object();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>A new instance of this class.</returns>
        private static ApplicationServices GetInstance()
        {
            lock (_instanceLock)
            {
                return _instance ?? (_instance = new ApplicationServices());
            }
        }

        /*static ApplicationServices()
        {
            _instance = null;
        }*/

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance to get.
        /// </value>
        public static ApplicationServices Instance => _instance ?? GetInstance();

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider ServiceProvider { get; }
    }
}
