using UnityEngine;
using System.Collections;

public class MatchObserver : MonoBehaviour 
{
    private static MatchObserver instance;
    public static MatchObserver Instance { get { return instance; } } 
    [SerializeField] private SessionTimer MatchTime;
    [SerializeField] private MatchEnd EndSession;

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
        observerState = State.Ignore;
	}

    public void StartMatch()
    {
        if (!IsEnabled())
        {
            observerState = State.Observe;
            MatchTime.StartTimer();
        }
    }

    public bool IsEnabled()
    {
        return observerState.Equals(State.Observe);
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

    private void Update()
    {
        if (observerState.Equals(State.Ignore)) return;
        if (MatchTime.HasFinished()) EndMatchByScore();
        if (!PlayerInRoom.Instance.IsRoomFilled()) WinsPlayerInRoom();
    }

    public void EndMatchByScore()
    {
        if (playerTowerDestroyed < opponentTowerDestroyed) PlayerWins();
        else if (playerTowerDestroyed > opponentTowerDestroyed) OpponentWins();
        else Tie();
    }

    private void WinsPlayerInRoom()
    {
        EndSession.DisplayWinner(PlayerInRoom.Instance.GetPlayerInRoom());
        observerState = State.Ignore;
    }

    private void OpponentWins()
    {
        EndSession.DisplayWinner(PlayerInRoom.Instance.Opponent);
        observerState = State.Ignore;
    }

    private void PlayerWins()
    {
        EndSession.DisplayWinner(PlayerInRoom.Instance.Player);
        observerState = State.Ignore;
    }

    private void Tie()
    {
        EndSession.DisplayWinner(null);
        observerState = State.Ignore;
    }
}
