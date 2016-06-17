using UnityEngine;
using System.Collections.Generic;

public class AreaDamage : MonoBehaviour {
    
    [SerializeField] IntVector2 attackArea;
    public IntVector2 AttackArea { get { return attackArea; } }

    private List<GridCell> areaCells = new List<GridCell>();
    private List<Damagable> damagableContents = new List<Damagable>();
    public List<Damagable> TargetContents { get { return damagableContents; } }

    public void GetCellsInArea(GridCell currentCell)
    {
        if (currentCell == null)
            return;

        ClearAreaCells();
        PopulateAreaCells(currentCell);
    }

    private void ClearAreaCells()
    {
        for (int i = 0; i < areaCells.Count; i++)
            areaCells[i].ResetHighlight();
        areaCells.Clear();
    }

    private void PopulateAreaCells(GridCell currentCell)
    {
        IntVector2 cellPos = currentCell.CellId;

        for (int x = cellPos.X - attackArea.X; x <= cellPos.X + attackArea.X; x++)
            for (int y = cellPos.Y - attackArea.Y; y <= cellPos.Y + attackArea.Y; y++)
                if (Grid.Instance.IsCellInGrid(x, y) && 
                    (x != cellPos.X || y != cellPos.Y))
                {
                    GridCell cell = Grid.Instance.GetCell(x, y);
                    cell.Highlight();
                    areaCells.Add(cell);
                }
    }

    private void OnDestroy()
    {
        ClearAreaCells();
    }

    public bool ContainsTargets()
    {
        if (areaCells == null || areaCells.Count == 0)
            return false;

        damagableContents.Clear();

        for (int i = 0; i < areaCells.Count; i++)
        {
            //if (areaCells[i].HasObstacle())
            {
                Damagable d = areaCells[i].CellContent as Damagable;
                if (d != null && d.IsOpponent()) damagableContents.Add(d);
            }
        }

        return damagableContents.Count > 0;
    }
}
