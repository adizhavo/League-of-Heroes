using UnityEngine;
using System.Collections;
using Photon;

public class Soldier : PunBehaviour, IPunObservable, Deployable, Content, Movable, Damagable {

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion

    #region Deployable Implementation
    public GridCell MovingCell { get; set;} 

    public void InitialDeploy(IntVector2 deployCellId)
    {
        soldier.InitialDeploy(deployCellId);
    }

    public void Destroy()
    {
        if (IsDestroyed()) return;

        if (MovingCell != null) MovingCell.CellContent = null;

        SoldierState = State.Destroyed;
        if (photonView.isMine) PhotonNetwork.Destroy(gameObject);
    }
    #endregion

    #region Content Implementation
    public bool IsObstacle()
    {
        return !IsDestroyed();
    }
    #endregion

    #region Movable Implementation
    private Vector3 movePos;
    private Vector3 initPos;

    public void Position(Vector3 initPos, Vector3 movePos, bool snap = false)
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

    public void Damage(float value)
    {
        if (IsDestroyed())
            return; 
        
        photonView.RPC("DamageNetwork", PhotonTargets.All, value);
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
    [SerializeField] protected AreaDamage attackingArea;
    [SerializeField] protected Attacker attacker;
    [SerializeField] protected float moveSecLength;

    private int direction = 1;
    public int Direction { get { return direction; } }

    protected virtual void Awake()
    {
        SoldierState = State.Spawned;

        if (photonView.isMine)
        {
            soldier = new LocalSoldier(this);
            damagable = new LocalDamagable(this);
        }
        else
        {
            soldier = new SyncSoldier(this);
            damagable = new SyncDamagable(this);
        }
    }

    public void InvertDirection()
    {
        direction *= -1;
    }

    [PunRPC]
    public void MoveCell(int x, int y)
    {
        if (IsDestroyed()) return;

        SoldierState = State.Moving;
        soldier.MoveCell(x, y);
    }

    [PunRPC]
    public void DamageNetwork(float value)
    {
        damagable.Damage(value);
    }

    private void Update()
    {
        if (!SoldierState.Equals(State.Moving) || photonView == null) return;

        if (photonView.isMine)
        {
            attackingArea.GetCellsInArea(MovingCell);
            if(attackingArea.ContainsTargets())
            {
                if (attacker.CanAttack()) attacker.Attack(attackingArea.TargetContents);
                return;
            }
        }

        soldier.FrameUpdate(initPos, movePos, moveSecLength);
    }
    #endregion
}
