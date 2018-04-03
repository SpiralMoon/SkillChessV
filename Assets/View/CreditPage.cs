using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;

public class CreditPage : MonoBehaviour, IPage
{
    private void Awake()
    {

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
