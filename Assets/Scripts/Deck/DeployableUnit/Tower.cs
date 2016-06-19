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

    #region Deployable Implementation
    public override void InitialDeploy(IntVector2 deployCellId)
    {
        base.InitialDeploy(deployCellId);
        Grid.Instance.FillWithSimpleObstacles(CurrentCell.CellId, StopAreaSize);
    }

    public override void Destroy()
    {
        if (IsDestroyed()) return;
        CameraShake.Instance.DoShake(ShakeType.Medium);
        Grid.Instance.ReleaseCells(CurrentCell.CellId, StopAreaSize);
        base.Destroy();

        if (photonView.isMine) photonView.RPC("DestroyTower", PhotonTargets.All);
    }
    #endregion

    #region Concrete Tower Implementation
    [PunRPC]
    protected virtual void DestroyTower()
    {
        MatchObserver.Instance.Observe(this);
    }

    [SerializeField] protected IntVector2 StopAreaSize;

    protected override void Awake()
    {
        base.Awake();
        direction = 0;
    }

    protected override void Update()
    {
        if(attacker.CanAttack(CurrentCell, photonView.isMine) && photonView.isMine)
            attacker.Attack();
    }
    #endregion
}