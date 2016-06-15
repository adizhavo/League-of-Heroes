using UnityEngine;
using System.Collections;

public class PlayableCard : MonoBehaviour 
{
    [SerializeField] private int cardToHold;
    [SerializeField] private NextCard nextCard;
    [SerializeField] private CardPositioner positioner;

    private Movable[] cards;

    private void Awake()
    {
        cards = new PlayCard[cardToHold];
    }

    private void LateUpdate()
    {
        int emptySlot = EmptySpace();

        if (emptySlot != -1 && nextCard.HasNext())
        {
            cards[emptySlot] = nextCard.GetNextCard();
            cards[emptySlot].Position(positioner.GetPosition(emptySlot), Vector3.one);
        }
    }

    private int EmptySpace()
    {
        for (int c = 0; c < cards.Length; c ++)
            if (cards[c] == null) return c;

        return -1;
    } 
}
