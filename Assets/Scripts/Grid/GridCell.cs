using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour {

    private IntVector2 cellId;
    public IntVector2 CellId { get { return cellId; } }

    private NullContent nullContent;
    private Content content;
    public Content CellContent
    {
        set { content = (value == null) ? nullContent : value; }
        get { return content == null ? nullContent : content; } } 

    [SerializeField] private CellHighlighter highlighter;
    public bool CanDeploy = false;

    private void Awake()
    {
        nullContent = new NullContent();
        CellContent = nullContent;
    }

    public void Init(int x, int y, Content content = null)
    {
        cellId = new IntVector2(x, y);
        this.CellContent = content;
    }

    public void Highlight()
    {
        highlighter.Highlight();
    }

    public void ResetHighlight()
    {
        highlighter.Release();
    }

    public bool HasObstacle()
    {
        return CellContent.IsObstacle();
    }
}