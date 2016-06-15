using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour, Deployable, Content, Movable {

    #region Content Implementation
    public bool IsObstacle()
    {
        return true;
    }
    #endregion

    #region Deployable Implementation
    private GridCell nextCell;
    public GridCell MovingCell { get { return nextCell; } }
    // TODO : the non local clone will have inverted y/x axis and will have opposite direction
    // id communication for moving in board and battle
    // the cell will be dispached as properties information
    public void Deploy(IntVector2 deployCellId)
    {
        Deploy(Grid.Instance.GetCell(deployCellId));
    }

    public void Deploy(GridCell deployCell)
    {
        if (deployCell == null)
        {
            Destroy();
            return;
        }

        IntVector2 nextCoodrinate = deployCell.CellId + new IntVector2(0, direction);
        // invert for non local also x Axis 

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
        // TODO : convert into a network destroy
        gameObject.SetActive(false);
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
        return gameObject.activeSelf;
    }
    #endregion

    private float timeCounter;
    private void Update()
    {
        transform.position = Vector3.Lerp(initPos, movePos, timeCounter);
        timeCounter += Time.deltaTime / moveSecLength;

        if(timeCounter > 1f) Deploy(nextCell);

        timeCounter = Mathf.Clamp01(timeCounter);
    }
}
