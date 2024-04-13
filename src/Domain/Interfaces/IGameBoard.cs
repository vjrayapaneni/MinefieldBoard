namespace MinefieldBoard.Domain.Interfaces
{
    /// <summary>
    /// Minefield game board
    /// </summary>
    public interface IGameBoard
    {
        /// <summary>
        /// Initialize the board
        /// </summary>
        void InitializeBoard();

        /// <summary>
        /// Get Board layout
        /// </summary>
        /// <returns></returns>
        ITile[,] GetBoardLayout();

        /// <summary>
        /// Move Up 
        /// </summary>
        /// <returns></returns>
        bool MoveUp();

        /// <summary>
        /// Move Down
        /// </summary>
        /// <returns></returns>
        bool MoveDown();

        /// <summary>
        /// Move Left
        /// </summary>
        /// <returns></returns>
        bool MoveLeft();

        /// <summary>
        /// Move Right
        /// </summary>
        /// <returns></returns>
        bool MoveRight();

        /// <summary>
        /// Get active tile where player is positioned now
        /// </summary>
        /// <returns></returns>
        ITile GetActiveTile();
    }
}
