using UnityEngine;
using System.Collections;

public class NextCard : MonoBehaviour {

    public PlayerDeck currentPlayerDeck;
    public Transform nextCardTr;

    private Card nextCard;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        GetCardFromDeck();
    }

    private void GetCardFromDeck()
    {
        nextCard = currentPlayerDeck.GetCard();
        nextCard.Present();
        nextCard.Position(nextCardTr.position, Vector3.one * 0.5f, true);
    }

    public Card GetNextCard()
    {
        Card card = nextCard;
        GetCardFromDeck();
        return card;
    }

    public bool HasNext()
    {
        return nextCard != null;
    }
}
