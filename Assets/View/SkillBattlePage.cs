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
using Assets.Model.ChessSkill;
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
        public GameObject TypeSprites;

        public GameObject PieceSprites;

        public GameObject ClassSprites;

        public GameObject SkillFrame;

        public GameObject SelectedClass;

        public GameObject SelectedType;

        public GameObject SelectedPiece;

        public Text TxtSelectedClassName;

        public Text TxtSelectedLevel;

        public Text TxtSelectedStatus;

        public Text TxtSelectedStatusTurn;

        public Slider SldSelectedHp;

        public Slider SldSelectedMp;

        public Slider SldSelectedExp;

        public Text TxtSelectedHp;

        public Text TxtSelectedMp;

        public GameObject SkillExplain;

        public GameObject SkillElement;

        public Text TxtSkillExplain;

        public Text TxtSkillName;

        public Button BtnAttack;

        public Button BtnSkill1;

        public Button BtnSkill2;

        public Button BtnSkill3;

        /// <summary>
        /// Attack을 선택한 상태일 때
        /// </summary>
        private bool _onAttack;

        /// <summary>
        /// Skill을 선택한 상태일 때
        /// </summary>
        private bool _onSkill;

        /// <summary>
        /// 스킬 동작 함수의 실행 상태
        /// </summary>
        private bool _isSkillRunning;

        /// <summary>
        /// 선택한 스킬 레벨
        /// </summary>
        private int _selectedSkillLevel;

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
            _whiteDashBoard = new DashBoard();
            _blackDashBoard = new DashBoard();

            SkillFrame.SetActive(true);

            BtnAttack.onClick.AddListener(OnClickAttack);
            BtnSkill1.onClick.AddListener(delegate { OnClickSkill(1); });
            BtnSkill2.onClick.AddListener(delegate { OnClickSkill(2); });
            BtnSkill3.onClick.AddListener(delegate { OnClickSkill(3); });

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

                // 화면 하단에 스킬 아이콘 업데이트
                var piece = _board[GetLocation(_selectedObject).X][GetLocation(_selectedObject).Y].Piece as SkillPiece;
                if (piece?.Color == _myColor)
                {
                    SetSkillIcons(piece);
                }
                else
                {
                    CleanSkillIcons();
                }

                // 이번 턴이 내 턴이고 실행중인 스킬이 완전히 끝난 경우
                if (_isMyTurn && !_isSkillRunning)
                {
                    if (_endLocation == null)
                    {
                        return;
                    }

                    var targetCell = _board[_endLocation.X][_endLocation.Y];

                    // 공격 & 무빙
                    if (_onAttack)
                    {
                        if (targetCell.IsPossibleAttack)
                        {

                        }
                        else if (targetCell.IsPossibleMove)
                        {

                        }
                        else if (targetCell.Piece?.Color == _myColor)
                        {

                        }
                        else
                        {

                        }
                    }

                    // 스킬
                    if (_onSkill)
                    {
                        // 스킬을 사용할 수 있는 경우
                        if (targetCell.IsPossibleSkill)
                        {
                            _networkManager.Relay(new RelayForm
                            {
                                Pattern = Pattern.SKILL,
                                StartLocation = _startLocation,
                                EndLocation = _endLocation,
                                SkillLevel = _selectedSkillLevel,
                                Color = _myColor,
                                TurnFinished = true
                            });
                            _onSkill = false;
                            _selectedMyPiece = false;
                        }
                        // 스킬을 사용할 수 없고 내 기물을 재선택한 경우
                        else if (targetCell.Piece?.Color == _myColor)
                        {
                            _startLocation = _endLocation;
                            _effectManager.Select(_board, _endLocation, _myColor);
                        }
                        // 스킬을 사용할 수 없는 경우
                        else
                        {
                            _effectManager.Select(_board, _endLocation, _myColor);
                        }

                        CleanSkillStatus();
                    }
                }
            }
        }

        /// <summary>
        /// Piece의 정보를 화면 우측에 표현
        /// </summary>
        /// <param name="piece"></param>
        private void OnGUI()
        {
            if (_startLocation == null)
            {
                return;
            }

            var piece = _board[_startLocation.X][_startLocation.Y].Piece as SkillPiece;

            if (piece != null)
            {
                SelectedPiece.SetActive(true);
                SelectedClass.SetActive(true);
                SelectedType.SetActive(true);

                /*
                 * HP, MP, EXP, Level 표시
                 */

                SldSelectedHp.maxValue = piece.MaxHp;
                SldSelectedHp.value = piece.CurrentHp;
                TxtSelectedHp.text = $"{piece.CurrentHp} / {piece.MaxHp}";

                TxtSelectedLevel.text = piece.Level + "";

                if (piece.Color == _myColor)
                {
                    SldSelectedMp.maxValue = piece.MaxMp;
                    SldSelectedMp.value = piece.CurrentMp;

                    // 레벨이 1, 2이면 정상적으로 출력
                    if (piece.Level == 1 || piece.Level == 2)
                    {
                        SldSelectedExp.maxValue = piece.MaxExp[piece.Level - 1];
                        SldSelectedExp.value = piece.CurrentExp;
                    }
                    // 레벨이 3이면 꽉 채워서 출력
                    else
                    {
                        SldSelectedExp.maxValue = 1;
                        SldSelectedExp.value = 1;
                    }

                    TxtSelectedMp.text = $"{piece.CurrentMp} / {piece.MaxMp}";
                }
                else
                {
                    SldSelectedMp.maxValue = 1;
                    SldSelectedMp.value = 0;
                    SldSelectedExp.maxValue = 1;
                    SldSelectedExp.value = 0;

                    TxtSelectedMp.text = "???";
                }

                /*
                 * Piece, Class, Type, Status 표시
                 */

                TxtSelectedClassName.text = piece.ClassName;

                string element = null;
                switch (piece.Element)
                {
                    case Element.NORMAL:
                        element = "Normal";
                        break;
                    case Element.ICE:
                        element = "Ice";
                        break;
                    case Element.FIRE:
                        element = "Fire";
                        break;
                    case Element.HOLY:
                        element = "Holy";
                        break;
                    case Element.DARK:
                        element = "Dark";
                        break;
                    case Element.LIGHTNING:
                        element = "Lightning";
                        break;
                    case Element.POISON:
                        element = "Poison";
                        break;
                    case Element.WATER:
                        element = "Water";
                        break;
                }

                SelectedPiece.GetComponent<Image>().sprite = PieceSprites.transform.Find(piece.PieceName).GetComponent<Image>().sprite;
                SelectedClass.GetComponent<Image>().sprite = ClassSprites.transform.Find(piece.GetType().Name).GetComponent<Image>().sprite;
                SelectedType.GetComponent<Image>().sprite = TypeSprites.transform.Find(element).GetComponent<Image>().sprite;

                string status = null;
                switch (piece.Status)
                {
                    case Status.NONE:
                        status = "(Status : Normal)";
                        break;
                    case Status.FREEZING:
                        status = "<color=#00C6ED>(Status : Freezing)</color>";
                        break;
                    case Status.BURN:
                        status = "<color=#ED4C00>(Status : Burn)</color>";
                        break;
                    case Status.INVINCIBLE:
                        status = "<color=#FFFF6C>(Status : Invincible)</color>";
                        break;
                    case Status.STUN:
                        status = "<color=#FFE400>(Status : Stun)</color>";
                        break;
                    case Status.POISONING:
                        status = "<color=#47C83E>(Status : Poisoning)</color>";
                        break;
                }

                TxtSelectedStatus.text = status;
                if (piece.StatusCount > 0)
                {
                    TxtSelectedStatusTurn.text = (piece.Color == _myColor)
                        ? $"{piece.StatusCount} turn remained"
                        : "? turn remained";
                }
                else
                {
                    TxtSelectedStatusTurn.text = "";
                }
            }
            else
            {
                SelectedPiece.SetActive(false);
                SelectedClass.SetActive(false);
                SelectedType.SetActive(false);

                SldSelectedHp.maxValue = 1;
                SldSelectedHp.value = 0;
                SldSelectedMp.maxValue = 1;
                SldSelectedMp.value = 0;
                SldSelectedExp.maxValue = 1;
                SldSelectedExp.value = 0;

                TxtSelectedHp.text = "";
                TxtSelectedMp.text = "";
                TxtSelectedLevel.text = "";

                TxtSelectedClassName.text = "";
                TxtSelectedStatus.text = "";
                TxtSelectedStatusTurn.text = "";
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
            aLine[0] = new Board(GameObject.Find("A8"), GameObject.Find("BlackRook1"), AllocateClass(myLineUp.Rook[0], Support.Color.BLACK), Support.Color.WHITE);
            aLine[1] = new Board(GameObject.Find("A7"), GameObject.Find("BlackPawn1"), AllocateClass(myLineUp.Pawn[0], Support.Color.BLACK), Support.Color.BLACK);
            aLine[2] = new Board(GameObject.Find("A6"), Support.Color.WHITE);
            aLine[3] = new Board(GameObject.Find("A5"), Support.Color.BLACK);
            aLine[4] = new Board(GameObject.Find("A4"), Support.Color.WHITE);
            aLine[5] = new Board(GameObject.Find("A3"), Support.Color.BLACK);
            aLine[6] = new Board(GameObject.Find("A2"), GameObject.Find("WhitePawn1"), AllocateClass(myLineUp.Pawn[0], Support.Color.WHITE), Support.Color.WHITE);
            aLine[7] = new Board(GameObject.Find("A1"), GameObject.Find("WhiteRook1"), AllocateClass(myLineUp.Rook[0], Support.Color.WHITE), Support.Color.BLACK);

            //B열 초기화
            bLine[0] = new Board(GameObject.Find("B8"), GameObject.Find("BlackKnight1"), AllocateClass(myLineUp.Knight[0], Support.Color.BLACK), Support.Color.BLACK);
            bLine[1] = new Board(GameObject.Find("B7"), GameObject.Find("BlackPawn2"), AllocateClass(myLineUp.Pawn[1], Support.Color.BLACK), Support.Color.WHITE);
            bLine[2] = new Board(GameObject.Find("B6"), Support.Color.BLACK);
            bLine[3] = new Board(GameObject.Find("B5"), Support.Color.WHITE);
            bLine[4] = new Board(GameObject.Find("B4"), Support.Color.BLACK);
            bLine[5] = new Board(GameObject.Find("B3"), Support.Color.WHITE);
            bLine[6] = new Board(GameObject.Find("B2"), GameObject.Find("WhitePawn2"), AllocateClass(myLineUp.Pawn[1], Support.Color.WHITE), Support.Color.BLACK);
            bLine[7] = new Board(GameObject.Find("B1"), GameObject.Find("WhiteKnight1"), AllocateClass(myLineUp.Knight[0], Support.Color.WHITE), Support.Color.WHITE);

            //C열 초기화
            cLine[0] = new Board(GameObject.Find("C8"), GameObject.Find("BlackBishop1"), AllocateClass(myLineUp.Bishop[0], Support.Color.BLACK), Support.Color.WHITE);
            cLine[1] = new Board(GameObject.Find("C7"), GameObject.Find("BlackPawn3"), AllocateClass(myLineUp.Pawn[2], Support.Color.BLACK), Support.Color.BLACK);
            cLine[2] = new Board(GameObject.Find("C6"), Support.Color.WHITE);
            cLine[3] = new Board(GameObject.Find("C5"), Support.Color.BLACK);
            cLine[4] = new Board(GameObject.Find("C4"), Support.Color.WHITE);
            cLine[5] = new Board(GameObject.Find("C3"), Support.Color.BLACK);
            cLine[6] = new Board(GameObject.Find("C2"), GameObject.Find("WhitePawn3"), AllocateClass(myLineUp.Pawn[2], Support.Color.WHITE), Support.Color.WHITE);
            cLine[7] = new Board(GameObject.Find("C1"), GameObject.Find("WhiteBishop1"), AllocateClass(myLineUp.Bishop[0], Support.Color.WHITE), Support.Color.BLACK);

            //D열 초기화
            dLine[0] = new Board(GameObject.Find("D8"), GameObject.Find("BlackQueen"), AllocateClass(myLineUp.Queen, Support.Color.BLACK), Support.Color.BLACK);
            dLine[1] = new Board(GameObject.Find("D7"), GameObject.Find("BlackPawn4"), AllocateClass(myLineUp.Pawn[3], Support.Color.BLACK), Support.Color.WHITE);
            dLine[2] = new Board(GameObject.Find("D6"), Support.Color.BLACK);
            dLine[3] = new Board(GameObject.Find("D5"), Support.Color.WHITE);
            dLine[4] = new Board(GameObject.Find("D4"), Support.Color.BLACK);
            dLine[5] = new Board(GameObject.Find("D3"), Support.Color.WHITE);
            dLine[6] = new Board(GameObject.Find("D2"), GameObject.Find("WhitePawn4"), AllocateClass(myLineUp.Pawn[3], Support.Color.WHITE), Support.Color.BLACK);
            dLine[7] = new Board(GameObject.Find("D1"), GameObject.Find("WhiteQueen"), AllocateClass(myLineUp.Queen, Support.Color.WHITE), Support.Color.WHITE);

            //E열 초기화
            eLine[0] = new Board(GameObject.Find("E8"), GameObject.Find("BlackKing"), AllocateClass(myLineUp.King, Support.Color.BLACK), Support.Color.WHITE);
            eLine[1] = new Board(GameObject.Find("E7"), GameObject.Find("BlackPawn5"), AllocateClass(myLineUp.Pawn[4], Support.Color.BLACK), Support.Color.BLACK);
            eLine[2] = new Board(GameObject.Find("E6"), Support.Color.WHITE);
            eLine[3] = new Board(GameObject.Find("E5"), Support.Color.BLACK);
            eLine[4] = new Board(GameObject.Find("E4"), Support.Color.WHITE);
            eLine[5] = new Board(GameObject.Find("E3"), Support.Color.BLACK);
            eLine[6] = new Board(GameObject.Find("E2"), GameObject.Find("WhitePawn5"), AllocateClass(myLineUp.Pawn[4], Support.Color.WHITE), Support.Color.WHITE);
            eLine[7] = new Board(GameObject.Find("E1"), GameObject.Find("WhiteKing"), AllocateClass(myLineUp.King, Support.Color.WHITE), Support.Color.BLACK);

            //F열 초기화
            fLine[0] = new Board(GameObject.Find("F8"), GameObject.Find("BlackBishop2"), AllocateClass(myLineUp.Bishop[1], Support.Color.BLACK), Support.Color.BLACK);
            fLine[1] = new Board(GameObject.Find("F7"), GameObject.Find("BlackPawn6"), AllocateClass(myLineUp.Pawn[5], Support.Color.BLACK), Support.Color.WHITE);
            fLine[2] = new Board(GameObject.Find("F6"), Support.Color.BLACK);
            fLine[3] = new Board(GameObject.Find("F5"), Support.Color.WHITE);
            fLine[4] = new Board(GameObject.Find("F4"), Support.Color.BLACK);
            fLine[5] = new Board(GameObject.Find("F3"), Support.Color.WHITE);
            fLine[6] = new Board(GameObject.Find("F2"), GameObject.Find("WhitePawn6"), AllocateClass(myLineUp.Pawn[5], Support.Color.WHITE), Support.Color.BLACK);
            fLine[7] = new Board(GameObject.Find("F1"), GameObject.Find("WhiteBishop2"), AllocateClass(myLineUp.Bishop[1], Support.Color.WHITE), Support.Color.WHITE);

            //G열 초기화
            gLine[0] = new Board(GameObject.Find("G8"), GameObject.Find("BlackKnight2"), AllocateClass(myLineUp.Knight[1], Support.Color.BLACK), Support.Color.WHITE);
            gLine[1] = new Board(GameObject.Find("G7"), GameObject.Find("BlackPawn7"), AllocateClass(myLineUp.Pawn[6], Support.Color.BLACK), Support.Color.BLACK);
            gLine[2] = new Board(GameObject.Find("G6"), Support.Color.WHITE);
            gLine[3] = new Board(GameObject.Find("G5"), Support.Color.BLACK);
            gLine[4] = new Board(GameObject.Find("G4"), Support.Color.WHITE);
            gLine[5] = new Board(GameObject.Find("G3"), Support.Color.BLACK);
            gLine[6] = new Board(GameObject.Find("G2"), GameObject.Find("WhitePawn7"), AllocateClass(myLineUp.Pawn[6], Support.Color.WHITE), Support.Color.WHITE);
            gLine[7] = new Board(GameObject.Find("G1"), GameObject.Find("WhiteKnight2"), AllocateClass(myLineUp.Knight[1], Support.Color.WHITE), Support.Color.BLACK);

            //H열 초기화
            hLine[0] = new Board(GameObject.Find("H8"), GameObject.Find("BlackRook2"), AllocateClass(myLineUp.Rook[1], Support.Color.BLACK), Support.Color.BLACK);
            hLine[1] = new Board(GameObject.Find("H7"), GameObject.Find("BlackPawn8"), AllocateClass(myLineUp.Pawn[7], Support.Color.BLACK), Support.Color.WHITE);
            hLine[2] = new Board(GameObject.Find("H6"), Support.Color.BLACK);
            hLine[3] = new Board(GameObject.Find("H5"), Support.Color.WHITE);
            hLine[4] = new Board(GameObject.Find("H4"), Support.Color.BLACK);
            hLine[5] = new Board(GameObject.Find("H3"), Support.Color.WHITE);
            hLine[6] = new Board(GameObject.Find("H2"), GameObject.Find("WhitePawn8"), AllocateClass(myLineUp.Pawn[7], Support.Color.WHITE), Support.Color.BLACK);
            hLine[7] = new Board(GameObject.Find("H1"), GameObject.Find("WhiteRook2"), AllocateClass(myLineUp.Rook[1], Support.Color.WHITE), Support.Color.WHITE);

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
            switch ((ClassCode)classCode)
            {
                // Pawn
                case ClassCode.ElementalKnight:
                    piece = new ElementalKnight(color);
                    break;
                case ClassCode.Bomber:
                    piece = new Bomber(color);
                    break;
                case ClassCode.Fighter:
                    piece = new Fighter(color);
                    break;
                case ClassCode.Launcher:
                    piece = new Launcher(color);
                    break;
                
                // Rook
                case ClassCode.Assassin:
                    piece = new Assassin(color);
                    break;
                case ClassCode.FireBat:
                    piece = new FireBat(color);
                    break;

                // Bishop
                case ClassCode.Archmage:
                    piece = new Archmage(color);
                    break;
                case ClassCode.Priest:
                    piece = new Priest(color);
                    break;

                // Knight
                case ClassCode.Archer:
                    piece = new Archer(color);
                    break;
                case ClassCode.Paladin:
                    piece = new Paladin(color);
                    break;

                // Queen
                case ClassCode.BlackMagician:
                    piece = new BlackMagician(color);
                    break;
                case ClassCode.Lich:
                    piece = new Lich(color);
                    break;

                // King
                case ClassCode.Angel:
                    piece = new Angel(color);
                    break;
                case ClassCode.Iuppiter:
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

        /// <summary>
        /// 모든 발판의 IsPossibleAttack 초기화.
        /// </summary>
        protected void CleanAttackStatus()
        {
            foreach (var line in _board)
            {
                foreach (var cell in line)
                {
                    cell.IsPossibleAttack = false;
                }
            }
        }

        /// <summary>
        /// 모든 발판의 IsPossibleSkill 초기화.
        /// </summary>
        protected void CleanSkillStatus()
        {
            foreach (var line in _board)
            {
                foreach (var cell in line)
                {
                    cell.IsPossibleSkill = false;
                }
            }
        }

        protected void OnRelayBattle(object sender, RelayForm relayForm)
        {
            Invoke(() =>
            {
                if (relayForm.Pattern == Pattern.MOVE)
                {
                    var pieceLocation = relayForm.StartLocation;
                    var boardLocation = relayForm.EndLocation;

                    _objectMoveManager.Move(
                        _board[pieceLocation.X][pieceLocation.Y].PieceObj,
                        _board[boardLocation.X][boardLocation.Y].BoardObj);

                    // move counting
                    if (relayForm.Color == Support.Color.WHITE)
                    {
                        _whiteDashBoard.MovingCount++;
                    }
                    else
                    {
                        _blackDashBoard.MovingCount++;
                    }

                    // 기물 모델이 있으면 제거
                    if (_board[boardLocation.X][boardLocation.Y].Piece != null)
                    {
                        Destroy(_board[boardLocation.X][boardLocation.Y].PieceObj);
                        _effectManager.Kill(_board, boardLocation);

                        // Kill and Death counting
                        if (relayForm.Color == Support.Color.WHITE)
                        {
                            _whiteDashBoard.KillCount++;
                            _blackDashBoard.DeathCount++;
                        }
                        else
                        {
                            _blackDashBoard.KillCount++;
                            _whiteDashBoard.DeathCount++;
                        }
                    }

                    MovePieceData(pieceLocation, boardLocation);

                    CleanMoveStatus();
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
                else if (relayForm.Pattern == Pattern.SKILL)
                {
                    var pieceLocation = relayForm.StartLocation;
                    var boardLocation = relayForm.EndLocation;
                    var i = relayForm.SkillLevel - 1;
                    var piece = _board[pieceLocation.X][pieceLocation.Y].Piece as SkillPiece;

                    _isSkillRunning = true;
                    piece.Skill[i].Trigger(_board, pieceLocation, boardLocation, OnSkillFinished);
                }

                if (relayForm.TurnFinished)
                {
                    _isMyTurn = !_isMyTurn;
                }
            });
        }

        /// <summary>
        /// 화면 하단에 스킬 아이콘 설정.
        /// </summary>
        /// <param name="piece"></param>
        private void SetSkillIcons(SkillPiece piece)
        {
            BtnSkill1.transform.Find("IMG_Icon").GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>($"UI/Icon/Skill/{piece.Skill[0].GetType().Name}"));
            BtnSkill2.transform.Find("IMG_Icon").GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>($"UI/Icon/Skill/{piece.Skill[1].GetType().Name}"));
            BtnSkill3.transform.Find("IMG_Icon").GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>($"UI/Icon/Skill/{piece.Skill[2].GetType().Name}"));
        }

        /// <summary>
        /// 화면 하단에 스킬 아이콘 삭제.
        /// </summary>
        private void CleanSkillIcons()
        {
            BtnSkill1.transform.Find("IMG_Icon").GetComponent<Image>().sprite = null;
            BtnSkill2.transform.Find("IMG_Icon").GetComponent<Image>().sprite = null;
            BtnSkill3.transform.Find("IMG_Icon").GetComponent<Image>().sprite = null;
        }

        /// <summary>
        /// 스킬 버튼을 눌렀을 때
        /// </summary>
        private void OnClickSkill(int level)
        {
            if (!_isMyTurn)
            {
                return;
            }

            var i = level - 1;
            var piece = _board[_startLocation.X][_startLocation.Y].Piece as SkillPiece;
            
            if (!piece.Skill[i].CheckCondition())
            {
                return;
            }

            _onAttack = false;
            _onSkill = true;
            
            CleanSkillStatus();
            _selectedMyPiece = true;
            _selectedSkillLevel = level;

            piece.Skill[i].SetSkillStatus(_board, _startLocation);
            piece.Skill[i].ShowSkillScope(_board, _startLocation);
        }

        /// <summary>
        /// 공격/이동 버튼을 눌렀을 때
        /// </summary>
        private void OnClickAttack()
        {
            if (!_isMyTurn)
            {
                return;
            }

            _onSkill = false;
            _onAttack = true;
        }

        /// <summary>
        /// 스킬 설명 열기
        /// </summary>
        private void OpenSkillExplain()
        {
            TxtSkillName.text = "";
            TxtSkillExplain.text = "";
            // TODO : 속성 아이콘 변경

            SkillExplain.SetActive(true);
        }

        /// <summary>
        /// 스킬 설명 닫기
        /// </summary>
        private void CloseSkillExplain()
        {
            SkillExplain.SetActive(false);
        }

        /// <summary>
        /// 스킬 실행이 모두 완료되었을 때 호출되는 이벤트
        /// </summary>
        private void OnSkillFinished()
        {
            _isSkillRunning = false;
        }

        /// <summary>
        /// 체력을 검사하고 기물을 파괴하는 함수.
        /// </summary>
        /// <returns></returns>
        private void CheckHp()
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var piece = _board[i][j].Piece as SkillPiece;

                    if (piece == null)
                    {
                        continue;
                    }

                    // 이번 회차에 체력이 0인가?
                    if (piece.CurrentHp <= 0)
                    {
                        // TODO : 경험치 계산
                        _effectManager.Kill(_board, new Location(i, j));
                        _board[i][j].Piece = null;
                        Destroy(_board[i][j].PieceObj);

                        // Kill and Death counting
                        if (piece.Color == Support.Color.WHITE)
                        {
                            _blackDashBoard.KillCount++;
                            _whiteDashBoard.DeathCount++;
                        }
                        else
                        {
                            _whiteDashBoard.KillCount++;
                            _blackDashBoard.DeathCount++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 상태이상을 검사하고 데미지를 주는 함수.
        /// </summary>
        /// <returns></returns>
        private void CheckStatus()
        {

        }
    }
}
