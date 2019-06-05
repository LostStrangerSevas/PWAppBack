using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PWApp.AutoMapper;

namespace WebAPITest
{
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            AutoMapperInitializer.Initialize(); //инициализация автомаппера
            //BuildHost(args).Run(); //конфигурация и запуск хоста
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        /*
        /// <summary>
        /// Конфигурация хоста
        /// </summary>
        /// <param name="args">Массив аргументов</param>
        /// <returns>IWebHost</returns>
        public static IWebHost BuildHost(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(
                    (hostingContext, config) =>
                    {
                        var hostingEnvironment = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile(string.Format("appsettings.{0}.json", hostingEnvironment.EnvironmentName), true, true);

                        if (hostingEnvironment.IsDevelopment())
                        {
                            var assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                            if (assembly != null)
                                config.AddUserSecrets(assembly, true);
                        }

                        config.AddEnvironmentVariables();
                        if (args == null)
                            return;
                        config.AddCommandLine(args);
                    })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseStartup<Startup>()
                .UseUrls(new string[] { "http://localhost:50361" })

                .Build();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
                */
    }
}
