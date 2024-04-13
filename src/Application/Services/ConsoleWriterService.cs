using Microsoft.Extensions.Logging;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.Application.Services
{
    public class ConsoleWriterService : IConsoleWriterService
    {
        private readonly ILogger<ConsoleWriterService> _logger;

        public ConsoleWriterService(ILogger<ConsoleWriterService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DisplayGameInstructions()
        {
            _logger.LogInformation("Begin of DisplayGameInstructions");

            Console.WriteLine();
            Console.WriteLine(" Welcome to Minefield Game board. Please try to reach the end while avoiding the mines");
            Console.WriteLine(" Press Enter to restart, or Escape to exit");
        }

        public void DisplayGameBoard(ITile[,] boardTiles, ITile currentTile)
        {
            _logger.LogInformation("Begin of DisplayGameBoard");

            var width = boardTiles.GetLength(0);
            var height = boardTiles.GetLength(1) - 1;

            Console.WriteLine();

            // Display grid
            DisplayGrid(boardTiles, currentTile, height, width);

            // Write column headers
            Console.Write("   ");
            for (var x = 0; x < width; x++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(boardTiles[x, 0].GetXValue());
                Console.Write("    ");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Grid X,Y values
        /// </summary>
        /// <param name="boardTiles"></param>
        /// <param name="currentTile"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        private static void DisplayGrid(ITile[,] boardTiles, ITile currentTile, int height, int width)
        {
            for (var y = height; y >= 0; y--)
            {
                Console.Write("  ");
                for (var x = 0; x < width; x++)
                {
                    if (boardTiles[x, y] == currentTile)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write("[x]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }

                    Console.Write("  ");
                }

                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(boardTiles[0, y].GetYValue());
                Console.Write("  ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void DisplayLives(int livesLeft)
        {
            _logger.LogInformation("Begin of DisplayLives");
            Console.WriteLine($" Lives left: {livesLeft}");
        }

        public void DisplayCurrentPosition(ITile currentTile)
        {
            _logger.LogInformation("Begin of DisplayCurrentPosition");
            Console.WriteLine($" Current position: {currentTile.GetValue()}");
        }

        public void DisplayMoves(int totalMoves)
        {
            _logger.LogInformation("Begin of DisplayMoves");
            Console.WriteLine($" The total moves so far are : {totalMoves}");
        }

        public void DisplayHitByMineWarn()
        {
            _logger.LogInformation("Begin of DisplayHitByMineWarn");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ****Boom! It's a Mine****. You lost one more life, Continue the game carefully");
            Console.ResetColor();
        }

        public void DisplayGameEnd(int moves)
        {
            _logger.LogInformation("Begin of DisplayGameEnd");
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" ***GAME OVER!***");
            Console.WriteLine(" You have ran out of lives!");
            Console.WriteLine($" The total moves are : {moves}");

            DisplayRestartGameInstruction();
        }

        private static void DisplayRestartGameInstruction()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Press Enter to play again, or Escape to exit");
            Console.ResetColor();
        }

        public void DisplayScore(int moves, int lives)
        {
            _logger.LogInformation("Begin of DisplayScore");
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" ***WELL DONE!***");
            Console.WriteLine($" You reached the end with lives left : {lives}");
            Console.WriteLine($" Final Score (moves taken): {moves}");
            DisplayRestartGameInstruction();
        }

        /// <summary>
        /// Display user/player journey
        /// </summary>
        /// <param name="route"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DisplayJourney(string route)
        {
            _logger.LogInformation("Begin of DisplayJourney");
            Console.WriteLine();
            Console.WriteLine($" Player is navigated : {route}");
        }

        public void Clean()
        {
            _logger.LogInformation("Begin of Clean");
            Console.Clear();
        }

        public void DisplayContent(string content, ConsoleColor color = ConsoleColor.White)
        {
            _logger.LogInformation("Begin of DisplayContent");
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }
    }
}
