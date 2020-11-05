using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    public Text stateText;
    private void Awake()
    {
        Instance = this;
        Connect();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.InRoom)
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRoom()
    {
        if(PhotonNetwork.CountOfRooms <= 0)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = 20,
                IsOpen = true,
                IsVisible = true,
            };
            PhotonNetwork.CreateRoom("피코모", roomOptions);
        }
        else
            PhotonNetwork.JoinRoom("피코모");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnect to master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        stateText.text = "Joined Lobby";
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
    }

}
