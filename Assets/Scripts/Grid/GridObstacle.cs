using UnityEngine;
using System.Collections;

public class GridObstacle : MonoBehaviour {

    // Setted in the editor
    [SerializeField] private ObstacleData[] obstacles;

    private void Start()
    {
        for (int i = 0; i < obstacles.Length; i ++)
            obstacles[i].CreateObstacle();
    }
}

[System.Serializable]
public struct ObstacleData
{
    public IntVector2 cellIdToPosition;
    public string ObstacleCodeCall;
  
    public ObstacleData(IntVector2 cellIdToPosition, string ObstacleCodeCall)
    {
        this.cellIdToPosition = cellIdToPosition;
        this.ObstacleCodeCall = ObstacleCodeCall;
    }

    public void CreateObstacle()
    {
        GridCell deployCell = Grid.Instance.GetCell(cellIdToPosition);
        Transform tr = PhotonNetwork.Instantiate(ObstacleCodeCall, deployCell.transform.position, Quaternion.identity, 0).transform;
        Deployable towerComp = tr.GetComponent<Deployable>();
        towerComp.InitialDeploy(deployCell.CellId);
    }
}