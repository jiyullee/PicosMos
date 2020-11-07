using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class LobbyUI : PunTeams
{
    public static LobbyUI Instance;

    public Button btn_GameStart;
    public Button btn_EnterSeason;

    public GameObject RoomState;
    public Text text_pico_count;
    public Text text_mo_count;

    private void Awake()
    {
        Instance = this;

        btn_GameStart = transform.Find("StartButton").GetComponent<Button>();
        btn_EnterSeason = transform.Find("EnterSeason").GetComponent<Button>();
        RoomState = transform.Find("RoomState").gameObject;
        text_pico_count = transform.Find("RoomState/Team_PICO_Text/Count").GetComponent<Text>();
        text_mo_count = transform.Find("RoomState/Team_MO_Text/Count").GetComponent<Text>();
        
        btn_GameStart.onClick.AddListener(StartGame);
        btn_EnterSeason.onClick.AddListener(EnterSeason);

        btn_GameStart.gameObject.SetActive(false);
        btn_EnterSeason.gameObject.SetActive(false);
        RoomState.SetActive(false);

    }

    public void SetViewEnterSeasonBtn(bool state)
    {
        btn_EnterSeason.gameObject.SetActive(state);
    }

    public void SetViewStartGameBtn(bool state)
    {
        btn_GameStart.gameObject.SetActive(state);
    }

    public void ShowTeamState()
    {
        photonView.RPC("RPC_ShowTeamState", RpcTarget.All, PlayersPerTeam[Team.PICO].Count, PlayersPerTeam[Team.MO].Count);      
    }

    [PunRPC]
    private void RPC_ShowTeamState(int team_pico_count, int team_mo_count)
    {
        RoomState.SetActive(true);
        text_pico_count.text = team_pico_count.ToString();
        text_mo_count.text = team_mo_count.ToString();
    }

    private void EnterSeason()
    {
        SetViewEnterSeasonBtn(false);
        LobbyManager.Instance.JoinRoom();   
    }
    private void StartGame()
    {
        // 게임 씬 입장
       
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Load Game Scene");
            PhotonNetwork.LoadLevel("Game");
           
        }
        
    }
}
