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

    [PunRPC]
    public void MoveCell(int x, int y)
    {
        SoldierState = State.Spawned;
        soldier.MoveCell(x, y);
    }

    public void Deploy(GridCell deployCell)
    {
        soldier.Deploy(deployCell);
    }

    public void Destroy()
    {
        SoldierState = State.Destroyed;
        PhotonNetwork.Destroy(gameObject);
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

    private void Update()
    {
        soldier.FrameUpdate(initPos, movePos, moveSecLength);
    }
}
