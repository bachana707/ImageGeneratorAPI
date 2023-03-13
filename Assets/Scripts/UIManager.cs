using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SFB;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Image centralImage;
    [Header("Buttons")] public Button sendRequestBtn;
    public Button img2ImgRequest;
    public Button saveButton;
    public Button clearButton;
    public GameObject loading;

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sendRequestBtn.onClick.AddListener(OnSendRequestBtnClick);
        img2ImgRequest.onClick.AddListener(OnImg2ImgRequestBtnClick);
        saveButton.onClick.AddListener(OnSaveBtnClick);
        clearButton.onClick.AddListener(OnClearButtonClick);
    }

    public void OnSendRequestBtnClick()
    {
        AllButtonInteractable(false);
        StartLoading();
        ApiManagerTxt2Img.Instance.SendRequestTxt2Img(OnGetGeneratedImage);
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
        Texture2D texture = (Texture2D)centralImage.sprite.texture;
        byte[] bytes = texture.EncodeToPNG();
        string path =
            StandaloneFileBrowser.SaveFilePanel("Save file", "", ApiManagerTxt2Img.Instance.promptInput.text, "PNG");
        if (path.Length != 0)
        {
            File.WriteAllBytes(path, bytes);
            Debug.Log("File saved at: " + path);
        }
    }

    public void OnClearButtonClick()
    {
    }
}