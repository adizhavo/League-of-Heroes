using System;
using Photon;
using UnityEngine;

public class Soldier : PunBehaviour, IPunObservable, Deployable, Content, Movable, Damagable {

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion

    #region Deployable Implementation
    public GridCell CurrentCell { get; set;} 

    public virtual void InitialDeploy(IntVector2 deployCellId)
    {
        soldier.InitialDeploy(deployCellId);
    }

    public virtual void Destroy()
    {
        if (IsDestroyed()) return;
        SoldierState = State.Destroyed;

        if (CurrentCell != null) CurrentCell.CellContent = null;
        unitAnimation.AnimateDestroy(
            () =>
            {
                if (photonView.isMine) PhotonNetwork.Destroy(gameObject);
            }
        );
    }

    public Vector3 GetPosition()
    {
        return soldier.GetPosition();
    }
    #endregion

    #region Content Implementation
    public bool IsObstacle()
    {
        return false;
    }
    #endregion

    #region Movable Implementation
    protected Vector3 movePos;
    protected Vector3 initPos;

    public virtual void Position(Vector3 initPos, Vector3 movePos, bool snap = false)
    {
        this.movePos = movePos;
        this.initPos = initPos;

        if (snap) transform.position = initPos;
    }

    public bool IsDestroyed()
    {
        return SoldierState.Equals(State.Destroyed);
    }
    #endregion

    #region Damagable Implementation
    protected Damagable damagable;

    public float GetHp()
    {
        return damagable.GetHp();
    }

    public void Damage(float value)
    {
        if (IsDestroyed()) return; 
        
        photonView.RPC("DamageNetwork", PhotonTargets.All, value);
    }

    public void FixDamages()
    {
        damagable.FixDamages();
    }

    public bool IsOpponent()
    {
        return damagable.IsOpponent();
    }
    #endregion

    #region Concrete Soldier Implementation
    protected enum State
    {
        Spawned,
        Moving,
        Destroyed
    }
    protected State SoldierState;

    protected NetworkSoldier soldier;
    [SerializeField] protected Attacker attacker;
    [SerializeField] protected SoldierHPBar soldierHp;
    [SerializeField] private MovableAnimation unitAnimation;
    [SerializeField] private float moveSecLength;

    private bool isOpponent = false;

    protected int direction = 1;
    public int Direction { get { return direction; } }

    protected virtual void Awake()
    {
        SoldierState = State.Spawned;

        if (photonView.isMine)
        {
            soldier = new LocalSoldier(this);
            damagable = new LocalDamagable(this, soldierHp);
        }
        else
        {
            soldier = new SyncSoldier(this);
            damagable = new SyncDamagable(this, soldierHp);
            isOpponent = true;
        }
    }

    public void InvertDirection()
    {
        direction *= -1;
    }

    [PunRPC]
    public virtual void MoveCell(int x, int y)
    {
        if (IsDestroyed()) return;

        if (!SoldierState.Equals(State.Moving))
            unitAnimation.AnimateEntry(Vector3.zero, Vector3.one);

        SoldierState = State.Moving;
        soldier.MoveCell(x, y);
    }

    [PunRPC]
    public void DamageNetwork(float value)
    {
        damagable.Damage(value);
    }

    protected virtual void Update()
    {
        if (!SoldierState.Equals(State.Moving) || photonView == null) return;

        if(attacker.CanAttack(CurrentCell, photonView.isMine) && photonView.isMine)
        {
            attacker.Attack();
            return;
        }

        soldier.FrameUpdate(initPos, movePos, moveSecLength);
    }
    #endregion
}