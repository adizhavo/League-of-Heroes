using UnityEngine;
using System.Collections;
using Photon;

public class Tower : Soldier {

    #region Movable Implementation
    public override void Position(Vector3 initPos, Vector3 movePos, bool snap = false)
    {
        transform.position = initPos;
    }
    #endregion

    [SerializeField] protected IntVector2 StopAreaSize;

    protected override void Awake()
    {
        base.Awake();
        direction = 0;
    }

    public override void InitialDeploy(IntVector2 deployCellId)
    {
        base.InitialDeploy(deployCellId);
        Grid.Instance.FillWithSimpleObstacles(CurrentCell.CellId, StopAreaSize);
    }

    protected override void Update()
    {
        if(attacker.CanAttack(CurrentCell, photonView.isMine) && photonView.isMine)
            attacker.Attack();
    }
}