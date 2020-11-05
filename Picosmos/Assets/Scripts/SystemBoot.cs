using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemBoot : MonoBehaviour
{

    /// <summary>
    /// 씬 로드 완료시에 호출되는 콜백 대리자
    /// </summary>
    public Action LevelWasLoaded;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        Screen.SetResolution(720, 1280, false);
        Initialize();
    }

    private void Initialize()
    {
        gameObject.AddComponent<GameManager>();
    
        SceneLoader.LoadScene(2);
    }

    
}
