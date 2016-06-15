using UnityEngine;

[System.Serializable]
public class Grid : MonoBehaviour 
{
    private static Grid instance;
    public static Grid Instance { get { return instance; } } 

    [SerializeField] private string PrefabGridCall = "GridCell";
    [SerializeField] private IntVector2 GridSize;
    [SerializeField] private float CellSpace;

	private void Start () 
    {
        instance = this;
        CreateGrid();
	}

    private void CreateGrid()
    {
        for (int x = 0; x < GridSize.X; x ++)
            for (int y = 0; y < GridSize.Y; y ++)
            {
                GameObject cellInstance = ObjectFactory.Instance.CreateObjectCode(PrefabGridCall);
                cellInstance.transform.position = transform.position + new Vector3((x - GridSize.X/2f) * CellSpace, (y - GridSize.Y/2f) * CellSpace, 0f);
            }
    }

    public bool IsCellInGrid(int x, int y)
    {
        return y < GridSize.Y && x < GridSize.X && y >= 0 && x >= 0 ;
    }
}

[System.Serializable]
public struct IntVector2
{
    public int X;
    public int Y;

    public IntVector2(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X + b.X, a.Y + b.Y);
    }
}