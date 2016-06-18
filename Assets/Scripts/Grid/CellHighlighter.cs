using UnityEngine;
using System.Collections;

public class CellHighlighter : MonoBehaviour {

    [SerializeField] private Color HiglightColor;
    private Color releaseColor = new Color(1f, 1f, 1f, 0.3f);

    [SerializeField] private SpriteRenderer SpRenderer;

    private void Awake()
    {
        Release();
    }

    public void Highlight()
    {
        if (SpRenderer != null) SpRenderer.color = HiglightColor;
    }

    public void Release()
    {
        if (SpRenderer != null) SpRenderer.color = releaseColor;
    }
}
