using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model;
using Assets.Model.Impl;
using Assets.Model.Bean;
using Assets.Model.SceneParameter;
using Assets.Model.ChessPiece;
using Assets.Support;
using Assets.Support.Extension;
using Assets.Support.Language;
using Assets.Service;
using System.Collections;

namespace Assets.View
{
    public class ClassicBattlePage : BattlePage
    {
        public GameObject PromotionFrame;

        public Button BtnPromotionRook;

        public Button BtnPromotionKnight;

        public Button BtnPromotionBishop;

        public Button BtnPromotionQueen;

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

            BtnPromotionRook.onClick.AddListener(delegate { OnClickPromotion("Rook"); });
            BtnPromotionKnight.onClick.AddListener(delegate { OnClickPromotion("Knight"); });
            BtnPromotionBishop.onClick.AddListener(delegate { OnClickPromotion("Bishop"); });
            BtnPromotionQueen.onClick.AddListener(delegate { OnClickPromotion("Queen"); });

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
                _effectManager.Clear();
                
                if ((_selectedObject = Touch()) == null)
                {
                    return;
                }

                // 이전터치가 적 기물이거나 첫 터치인경우
                if (!_selectedMyPiece)
                {
                    _startLocation = GetLocation(_selectedObject);
                    _effectManager.Select(_board, _startLocation, _myColor);
                }
                // 이전터치가 내 기물인 경우
                else
                {
                    _endLocation = GetLocation(_selectedObject);
                }

                if (_isMyTurn)
                {
                    // 이전터치가 적 기물이거나 첫 터치인경우
                    if (!_selectedMyPiece)
                    {
                        var piece = _board[_startLocation.X][_startLocation.Y].Piece;

                        if (piece?.Color == _myColor)
                        {
                            CleanMoveStatus();
                            piece.SetMoveStatus(_board, _startLocation);
                            _effectManager.MoveScope(_board, _startLocation);
                            _selectedMyPiece = true;
                        }
                    }
                    // 이전터치가 내 기물인 경우
                    else
                    {
                        var piece = _board[_startLocation.X][_startLocation.Y].Piece;
                        var target = _board[_endLocation.X][_endLocation.Y].Piece;

                        // 이번터치가 이동 가능한 곳인 경우
                        if (_board[_endLocation.X][_endLocation.Y].IsPossibleMove)
                        {
                            // 프로모션이 가능한 턴 (King을 죽인 경우는 제외)
                            if (piece is Pawn && (_endLocation.Y == 0 || _endLocation.Y == 7) && !(target is King))
                            {
                                _networkManager.Relay(new RelayForm
                                {
                                    Pattern = Pattern.MOVE,
                                    StartLocation = _startLocation,
                                    EndLocation = _endLocation,
                                    Color = _myColor,
                                    TurnFinished = false
                                });

                                // 프로모션 창 진입
                                PromotionFrame.SetActive(true);
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
                                        Pattern = Pattern.CASTLING,
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

                            _selectedMyPiece = false;
                        }
                        // 이번터치가 내 기물인 경우
                        else if (target?.Color == _myColor)
                        {
                            _startLocation = _endLocation;

                            _effectManager.Select(_board, _endLocation, _myColor);
                            CleanMoveStatus();
                            target.SetMoveStatus(_board, _endLocation);
                            target.ShowMoveScope(_board, _endLocation);
                            // 이동범위 표시
                        }
                        // 이번터치가 이동 불가능한 곳인 경우
                        else
                        {
                            _effectManager.Select(_board, _endLocation, _myColor);
                            CleanMoveStatus();
                            _selectedMyPiece = false;
                        }
                    }
                }
            }
        }

        protected void SetBoard()
        {
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
            aLine[0] = new Board(GameObject.Find("A8"), GameObject.Find("BlackRook1"), new Rook(Support.Color.BLACK), Support.Color.WHITE);
            aLine[1] = new Board(GameObject.Find("A7"), GameObject.Find("BlackPawn1"), new Pawn(Support.Color.BLACK), Support.Color.BLACK);
            aLine[2] = new Board(GameObject.Find("A6"), Support.Color.WHITE);
            aLine[3] = new Board(GameObject.Find("A5"), Support.Color.BLACK);
            aLine[4] = new Board(GameObject.Find("A4"), Support.Color.WHITE);
            aLine[5] = new Board(GameObject.Find("A3"), Support.Color.BLACK);
            aLine[6] = new Board(GameObject.Find("A2"), GameObject.Find("WhitePawn1"), new Pawn(Support.Color.WHITE), Support.Color.WHITE);
            aLine[7] = new Board(GameObject.Find("A1"), GameObject.Find("WhiteRook1"), new Rook(Support.Color.WHITE), Support.Color.BLACK);

            //B열 초기화
            bLine[0] = new Board(GameObject.Find("B8"), GameObject.Find("BlackKnight1"), new Knight(Support.Color.BLACK), Support.Color.BLACK);
            bLine[1] = new Board(GameObject.Find("B7"), GameObject.Find("BlackPawn2"), new Pawn(Support.Color.BLACK), Support.Color.WHITE);
            bLine[2] = new Board(GameObject.Find("B6"), Support.Color.BLACK);
            bLine[3] = new Board(GameObject.Find("B5"), Support.Color.WHITE);
            bLine[4] = new Board(GameObject.Find("B4"), Support.Color.BLACK);
            bLine[5] = new Board(GameObject.Find("B3"), Support.Color.WHITE);
            bLine[6] = new Board(GameObject.Find("B2"), GameObject.Find("WhitePawn2"), new Pawn(Support.Color.WHITE), Support.Color.BLACK);
            bLine[7] = new Board(GameObject.Find("B1"), GameObject.Find("WhiteKnight1"), new Knight(Support.Color.WHITE), Support.Color.WHITE);

            //C열 초기화
            cLine[0] = new Board(GameObject.Find("C8"), GameObject.Find("BlackBishop1"), new Bishop(Support.Color.BLACK), Support.Color.WHITE);
            cLine[1] = new Board(GameObject.Find("C7"), GameObject.Find("BlackPawn3"), new Pawn(Support.Color.BLACK), Support.Color.BLACK);
            cLine[2] = new Board(GameObject.Find("C6"), Support.Color.WHITE);
            cLine[3] = new Board(GameObject.Find("C5"), Support.Color.BLACK);
            cLine[4] = new Board(GameObject.Find("C4"), Support.Color.WHITE);
            cLine[5] = new Board(GameObject.Find("C3"), Support.Color.BLACK);
            cLine[6] = new Board(GameObject.Find("C2"), GameObject.Find("WhitePawn3"), new Pawn(Support.Color.WHITE), Support.Color.WHITE);
            cLine[7] = new Board(GameObject.Find("C1"), GameObject.Find("WhiteBishop1"), new Bishop(Support.Color.WHITE), Support.Color.BLACK);

            //D열 초기화
            dLine[0] = new Board(GameObject.Find("D8"), GameObject.Find("BlackQueen"), new Queen(Support.Color.BLACK), Support.Color.BLACK);
            dLine[1] = new Board(GameObject.Find("D7"), GameObject.Find("BlackPawn4"), new Pawn(Support.Color.BLACK), Support.Color.WHITE);
            dLine[2] = new Board(GameObject.Find("D6"), Support.Color.BLACK);
            dLine[3] = new Board(GameObject.Find("D5"), Support.Color.WHITE);
            dLine[4] = new Board(GameObject.Find("D4"), Support.Color.BLACK);
            dLine[5] = new Board(GameObject.Find("D3"), Support.Color.WHITE);
            dLine[6] = new Board(GameObject.Find("D2"), GameObject.Find("WhitePawn4"), new Pawn(Support.Color.WHITE), Support.Color.BLACK);
            dLine[7] = new Board(GameObject.Find("D1"), GameObject.Find("WhiteQueen"), new Queen(Support.Color.WHITE), Support.Color.WHITE);

            //E열 초기화
            eLine[0] = new Board(GameObject.Find("E8"), GameObject.Find("BlackKing"), new King(Support.Color.BLACK), Support.Color.WHITE);
            eLine[1] = new Board(GameObject.Find("E7"), GameObject.Find("BlackPawn5"), new Pawn(Support.Color.BLACK), Support.Color.BLACK);
            eLine[2] = new Board(GameObject.Find("E6"), Support.Color.WHITE);
            eLine[3] = new Board(GameObject.Find("E5"), Support.Color.BLACK);
            eLine[4] = new Board(GameObject.Find("E4"), Support.Color.WHITE);
            eLine[5] = new Board(GameObject.Find("E3"), Support.Color.BLACK);
            eLine[6] = new Board(GameObject.Find("E2"), GameObject.Find("WhitePawn5"), new Pawn(Support.Color.WHITE), Support.Color.WHITE);
            eLine[7] = new Board(GameObject.Find("E1"), GameObject.Find("WhiteKing"), new King(Support.Color.WHITE), Support.Color.BLACK);

            //F열 초기화
            fLine[0] = new Board(GameObject.Find("F8"), GameObject.Find("BlackBishop2"), new Bishop(Support.Color.BLACK), Support.Color.BLACK);
            fLine[1] = new Board(GameObject.Find("F7"), GameObject.Find("BlackPawn6"), new Pawn(Support.Color.BLACK), Support.Color.WHITE);
            fLine[2] = new Board(GameObject.Find("F6"), Support.Color.BLACK);
            fLine[3] = new Board(GameObject.Find("F5"), Support.Color.WHITE);
            fLine[4] = new Board(GameObject.Find("F4"), Support.Color.BLACK);
            fLine[5] = new Board(GameObject.Find("F3"), Support.Color.WHITE);
            fLine[6] = new Board(GameObject.Find("F2"), GameObject.Find("WhitePawn6"), new Pawn(Support.Color.WHITE), Support.Color.BLACK);
            fLine[7] = new Board(GameObject.Find("F1"), GameObject.Find("WhiteBishop2"), new Bishop(Support.Color.WHITE), Support.Color.WHITE);

            //G열 초기화
            gLine[0] = new Board(GameObject.Find("G8"), GameObject.Find("BlackKnight2"), new Knight(Support.Color.BLACK), Support.Color.WHITE);
            gLine[1] = new Board(GameObject.Find("G7"), GameObject.Find("BlackPawn7"), new Pawn(Support.Color.BLACK), Support.Color.BLACK);
            gLine[2] = new Board(GameObject.Find("G6"), Support.Color.WHITE);
            gLine[3] = new Board(GameObject.Find("G5"), Support.Color.BLACK);
            gLine[4] = new Board(GameObject.Find("G4"), Support.Color.WHITE);
            gLine[5] = new Board(GameObject.Find("G3"), Support.Color.BLACK);
            gLine[6] = new Board(GameObject.Find("G2"), GameObject.Find("WhitePawn7"), new Pawn(Support.Color.WHITE), Support.Color.WHITE);
            gLine[7] = new Board(GameObject.Find("G1"), GameObject.Find("WhiteKnight2"), new Knight(Support.Color.WHITE), Support.Color.BLACK);

            //H열 초기화
            hLine[0] = new Board(GameObject.Find("H8"), GameObject.Find("BlackRook2"), new Rook(Support.Color.BLACK), Support.Color.BLACK);
            hLine[1] = new Board(GameObject.Find("H7"), GameObject.Find("BlackPawn8"), new Pawn(Support.Color.BLACK), Support.Color.WHITE);
            hLine[2] = new Board(GameObject.Find("H6"), Support.Color.BLACK);
            hLine[3] = new Board(GameObject.Find("H5"), Support.Color.WHITE);
            hLine[4] = new Board(GameObject.Find("H4"), Support.Color.BLACK);
            hLine[5] = new Board(GameObject.Find("H3"), Support.Color.WHITE);
            hLine[6] = new Board(GameObject.Find("H2"), GameObject.Find("WhitePawn8"), new Pawn(Support.Color.WHITE), Support.Color.BLACK);
            hLine[7] = new Board(GameObject.Find("H1"), GameObject.Find("WhiteRook2"), new Rook(Support.Color.WHITE), Support.Color.WHITE);

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
            Invoke(() =>
            {
                //TxtDebug.text += relayForm.Color + ", " + relayForm.StartLocation.ToString() + "," + relayForm.EndLocation.ToString() + "\n";
                if (relayForm.Pattern == Pattern.MOVE)
                {
                    var pieceLocation = relayForm.StartLocation;
                    var boardLocation = relayForm.EndLocation;

                    _objectMoveManager.Move(
                        _board[pieceLocation.X][pieceLocation.Y].PieceObj,
                        _board[boardLocation.X][boardLocation.Y].BoardObj);

                    // 기물 모델이 있으면 제거
                    if (_board[boardLocation.X][boardLocation.Y].Piece != null)
                    {
                        Destroy(_board[boardLocation.X][boardLocation.Y].PieceObj);
                        _effectManager.Kill(_board, boardLocation);
                    }

                    MovePieceData(pieceLocation, boardLocation);

                    CleanMoveStatus();
                }
                else if (relayForm.Pattern == Pattern.PROMOTION)
                {
                    var pieceLocation = relayForm.EndLocation;
                    var colorAndPiece = relayForm.Color + relayForm.PromotionType;

                    Piece newPiece = null;

                    switch (relayForm.PromotionType)
                    {
                        case "Rook":
                            newPiece = new Rook(relayForm.Color);
                            newPiece.IsPossibleCastling = false;
                            break;
                        case "Knight":
                            newPiece = new Knight(relayForm.Color);
                            break;
                        case "Bishop":
                            newPiece = new Bishop(relayForm.Color);
                            break;
                        case "Queen":
                            newPiece = new Queen(relayForm.Color);
                            break;
                    }

                    Destroy(_board[pieceLocation.X][pieceLocation.Y].PieceObj);

                    var newPieceObj = Instantiate(Resources.Load<GameObject>("3D/Piece/" + colorAndPiece));
                        newPieceObj.SetPosition(_board, pieceLocation);

                    _board[pieceLocation.X][pieceLocation.Y].Piece = newPiece;
                    _board[pieceLocation.X][pieceLocation.Y].PieceObj = newPieceObj;
                    
                    _effectManager.Promotion(_board, pieceLocation);
                }
                else if (relayForm.Pattern == Pattern.CASTLING)
                {
                    var pieceLocation = relayForm.StartLocation;
                    var boardLocation = relayForm.EndLocation;
                    var castlingPieceLocation = relayForm.CastlingStartLocation;
                    var castlingBoardLocation = relayForm.CastlingEndLocation;

                    _objectMoveManager.Move(
                        _board[pieceLocation.X][pieceLocation.Y].PieceObj,
                        _board[boardLocation.X][boardLocation.Y].BoardObj);

                    _objectMoveManager.Move(
                        _board[castlingPieceLocation.X][castlingPieceLocation.Y].PieceObj,
                        _board[castlingBoardLocation.X][castlingBoardLocation.Y].BoardObj);

                    MovePieceData(pieceLocation, boardLocation);
                    MovePieceData(castlingPieceLocation, castlingBoardLocation);

                    CleanMoveStatus();
                }
                else if (relayForm.Pattern == Pattern.ENPASSANT)
                {

                }

                if (relayForm.TurnFinished)
                {
                    _isMyTurn = !_isMyTurn;
                }
            });
        }

        protected void OnResultBattle(object sender, ResultForm resultForm)
        {
            Invoke(() =>
            {
                // 정상적인 게임 종료
                if (resultForm.Pattern == Pattern.FINISH)
                {
                    // TODO
                    TxtDebug.text += "game over\n";
                    NextPage("ResultPage");
                }
                // 항복을 통한 게임 종료
                else if (resultForm.Pattern == Pattern.SURRENDER)
                {
                    // TODO
                    NextPage("ResultPage");
                }
            });
        }

        private void OnClickPromotion(string pieceType)
        {
            _networkManager.Relay(new RelayForm
            {
                Pattern = Pattern.PROMOTION,
                EndLocation = _endLocation,
                PromotionType = pieceType,
                Color = _myColor,
                TurnFinished = true
            });

            PromotionFrame.SetActive(false);
        }
    }
}
