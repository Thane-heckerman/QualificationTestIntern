using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public Jewel jewel;

    public Jewel Jewel { get => jewel; set => jewel = value; }
    public int x;
    public int y;
    public GridManager grid;
    public Vector2 position;
    public bool isContainingGem;


    public void SetUp(int x, int y, GridManager grid)
    {
        position = transform.position;
        this.x = x;
        this.y = y;
        this.grid = grid;
        isContainingGem = false;
    }

    public void AssignToCell(Jewel _jewel)
    {
        jewel = _jewel;
        isContainingGem = true;
    }

    public Cell GetTopNeighbor()
    {
        if (y < grid.gridHeight - 1) return grid.GetGrid(x, y + 1);
        else return null;
    }

    public Cell GetBottomNeighbor()
    {
        if (y > 0)
            return grid.GetGrid(x, y - 1);
        else return null;
    }
    public Cell GetLeftNeighbor()
    {
        if (x > 0)
            return grid.GetGrid(x - 1, y);
        else return null;
    }

    public Cell GetRightNeighbor()
    {
        if (x < grid.gridWidth - 1)
            return grid.GetGrid(x + 1, y);
        else return null;
    }

    public List<Cell> CheckForSameGem()
    {

        List<Cell> sameGem = new List<Cell>();
        List<Cell> allPosibleNeighbors = GetAllPosibleNeighbor();
        Debug.Log("all posible neighbor" + allPosibleNeighbors.Count);
        foreach (var cell in allPosibleNeighbors)
        {
            if (cell.isContainingGem)
            {
                if (cell.jewel.GetType() == jewel.GetType())
                {
                    sameGem.Add(cell);
                    sameGem.Add(this);
                }
            }
        }
        return sameGem;
    }

    private List<Cell> GetAllPosibleNeighbor()
    {
        List<Cell> neighbors = new List<Cell>();
        if (GetBottomNeighbor() != null)
        {
            neighbors.Add(GetBottomNeighbor());

        }
        if (GetTopNeighbor() != null)
        {
            neighbors.Add(GetTopNeighbor());

        }
        if (GetRightNeighbor() != null)
        {
            neighbors.Add(GetRightNeighbor());

        }
        if (GetLeftNeighbor() != null)
        {
            neighbors.Add(GetLeftNeighbor());

        }
        return neighbors;
    }
    public void DestroyGem()
    {
        jewel.SeflDestroy();
        jewel = null;
        isContainingGem = false;
    }

    public Jewel GetJewel()
    {
        return jewel;
    }

    public void ResetCell()
    {
        Jewel = null;
        isContainingGem = false;
    }

    public void ReceiveGem(Jewel _jewel)
    {
        jewel = _jewel;
        isContainingGem = true;
    }

}
