using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinefieldBoard.Domain.Enums;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.Application.Models
{
    public class MinefieldGameBoard : IGameBoard
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConsoleWriterService _consoleWriterService;
        private readonly BoardOptions _boardOptions;
        private ITile[,] _tiles;
        private ITile _currentTile;
        private const int _defaultStartColPosition = 0;

        /// <summary>
        /// Ctor to initialize dependencies
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="consoleWriterService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MinefieldGameBoard(IOptions<BoardOptions> options,
                                  IServiceProvider serviceProvider,
                                  IConsoleWriterService consoleWriterService
        )
        {
            _boardOptions = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _consoleWriterService = consoleWriterService ?? throw new ArgumentNullException(nameof(consoleWriterService));
        }

        /// <summary>
        /// Initialize board
        /// </summary>
        public void InitializeBoard()
        {
            var width = _boardOptions.BoardSize;
            var height = _boardOptions.BoardSize;

            _tiles = CreateTiles(width, height);

            _currentTile = GetRandomStartPosition();

            DisplayBoard();
        }

        /// <summary>
        /// Get board tiles
        /// </summary>
        /// <returns></returns>
        public ITile[,] GetBoardLayout()
        {
            return _tiles;
        }

        /// <summary>
        /// Get random start without Mine located
        /// </summary>
        /// <returns></returns>
        private ITile GetRandomStartPosition()
        {
            var startRowPosition = new Random().Next(0, _boardOptions.BoardSize);
            var tile = _tiles[startRowPosition, _defaultStartColPosition];
            while (tile.TileType == TileType.Mine)
            {
                startRowPosition = new Random().Next(0, _boardOptions.BoardSize);
                tile = _tiles[startRowPosition, _defaultStartColPosition];
            }

            return tile;
        }

        /// <summary>
        /// Display instructions, board and position
        /// </summary>
        private void DisplayBoard()
        {
            _consoleWriterService.Clean();
            _consoleWriterService.DisplayGameInstructions();
            _consoleWriterService.DisplayGameBoard(_tiles, _currentTile);
            _consoleWriterService.DisplayCurrentPosition(_currentTile);

            var mineTiles = from ITile tile in _tiles.Cast<ITile>()
                            where tile.TileType == TileType.Mine
                            select tile;

            _consoleWriterService.DisplayContent($" Total number of mines are {mineTiles.Count()}");
        }

        /// <summary>
        /// Create tiles
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private ITile[,] CreateTiles(int width, int height)
        {
            var tiles = new ITile[width, height];
            var randomMineSet = _boardOptions.RandomMineSpot;
            var totalMines = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    // set mines randomly
                    var randomMine = new Random().Next(0, width - 1) == randomMineSet;
                    var tileType = TileType.Default;
                    if (totalMines <= _boardOptions.MaxMines && randomMine)
                    {
                        tileType = TileType.Mine;
                        totalMines++;
                    }

                    CreateNewTile(tiles, x, y, tileType);
                }
            }

            return tiles;
        }


        /// <summary>
        /// Create new tile with required options
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileType"></param>
        private void CreateNewTile(ITile[,] tiles, int x, int y, TileType tileType)
        {
            var tile = _serviceProvider.GetService<ITile>();
            tiles[x, y] = tile.Create(x, y);
            tiles[x, y].TileType = tileType;
        }

        /// <summary>
        /// Move Up on the board
        /// </summary>
        /// <returns></returns>
        public bool MoveUp()
        {
            if (_currentTile.GetYPosition() >= _boardOptions.BoardSize - 1)
            {
                return false;
            }

            _currentTile = _tiles[_currentTile.GetXPosition(), _currentTile.GetYPosition() + 1];
            DisplayBoard();
            return true;
        }

        /// <summary>
        /// Move Down on the board
        /// </summary>
        /// <returns></returns>
        public bool MoveDown()
        {
            if (_currentTile.GetYPosition() <= 0)
            {
                return false;
            }

            _currentTile = _tiles[_currentTile.GetXPosition(), _currentTile.GetYPosition() - 1];
            DisplayBoard();
            return true;
        }

        public bool MoveLeft()
        {
            if (_currentTile.GetXPosition() <= 0)
            {
                return false;
            }

            _currentTile = _tiles[_currentTile.GetXPosition() - 1, _currentTile.GetYPosition()];
            DisplayBoard();
            return true;
        }

        public bool MoveRight()
        {
            if (_currentTile.GetXPosition() >= _boardOptions.BoardSize - 1)
            {
                return false;
            }

            _currentTile = _tiles[_currentTile.GetXPosition() + 1, _currentTile.GetYPosition()];
            DisplayBoard();
            return true;
        }

        public ITile GetActiveTile()
        {
            return _currentTile;
        }
    }
}
