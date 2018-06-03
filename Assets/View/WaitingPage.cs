using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Animation;
using Assets.Model;
using Assets.Model.Bean;
using Assets.Model.Impl;
using Assets.Model.SceneParameter;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

public class WaitingPage : MonoBehaviour, IPage, ISocketPage
{
    private NetworkManager _networkManager;

    private Setting _setting;

    private TextFadeInOutAnimation _textFadeInOutAnimation;

    private TextResource _textResource;

    private bool _isShowWaiting;

    /// <summary>
    /// 게임 방 정보. 자신이 방장일 때만 유효함.
    /// </summary>
    private RoomForm _room;

    /// <summary>
    /// 매칭 정보. 게임
    /// </summary>
    private MatchForm _match;

    public Button BtnBack;

    public Image ImgGameMode;

    public Text TxtGameMode;

    public Text TxtDescription;

    public Text TxtWaiting;

    public Text TxtGameMatched;

    public Text TxtGameStart;

    public Text TxtMatchTime;

    public Text TxtWaitTime;

    public GameObject PnlWaiting;

    public GameObject PnlMatching;

    public int TimeCount { get; set; }

    private void Awake()
    {
        _setting = Setting.GetInstance();
        _networkManager = NetworkManager.GetInstance();
        _textResource = TextResource.GetInstance();
        _textFadeInOutAnimation = new TextFadeInOutAnimation();

        var param = PageParameterDispatcher.Instance().GetPageParameter() as WaitingPageParameter;

        if (param.RoomForm != null)
        {
            TimeCount = 120;

            _isShowWaiting = true;
            _room = param.RoomForm;
            _textFadeInOutAnimation.Target = TxtWaiting; 
        }
        else if (param.MatchForm != null)
        {
            TimeCount = 5;

            _match = param.MatchForm;
            _isShowWaiting = false;
        }

        BtnBack.onClick.AddListener(OnClickBtnBack);

        SetTextSize();
        SetTextValue();
        SetIconImage();
        SetSocketEvents(_networkManager);
    }

    private void Start()
    {
        if (_isShowWaiting)
        {
            PnlWaiting.SetActive(true);
            PnlMatching.SetActive(false);

            _textFadeInOutAnimation.Show();
            StartCoroutine(WaitTimeCount());
        }
        else
        {
            PnlMatching.SetActive(true);
            PnlWaiting.SetActive(false);

            StartCoroutine(MatchTimeCount());
        }
    }

    /// <summary>
    /// 매칭대기 타이머
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitTimeCount()
    {
        while (TimeCount >= 0)
        {
            TxtWaitTime.text = TimeCount / 60 + ":" + TimeCount % 60;

            yield return new WaitForSeconds(Time.deltaTime * 60);

            if (TimeCount == 0)
            {
                StopCoroutine("WaitTimeCount");
                // TODO : 진짜 방 정보로 바꾸기
                _networkManager.CloseRoom(_room);
            }

            TimeCount--;
        }
    }

    /// <summary>
    /// 게임시작대기 타이머
    /// </summary>
    /// <returns></returns>
    private IEnumerator MatchTimeCount ()
    {
        while (TimeCount >= 0)
        {
            TxtMatchTime.text = TimeCount + "";

            yield return new WaitForSeconds(Time.deltaTime * 60);

            if (TimeCount == 0)
            {
                PageParameterDispatcher.Instance().SetPageParameter(new BattlePageParameter
                {
                    MyRank = new Rank
                    {
                        Nickname = _setting.Nickname,
                        Score = _setting.Score
                    },
                    MatchForm = _match
                });
                NextPage("BattlePage");
            }

            TimeCount--;
        }
    }

    public void SetSocketEvents(NetworkManager networkManager)
    {
        networkManager.OnMatchBattle -= OnMatchBattle;
        networkManager.OnCloseRoom -= OnCloseRoom;

        networkManager.OnMatchBattle += OnMatchBattle;
        networkManager.OnCloseRoom += OnCloseRoom;
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
        if (_room != null)
        {
            TxtWaiting.text = _textResource.GetText(TextCode.WAITING_FOR_THE_OTHER_SIDE);

            if (_room.GameMode == GameMode.NORMAL)
            {
                TxtGameMode.text = _textResource.GetText(TextCode.CLASSIC);
                TxtDescription.text = _textResource.GetText(TextCode.DESCRIPTION_CLASSIC_MODE);
            }
            if (_room.GameMode == GameMode.SKILL)
            {
                TxtGameMode.text = _textResource.GetText(TextCode.SKILL);
                TxtDescription.text = _textResource.GetText(TextCode.DESCRIPTION_SKILL_MODE);
            }
        }

        if (_match != null)
        {
            TxtGameMatched.text = _textResource.GetText(TextCode.MATCHING_COMPLETE);
            TxtGameStart.text = _textResource.GetText(TextCode.THE_GAME_IS_ABOUT_TO_START_RIGHT_NOW);
        }
    }

    public void SetIconImage()
    {
        if (_room != null)
        {
            if (_room.GameMode == GameMode.NORMAL)
            {
                ImgGameMode.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Icon/classic");
            }
            if (_room.GameMode == GameMode.SKILL)
            {
                ImgGameMode.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Icon/skill");
            }
        }
    }

    public void OnClickBtnBack()
    {
        // TODO : 진짜 방 정보로 바꾸기
        _networkManager.CloseRoom(_room);
    }

    public void OnMatchBattle(object sender, MatchForm matchForm)
    {
        Invoke(() =>
        {
            SetTextValue();

            PnlMatching.SetActive(true);
            PnlWaiting.SetActive(false);

            TimeCount = 5;

            _match = matchForm;

            StopCoroutine("WaitTimeCount");
            StartCoroutine(MatchTimeCount());
        });
    }

    public void OnCloseRoom(object sender, RoomForm roomForm)
    {
        Invoke(() => NextPage("LobbyPage"));
    }

    public void NextPage(string pageName)
    {
        SceneManager.LoadSceneAsync(pageName);
    }
}
