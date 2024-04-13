// Copyright (c) 2024, Elekta, AB


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinefieldBoard.Application.Models;
using MinefieldBoard.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Models
{
    [TestClass]
    public class PlayerTests
    {
        private static IConfigurationRoot _configRoot;
        private static BoardOptions _boardOptions;
        private static IOptions<BoardOptions> _boardOptionSettings;
        private static IConsoleWriterService _consoleWriterService;
        private static IOptions<BoardOptions> _nullBoardOptionSettings;
        private static IGameBoard _gameBoard;
        private Player _player;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCtorParams();

            _player = new Player(_boardOptionSettings,
                                 _gameBoard,
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

            _gameBoard = Substitute.For<IGameBoard>();
            _consoleWriterService = Substitute.For<IConsoleWriterService>();
        }


        [TestMethod]
        [DynamicData(nameof(GetCtorArgs), DynamicDataSourceType.Method)]
        public void Ctor_Args_Check_Test(IOptions<BoardOptions> options,
                                         IGameBoard gameBoard,
                                         IConsoleWriterService consoleWriterService,
                                         string errorMessage)
        {
            // Arrange 

            // Act
            Action ar = () => _ = new Player(options, gameBoard, consoleWriterService);

            // Assert
            ar.ShouldThrow<ArgumentNullException>().Message.ShouldBe(errorMessage);
        }

        private static IEnumerable<object[]> GetCtorArgs()
        {
            InitializeCtorParams();

            yield return new object[]
            {
                _nullBoardOptionSettings,
                _gameBoard,
                _consoleWriterService,
                "Value cannot be null. (Parameter 'Value')"
            };
            yield return new object[]
            {
                _boardOptionSettings,
                null,
                _consoleWriterService,
                "Value cannot be null. (Parameter 'gameBoard')"
            };
            yield return new object[]
            {
                _boardOptionSettings,
                _gameBoard,
                null,
                "Value cannot be null. (Parameter 'consoleWriterService')"
            };
        }


        [TestMethod]
        public void MoveUp_Test()
        {
            // Arrange 
            _gameBoard.MoveUp().Returns(true);

            // Act
            _player.MoveUp();

            // Assert
            _gameBoard.Received(1).MoveUp();
            _consoleWriterService.Received(1).DisplayMoves(Arg.Any<int>());
        }


        [TestMethod]
        public void MoveDown_Test()
        {
            // Arrange 
            _gameBoard.MoveDown().Returns(true);

            // Act
            _player.MoveDown();

            // Assert
            _gameBoard.Received(1).MoveDown();
            _consoleWriterService.Received(1).DisplayMoves(Arg.Any<int>());
        }


        [TestMethod]
        public void MoveLeft_Test()
        {
            // Arrange 
            _gameBoard.MoveLeft().Returns(true);

            // Act
            _player.MoveLeft();

            // Assert
            _gameBoard.Received(1).MoveLeft();
            _consoleWriterService.Received(1).DisplayMoves(Arg.Any<int>());
        }


        [TestMethod]
        public void MoveRight_Test()
        {
            // Arrange 
            _gameBoard.MoveRight().Returns(true);

            // Act
            _player.MoveRight();

            // Assert
            _gameBoard.Received(1).MoveRight();
            _consoleWriterService.Received(1).DisplayMoves(Arg.Any<int>());
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
