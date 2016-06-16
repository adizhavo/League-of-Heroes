using UnityEngine;
using System.Collections;

public class CardBar : MonoBehaviour 
{
    [SerializeField] private int cardToHold;
    [SerializeField] private NextCard nextCard;
    [SerializeField] private CardBarPositioner positioner;

    private Card[] cards;

    private void Awake()
    {
        cards = new PlayerCard[cardToHold];
    }

    private void LateUpdate()
    {
        int emptySlot = EmptySpace();

        if (emptySlot != -1 && nextCard.HasNext())
        {
            cards[emptySlot] = nextCard.GetNextCard();
            cards[emptySlot].Enable();
            cards[emptySlot].Position(positioner.GetPosition(emptySlot), Vector3.one);
        }
    }

    private int EmptySpace()
    {
        for (int c = 0; c < cards.Length; c ++)
            if (cards[c] == null || cards[c].IsDestroyed()) return c;

        return -1;
    } 
}
