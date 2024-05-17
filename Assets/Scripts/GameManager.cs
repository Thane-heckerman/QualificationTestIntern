using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public bool isBusy;
    public List<Transform> jewels = new List<Transform>();
    public Jewel selectedJewel;
    int gemsNumber = 2;
    public Cell test; //
    public List<Cell> hasSameGemTypeCells;
    public JewelTypeSOList jewelTypes;
    private Camera mainCamera;
    [SerializeField] GridManager gridManager;

    private void Awake()
    {
        mainCamera = Camera.main;

    }

    private void Jewel_OnJewelDrop(object sender, EventArgs e)
    {
        isBusy = true;
        if (CheckForPossibleMatches())
        {
            CoinAnimManager.Instance.PlayAnim("CoinAnim", hasSameGemTypeCells[0].transform, hasSameGemTypeCells[1].transform);
            DestroyGems();
        }
        isBusy = false;
    }

    private void Update()
    {
        if (isAllGemsCleared())
        {
            StartCoroutine(SpawnMoreGems());
        }
        //test
        if (Input.GetKeyDown(KeyCode.A))
        {
            jewels.Remove(jewels.Where(j => j.GetComponent<Jewel>().GetType() == jewelTypes.jewels[1]).FirstOrDefault());
        }
    }

    private bool CheckForPossibleMatches()
    {
        hasSameGemTypeCells = new List<Cell>();
        for (int y = 0; y < GridManager.Instance.gridHeight; y++)
        {
            for (int x = 0; x < GridManager.Instance.gridWidth; x++)
            {

                if (!gridManager.GetGrid(x, y).isContainingGem) continue;
                if (gridManager.GetGrid(x, y).CheckForSameGem().Count != 0)
                {
                    Debug.Log("check same gem");
                    foreach (var cell in gridManager.GetGrid(x, y).CheckForSameGem())
                    {
                        if (hasSameGemTypeCells.Contains(cell))
                        {
                            continue;
                        }
                        else
                        {
                            hasSameGemTypeCells.Add(cell);
                        }
                    }

                    foreach (var cell in hasSameGemTypeCells)
                    {
                        Debug.Log("Cell has same type" + cell.x + ", " + cell.y);
                    }
                }
                Debug.Log(hasSameGemTypeCells.Count);
            }
        }
        return hasSameGemTypeCells.Count != 0;

    }

    void DestroyGems()
    {
        foreach (var cell in hasSameGemTypeCells)
        {
            jewels.Remove(cell.GetJewel().transform);
            cell.DestroyGem();
        }
        hasSameGemTypeCells.Clear();

    }


    private Vector2 SetRandomPostionFromLowestCell()
    {
        var lowestY = GridManager.Instance.GetGrid(0, 0).y;
        Vector2 pos = new Vector2(UnityEngine.Random.Range(-GridManager.Instance.GetXSpacing(), GridManager.Instance.GetXSpacing()),
                                    lowestY - UnityEngine.Random.Range(3f, 5f));
        return pos;
    }

    private void Start()
    {

        GridManager.Instance.InitGrid(GridManager.Instance.gridHeight, GridManager.Instance.gridWidth);
        SpawnGem();
        Jewel.OnJewelDrop += Jewel_OnJewelDrop;
    }

    private void HandleClick(RaycastHit2D hit)
    {
        if (hit.collider.GetComponent<Jewel>() != null)
        {
            selectedJewel = hit.collider.GetComponent<Jewel>();

        }
    }
    private void SpawnGem()
    {
        for (var i = 0; i < jewelTypes.jewels.Count; i++)
        {
            for (int j = 0; j < gemsNumber; j++)
            {
                Transform colorJewel = JewelSpawnManager.Instance.Spawn(jewelTypes.jewels[i].prefab, SetRandomPostionFromLowestCell());
                jewels.Add(colorJewel);
            }
        }
    }
    IEnumerator SpawnMoreGems()
    {
        if (isBusy) yield return new WaitUntil(() => isBusy == false);
        SpawnGem();
    }

    private bool isAllGemsCleared()
    {
        return jewels.Count == 0;
    }
}

