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

        public delegate void MoveStatusDelegate(List<Board[]> board);

        /// <summary>
        /// 발판의 상태를 초기화하고,
        /// 이 기물이 이동할 수 있는 지역까지 한 번에 표시하는 체인
        /// </summary>
        public MoveStatusDelegate ResetMoveStatus;

        public Piece(string color)
        {
            ResetMoveStatus += CleanMoveStatus;
            ResetMoveStatus += SetMoveStatus;
        }

        /// <summary>
        /// 모든 발판의 IsPossibleMove를 초기화
        /// </summary>
        /// <param name="board"></param>
        private void CleanMoveStatus(List<Board[]> board)
        {
            foreach (var line in board)
            {
                foreach (var cell in line)
                {
                    cell.IsPossibleMove = false;
                }
            }
        }

        /// <summary>
        /// 기물이 이동할 수 있는 발판의 IsPossibleMove를 true로 변경
        /// </summary>
        /// <param name="board"></param>
        protected abstract void SetMoveStatus(List<Board[]> board);
    }
}
