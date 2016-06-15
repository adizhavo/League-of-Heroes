using UnityEngine;
using System.Collections;

public class PlayerCardHighlighter : MonoBehaviour {

    [SerializeField] private PlayerCard card;
    [SerializeField] private SpriteRenderer SpRenderer;
    [SerializeField] private Color DisableColor;

    private void Update()
    {
        Color colorToApply = card.IsEnabled() ? Color.white : DisableColor;
        SpRenderer.color = colorToApply;
    }
}
