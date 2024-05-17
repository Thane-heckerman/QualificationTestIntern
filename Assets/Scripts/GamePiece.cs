using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePiece : MonoBehaviour
{
    public List<Tile> tiles;
    public TetrominoShape shape;
    public Vector2Int position { get; private set; }
    public Vector3Int[] cells;

    private void Awake()
    {

    }
    public void Initialize(Vector2Int pos, TetrominoShape _shape)
    {
        shape = _shape;
        position = pos;
        if (cells.Length == 0)
        {
            cells = new Vector3Int[shape.cells.Length];
        }

        for (int i = 0; i < shape.cells.Length; i++)
        {
            cells[i] = (Vector3Int)shape.cells[i];
        }
    }
}
