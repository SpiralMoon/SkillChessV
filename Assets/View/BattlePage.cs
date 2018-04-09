using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using Assets.Model;
using Assets.Model.Bean;
using Assets.Model.Impl;
using Assets.Model.SceneParameter;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

namespace Assets.View
{
    public class BattlePage : MonoBehaviour, IPage, ISocketPage
    {
        protected NetworkManager _networkManager;

        protected Setting _setting;

        protected TextResource _textResource;

        protected List<Board[]> _board;

        protected string _myColor;

        protected MatchForm _matchForm;

        protected bool _gameStarted;

        public GameObject SkillBattle;

        public GameObject ClassicBattle;

        public GameObject MyFrame;

        public GameObject EnemyFrame;



        private void Awake()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as BattlePageParameter;

            if (param.MatchForm.GameMode == GameMode.NORMAL)
            {
                SkillBattle.SetActive(true);
            }
            if (param.MatchForm.GameMode == GameMode.SKILL)
            {
                ClassicBattle.SetActive(true);
            }
        }

        public void SetSocketEvents(NetworkManager networkManager)
        {
            throw new NotImplementedException("");
        }

        public void Invoke(Action action)
        {
            throw new NotImplementedException("");
        }

        public void SetTextSize()
        {
            throw new NotImplementedException("");
        }

        public void SetTextValue()
        {
            throw new NotImplementedException("");
        }

        public void NextPage(string pageName)
        {
            throw new NotImplementedException("");
        }

        protected void SetRankIcon(Image rankIcon, int score)
        {
            if (score > 2000)
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_King");
            else if (score > 1800)
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_Queen");
            else if (score > 1600)
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_Knight");
            else if (score > 1400)
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_Bishop");
            else if (score > 1200)
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_Rook");
            else
                rankIcon.sprite = Resources.Load<Sprite>("UI/Icon/IMG_Pawn");
        }

        protected void OnStartBattle(object sender, EventArgs e)
        {
            throw new NotImplementedException("");
        }
    }
}
