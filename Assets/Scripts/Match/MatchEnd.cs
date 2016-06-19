using UnityEngine;
using System.Collections;

public class MatchEnd : MonoBehaviour {

    [SerializeField] private MatchEndAnimation endAnimations;

    public void DisplayWinner(PhotonPlayer player)
    {
        string message = player == null ? "Is a Tie!" : string.Format("{0} has won!", player.name);
        endAnimations.Display(message);
        endAnimations.AnimateEntry();
    }
}
