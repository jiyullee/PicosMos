using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI Instance;
    private Image _LoadingBG;
    private Image _Slider;
    private Text _LoadingText;

    public enum LoadingType
    {
        Loading,
        DownLoad,
    }

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Instance = this;
        _Slider = transform.Find("Loading_Bar/Loading_bg_back/Loading_bg_bar").GetComponent<Image>();
        _LoadingBG = transform.Find("Background").GetComponent<Image>();
        _LoadingText = transform.Find("Loading_Bar/Text").GetComponent<Text>();
        Progress(0);
    }

    public void Progress(float val, LoadingType loadingType = LoadingType.Loading)
    {
        _Slider.fillAmount = val;
        switch (loadingType)
        {
            case LoadingType.Loading: _LoadingText.text = $"Loading... {val * 100}%"; break;
            case LoadingType.DownLoad: _LoadingText.text = $"Downloading... <color=#ffffff>{val * 100}</color>%"; break;
        }
    }
    
}
