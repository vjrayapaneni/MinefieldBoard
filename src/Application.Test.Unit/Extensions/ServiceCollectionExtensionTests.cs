using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinefieldBoard.Application.Extensions;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Extensions
{
    [TestClass]
    public class ServiceCollectionExtensionTests
    {
        private static IConfiguration _configRoot;
        private static IConfigurationSection _boardOptionSettings;
        private static ServiceCollection _serviceCollection;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCtorParams();
        }

        private static void InitializeCtorParams()
        {
            BuildConfiguration();

            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton(_configRoot);
            _boardOptionSettings = _configRoot.GetSection("board");
        }

        [TestMethod]
        [DynamicData(nameof(GetCtorArgs), DynamicDataSourceType.Method)]
        public void ConfigureAuthentication_ServiceCollection_ArgumentNullException(
            IServiceCollection services,
            IConfigurationSection section,
            string errorMessage
        )
        {
            // Arrange

            // Act
            Action sut = () => Application.Extensions.ServiceCollectionExtensions
                                          .AddApplicationServices(services, section);

            // Assert
            sut.ShouldThrow<ArgumentNullException>().Message
               .ShouldBe(errorMessage);
        }

        /// <summary>
        /// returning constructor arguments
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<object[]> GetCtorArgs()
        {
            InitializeCtorParams();

            yield return new object[]
            {
                null,
                _boardOptionSettings,
                "Value cannot be null. (Parameter 'services')"
            };

            yield return new object[]
            {
                _serviceCollection,
                null,

                "Value cannot be null. (Parameter 'configuration')"
            };
        }


        [TestMethod]
        public void AddApplicationServices_Success_Test()
        {
            // Arrange 

            // Act
            var services = _serviceCollection.AddApplicationServices(_configRoot);

            // Assert
            services.ShouldNotBeNull();
        }

        /// <summary>
        /// Build configuration from json
        /// </summary>
        private static void BuildConfiguration()
        {
            _configRoot = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.test.json", true)
                          .Build();
        }
    }
}
