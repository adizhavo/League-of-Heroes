using UnityEngine;
using System.Collections;

public class PlayerCardUI : MonoBehaviour {

    [SerializeField] private PlayerCard card;
    [SerializeField] private SpriteRenderer SpRenderer;
    [SerializeField] private TextMesh manaCost;

    private void OnEnable()
    {
        SpRenderer.sprite = card.CardSprite;
        manaCost.text = card.ManaCost.ToString();
    }
}
