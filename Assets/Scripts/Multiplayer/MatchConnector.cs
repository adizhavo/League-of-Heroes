using UnityEngine;
using Photon;
using System.Collections;
using PHashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public enum RoomJoinType
{
    Create,
    Join
}

public class MatchConnector : PunBehaviour
{
    public GameObject  NameSelection;
    public GameObject Loading;

    public Text statusUI;
    public InputField nameInput;

    private RoomJoinType playerRoomJoinType;
    private RoomOptions options;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (!PlayerPrefs.HasKey("playername"))
            PlayerPrefs.SetString("playername", "Player" + Random.Range(1000000, 10000000));

        nameInput.text = PlayerPrefs.GetString("playername");
    }

    public void MatchMake()
    {
        playerRoomJoinType = RoomJoinType.Join;
        PhotonNetwork.ConnectUsingSettings("1.0");
        SetStatus("Connecting to master server...");
    }

    public void StartMatch()
    {
        PhotonNetwork.LoadLevel("Battle");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.autoCleanUpPlayerObjects = true;

        PhotonNetwork.playerName = PlayerPrefs.GetString("playername");
        PhotonNetwork.JoinRandomRoom();

        SetStatus("Waiting for other player...");
    }

    public override void OnJoinedRoom()
    {
        SetPlayers();
        string stateMessage = playerRoomJoinType.Equals(RoomJoinType.Create) ? "Waiting for other player..." : "Player connected, starting match...";
        SetStatus(stateMessage);
    }

    public override void OnCreatedRoom()
    {
        playerRoomJoinType = RoomJoinType.Create;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        options = new RoomOptions();
        options.maxPlayers = 2;
        PhotonNetwork.CreateRoom("", options, null);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.playerList.Length >= options.maxPlayers) 
        {
            SetPlayers();
            StartMatch();
        }

        SetStatus("Player connected, starting match...");
    }

    public void SetPlayers()
    {
        if (PhotonNetwork.playerList == null || PhotonNetwork.room.playerCount == 0) return;

        for (int i = 0; i < PhotonNetwork.room.playerCount; i++)
            if (PhotonNetwork.playerList[i].isLocal)
                PlayerInRoom.Instance.Player = PhotonNetwork.playerList[i];
            else
                PlayerInRoom.Instance.Opponent = PhotonNetwork.playerList[i];
    }

    private void SetStatus(string text)
    {
        statusUI.text = text.ToUpper();
    }

    public void OnChangePlayerName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            PlayerPrefs.SetString("playername", name.ToUpper());
    }


    public void OnClickStart()
    {
        NameSelection.SetActive(false);
        Loading.SetActive(true);
        MatchMake();
    }
}