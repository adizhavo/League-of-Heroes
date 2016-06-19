using UnityEngine;
using System.Collections;

public class PlayerInRoom
{
    private PhotonPlayer player;
    public PhotonPlayer Player 
    { 
        set { if (value != null)
            player = value; } 
        get { return player; }
    }

    private PhotonPlayer opponent;
    public PhotonPlayer Opponent 
    { 
        set { if (value != null) 
            opponent = value; } 
        get { return opponent; }
    }

    private static PlayerInRoom instance;
    public static PlayerInRoom Instance 
    {
        get { if (instance == null) instance = new PlayerInRoom();
            return instance; } 
    }

    public bool IsRoomFilled()
    {
        return opponent != null && player != null;
    }

    public PhotonPlayer GetMissedPlayer()
    {
        return opponent == null ? opponent : player;
    }

    public PhotonPlayer GetPlayerInRoom()
    {
        if (IsRoomFilled())
        {
            Debug.Log("The room has two players, will return local player");
            return player;
        }

        return opponent != null ? opponent : player;
    }
}
