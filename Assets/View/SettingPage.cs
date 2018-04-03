using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Assets.Model.Impl;
using Assets.Model;

public class SettingPage : MonoBehaviour, IPage
{
    private static Setting _setting;

    private float _defaultVolume;

    private float _defaultQuality;

    public Button BtnSave;

    public Button BtnReset;

    public Button BtnBack;

    public Slider SldVolume;

    public Slider SldQuality;

    public float Volume
    {
        get { return SldVolume.value; }
        set { SldVolume.value = value; }
    }

    public float Quality
    {
        get { return SldQuality.value; }
        set { SldQuality.value = value; }
    }

    private void Awake()
    {
        _setting = Setting.GetInstance();
        _defaultVolume = 0.5f;
        _defaultQuality = 3f;

        Volume = _setting.Volume;
        Quality = _setting.Quality;

        BtnSave.onClick.AddListener(OnClickBtnSave);
        BtnReset.onClick.AddListener(OnClickBtnReset);
        BtnBack.onClick.AddListener(OnClickBtnBack);

        SetTextSize();
        SetTextValue();
    }

    public void OnClickBtnSave()
    {
        _setting.Volume = Volume;
        _setting.Quality = Quality;
    }

    public void OnClickBtnReset()
    {
        Volume = _defaultVolume;
        Quality = _defaultQuality;

        _setting.Volume = Volume;
        _setting.Quality = Quality;
    }

    public void OnClickBtnBack()
    {
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
