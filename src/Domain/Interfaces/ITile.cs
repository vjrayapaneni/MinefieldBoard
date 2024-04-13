// Copyright (c) 2024, <Company>

using MinefieldBoard.Domain.Enums;

namespace MinefieldBoard.Domain.Interfaces;

public interface ITile
{
    /// <summary>
    /// Type of tile
    /// </summary>
    TileType TileType { get; set; }

    /// <summary>
    /// Create tile with specific identifiers
    /// </summary>
    /// <param name="xVal"></param>
    /// <param name="yVal"></param>
    ITile Create(int xVal, int yVal);

    /// <summary>
    /// Set the current tile as active
    /// </summary>
    void SetActive();

    /// <summary>
    /// Get X position within a grid/game board
    /// </summary>
    /// <returns></returns>
    int GetXPosition();

    /// <summary>
    /// Get Y position 
    /// </summary>
    /// <returns></returns>
    int GetYPosition();

    /// <summary>
    /// Get value of the X (Row position) as named
    /// </summary>
    /// <returns></returns>
    string GetXValue();

    /// <summary>
    /// Get value of Y (column position) as named
    /// </summary>
    /// <returns></returns>
    string GetYValue();

    /// <summary>
    /// Get Cell/Tile value 
    /// </summary>
    /// <returns></returns>
    string GetValue();
}