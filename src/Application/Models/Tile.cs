// Copyright (c) 2024, <Company>

using MinefieldBoard.Domain.Enums;
using MinefieldBoard.Domain.Helpers;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.Application.Models
{
    public sealed class Tile : ITile
    {
        private readonly IPlayer _player;
        private readonly IConsoleWriterService _consoleWriterService;
        private string _val;
        private int _xPosition;
        private int _yPosition;
        private string _xVal;
        private string _yVal;

        /// <summary>
        /// Ctor Initialize dependencies
        /// </summary>
        /// <param name="player"></param>
        /// <param name="consoleWriterService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Tile(IPlayer player,
                    IConsoleWriterService consoleWriterService)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _consoleWriterService = consoleWriterService ?? throw new ArgumentNullException(nameof(consoleWriterService));
        }

        /// <summary>
        /// Type of tile
        /// </summary>
        public TileType TileType { get; set; }

        /// <summary>
        /// Create tile
        /// </summary>
        /// <param name="xVal"></param>
        /// <param name="yVal"></param>
        /// <returns></returns>
        public ITile Create(int xVal, int yVal)
        {
            _xPosition = xVal;
            _yPosition = yVal;

            _xVal = BoardLabelHelper.GetXLabel(xVal);
            _yVal = (yVal + 1).ToString(); // index 1 

            _val = _xVal + _yVal;

            return this;
        }

        /// <summary>
        /// Set this tile as active
        /// </summary>
        public void SetActive()
        {
            if (TileType != TileType.Mine)
            {
                return;
            }

            // reduce lives 
            _player.ReduceLives();
            _consoleWriterService.DisplayHitByMineWarn();
        }

        public int GetXPosition()
        {
            return _xPosition;
        }

        public int GetYPosition()
        {
            return _yPosition;
        }

        public string GetXValue()
        {
            return _xVal;
        }

        public string GetYValue()
        {
            return _yVal;
        }

        public string GetValue()
        {
            return _val;
        }
    }
}