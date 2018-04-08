using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Assets.Model;
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

        public GameObject SkillBattle;

        public GameObject ClassicBattle;

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
    }
}
