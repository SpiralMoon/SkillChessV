using System.Collections.Generic;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Rook의 모델.
    /// </summary>
    public class Rook : Piece
    {
        public Rook(string color) : base(color)
        {
            this.PieceName = "Rook";
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
