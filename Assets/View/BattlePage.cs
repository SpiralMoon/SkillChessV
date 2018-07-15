using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model;
using Assets.Model.Bean;
using Assets.Model.Impl;
using Assets.Model.ChessPiece;
using Assets.Model.SceneParameter;
using Assets.Model.SkillChessPiece;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

namespace Assets.View
{
    public class BattlePage : MonoBehaviour, IPage, ISocketPage
    {
        protected NetworkManager _networkManager;

        protected CameraManager _cameraManager;

        protected EffectManager _effectManager;

        protected ObjectMoveManager _objectMoveManager;

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

        protected DashBoard _whiteDashBoard;

        protected DashBoard _blackDashBoard;

        public GameObject SkillBattle;

        public GameObject ClassicBattle;

        public GameObject MyFrame;

        public GameObject EnemyFrame;

        public GameObject CameraService;

        public Text TxtTimeCount;

        public GameObject MyTimeLife;

        public GameObject EnemyTimeLife;

        public Text TxtDebug;

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
            UnityMainThreadDispatcher.Instance().Enqueue(action);
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
            SceneManager.LoadSceneAsync(pageName);
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

        protected void SetMap()
        {
            // TODO
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

            return raycastHit.collider?.gameObject ?? null;
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

        /// <summary>
       /// 기물의 데이터를 이동.
       /// </summary>
       /// <param name="startLocation"></param>
       /// <param name="endLocation"></param>
        protected void MovePieceData(Location startLocation, Location endLocation)
        {
            var piece = _board[startLocation.X][startLocation.Y];

            if (piece.Piece is Pawn)
            {
                piece.Piece.IsPossibleFirstChance = false;
            }
            else if (piece.Piece is King || piece.Piece is Rook)
            {
                piece.Piece.IsPossibleCastling = false;
            }

            _board[endLocation.X][endLocation.Y].PieceObj = piece.PieceObj;
            _board[endLocation.X][endLocation.Y].Piece = piece.Piece;

            piece.PieceObj = null;
            piece.Piece = null;
        }

        protected void OnStartBattle(object sender, EventArgs e)
        {
            _gameStarted = true;
        }

        protected void OnRelayBattle(object sender, RelayForm e)
        {
            throw new NotImplementedException("");
        }

        protected void OnResultBattle(object sender, ResultForm resultForm)
        {
            Invoke(() =>
            {
                var param = new ResultPageParameter
                {
                    WhiteDashBoard = _whiteDashBoard,
                    BlackDashBoard = _blackDashBoard
                };

                // 정상적인 게임 종료
                if (resultForm.Pattern == Pattern.FINISH)
                {
                    // TODO
                    PageParameterDispatcher.Instance().SetPageParameter(param);
                    NextPage("ResultPage");
                }
                // 항복을 통한 게임 종료
                else if (resultForm.Pattern == Pattern.SURRENDER)
                {
                    // TODO
                    PageParameterDispatcher.Instance().SetPageParameter(param);
                    NextPage("ResultPage");
                }
            });
        }

        /// <summary>
        /// 실시간으로 게임오버를 검사하는 함수.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator CheckGameOver()
        {
            string winColor = null;

            // 1. King이 사망한 경우
            // 2. King 이외의 모든 기물이 사망한 경우            
            while (true)
            {
                EXIT:
                yield return new WaitForSeconds(3);

                var existWhiteKing = false;
                var existWhitePieces = false;
                var existBlackKing = false;
                var existBlackPieces = false;

                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (_board[i][j].Piece != null)
                        {
                            var piece = _board[i][j].Piece;

                            if (piece is King || piece is SkillKing)
                            {
                                var temp = (piece.Color == Support.Color.WHITE)
                                    ? (existWhiteKing = true)
                                    : (existBlackKing = true);
                            }
                            else
                            {
                                var temp = (piece.Color == Support.Color.WHITE)
                                    ? (existWhitePieces = true)
                                    : (existBlackPieces = true);
                            }

                            if (existWhiteKing && existWhitePieces && existBlackKing && existBlackPieces)
                            {
                                goto EXIT;
                            }
                        }
                
                if (existWhiteKing && existBlackKing)
                {
                    continue;
                }

                if (existWhiteKing || !existBlackPieces)
                {
                    winColor = Support.Color.WHITE;
                }
                else if (existBlackKing || !existWhitePieces)
                {
                    winColor = Support.Color.BLACK;
                }

                if (winColor == _myColor)
                {
                    _networkManager.Result(new ResultForm
                    {
                        Pattern = Pattern.FINISH,
                        Id = _setting.Email,
                        Color = winColor
                    });
                }

                break;
                StopCoroutine("CheckGameOver");
            }
        }

        protected IEnumerator CheckTimeOver()
        {
            // TODO
            yield return null;
            //throw new NotImplementedException("");
        }
    }
}
