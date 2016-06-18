using UnityEngine;

[System.Serializable]
public class Grid : MonoBehaviour 
{
    private static Grid instance;
    public static Grid Instance { get { return instance; } } 

    public int XSize { get { return GridSize.X; } }
    public int YSize { get { return GridSize.Y; } }

    [SerializeField] private string PrefabGridCall = "GridCell";
    [SerializeField] private IntVector2 GridSize;
    [SerializeField] private float CellSpace;

    private GridCell[,] cells;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        cells = new GridCell[GridSize.X, GridSize.Y];

        for (int y = 0; y < GridSize.Y; y ++)
            for (int x = 0; x < GridSize.X; x ++)
            {
                GameObject cellInstance = ObjectFactory.Instance.CreateObjectCode(PrefabGridCall);
                cellInstance.transform.position = transform.position + new Vector3((x - GridSize.X/2f) * CellSpace, (y - GridSize.Y/2f) * CellSpace, 0f);

                GridCell cell = cellInstance.GetComponent<GridCell>();
                cells[x, y] = cell != null ? cell : cellInstance.AddComponent<GridCell>();
                cells[x, y].Init(x, y);
            }
    }

    public bool IsCellInGrid(IntVector2 id)
    {
        return IsCellInGrid(id.X, id.Y);
    }

    public bool IsCellInGrid(int x, int y)
    {
        return y < GridSize.Y && x < GridSize.X && y >= 0 && x >= 0 ;
    }

    public GridCell GetCell(IntVector2 id)
    {
        return GetCell(id.X, id.Y);
    }

    public GridCell GetCell(int x, int y)
    {
        return IsCellInGrid(x, y) ? cells[x, y] : null;
    }

    public void FillWithSimpleObstacles(IntVector2 cellPos, IntVector2 areaSize)
    {
        SetCellContent(cellPos, areaSize, new SimpleObstacle());
    }

    public void ReleaseCells(IntVector2 cellPos, IntVector2 areaSize)
    {
        SetCellContent(cellPos, areaSize, null);
    }

    private void SetCellContent(IntVector2 cellPos, IntVector2 areaSize, Content content)
    {
        for (int x = cellPos.X - areaSize.X; x <= cellPos.X + areaSize.X; x++)
            for (int y = cellPos.Y - areaSize.Y; y <= cellPos.Y + areaSize.Y; y++)
                if (Grid.Instance.IsCellInGrid(x, y) && 
                    (x != cellPos.X || y != cellPos.Y))
                {
                    GridCell cell = Grid.Instance.GetCell(x, y);
                    cell.CellContent = content;
                }
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

    public override string ToString()
    {
        return string.Format("[IntVector2] X: {0}, Y: {1}", X, Y);
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X + b.X, a.Y + b.Y);
    }

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X - b.X, a.Y - b.Y);
    }
}