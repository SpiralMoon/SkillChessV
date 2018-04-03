using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Service;

public class RankPage : MonoBehaviour, IPage
{
    private NetworkManager _networkManager;

    private int _pageNumber;

    public List<GameObject> LstRank;

    public Button BtnPrev;

    public Button BtnNext;

    public Button BtnBack;

    private void Awake()
    {
        _networkManager = NetworkManager.GetInstance();
        _pageNumber = 1;

        BtnPrev.onClick.AddListener(OnClickBtnPrev);
        BtnNext.onClick.AddListener(OnClickBtnNext);
        BtnBack.onClick.AddListener(OnClickBtnBack);

        SetTextSize();
        SetTextValue();

        LoadRankList();
    }

    public void OnClickBtnBack()
    {
        NextPage("LobbyPage");
    }

    /// <summary>
    /// 이전페이지 랭킹 확인
    /// </summary>
    public void OnClickBtnPrev()
    {
        if (_pageNumber > 1)
        {
            _pageNumber--;
            LoadRankList();
        }
    }

    /// <summary>
    /// 다음페이지 랭킹 확인
    /// </summary>
    public void OnClickBtnNext()
    {
        _pageNumber++;
        LoadRankList();
    }

    /// <summary>
    /// 랭킹 목록 출력
    /// </summary>
    public async void LoadRankList()
    {
        var rankList = await _networkManager.Rank(_pageNumber);

        // 리더보드 지우기
        foreach(var line in LstRank)
        {
            var rankIcon = line.GetComponentInChildren<Image>();
            var rank = line.GetComponentsInChildren<Text>()[0];
            var nickname = line.GetComponentsInChildren<Text>()[1];
            var score = line.GetComponentsInChildren<Text>()[2];

            rank.text = "";
            nickname.text = "";
            score.text = "";
        }

        // 리더보드 채우기
        for(int i = 0; i < rankList.Count; i++)
        {
            var rankIcon = LstRank[i].GetComponentInChildren<Image>();
            var rank = LstRank[i].GetComponentsInChildren<Text>()[0];
            var nickname = LstRank[i].GetComponentsInChildren<Text>()[1];
            var score = LstRank[i].GetComponentsInChildren<Text>()[2];

            rank.text = (i + 1) + ((_pageNumber - 1) * LstRank.Count) + "";
            nickname.text = rankList[i].Nickname;
            score.text = rankList[i].Score + "";
        }
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
