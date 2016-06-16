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
    private GridCell nextCell;
    public GridCell MovingCell { get { return nextCell; } }

    public void Deploy(IntVector2 deployCellId)
    {
        if (photonView.isMine)
            photonView.RPC("MoveCell", PhotonTargets.All, deployCellId.X, deployCellId.Y);
    }

    [PunRPC]
    public void MoveCell(int x, int y)
    {
        SoldierState = State.Deployed;
        IntVector2 cellId = new IntVector2(x, y);

        if (!photonView.isMine)
        {
            int gridXSize = Grid.Instance.XSize - 1;
            int gridYSize = Grid.Instance.YSize - 1;
            IntVector2 tempId = new IntVector2(gridXSize, gridYSize);
            cellId = tempId - cellId;
        }

        Deploy(Grid.Instance.GetCell(cellId));
    }

    public void Deploy(GridCell deployCell)
    {
        if (deployCell == null)
        {
            Destroy();
            return;
        }

        IntVector2 nextCoodrinate = deployCell.CellId + new IntVector2(0, direction);
        if(nextCell != null) nextCell.CellContent = null;

        // Do Some enemy calculation
        // For now we are just moving forward
        nextCell = Grid.Instance.GetCell(nextCoodrinate);
        if (nextCell != null)
        {
            nextCell.CellContent = this;
            Position(deployCell.transform.position, nextCell.transform.position, true);
        }
        else
            Destroy();
    }

    private void Destroy()
    {
        SoldierState = State.Destroyed;
        PhotonNetwork.Destroy(gameObject);
    }
    #endregion

    [SerializeField] private float moveSecLength;
    // direction will be inverted for non local object
    private int direction = 1;

    #region Movable Implementation
    private Vector3 movePos;
    private Vector3 initPos;

    public void Position(Vector3 initPos, Vector3 movePos, bool snap = false)
    {
        this.movePos = movePos;
        this.initPos = initPos;
        timeCounter = 0f;

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

    private float timeCounter = 0f;
    private void Awake()
    {
        SoldierState = State.Spawned;

        if(!photonView.isMine)
            direction = -1;
    }

    private void Update()
    {
        if (SoldierState.Equals(State.Spawned)) return;

        transform.position = Vector3.Lerp(initPos, movePos, timeCounter);
        timeCounter += Time.deltaTime / moveSecLength;

        if(timeCounter > 1f && photonView.isMine)
        {
            Deploy(nextCell);
            if (nextCell != null) photonView.RPC("MoveCell", PhotonTargets.Others, nextCell.CellId.X, nextCell.CellId.Y);
        }

        timeCounter = Mathf.Clamp01(timeCounter);
    }
}
