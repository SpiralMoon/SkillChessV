using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;

public class TitlePage : MonoBehaviour, IPage
{
    public Button BtnStart;

    public Button BtnSetting;

    public Button BtnExit;

    private void Awake()
    {
        BtnStart.onClick.AddListener(OnClickBtnStart);
        BtnSetting.onClick.AddListener(OnClickBtnSetting);
        BtnExit.onClick.AddListener(OnClickBtnExit);

        SetTextSize();
        SetTextValue();
    }

    private void OnClickBtnStart()
    {
        NextPage("LoginPage");
    }

    private void OnClickBtnSetting()
    {
        NextPage("SettingPage");
    }

    private void OnClickBtnExit()
    {
        // TODO : 임시코드
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void SetTextSize()
    {

    }

    public void SetTextValue()
    {

    }

    public void NextPage(string pageName)
    {
        SceneManager.LoadSceneAsync(pageName);
    }
}
