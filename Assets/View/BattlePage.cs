using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Model.Bean;
using Assets.Service;

public class BattlePage : MonoBehaviour, IPage, ISocketPage
{
    private NetworkManager _networkManager;

    private void Awake()
    {
        SetTextSize();
        SetTextValue();
        SetSocketEvents(_networkManager);
    }

    public void SetSocketEvents(NetworkManager networkManager)
    {
        
    }

    public void Invoke(Action action)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(action);
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
