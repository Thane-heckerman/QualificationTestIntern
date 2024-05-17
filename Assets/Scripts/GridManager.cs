using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private Transform BGPrefab;
    [SerializeField] private JewelTypeSOList jewelTypeSOList;
    public int gridHeight;
    public int gridWidth;
    public Vector2 origin;
    private Cell[,] grid;
    float xSpacing;
    float ySpacing;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

    }


    public void InitGrid(int gridHeight, int gridWidth)
    {
        grid = new Cell[gridWidth, gridHeight];
        xSpacing = (float)(gridWidth - 1) / 2;
        ySpacing = (float)(gridHeight - 1) / 2;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 position = new Vector2(x - xSpacing, y - ySpacing);
                Debug.Log(position);

                Vector2Int.FloorToInt(position);
                Debug.Log("position vector2Int" + position);//
                Transform backGroundTransform = Instantiate(BGPrefab, position, Quaternion.identity);
                // backGroundTransform.transform.parent = this.transform;
                backGroundTransform.gameObject.name = $"Cell[{x},{y}]";
                Cell cell = backGroundTransform.GetComponent<Cell>();
                cell.SetUp(x, y, this);
                grid[x, y] = cell;

            }
        }
    }

    public Vector2 GetGridPos(Cell cell)
    {
        return cell.transform.position;
    }
    public bool CheckForValidPosition(Vector2 position)
    {
        return position.x >= -xSpacing && position.x <= xSpacing && position.y <= ySpacing && position.y >= -ySpacing;
    }
    public Cell GetGrid(int x, int y)
    {
        return grid[x, y];
    }
    public float GetXSpacing()
    {
        return xSpacing;
    }

    public float GetYSpacing()
    {
        return ySpacing;
    }

}
