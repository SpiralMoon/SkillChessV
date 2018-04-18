using System.Collections.Generic;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// King의 모델.
    /// </summary>
    public class King : Piece
    {
        public King(string color) : base(color)
        {
            this.PieceName = "King";
            this.Color = color;
            this.IsPossibleCastling = true;
            this.IsPossibleFirstChance = false;
        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            throw new System.NotImplementedException();
        }
    }
}
