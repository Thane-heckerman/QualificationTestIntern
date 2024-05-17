using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Timeline;
public class Jewel : MonoBehaviour
{
    public Cell cell;
    private Rigidbody2D rb;
    [SerializeField] private JewelTypeSO type;
    public LayerMask CellLayer;
    Vector2 originalPos;
    private bool dragging;
    Vector3 mousePos;
    private float sizeLap = 1f;
    public static event EventHandler OnJewelDrop;
    public bool isBusy = false;
    private void Start()
    {
        originalPos = transform.position;
        dragging = false;
        Debug.Log("min x" + Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x);
        Debug.Log("max x" + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Mathf.Clamp(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Mathf.Clamp(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
    }
    private Vector3 GetWorldPos()
    {

        return Camera.main.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        Debug.Log("mouse Down");
        if (cell != null)
        {
            cell.ResetCell();
        }
        cell = null;
        dragging = true;
        mousePos = Input.mousePosition - GetWorldPos();

    }

    private void OnMouseDrag()
    {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos).x, Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos).y);
        LookForNearestCell();
    }
    private void Update()
    {
    }
    private void OnMouseUp()
    {
        Debug.Log("mouse up");
        dragging = false;
        // if(GridManager.Instance.CheckForValidPosition(transform.position)){
        //     transform.position = cell.transform.localPosition;
        //     originalPos = transform.position;
        // }
        if (cell != null)
        {
            UpdateCellPos();
            OnJewelDrop?.Invoke(this, EventArgs.Empty);
        }
        else transform.position = originalPos;
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void SetCell(Cell _cell)
    {
        if (cell != null)
        {
            cell.isContainingGem = false;
            cell.Jewel = null;
        }
        cell = _cell;
    }

    private void LookForNearestCell()
    {

        var colliderArray = Physics2D.OverlapCircleAll(transform.position, sizeLap, CellLayer);
        if (colliderArray.Length > 0)
        {
            // cell around
            foreach (var collider in colliderArray)
            {
                Cell currentCell = collider.GetComponent<Cell>();
                if (collider.GetComponent<Cell>().isContainingGem) continue;
                if (cell == null)
                {
                    cell = currentCell;
                }

                else
                {
                    var distanceToCurrent = Vector2.Distance(transform.position, cell.transform.position);
                    var distanceToOther = Vector2.Distance(transform.position, currentCell.transform.position);

                    if (distanceToOther < distanceToCurrent)
                    {
                        cell = currentCell;
                    }

                }
            }

        }
        else
        {
            Debug.Log("found nothing");
        }
    }

    private void UpdateCellPos()
    {

        transform.position = cell.transform.position;
        cell.AssignToCell(this);
    }

    public JewelTypeSO GetType()
    {
        return type;
    }
    public void SeflDestroy()
    {
        cell.ResetCell();
        isBusy = true;
        transform.DOScale(Vector2.zero, .3f).OnComplete(() => Destroy(this.gameObject));
        isBusy = false;
    }
}
