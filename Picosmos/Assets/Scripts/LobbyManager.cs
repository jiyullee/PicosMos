using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using ExitGames.Client.Photon;
public class LobbyManager : PunTeams
{
    public static LobbyManager Instance;
    public Text stateText;
    public Text IDText;
    public const int MAX_TEAM_PICO = 5;
    public const int MAX_TEAM_MO = 5;
    public const string ROOM_NAME = "Test_4";
    public static string UserId;

    public RecoverDisconnect recoverDisconnect;
    private AuthenticationValues authValues;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey("UserId"))
        {
  
            UserId = PlayerPrefs.GetString("UserId");

            authValues = new AuthenticationValues();
            authValues.AuthType = CustomAuthenticationType.Custom;
            authValues.UserId = UserId;
            Debug.Log(UserId);
            IDText.text = UserId;
            authValues.AddAuthParameter("user", UserId);
            PhotonNetwork.AuthValues = authValues;
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0";
        
        recoverDisconnect = new RecoverDisconnect();
        Connect();

    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.CountOfRooms <= 0)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = 20,
                IsOpen = true,
                IsVisible = true,
                EmptyRoomTtl = 60000,
                PlayerTtl = 60000,
                PublishUserId = true,
            };
            PhotonNetwork.CreateRoom(ROOM_NAME, roomOptions);
        }
        else
            PhotonNetwork.JoinRoom(ROOM_NAME);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnect to master");
        recoverDisconnect.OnConnectedToMaster();

        PlayerPrefs.SetString("UserId", PhotonNetwork.AuthValues.UserId);

        PhotonNetwork.JoinLobby();
        LobbyUI.Instance.SetViewEnterSeasonBtn(true);

      
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        stateText.text = "Joined Lobby";

        PhotonNetwork.Reconnect();
        if (PhotonNetwork.RejoinRoom(ROOM_NAME) && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        stateText.text = "Created Room";
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("Joined Room");
        stateText.text = "Joined Room";
        recoverDisconnect.OnJoinedRoom();

        LobbyUI.Instance.SetViewEnterSeasonBtn(false);

        SetTeam();
         
        UpdateTeams();

        if (PhotonNetwork.IsMasterClient)
            LobbyUI.Instance.SetViewStartGameBtn(true);

        LobbyUI.Instance.ShowTeamState();

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        stateText.text = message;
        recoverDisconnect.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnLeftRoom()
    {
        recoverDisconnect.OnLeftRoom();
    }

    private void SetTeam()
    {
        if (IsFull())
        {
            if (PlayersPerTeam[Team.PICO].Count == MAX_TEAM_PICO)
            {
                TeamExtensions.SetTeam(PhotonNetwork.LocalPlayer, Team.MO);
            }
            else
            {
                TeamExtensions.SetTeam(PhotonNetwork.LocalPlayer, Team.PICO);
            }
        }
        else
        {
            int randInt = Random.Range(0, 100);

            if (randInt < 50)
            {
                TeamExtensions.SetTeam(PhotonNetwork.LocalPlayer, Team.PICO);
            }
            else
            {
                TeamExtensions.SetTeam(PhotonNetwork.LocalPlayer, Team.MO);
            }
        }
         
        
    }

    private bool IsFull()
    {
        return PlayersPerTeam[Team.PICO].Count == MAX_TEAM_PICO || PlayersPerTeam[Team.PICO].Count == MAX_TEAM_MO;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        recoverDisconnect.OnDisconnected(cause);
        
    }

}
