using UnityEngine;
using System.Collections;

public class CellHighlighter : MonoBehaviour {

    [SerializeField] private Color HiglightColor;
    [SerializeField] private SpriteRenderer SpRenderer;

    public void Highlight()
    {
        SpRenderer.color = HiglightColor;
    }

    public void Release()
    {
        SpRenderer.color = Color.white;
    }
}
