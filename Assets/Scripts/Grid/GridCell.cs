using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour {

    private IntVector2 cellId;
    public IntVector2 CellId { get { return cellId; } }

    [SerializeField] private CellHighlighter highlighter;

    public void Init(int x, int y)
    {
        cellId = new IntVector2(x, y);
    }

    public void Highlight()
    {
        highlighter.Highlight();
    }

    public void ResetHighlight()
    {
        highlighter.Release();
    }
}
