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

        protected CameraManager _cameraManager;

        protected Setting _setting;

        protected TextResource _textResource;

        protected List<Board[]> _board;

        protected MatchForm _matchForm;

        protected string _myColor;
        
        protected bool _gameStarted;

        protected bool _isMyTurn;

        protected bool _selectedMyPiece;

        protected GameObject _selectedObject;

        protected Location _startLocation;

        protected Location _endLocation;

        public GameObject SkillBattle;

        public GameObject ClassicBattle;

        public GameObject MyFrame;

        public GameObject EnemyFrame;

        public GameObject CameraService;

        private void Awake()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as BattlePageParameter;

            if (param.MatchForm.GameMode == GameMode.NORMAL)
            {
                ClassicBattle.SetActive(true);
            }
            if (param.MatchForm.GameMode == GameMode.SKILL)
            {
                SkillBattle.SetActive(true);
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

        /// <summary>
        /// 객체 터치 인식.
        /// </summary>
        /// <returns>터치한 객체</returns>
        protected GameObject Touch()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            Physics.Raycast(ray.origin, ray.direction * 200, out raycastHit);

            return raycastHit.collider.gameObject ?? null;
        }

        /// <summary>
        /// 선택한 기물의 위치를 반환.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>기물의 위치</returns>
        protected Location GetLocation(GameObject obj)
        {
            Location location = null;

            for (int i = 0; i < _board.Count; i++)
            {
                for (int j = 0; j < _board[i].Length; j++)
                {
                    if (_board[i][j].BoardObj == obj ||
                        (_board[i][j].PieceObj != null && _board[i][j].PieceObj == obj))
                    {
                        location = new Location(i, j);
                        break;
                    }
                }
            }

            return location;
        }

        /// <summary>
        /// 모든 발판의 IsPossibleMove를 초기화.
        /// Piece 클래스에도 정의되어 있음.
        /// </summary>
        protected void CleanMoveStatus()
        {
            foreach(var line in _board)
            {
                foreach(var cell in line)
                {
                    cell.IsPossibleMove = false;
                }
            }
        }

        protected void OnStartBattle(object sender, EventArgs e)
        {
            throw new NotImplementedException("");
        }
    }
}
