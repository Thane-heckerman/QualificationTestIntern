using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class JewelSpawnManager : MonoBehaviour
{
    public TetrominoShape[] tetrominoShapes;
    public static JewelSpawnManager Instance { get; private set; }
    [SerializeField] private JewelTypeSOList jewels;
    public Tilemap grid;

    [SerializeField] private Transform gamePiecePrefab;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < tetrominoShapes.Length; i++)
        {
            tetrominoShapes[i].Init();
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = Vector2Int.RoundToInt(pos);
            SpawnPiece(position);
        }
    }
    public Transform Spawn(Transform prefab, Vector2 pos)
    {
        return Instantiate(prefab, pos, quaternion.identity);
    }
    /// <summary>
    /// Press A key to spawn tetromino piece at mousePos
    /// </summary>
    /// <param name="pos">Vector2Int position.</param>
    /// <returns>void.</returns>
    public void SpawnPiece(Vector2Int pos)
    {
        GamePiece piece = Instantiate(gamePiecePrefab, (Vector3Int)pos, quaternion.identity).GetComponent<GamePiece>();
        TetrominoShape data = tetrominoShapes[UnityEngine.Random.Range(0, tetrominoShapes.Length)];
        Debug.Log(data.tetromino);

        piece.Initialize(pos, data);
        Set(piece);
    }

    public void Set(GamePiece gamePiece)
    {
        Tile randTile = gamePiece.tiles[UnityEngine.Random.Range(0, gamePiece.tiles.Count)];
        for (int i = 0; i < gamePiece.cells.Length; i++)
        {
            Vector3Int tilePos = gamePiece.cells[i] + (Vector3Int)gamePiece.position;
            gamePiece.GetComponent<Tilemap>().SetTile(tilePos, randTile);
        }
    }
}
