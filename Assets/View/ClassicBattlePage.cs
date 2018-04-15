﻿using System;
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
using Assets.Support.Language;
using Assets.Service;

namespace Assets.View
{
    public class ClassicBattlePage : BattlePage
    {
        private void Awake()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as BattlePageParameter;

            _matchForm = param.MatchForm;
            _myColor = param.MatchForm.Color;
            _networkManager = NetworkManager.GetInstance();
            _cameraManager = CameraManager.GetInstance();
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
        }

        private void Update()
        {
            if (!_gameStarted)
            {
                return;
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

            networkManager.OnStartBattle += OnStartBattle;
        }

        public void Invoke(Action action)
        {
           
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

        public void NextPage(string pageName)
        {
            SceneManager.LoadSceneAsync(pageName);
        }

        protected void OnStartBattle(object sender, EventArgs e)
        {
            _gameStarted = true;
        }
    }
}