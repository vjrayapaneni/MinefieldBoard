namespace MinefieldBoard.Domain.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// UP key action
        /// </summary>
        void MoveUp();

        /// <summary>
        /// DOWN key action
        /// </summary>
        void MoveDown();

        /// <summary>
        /// LEFT key action
        /// </summary>
        void MoveLeft();

        /// <summary>
        /// RIGHT key action
        /// </summary>
        void MoveRight();

        /// <summary>
        /// Reduce lives when hit by mine
        /// </summary>
        void ReduceLives();

        /// <summary>
        /// Check for alive or not
        /// </summary>
        /// <returns></returns>
        bool IsAlive();

        /// <summary>
        /// Reset the game, i.e. lives to max
        /// </summary>
        void Reset();

        /// <summary>
        /// Is reached to finish line
        /// </summary>
        /// <returns></returns>
        bool IsFinished();
    }
}
