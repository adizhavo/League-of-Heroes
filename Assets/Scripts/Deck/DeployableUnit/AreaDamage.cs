using UnityEngine;
using System.Collections.Generic;

public class AreaDamage : MonoBehaviour {
    
    [SerializeField] IntVector2 attackArea;
    public IntVector2 AttackArea { get { return attackArea; } }

    private List<GridCell> areaCells = new List<GridCell>();

    public void ReadArea(GridCell currentCell, bool highlight)
    {
        if (currentCell == null)
            return;

        ClearAreaCells(highlight);
        PopulateAreaCells(currentCell, highlight);
    }

    private void ClearAreaCells(bool highlight)
    {
        if (highlight)
            for (int i = 0; i < areaCells.Count; i++)
                areaCells[i].ResetHighlight();
        
        areaCells.Clear();
    }

    private void PopulateAreaCells(GridCell currentCell, bool highLight)
    {
        IntVector2 cellPos = currentCell.CellId;

        for (int x = cellPos.X - attackArea.X; x <= cellPos.X + attackArea.X; x++)
            for (int y = cellPos.Y - attackArea.Y; y <= cellPos.Y + attackArea.Y; y++)
                if (Grid.Instance.IsCellInGrid(x, y) && 
                    (x != cellPos.X || y != cellPos.Y))
                {
                    GridCell cell = Grid.Instance.GetCell(x, y);
                    if (highLight) cell.Highlight();
                    areaCells.Add(cell);
                }
    }

    private void OnDestroy()
    {
        ClearAreaCells(true);
    }

    public bool HasDamagable()
    {
        for (int i = 0; i < areaCells.Count; i++)
        {
            Damagable d = areaCells[i].CellContent as Damagable;
            if (d != null && d.IsOpponent()) return true;
        }
        return false;
    }

    public List<Damagable> GetDamagables()
    {
        List<Damagable> damagableContents = new List<Damagable>();

        if (areaCells == null || areaCells.Count == 0)
            return damagableContents;
        
        for (int i = 0; i < areaCells.Count; i++)
        {
            Damagable d = areaCells[i].CellContent as Damagable;
            if (d != null && d.IsOpponent()) damagableContents.Add(d);
        }

        return damagableContents;
    }
}