using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    Button btn_GameStart;
    
    private void Awake()
    {
        btn_GameStart = transform.Find("GameStart/StartButton").GetComponent<Button>();

        btn_GameStart.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        NetworkManager.Instance.JoinRoom();
        
    }
}
