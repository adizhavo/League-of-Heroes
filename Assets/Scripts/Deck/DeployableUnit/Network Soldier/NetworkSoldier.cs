using UnityEngine;

public abstract class NetworkSoldier : Deployable
{
    public GridCell CurrentCell { get { return soldier.CurrentCell; } }
    protected Soldier soldier;

    protected float timeCounter = 0f;

    public NetworkSoldier(Soldier soldier)
    {
        this.soldier = soldier;
    }

    protected virtual void MoveTo(GridCell currentCell)
    {
        if (currentCell == null)
        {
            Destroy();
            return;
        }

        IntVector2 nextCoodrinate = currentCell.CellId + new IntVector2(0, soldier.Direction);
        GridCell nextCell = Grid.Instance.GetCell(nextCoodrinate);

        if (nextCell == null)
        {
            Destroy();
            return;
        }

        if (!nextCell.HasObstacle())
        {
            if (soldier.CurrentCell != null)
                soldier.CurrentCell.CellContent = null;

            soldier.CurrentCell = nextCell;
            nextCell.CellContent = soldier;
            soldier.Position(currentCell.transform.position, soldier.CurrentCell.transform.position, true);
            timeCounter = 0f;
        }
    }

    public void Destroy()
    {
        soldier.Destroy();
    }

    public virtual void FrameUpdate(Vector3 initPos, Vector3 movePos, float moveSecLength)
    {
        soldier.transform.position = Vector3.Lerp(initPos, movePos, timeCounter);
        timeCounter += Time.deltaTime / moveSecLength;
        timeCounter = Mathf.Clamp(timeCounter, 0f, 1f + Mathf.Epsilon );
    }

    public Vector3 GetPosition()
    {
        return soldier.transform.position;
    }

    public abstract void InitialDeploy(IntVector2 deployCellId);
    public abstract void MoveCell(int x, int y);
}