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

        if (hit2D == null) return;

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
        return selectedCard == null || !selectedCard.IsEnabled() || hit2D == null || hit2D.transform == null;
    }

    private void ReleaseCard()
    {
        selector.Release();

        if (selectedCard == null) return;

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

public class GridSelector
{
    private GridCell currentGridCell;

    public void HighlightPiece(GridCell cell)
    {
        if (cell == null)
        {
            Release();
            return;
        }

        if (currentGridCell != cell && currentGridCell != null)
            currentGridCell.ResetHighlight();
        
        currentGridCell = cell;
        currentGridCell.Highlight();
    }

    public void Release()
    {
        if (currentGridCell != null) currentGridCell.ResetHighlight();
        currentGridCell = null;
    }
}