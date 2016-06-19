using UnityEngine;

public class LocalSoldier : NetworkSoldier
{
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
        base.FrameUpdate(initPos, movePos, moveSecLength);
        if (timeCounter >= 1f - Mathf.Epsilon && soldier.CurrentCell != null)
            soldier.photonView.RPC("MoveCell", PhotonTargets.All, soldier.CurrentCell.CellId.X, soldier.CurrentCell.CellId.Y);
    }
}