namespace MinefieldBoard.Domain.Interfaces
{
    public interface IConsoleWriterService
    {
        /// <summary>
        /// Game instructions
        /// </summary>
        void DisplayGameInstructions();

        /// <summary>
        /// Display tiles
        /// </summary>
        /// <param name="boardTiles"></param>
        /// <param name="currentTile"></param>
        void DisplayGameBoard(ITile[,] boardTiles, ITile currentTile);

        /// <summary>
        /// Display lives information
        /// </summary>
        /// <param name="livesLeft"></param>
        void DisplayLives(int livesLeft);

        /// <summary>
        /// Display Current position
        /// </summary>
        /// <param name="currentTile"></param>
        void DisplayCurrentPosition(ITile currentTile);

        /// <summary>
        /// Display total moves
        /// </summary>
        /// <param name="totalMoves"></param>
        void DisplayMoves(int totalMoves);

        /// <summary>
        /// Display Mine hit warning
        /// </summary>
        void DisplayHitByMineWarn();

        /// <summary>
        /// Display Game end instructions
        /// </summary>
        /// <param name="moves"></param>
        void DisplayGameEnd(int moves);

        /// <summary>
        /// Display score
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="lives"></param>
        void DisplayScore(int moves, int lives);

        /// <summary>
        /// Display player journey
        /// </summary>
        /// <param name="route"></param>
        void DisplayJourney(string route);

        /// <summary>
        /// Clean
        /// </summary>
        void Clean();

        /// <summary>
        /// Display supplied content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="color"></param>
        void DisplayContent(string content, ConsoleColor color = ConsoleColor.White);
    }
}
