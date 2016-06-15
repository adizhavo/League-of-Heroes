using UnityEngine;
using System.Collections;

public class NextCard : MonoBehaviour {

    public PlayerDeck currentPlayerDeck;
    public Transform nextCardTr;

    private Movable nextCard;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        GetCardFromDeck();
    }

    private void GetCardFromDeck()
    {
        nextCard = currentPlayerDeck.GetCard();
        nextCard.Position(nextCardTr.position, Vector3.one * 0.5f, true);
    }

    public Movable GetNextCard()
    {
        Movable card = nextCard;
        GetCardFromDeck();
        return card;
    }

    public bool HasNext()
    {
        return nextCard != null;
    }
}
