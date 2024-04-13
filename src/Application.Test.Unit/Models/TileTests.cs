// Copyright (c) 2024, <CompanyName>


using MinefieldBoard.Application.Models;
using MinefieldBoard.Domain.Enums;
using MinefieldBoard.Domain.Interfaces;
using NSubstitute;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Models
{
    [TestClass]
    public class TileTests
    {
        private static IPlayer _player;
        private static IConsoleWriterService _consoleWriterService;
        private Tile _tile;

        [TestInitialize]
        public void Initialize()
        {
            InitializeCtorParams();

            _tile = new Tile(_player, _consoleWriterService);
        }

        private static void InitializeCtorParams()
        {
            _player = Substitute.For<IPlayer>();
            _consoleWriterService = Substitute.For<IConsoleWriterService>();
        }


        [TestMethod]
        [DynamicData(nameof(GetCtorArgs), DynamicDataSourceType.Method)]
        public void Ctor_Args_Check_Test(IPlayer player,
                                         IConsoleWriterService consoleWriterService,
                                         string errorMessage)
        {
            // Arrange 

            // Act
            Action ar = () => _ = new Tile(player, consoleWriterService);

            // Assert
            ar.ShouldThrow<ArgumentNullException>().Message.ShouldBe(errorMessage);
        }

        private static IEnumerable<object[]> GetCtorArgs()
        {
            InitializeCtorParams();

            yield return new object[]
            {
                null,
                _consoleWriterService,
                "Value cannot be null. (Parameter 'player')"
            };

            yield return new object[]
            {
                _player,
                null,
                "Value cannot be null. (Parameter 'consoleWriterService')"
            };
        }


        [TestMethod]
        public void Create_Test()
        {
            // Arrange 

            // Act
            var tile = _tile.Create(1, 1);

            // Assert
            tile.GetXValue().ShouldBe("B");
        }


        [TestMethod]
        public void SetActive_MineTile_Test()
        {
            // Arrange 
            var tile = _tile.Create(1, 1);
            tile.TileType = TileType.Mine;

            // Act
            tile.SetActive();

            // Assert
            _player.Received(1).ReduceLives();
            _consoleWriterService.Received(1).DisplayHitByMineWarn();
        }


        [TestMethod]
        public void SetActive_NormalTile_Test()
        {
            // Arrange 
            var tile = _tile.Create(1, 1);
            tile.TileType = TileType.Default;

            // Act
            tile.SetActive();

            // Assert
            _player.Received(0).ReduceLives();
        }


        [TestMethod]
        public void GetXPosition_Test()
        {
            // Arrange 
            var tile = _tile.Create(2, 2);

            // Act
            var xPos = tile.GetXPosition();

            // Assert
            xPos.ShouldBe(2);
        }


        [TestMethod]
        public void GetYPosition_Test()
        {
            // Arrange 
            var tile = _tile.Create(2, 2);

            // Act
            var yPos = tile.GetYPosition();

            // Assert
            yPos.ShouldBe(2);
        }

        [TestMethod]
        public void GetXValue_Test()
        {
            // Arrange 
            var tile = _tile.Create(2, 2);

            // Act
            var xVal = tile.GetXValue();

            // Assert
            xVal.ShouldBe("C");
        }


        [TestMethod]
        public void GetYValue_Test()
        {
            // Arrange 
            var tile = _tile.Create(2, 2);

            // Act
            var yVal = tile.GetYValue();

            // Assert
            yVal.ShouldBe("3");
        }


        [TestMethod]
        public void GetValue_Test()
        {
            // Arrange 
            var tile = _tile.Create(2, 2);

            // Act
            var val = tile.GetValue();

            // Assert
            val.ShouldBe("C3");
        }
    }
}