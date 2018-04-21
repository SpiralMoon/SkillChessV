using System.Collections.Generic;

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

        /// <summary>
        /// 기물이 이동할 수 있는 발판의 IsPossibleMove를 true로 변경
        /// </summary>
        /// <param name="board"></param>
        public abstract void SetMoveStatus(List<Board[]> board, Location location);

        /// <summary>
        /// 기물이 이동할 수 있는 발판과 이동할 수 없는 발판 표시
        /// </summary>
        /// <param name="board"></param>
        /// <param name="location"></param>
        public abstract void ShowMoveScope(List<Board[]> board, Location location);
    }
}
