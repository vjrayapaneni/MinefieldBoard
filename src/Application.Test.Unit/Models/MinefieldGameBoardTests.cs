using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinefieldBoard.Application.Models;
using MinefieldBoard.Domain.Enums;
using MinefieldBoard.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Models
{
    [TestClass]
    public class MinefieldGameBoardTests
    {
        private static IConfigurationRoot _configRoot;
        private static BoardOptions _boardOptions;
        private static IOptions<BoardOptions> _boardOptionSettings;
        private static ITile _tile;
        private static IServiceProvider _serviceProvider;
        private static IConsoleWriterService _consoleWriterService;
        private MinefieldGameBoard _minefieldBoard;
        private static IOptions<BoardOptions> _nullBoardOptionSettings;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCtorParams();

            _minefieldBoard = new MinefieldGameBoard(_boardOptionSettings,
                                                     _serviceProvider,
                                                     _consoleWriterService
                                                    );
        }

        private static void InitializeCtorParams()
        {
            BuildConfiguration();
            _boardOptions = new BoardOptions();
            _configRoot.GetSection(BoardOptions.Board)
                       .Bind(_boardOptions);

            _boardOptionSettings = Substitute.For<IOptions<BoardOptions>>();
            _nullBoardOptionSettings = Substitute.For<IOptions<BoardOptions>>();
            BoardOptions nullBoardOptions = null;
            _nullBoardOptionSettings.Value.Returns(nullBoardOptions);
            _boardOptionSettings.Value.Returns(_boardOptions);

            _tile = Substitute.For<ITile>();
            _serviceProvider = Substitute.For<IServiceProvider>();
            _serviceProvider.GetService(Arg.Is(typeof(ITile)))
                            .Returns(_tile);

            _consoleWriterService = Substitute.For<IConsoleWriterService>();
        }


        [TestMethod]
        [DynamicData(nameof(GetCtorArgs), DynamicDataSourceType.Method)]
        public void Ctor_Args_Check_Test(IOptions<BoardOptions> options,
                                         IServiceProvider serviceProvider,
                                         IConsoleWriterService consoleWriterService,
                                         string errorMessage)
        {
            // Arrange 

            // Act
            Action ar = () => _ = new MinefieldGameBoard(options, serviceProvider, consoleWriterService);

            // Assert
            ar.ShouldThrow<ArgumentNullException>().Message.ShouldBe(errorMessage);
        }

        private static IEnumerable<object[]> GetCtorArgs()
        {
            InitializeCtorParams();

            yield return new object[]
            {
                _nullBoardOptionSettings,
                _serviceProvider,
                _consoleWriterService,
                "Value cannot be null. (Parameter 'Value')"
            };
            yield return new object[]
            {
                _boardOptionSettings,
                null,
                _consoleWriterService,
                "Value cannot be null. (Parameter 'serviceProvider')"
            };
            yield return new object[]
            {
                _boardOptionSettings,
                _serviceProvider,
                null,
                "Value cannot be null. (Parameter 'consoleWriterService')"
            };
        }


        [TestMethod]
        public void InitializeBoard_Test()
        {
            // Arrange 

            // Act
            _minefieldBoard.InitializeBoard();

            // Assert
            _consoleWriterService.Received(1).Clean();
            _minefieldBoard.GetActiveTile().ShouldNotBeNull();
            _minefieldBoard.GetActiveTile().TileType.ShouldBe(TileType.Default);
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
