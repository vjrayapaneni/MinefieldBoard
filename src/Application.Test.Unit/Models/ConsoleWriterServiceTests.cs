// Copyright (c) 2024, <CompanyName>


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinefieldBoard.Application.Models;
using MinefieldBoard.Application.Services;
using MinefieldBoard.Application.Test.Unit.Extensions;
using MinefieldBoard.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Models
{
    [TestClass]
    public class ConsoleWriterServiceTests
    {
        private ConsoleWriterService _consoleWriterService;
        private static ILogger<ConsoleWriterService> _logger;
        private static BoardOptions _boardOptions;
        private static IConfigurationRoot _configRoot;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCtorParams();

            _consoleWriterService = new ConsoleWriterService(_logger);
        }

        private static void InitializeCtorParams()
        {
            _logger = Substitute.For<ILogger<ConsoleWriterService>>();
            BuildConfiguration();
            _boardOptions = new BoardOptions();
            _configRoot.GetSection(BoardOptions.Board)
                       .Bind(_boardOptions);
        }

        [TestMethod]
        public void Constructor_InvalidArgs_Test()
        {
            // Arrange

            // Act

            // Assert
            Should.Throw<ArgumentNullException>(() => new ConsoleWriterService(null))
                  .Message.ShouldBe("Value cannot be null. (Parameter 'logger')");
        }


        [TestMethod]
        public void DisplayGameInstructions_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayGameInstructions();

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayGameInstructions");
        }


        [TestMethod]
        public void DisplayGameBoard_Test()
        {
            // Arrange 
            var boardOptionSettings = Substitute.For<IOptions<BoardOptions>>();
            _boardOptions = new BoardOptions();
            var tile = Substitute.For<ITile>();
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider.GetService(Arg.Is(typeof(ITile)))
                           .Returns(tile);

            _configRoot.GetSection(BoardOptions.Board)
                       .Bind(_boardOptions);
            boardOptionSettings.Value.Returns(_boardOptions);
            var gameBoard = new MinefieldGameBoard(boardOptionSettings,
                                                   serviceProvider,
                                                   Substitute.For<IConsoleWriterService>());
            gameBoard.InitializeBoard();

            // Act
            _consoleWriterService.DisplayGameBoard(gameBoard.GetBoardLayout(), gameBoard.GetActiveTile());

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayGameBoard");
        }


        [TestMethod]
        public void DisplayLives_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayLives(10);

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayLives");
        }


        [TestMethod]
        public void DisplayCurrentPosition_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayCurrentPosition(Substitute.For<ITile>());

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayCurrentPosition");
        }

        [TestMethod]
        public void DisplayMoves_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayMoves(10);

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayMoves");
        }

        [TestMethod]
        public void DisplayHitByMineWarn_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayHitByMineWarn();

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayHitByMineWarn");
        }


        [TestMethod]
        public void DisplayGameEnd_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayGameEnd(10);

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayGameEnd");
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


        [TestMethod]
        public void DisplayScore_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayScore(10, 10);

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayScore");
        }


        [TestMethod]
        public void DisplayJourney_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayJourney("D1 -> D2");

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayJourney");
        }


        [TestMethod]
        public void Clean_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.Clean();

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "Clean");
        }


        [TestMethod]
        public void DisplayContent_Test()
        {
            // Arrange 

            // Act
            _consoleWriterService.DisplayContent("Sample");

            // Assert
            _logger.AssertLoggerEntry(LogLevel.Information, "DisplayContent");
        }
    }
}