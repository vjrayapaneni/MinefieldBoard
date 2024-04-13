using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.GameConsole
{
    internal class Game : IGame
    {
        private readonly IGameBoard _gameBoard;
        private readonly IPlayer _player;
        private readonly IConsoleWriterService _consoleWriterService;

        /// <summary>
        /// Ctor to initialize dependencies
        /// </summary>
        /// <param name="gameBoard"></param>
        /// <param name="player"></param>
        /// <param name="consoleWriterService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Game(IGameBoard gameBoard,
                    IPlayer player,
                    IConsoleWriterService consoleWriterService)
        {
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _consoleWriterService = consoleWriterService ?? throw new ArgumentNullException(nameof(consoleWriterService));
        }

        /// <summary>
        /// To Start the game
        /// </summary>
        public void Start()
        {
            _gameBoard.InitializeBoard();
            _player.Reset();
            while (_player.IsAlive() && !_player.IsFinished())
            {
                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    {
                        _player.MoveUp();
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    {
                        _player.MoveDown();
                        break;
                    }
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    {
                        _player.MoveLeft();
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    {
                        _player.MoveRight();
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        _gameBoard.InitializeBoard();
                        _player.Reset();
                        break;
                    }
                    case ConsoleKey.Escape:
                    {
                        return;
                    }
                    default:
                        _consoleWriterService.DisplayContent("Invalid key", ConsoleColor.DarkRed);
                        break;
                }
            }

            Reset();
        }

        /// <summary>
        /// To retry or end 
        /// </summary>
        private void Reset()
        {
            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.Enter:
                {
                    _consoleWriterService.Clean();
                    Start();
                    break;
                }
                case ConsoleKey.Escape:
                {
                    return;
                }
                default:
                {
                    Reset();
                    break;
                }
            }
        }
    }
}