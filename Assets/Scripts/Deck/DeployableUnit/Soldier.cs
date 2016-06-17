using UnityEngine;
using System.Collections;
using Photon;

public class Soldier : PunBehaviour, IPunObservable, Deployable, Content, Movable {

    private enum State
    {
        Spawned,
        Deployed,
        Destroyed
    }
    private State SoldierState;

    #region Content Implementation
    public bool IsObstacle()
    {
        return true;
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
        SoldierState = State.Destroyed;
        if (photonView.isMine) PhotonNetwork.Destroy(gameObject);
    }
    #endregion

    #region Movable Implementation
    private Vector3 movePos;
    private Vector3 initPos;

    public void Position(Vector3 initPos, Vector3 movePos, bool snap = false)
    {
        this.movePos = movePos;
        this.initPos = initPos;

        if (snap)
            transform.position = initPos;
    }

    public bool IsDestroyed()
    {
        return SoldierState.Equals(State.Destroyed) || gameObject.activeSelf;
    }
    #endregion

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    #endregion

    private NetworkSoldier soldier;
    [SerializeField] private float moveSecLength;

    [SerializeField] IntVector2 attackArea;
    public IntVector2 AttackArea { get { return attackArea; } }

    private int direction = 1;
    public int Direction { get { return direction; } }

    private void Awake()
    {
        SoldierState = State.Spawned;

        if (photonView.isMine)
            soldier = new LocalSoldier(this);
        else
            soldier = new SyncSoldier(this);
    }

    public void InvertDirection()
    {
        direction *= -1;
    }

    [PunRPC]
    public void MoveCell(int x, int y)
    {
        SoldierState = State.Deployed;
        soldier.MoveCell(x, y);
    }

    private void Update()
    {
        if (SoldierState.Equals(State.Spawned)) return;

        soldier.FrameUpdate(initPos, movePos, moveSecLength);
    }
}
