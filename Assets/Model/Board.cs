using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.ChessPiece;

namespace Assets.Model
{
    public class Board : MonoBehaviour
    {
        /// <summary>
        /// 발판 객체.
        /// </summary>
        public GameObject BoardObj;

        /// <summary>
        /// 기물 객체.
        /// </summary>
        public GameObject PieceObj;

        /// <summary>
        /// 이 발판 위에 서있는 기물 정보.
        /// </summary>
        public Piece Piece;

        /// <summary>
        /// 발판 색.
        /// </summary>
        public string Color;

        /// <summary>
        /// 이 발판으로 움직일 수 있는가?
        /// </summary>
        public bool IsPossibleMove;

        /// <summary>
        /// 이 발판에 스킬을 사용할 수 있는가?
        /// </summary>
        public bool IsPossibleSkill;

        /// <summary>
        /// 이 발판에 공격을 할 수 있는가?
        /// </summary>
        public bool IsPossibleAttack;

        /// <summary>
        /// 기물이 서 있는 발판의 생성자.
        /// </summary>
        /// <param name="boardObj"></param>
        /// <param name="pieceObj"></param>
        /// <param name="piece"></param>
        /// <param name="color"></param>
        public Board (GameObject boardObj, GameObject pieceObj, Piece piece, string color)
        {
            this.BoardObj = boardObj;
            this.PieceObj = pieceObj;
            this.Piece = piece;
            this.Color = color;
            this.IsPossibleMove = false;
            this.IsPossibleSkill = false;
            this.IsPossibleAttack = false;
        }

        /// <summary>
        /// 기물이 없는 발판의 생성자.
        /// </summary>
        /// <param name="boardObj"></param>
        /// <param name="color"></param>
        public Board(GameObject boardObj, string color)
        {
            this.BoardObj = boardObj;
            this.PieceObj = null;
            this.Piece = null;
            this.Color = color;
            this.IsPossibleMove = false;
            this.IsPossibleSkill = false;
            this.IsPossibleAttack = false;
        }
    }
}
