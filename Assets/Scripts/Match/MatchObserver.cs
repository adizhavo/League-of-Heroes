using UnityEngine;
using System.Collections;

public class MatchObserver : MonoBehaviour 
{
    private static MatchObserver instance;
    public static MatchObserver Instance { get { return instance; } } 

    private int maxEndTower = 3;
    private int playerTowerDestroyed = 0;
    private int opponentTowerDestroyed = 0;

    private enum State
    {
        Observe,
        Ignore
    }
    private State observerState;

	private void Awake()
    {
        instance = this;
        observerState = State.Observe;
	}

    public void Observe(Tower destroyedTower)
    {
        if (observerState.Equals(State.Ignore)) return;
        if (destroyedTower is Nexus) NexusDestroyed(destroyedTower);  
        AddDestroyedTower(destroyedTower);
    }

    private void NexusDestroyed(Tower nexus)
    {
        if (nexus.IsOpponent()) PlayerWins();
        else OpponentWins();
    }

    private void AddDestroyedTower(Tower destroyedTower)
    {
        if (destroyedTower.IsOpponent()) opponentTowerDestroyed ++;
        else playerTowerDestroyed ++;
    }

    public void EndMatchByScore()
    {
        if (playerTowerDestroyed > opponentTowerDestroyed)
            PlayerWins();
        else if (playerTowerDestroyed < opponentTowerDestroyed)
            OpponentWins();
        else
            Tie();
    }

    private void OpponentWins()
    {
        Debug.Log("Opponent wins");
        observerState = State.Ignore;
    }

    private void PlayerWins()
    {
        Debug.Log("Player wins");
        observerState = State.Ignore;
    }

    private void Tie()
    {
        Debug.Log("Tie");
        observerState = State.Ignore;
    }
}
