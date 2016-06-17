using UnityEngine;
using System.Collections;

public class CellHighlighter : MonoBehaviour {

    [SerializeField] private Color HiglightColor;
    [SerializeField] private SpriteRenderer SpRenderer;

    public void Highlight()
    {
        if (SpRenderer != null) SpRenderer.color = HiglightColor;
    }

    public void Release()
    {
        if (SpRenderer != null) SpRenderer.color = Color.white;
    }
}
