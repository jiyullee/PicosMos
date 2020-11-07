using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviourPunCallbacks
{
    private GameObject tempObject;

    private void Awake()
    {
        tempObject = Resources.Load<GameObject>("Prefabs/GameObject");
    }
    private void Start()
    {
        PhotonNetwork.Instantiate("Prefabs/GameObject", Vector3.zero, Quaternion.identity);
    }
}
