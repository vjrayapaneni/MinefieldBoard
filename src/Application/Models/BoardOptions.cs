namespace MinefieldBoard.Application.Models
{
    public class BoardOptions
    {
        public const string Board = "board";

        /// <summary>
        /// Board size n x n 
        /// </summary>
        public int BoardSize { get; set; }

        /// <summary>
        /// Max no of lives that user can use to cross the board
        /// </summary>
        public int MaxLives { get; set; }

        /// <summary>
        /// Max number of mines to place
        /// </summary>
        public int MaxMines { get; set; }

        /// <summary>
        /// Using this to place a mine in a specific tile
        /// </summary>
        public int RandomMineSpot { get; set; }
    }
}
