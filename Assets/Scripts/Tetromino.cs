using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}
[System.Serializable]
public struct TetrominoShape
{
    public Tetromino tetromino;
    public Vector2Int[] cells { get; private set; }

    public void Init()
    {
        this.cells = Data.Cells[tetromino];
    }
}