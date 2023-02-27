using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image centralImage;
    public TMP_InputField promptInput;
    public string promptTxt;
    [Header("Buttons")] public Button sendRequestBtn;
    public Button img2ImgRequest;
    public Button saveButton;
    public Button clearButton;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        promptInput.onValueChanged.AddListener(delegate {OnPromptValueChange(); });
        sendRequestBtn.onClick.AddListener(OnSendRequestBtnClick);
        img2ImgRequest.onClick.AddListener(OnImg2ImgRequestBtnClick);
        saveButton.onClick.AddListener(OnSaveBtnClick);
        clearButton.onClick.AddListener(OnClearButtonClick);
    }

    public void OnSendRequestBtnClick()
    {
        
    }

    public void OnImg2ImgRequestBtnClick()
    {
        
    }

    public void OnSaveBtnClick()
    {
        
    }

    public void OnClearButtonClick()
    {
        
    }
    private void OnPromptValueChange()
    {
        promptTxt = promptInput.text;
    }
}
