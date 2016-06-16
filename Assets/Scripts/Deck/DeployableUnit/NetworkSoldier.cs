using UnityEngine;

public abstract class NetworkSoldier : Deployable
{
    public GridCell MovingCell { get { return soldier.MovingCell; } }
    protected Soldier soldier;

    public NetworkSoldier(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public virtual void MoveTo(GridCell deployCell)
    {
        if (deployCell == null)
        {
            soldier.Destroy();
            return;
        }

        IntVector2 nextCoodrinate = deployCell.CellId + new IntVector2(0, soldier.Direction);
        if (soldier.MovingCell != null)
            soldier.MovingCell.CellContent = null;

        // Do Some enemy calculation
        // For now we are just moving forward
        soldier.MovingCell = Grid.Instance.GetCell(nextCoodrinate);
        if (soldier.MovingCell != null)
        {
            soldier.MovingCell.CellContent = soldier;
            soldier.Position(deployCell.transform.position, soldier.MovingCell.transform.position, true);
        }
        else
            soldier.Destroy();
    }

    public abstract void InitialDeploy(IntVector2 deployCellId);
    public abstract void MoveCell(int x, int y);
    public abstract void FrameUpdate(Vector3 initPos, Vector3 movePos, float moveSecLength);
}

public class LocalSoldier : NetworkSoldier
{
    private float timeCounter = 0f;

    public LocalSoldier(Soldier soldier) : base (soldier)
    {
    }

    public override void InitialDeploy(IntVector2 deployCellId)
    {
        soldier.photonView.RPC("MoveCell", PhotonTargets.All, deployCellId.X, deployCellId.Y);
    }

    public override void MoveCell(int x, int y)
    {
        IntVector2 cellId = new IntVector2(x, y);
        MoveTo(Grid.Instance.GetCell(cellId));
    }

    public override void FrameUpdate(Vector3 initPos, Vector3 movePos, float moveSecLength)
    {
        soldier.transform.position = Vector3.Lerp(initPos, movePos, timeCounter);
        timeCounter += Time.deltaTime / moveSecLength;

        if (timeCounter > 1f)
        {
            soldier.MoveTo(soldier.MovingCell);
            if (soldier.MovingCell != null)
                soldier.photonView.RPC("MoveCell", PhotonTargets.Others, soldier.MovingCell.CellId.X, soldier.MovingCell.CellId.Y);
        }

        timeCounter = Mathf.Clamp01(timeCounter);
    }

    public override void MoveTo(GridCell deployCell)
    {
        base.MoveTo(deployCell);
        timeCounter = 0f;
    }
}


public class SyncSoldier : NetworkSoldier
{
    private float timeCounter = 0f;

    public SyncSoldier(Soldier soldier) : base (soldier)
    {
        this.soldier.InvertDirection();
    }

    public override void InitialDeploy(IntVector2 deployCellId)
    {
    }

    public override void MoveTo(GridCell deployCell)
    {
        base.MoveTo(deployCell);
        timeCounter = 0f;
    }

    public override void MoveCell(int x, int y)
    {
        int gridXSize = Grid.Instance.XSize - 1;
        int gridYSize = Grid.Instance.YSize - 1;
        IntVector2 cellId = new IntVector2(x, y);
        cellId = new IntVector2(gridXSize, gridYSize) - cellId;
        MoveTo(Grid.Instance.GetCell(cellId));
    }

    public override void FrameUpdate(Vector3 initPos, Vector3 movePos, float moveSecLength)
    {
        soldier.transform.position = Vector3.Lerp(initPos, movePos, timeCounter);
        timeCounter += Time.deltaTime / moveSecLength;
        if (timeCounter > 1f) soldier.MoveTo(soldier.MovingCell);
        timeCounter = Mathf.Clamp01(timeCounter);
    }
}