using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_InputField promptInput;
    public string promptTxt;
    [Header("Buttons")] public Button sendRequestBtn;
    public Button img2ImgRequest;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        promptInput.onValueChanged.AddListener(delegate {OnPromptValueChange(); });
        sendRequestBtn.onClick.AddListener(OnSendRequestBtnClick);
        img2ImgRequest.onClick.AddListener(OnImg2ImgRequestBtnClick);
    }

    public void OnSendRequestBtnClick()
    {
        
    }

    public void OnImg2ImgRequestBtnClick()
    {
        
    }
    private void OnPromptValueChange()
    {
        promptTxt = promptInput.text;
    }
}
