using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinefieldBoard.Application.Extensions;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.GameConsole
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void Main(string[] args)
        {
            // setup service collection
            var services = new ServiceCollection();
            var root = BuildConfiguration();

            ConfigureServices(services, root);

            var serviceProvider = services.BuildServiceProvider();
            var game = serviceProvider.GetService<IGame>();
            game.Start();
        }


        private static IConfigurationRoot BuildConfiguration()
        {
            var configRoot = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", true)
                             .Build();

            return configRoot;
        }


        private static void ConfigureServices(ServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IGame, Game>();
            services.AddApplicationServices(configuration);
        }
    }
}
