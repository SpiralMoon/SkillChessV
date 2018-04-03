using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Bean;
using Assets.Model.Impl;
using Assets.Model.SceneParameter;
using Assets.Support.Language;
using Assets.Service;

public class WaitingPage : MonoBehaviour, IPage, ISocketPage
{
    private NetworkManager _networkManager;

    private TextResource _textResource;

    private bool _isShowWaiting;

    public Button BtnBack;

    public Image ImgGameMode;

    public Text TxtGameMode;

    public Text TxtTime;

    public GameObject PnlWaiting;

    public GameObject PnlMatching;

    public int TimeCount { get; set; }

    private void Awake()
    {
        _networkManager = NetworkManager.GetInstance();
        _isShowWaiting = true;
        TimeCount = 120;

        BtnBack.onClick.AddListener(OnClickBtnBack);

        SetTextSize();
        SetTextValue();
        SetSocketEvents(_networkManager);
    }

    private void Start()
    {
        StartCoroutine(WaitTimeCount());
    }

    /// <summary>
    /// 타이머
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitTimeCount()
    {
        while (TimeCount >= 0)
        {
            TxtTime.text = TimeCount / 60 + ":" + TimeCount % 60;

            yield return new WaitForSeconds(Time.deltaTime * 60);

            if (TimeCount == 0)
            {
                StopCoroutine("WaitTimeCount");
                // TODO : 진짜 방 정보로 바꾸기
                _networkManager.CloseRoom(new RoomForm());
            }

            TimeCount--;
        }
    }

    public void SetSocketEvents(NetworkManager networkManager)
    {
        networkManager.OnMatchBattle -= OnMatchBattle;
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
        //TxtGameMode.text = _textResource.GetText(TextCode.)
    }

    public void OnClickBtnBack()
    {
        // TODO : 진짜 방 정보로 바꾸기
        _networkManager.CloseRoom(new RoomForm());
        NextPage("LobbyPage");
    }

    public void OnMatchBattle(object sender, MatchForm other)
    {

    }

    public void NextPage(string pageName)
    {
        SceneManager.LoadSceneAsync(pageName);
    }
}
