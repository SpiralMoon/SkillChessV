using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Model;
using Assets.Support;

public class IntroPage : MonoBehaviour, IPage
{
    private Setting _setting;

    public Button BtnUsa;

    public Button BtnKorea;

    public Button BtnJapan;

    private void Awake()
    {
        _setting = Setting.GetInstance();

        if (_setting.Country != Country.NONE)
        {
            NextPage("TitlePage");
        }

        BtnUsa.onClick.AddListener(this.OnClickBtnUsa);
        BtnKorea.onClick.AddListener(this.OnClickBtnKorea);
        BtnJapan.onClick.AddListener(this.OnClickBtnJapan);

        SetTextSize();
    }

    private void OnClickBtnUsa()
    {
        _setting.Country = Country.USA;
        NextPage("TitlePage");
    }

    private void OnClickBtnKorea()
    {
        _setting.Country = Country.KOREA;
        NextPage("TitlePage");
    }

    private void OnClickBtnJapan()
    {
        _setting.Country = Country.JAPAN;
        NextPage("TitlePage");
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
