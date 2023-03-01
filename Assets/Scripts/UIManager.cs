using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;

public class UIManager : Singleton<UIManager>
{
    public Image centralImage;
    public TMP_InputField promptInput;
    public string promptTxt;
    [Header("Buttons")] public Button sendRequestBtn;
    public Button img2ImgRequest;
    public Button saveButton;
    public Button clearButton;
    public GameObject loading;
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
        if (promptTxt.Length == 0)
        {
            return;
        }
        AllButtonInteractable(false);
        StartLoading();
        ApiManager.Instance.SendRequestTxt2Img(promptTxt,OnGetGeneratedImage);
        
    }

    
    private void OnGetGeneratedImage()
    {
        StopLoading();
        AllButtonInteractable(true);
    }
    public void AllButtonInteractable(bool active)
    {
        //todo
        sendRequestBtn.interactable = active;
        promptInput.interactable = active;
        img2ImgRequest.interactable = active;
        saveButton.interactable = active;
    }

    public void StartLoading()
    {
        loading.SetActive(true);
        loading.transform.DORotate(new Vector3(0, 0, -360f), 1f, RotateMode.FastBeyond360).SetLoops(-1);
    }

    public void StopLoading()
    {
        loading.transform.DOKill();
        loading.transform.rotation = quaternion.identity;
        loading.SetActive(false);
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
