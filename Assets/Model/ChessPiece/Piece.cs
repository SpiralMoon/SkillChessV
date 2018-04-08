using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model.ChessPiece
{
    public abstract class Piece
    {
        /// <summary>
        /// 기물의 종류.
        /// </summary>
        public string PieceName;

        /// <summary>
        /// 소유 플레이어의 색.
        /// </summary>
        public string Color;

        /// <summary>
        /// 캐슬링이 가능한 상태인가?
        /// </summary>
        public bool IsPossibleCastling;

        /// <summary>
        /// 첫 이동 찬스 발동이 가능한가?
        /// </summary>
        public bool IsPossibleFirstChance;

        public Piece(string color)
        {

        }
    }
}
