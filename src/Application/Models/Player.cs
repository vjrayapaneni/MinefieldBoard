using Microsoft.Extensions.Options;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.Application.Models
{
    public class Player : IPlayer
    {
        private readonly IGameBoard _gameBoard;
        private readonly IConsoleWriterService _consoleWriterService;
        private readonly BoardOptions _boardOptions;
        private int _livesRemaining;
        private int _movesTaken;
        private string _journeyPath = string.Empty;

        /// <summary>
        /// Ctor to load dependencies
        /// </summary>
        /// <param name="options"></param>
        /// <param name="gameBoard"></param>
        /// <param name="consoleWriterService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Player(IOptions<BoardOptions> options,
                      IGameBoard gameBoard,
                      IConsoleWriterService consoleWriterService)
        {
            _consoleWriterService = consoleWriterService ?? throw new ArgumentNullException(nameof(consoleWriterService));
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            _boardOptions = options.Value ?? throw new ArgumentNullException(nameof(options.Value));

            _livesRemaining = _boardOptions.MaxLives;
        }

        /// <summary>
        /// Move Up
        /// </summary>
        public void MoveUp()
        {
            if (_gameBoard.MoveUp())
            {
                Move();
            }
        }

        public void MoveDown()
        {
            if (_gameBoard.MoveDown())
            {
                Move();
            }
        }

        public void MoveLeft()
        {
            if (_gameBoard.MoveLeft())
            {
                Move();
            }
        }

        public void MoveRight()
        {
            if (_gameBoard.MoveRight())
            {
                Move();
            }
        }

        /// <summary>
        /// Move actions and display on console
        /// </summary>
        private void Move()
        {
            _movesTaken++;
            _consoleWriterService.DisplayMoves(_movesTaken);
            var tile = _gameBoard.GetActiveTile();
            tile.SetActive();
            SetJourneyPath(tile);

            if (IsFinished())
            {
                _consoleWriterService.DisplayScore(_movesTaken, _livesRemaining);
            }

            if (!IsFinished())
            {
                _consoleWriterService.DisplayLives(_livesRemaining);
            }

            if (_livesRemaining == 0)
            {
                _consoleWriterService.DisplayGameEnd(_movesTaken);
            }

            _consoleWriterService.DisplayJourney(_journeyPath);
        }

        /// <summary>
        /// Set journey path
        /// </summary>
        /// <param name="tile"></param>
        private void SetJourneyPath(ITile tile)
        {
            if (string.IsNullOrWhiteSpace(_journeyPath))
            {
                _journeyPath = tile.GetValue();
            }
            else
            {
                _journeyPath += $" -> {tile.GetValue()}";
            }
        }

        /// <summary>
        /// Reduce lives when hit mine
        /// </summary>
        public void ReduceLives()
        {
            _livesRemaining--;
        }

        /// <summary>
        /// Is there more lives to continue?
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return _livesRemaining > 0;
        }

        /// <summary>
        /// Reset lives, board, player position etc
        /// </summary>
        public void Reset()
        {
            _livesRemaining = _boardOptions.MaxLives;
            _movesTaken = 0;

            SetJourneyPath(_gameBoard.GetActiveTile());
            _consoleWriterService.DisplayJourney(_journeyPath);
        }

        /// <summary>
        /// Is Player moved to other side of the board?
        /// </summary>
        /// <returns></returns>
        public bool IsFinished()
        {
            return _gameBoard.GetActiveTile().GetYPosition() == _boardOptions.BoardSize - 1;
        }
    }
}
