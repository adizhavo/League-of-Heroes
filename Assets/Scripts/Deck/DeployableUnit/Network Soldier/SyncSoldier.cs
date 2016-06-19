using UnityEngine;

public class SyncSoldier : NetworkSoldier
{
    public SyncSoldier(Soldier soldier) : base (soldier)
    {
        this.soldier.InvertDirection();
    }

    public override void InitialDeploy(IntVector2 deployCellId)
    {
    }

    public override void MoveCell(int x, int y)
    {
        int gridXSize = Grid.Instance.XSize - 1;
        int gridYSize = Grid.Instance.YSize - 1;
        IntVector2 cellId = new IntVector2(gridXSize - x, gridYSize - y);
        MoveTo(Grid.Instance.GetCell(cellId));
    }
}