using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingVisual : MonoBehaviour 
{
    [SerializeField] private TextMesh valueText;
    [SerializeField] private TextMesh shadowText;
    [SerializeField] private Renderer valueRend;
    [SerializeField] private Renderer shadowRend;

    private void Awake()
    {
        valueRend.sortingLayerName = "FloatingText";
        shadowRend.sortingLayerName = "FloatingText";
    }

    public void SetText(string text, Color textColor)
    {
        SetText(text);
        valueText.color = textColor;
    }

    public void SetText(string text)
    {
        valueText.text = text;
        shadowText.text = text;
    }
}
