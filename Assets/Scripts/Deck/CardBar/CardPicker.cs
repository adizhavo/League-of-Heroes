using UnityEngine;
using System.Collections;

public class CardPicker : MonoBehaviour {
    
    private Card selectedCard;
    private GridSelector selector = new GridSelector();

	private void Update () 
    {
        if (Input.GetMouseButtonDown(0))
            CheckForCardSelection();
        else if (Input.GetMouseButton(0))
            DragCardToGrid();
        else if (Input.GetMouseButtonUp(0))
            ReleaseCard();
	}

    private void CheckForCardSelection()
    {
        RaycastHit2D hit2D = GetHitOnMousePos();

        if (hit2D.transform == null) return;

        selectedCard = hit2D.transform.GetComponent<Card>();
    }

    private void DragCardToGrid()
    {
        RaycastHit2D hit2D = GetHitOnMousePos();

        if (isSelectionValid(hit2D))
        {
            selectedCard = null;
            return;
        }

        GridCell cell = hit2D.transform.GetComponent<GridCell>();
        selector.HighlightPiece(cell);
    }

    private bool isSelectionValid(RaycastHit2D hit2D)
    {
        return selectedCard == null || !selectedCard.IsEnabled() || hit2D.transform == null;
    }

    private void ReleaseCard()
    {
        if (selectedCard == null || selector.SelectedCell == null || selector.SelectedCell.HasObstacle())
        {
            selectedCard = null;
            selector.Release();
            return;
        }

        selectedCard.Deploy(selector.SelectedCell);
        selector.Release();
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        int manaCost = selectedCard.ManaCost;
        selectedCard.Discard();
        ManaBar.Instance.ConsumeMana(manaCost);
    }

    private RaycastHit2D GetHitOnMousePos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(mousePos, Vector2.up);
    }
}

public struct GridSelector
{
    public GridCell SelectedCell;

    public void HighlightPiece(GridCell cell)
    {
        if (cell == null)
        {
            Release();
            return;
        }

        if (SelectedCell != cell && SelectedCell != null)
            SelectedCell.ResetHighlight();
        
        SelectedCell = cell;
        SelectedCell.Highlight();
    }

    public void Release()
    {
        if (SelectedCell != null) SelectedCell.ResetHighlight();
        SelectedCell = null;
    }
}