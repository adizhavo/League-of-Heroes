using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeck", menuName = "Player Deck", order = 1)]
public class PlayerDeck : ScriptableObject
{
    [SerializeField] private PlayCard[] PlayerCards;   

    public PlayCard GetCard()
    {
        int index = Random.Range(0, PlayerCards.Length);
        GameObject pCard = GameObject.Instantiate(PlayerCards[index].gameObject) as GameObject;
        return pCard.GetComponent<PlayCard>();
    }
}
