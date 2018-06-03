using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model;
using Assets.Model.ChessPiece;
using Assets.Model.SkillChessPiece;
using Assets.Model.Impl;
using Assets.Model.Bean;
using Assets.Model.SceneParameter;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

namespace Assets.View
{
    public class SkillBattlePage : BattlePage
    {
        private void Awake()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as BattlePageParameter;

            _matchForm = param.MatchForm;
            _myColor = param.MatchForm.Color;
            _isMyTurn = (_myColor == Support.Color.WHITE) ? true : false;
            _networkManager = NetworkManager.GetInstance();
            _effectManager = EffectManager.GetInstance();
            _cameraManager = CameraManager.GetInstance();
            _objectMoveManager = ObjectMoveManager.GetInstance();
            _setting = Setting.GetInstance();
            _textResource = TextResource.GetInstance();
            _board = new List<Board[]>();

            SetBoard();

            SetSocketEvents(_networkManager);
            SetTextSize();
            SetTextValue();
            SetImageValue();
            
            _cameraManager.Run(CameraService, _myColor);
            _networkManager.ReadyGame(_matchForm.Id);

            StartCoroutine(CheckGameOver());
            StartCoroutine(CheckTimeOver());
        }

        private void Update()
        {
            if (!_gameStarted)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 마커정리
                
                _selectedObject = Touch();

                // 이전터치가 적 기물이거나 첫 터치인경우
                if (!_selectedMyPiece)
                {
                    _startLocation = GetLocation(_selectedObject);

                    // 마커표시
                }
                // 이전터치가 내 기물인 경우
                else
                {
                    _endLocation = GetLocation(_selectedObject);
                }

                if(_isMyTurn)
                {
                    // 이전터치가 적 기물이거나 첫 터치인경우
                    if (!_selectedMyPiece)
                    {
                        if (_board[_startLocation.X][_startLocation.Y].Piece.Color == _myColor)
                        {
                            // moveStatusClean
                            // moveStatusReset
                            // 이동범위 표시
                            _selectedMyPiece = true;
                        }
                    }
                    // 이전터치가 내 기물인 경우
                    else
                    {
                        // 이번터치가 이동 가능한 곳인 경우
                        if (_board[_endLocation.X][_endLocation.Y].IsPossibleMove)
                        {
                            var piece = _board[_startLocation.X][_startLocation.Y].Piece;

                            // 프로모션이 가능한 턴
                            if (piece is Pawn && (_endLocation.Y == 0 || _endLocation.Y == 7))
                            {
                                _networkManager.Relay(new RelayForm
                                {
                                    Pattern = Pattern.MOVE,
                                    StartLocation = _startLocation,
                                    EndLocation = _endLocation,
                                    Color = _myColor,
                                    TurnFinished = false
                                });
                                // TODO : 프로모션 창 진입
                            }
                            // 캐슬링이 가능한 턴
                            else if (piece is King && piece.IsPossibleCastling)
                            {
                                int? tempStartX = null;
                                int? tempEndX = null;

                                // 왼쪽 캐슬링을 선택한 경우
                                if (_endLocation.X == 2)
                                {
                                    tempStartX = 0;
                                    tempEndX = 3;
                                }
                                // 오른쪽 캐슬링을 선택한 경우
                                if (_endLocation.X == 6)
                                {
                                    tempStartX = 7;
                                    tempEndX = 5;
                                }
                                
                                // 캐슬링 이동 턴
                                if (tempStartX.HasValue && tempEndX.HasValue)
                                {
                                    _networkManager.Relay(new RelayForm
                                    {
                                        Pattern = Pattern.PROMOTION,
                                        StartLocation = _startLocation,
                                        EndLocation = _endLocation,
                                        CastlingStartLocation = new Location(tempStartX.Value, _startLocation.Y),
                                        CastlingEndLocation = new Location(tempEndX.Value, _endLocation.Y),
                                        Color = _myColor,
                                        TurnFinished = true
                                    });
                                }
                                // 평범한 이동 턴
                                else
                                {
                                    _networkManager.Relay(new RelayForm
                                    {
                                        Pattern = Pattern.MOVE,
                                        StartLocation = _startLocation,
                                        EndLocation = _endLocation,
                                        Color = _myColor,
                                        TurnFinished = true
                                    });
                                }
                            }
                            // 평범한 이동 턴
                            else
                            {
                                _networkManager.Relay(new RelayForm
                                {
                                    Pattern = Pattern.MOVE,
                                    StartLocation = _startLocation,
                                    EndLocation = _endLocation,
                                    Color = _myColor,
                                    TurnFinished = true
                                });
                            }
                        }
                        // 이번터치가 내 기물인 경우
                        else if (_board[_endLocation.X][_endLocation.Y].Piece.Color == _myColor)
                        {
                            // moveStatusClean
                            // moveStatusReset
                            // 이동범위 표시
                            _startLocation = _endLocation;
                        }
                        // 이번터치가 이동 불가능한 곳인 경우
                        else
                        {
                            // moveStatusClean
                            _selectedMyPiece = false;
                        }
                    }
                }
            }
        }

        protected void SetBoard()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as BattlePageParameter;
            var myLineUp = param.MatchForm.MyLineUp;
            var enemyLineUp = param.MatchForm.EnemyLineUp;

            // 임시 1차원 배열 선언
            var aLine = new Board[8];
            var bLine = new Board[8];
            var cLine = new Board[8];
            var dLine = new Board[8];
            var eLine = new Board[8];
            var fLine = new Board[8];
            var gLine = new Board[8];
            var hLine = new Board[8];

            //A열 초기화
            aLine[0] = new Board(GameObject.Find("A8"), GameObject.Find("BlackRook1"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Rook[0], Support.Color.BLACK) : AllocateClass(enemyLineUp.Rook[0], Support.Color.WHITE), Support.Color.WHITE);
            aLine[1] = new Board(GameObject.Find("A7"), GameObject.Find("BlackPawn1"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[0], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[0], Support.Color.WHITE), Support.Color.BLACK);
            aLine[2] = new Board(GameObject.Find("A6"), Support.Color.WHITE);
            aLine[3] = new Board(GameObject.Find("A5"), Support.Color.BLACK);
            aLine[4] = new Board(GameObject.Find("A4"), Support.Color.WHITE);
            aLine[5] = new Board(GameObject.Find("A3"), Support.Color.BLACK);
            aLine[6] = new Board(GameObject.Find("A2"), GameObject.Find("WhitePawn1"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[0], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[0], Support.Color.BLACK), Support.Color.WHITE);
            aLine[7] = new Board(GameObject.Find("A1"), GameObject.Find("WhiteRook1"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Rook[0], Support.Color.WHITE) : AllocateClass(enemyLineUp.Rook[0], Support.Color.BLACK), Support.Color.BLACK);

            //B열 초기화
            bLine[0] = new Board(GameObject.Find("B8"), GameObject.Find("BlackKnight1"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Knight[0], Support.Color.BLACK) : AllocateClass(enemyLineUp.Knight[0], Support.Color.WHITE), Support.Color.BLACK);
            bLine[1] = new Board(GameObject.Find("B7"), GameObject.Find("BlackPawn2"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[1], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[1], Support.Color.WHITE), Support.Color.WHITE);
            bLine[2] = new Board(GameObject.Find("B6"), Support.Color.BLACK);
            bLine[3] = new Board(GameObject.Find("B5"), Support.Color.WHITE);
            bLine[4] = new Board(GameObject.Find("B4"), Support.Color.BLACK);
            bLine[5] = new Board(GameObject.Find("B3"), Support.Color.WHITE);
            bLine[6] = new Board(GameObject.Find("B2"), GameObject.Find("WhitePawn2"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[1], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[1], Support.Color.BLACK), Support.Color.BLACK);
            bLine[7] = new Board(GameObject.Find("B1"), GameObject.Find("WhiteKnight1"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Knight[0], Support.Color.WHITE) : AllocateClass(enemyLineUp.Knight[0], Support.Color.BLACK), Support.Color.WHITE);

            //C열 초기화
            cLine[0] = new Board(GameObject.Find("C8"), GameObject.Find("BlackBishop1"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Bishop[0], Support.Color.BLACK) : AllocateClass(enemyLineUp.Bishop[0], Support.Color.WHITE), Support.Color.WHITE);
            cLine[1] = new Board(GameObject.Find("C7"), GameObject.Find("BlackPawn3"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[2], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[2], Support.Color.WHITE), Support.Color.BLACK);
            cLine[2] = new Board(GameObject.Find("C6"), Support.Color.WHITE);
            cLine[3] = new Board(GameObject.Find("C5"), Support.Color.BLACK);
            cLine[4] = new Board(GameObject.Find("C4"), Support.Color.WHITE);
            cLine[5] = new Board(GameObject.Find("C3"), Support.Color.BLACK);
            cLine[6] = new Board(GameObject.Find("C2"), GameObject.Find("WhitePawn3"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[2], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[2], Support.Color.BLACK), Support.Color.WHITE);
            cLine[7] = new Board(GameObject.Find("C1"), GameObject.Find("WhiteBishop1"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Bishop[0], Support.Color.WHITE) : AllocateClass(enemyLineUp.Bishop[0], Support.Color.BLACK), Support.Color.BLACK);

            //D열 초기화
            dLine[0] = new Board(GameObject.Find("D8"), GameObject.Find("BlackQueen"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Queen, Support.Color.BLACK) : AllocateClass(enemyLineUp.Queen, Support.Color.WHITE), Support.Color.BLACK);
            dLine[1] = new Board(GameObject.Find("D7"), GameObject.Find("BlackPawn4"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[3], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[3], Support.Color.WHITE), Support.Color.WHITE);
            dLine[2] = new Board(GameObject.Find("D6"), Support.Color.BLACK);
            dLine[3] = new Board(GameObject.Find("D5"), Support.Color.WHITE);
            dLine[4] = new Board(GameObject.Find("D4"), Support.Color.BLACK);
            dLine[5] = new Board(GameObject.Find("D3"), Support.Color.WHITE);
            dLine[6] = new Board(GameObject.Find("D2"), GameObject.Find("WhitePawn4"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[3], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[3], Support.Color.BLACK), Support.Color.BLACK);
            dLine[7] = new Board(GameObject.Find("D1"), GameObject.Find("WhiteQueen"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Queen, Support.Color.WHITE) : AllocateClass(enemyLineUp.Queen, Support.Color.BLACK), Support.Color.WHITE);

            //E열 초기화
            eLine[0] = new Board(GameObject.Find("E8"), GameObject.Find("BlackKing"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.King, Support.Color.BLACK) : AllocateClass(enemyLineUp.King, Support.Color.WHITE), Support.Color.WHITE);
            eLine[1] = new Board(GameObject.Find("E7"), GameObject.Find("BlackPawn5"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[4], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[4], Support.Color.WHITE), Support.Color.BLACK);
            eLine[2] = new Board(GameObject.Find("E6"), Support.Color.WHITE);
            eLine[3] = new Board(GameObject.Find("E5"), Support.Color.BLACK);
            eLine[4] = new Board(GameObject.Find("E4"), Support.Color.WHITE);
            eLine[5] = new Board(GameObject.Find("E3"), Support.Color.BLACK);
            eLine[6] = new Board(GameObject.Find("E2"), GameObject.Find("WhitePawn5"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[4], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[4], Support.Color.BLACK), Support.Color.WHITE);
            eLine[7] = new Board(GameObject.Find("E1"), GameObject.Find("WhiteKing"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.King, Support.Color.WHITE) : AllocateClass(enemyLineUp.King, Support.Color.BLACK), Support.Color.BLACK);

            //F열 초기화
            fLine[0] = new Board(GameObject.Find("F8"), GameObject.Find("BlackBishop2"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Bishop[1], Support.Color.BLACK) : AllocateClass(enemyLineUp.Bishop[1], Support.Color.WHITE), Support.Color.BLACK);
            fLine[1] = new Board(GameObject.Find("F7"), GameObject.Find("BlackPawn6"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[5], Support.Color.BLACK) : AllocateClass(enemyLineUp.King, Support.Color.WHITE), Support.Color.WHITE);
            fLine[2] = new Board(GameObject.Find("F6"), Support.Color.BLACK);
            fLine[3] = new Board(GameObject.Find("F5"), Support.Color.WHITE);
            fLine[4] = new Board(GameObject.Find("F4"), Support.Color.BLACK);
            fLine[5] = new Board(GameObject.Find("F3"), Support.Color.WHITE);
            fLine[6] = new Board(GameObject.Find("F2"), GameObject.Find("WhitePawn6"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[5], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[5], Support.Color.BLACK), Support.Color.BLACK);
            fLine[7] = new Board(GameObject.Find("F1"), GameObject.Find("WhiteBishop2"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Bishop[1], Support.Color.WHITE) : AllocateClass(enemyLineUp.Bishop[1], Support.Color.BLACK), Support.Color.WHITE);

            //G열 초기화
            gLine[0] = new Board(GameObject.Find("G8"), GameObject.Find("BlackKnight2"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Knight[1], Support.Color.BLACK) : AllocateClass(enemyLineUp.Knight[1], Support.Color.WHITE), Support.Color.WHITE);
            gLine[1] = new Board(GameObject.Find("G7"), GameObject.Find("BlackPawn7"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[6], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[6], Support.Color.WHITE), Support.Color.BLACK);
            gLine[2] = new Board(GameObject.Find("G6"), Support.Color.WHITE);
            gLine[3] = new Board(GameObject.Find("G5"), Support.Color.BLACK);
            gLine[4] = new Board(GameObject.Find("G4"), Support.Color.WHITE);
            gLine[5] = new Board(GameObject.Find("G3"), Support.Color.BLACK);
            gLine[6] = new Board(GameObject.Find("G2"), GameObject.Find("WhitePawn7"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[6], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[6], Support.Color.BLACK), Support.Color.WHITE);
            gLine[7] = new Board(GameObject.Find("G1"), GameObject.Find("WhiteKnight2"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Knight[1], Support.Color.WHITE) : AllocateClass(enemyLineUp.Knight[1], Support.Color.BLACK), Support.Color.BLACK);

            //H열 초기화
            hLine[0] = new Board(GameObject.Find("H8"), GameObject.Find("BlackRook2"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Rook[1], Support.Color.BLACK) : AllocateClass(enemyLineUp.Rook[1], Support.Color.WHITE), Support.Color.BLACK);
            hLine[1] = new Board(GameObject.Find("H7"), GameObject.Find("BlackPawn8"), (_myColor == Support.Color.BLACK)
                ? AllocateClass(myLineUp.Pawn[7], Support.Color.BLACK) : AllocateClass(enemyLineUp.Pawn[7], Support.Color.WHITE), Support.Color.WHITE);
            hLine[2] = new Board(GameObject.Find("H6"), Support.Color.BLACK);
            hLine[3] = new Board(GameObject.Find("H5"), Support.Color.WHITE);
            hLine[4] = new Board(GameObject.Find("H4"), Support.Color.BLACK);
            hLine[5] = new Board(GameObject.Find("H3"), Support.Color.WHITE);
            hLine[6] = new Board(GameObject.Find("H2"), GameObject.Find("WhitePawn8"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Pawn[7], Support.Color.WHITE) : AllocateClass(enemyLineUp.Pawn[7], Support.Color.BLACK), Support.Color.BLACK);
            hLine[7] = new Board(GameObject.Find("H1"), GameObject.Find("WhiteRook2"), (_myColor == Support.Color.WHITE)
                ? AllocateClass(myLineUp.Rook[1], Support.Color.WHITE) : AllocateClass(enemyLineUp.Rook[1], Support.Color.BLACK), Support.Color.WHITE);

            //각 열들을 2차원 배열으로 합침
            _board.Add(aLine);
            _board.Add(bLine);
            _board.Add(cLine);
            _board.Add(dLine);
            _board.Add(eLine);
            _board.Add(fLine);
            _board.Add(gLine);
            _board.Add(hLine);
        }

        private SkillPiece AllocateClass(int classCode, string color)
        {
            SkillPiece piece = null;

            // 직업코드에 맞게 기물 생성.
            switch (classCode)
            {
                // Pawn
                case ClassCode.ELEMENTALKNIGHT:
                    piece = new ElementalKnight(color);
                    break;
                case ClassCode.BOMBER:
                    piece = new Bomber(color);
                    break;
                case ClassCode.FIGHTER:
                    piece = new Fighter(color);
                    break;
                case ClassCode.LAUNCHER:
                    piece = new Launcher(color);
                    break;
                
                // Rook
                case ClassCode.ASSASSIN:
                    piece = new Assassin(color);
                    break;
                case ClassCode.FIREBAT:
                    piece = new FireBat(color);
                    break;

                // Bishop
                case ClassCode.ARCHMAGE:
                    piece = new Archmage(color);
                    break;
                case ClassCode.PRIEST:
                    piece = new Priest(color);
                    break;

                // Knight
                case ClassCode.ARCHER:
                    piece = new Archer(color);
                    break;
                case ClassCode.PALADIN:
                    piece = new Paladin(color);
                    break;

                // Queen
                case ClassCode.BLACKMAGICIAN:
                    piece = new BlackMagician(color);
                    break;
                case ClassCode.LICH:
                    piece = new Lich(color);
                    break;

                // King
                case ClassCode.ANGEL:
                    piece = new Angel(color);
                    break;
                case ClassCode.IUPPITER:
                    piece = new Iuppiter(color);
                    break;

                default:
                    throw new Exception("[ClassCode : " + classCode + "]와 일치하는 직업군이 없습니다.");
            }

            return piece;
        }

        public void SetSocketEvents(NetworkManager networkManager)
        {
            networkManager.OnStartBattle -= OnStartBattle;
            networkManager.OnRelayBattle -= OnRelayBattle;
            networkManager.OnResultBattle -= OnResultBattle;

            networkManager.OnStartBattle += OnStartBattle;
            networkManager.OnRelayBattle += OnRelayBattle;
            networkManager.OnResultBattle += OnResultBattle;
        }

        public void SetTextSize()
        {
            
        }

        public void SetTextValue()
        {
            MyFrame.transform.Find("TXT_Nickname").GetComponent<Text>().text = _setting.Nickname;
            EnemyFrame.transform.Find("TXT_Nickname").GetComponent<Text>().text = _matchForm.Enemy.Nickname;
        }

        public void SetImageValue()
        {
            SetRankIcon(MyFrame.transform.Find("IMG_Rank").GetComponent<Image>(), _setting.Score);
            SetRankIcon(EnemyFrame.transform.Find("IMG_Rank").GetComponent<Image>(), _matchForm.Enemy.Score);
        }

        protected void OnRelayBattle(object sender, RelayForm relayForm)
        {
            throw new NotImplementedException("");
        }

        protected void OnResultBattle(object sender, ResultForm resultForm)
        {
            throw new NotImplementedException("");
        }
    }
}
