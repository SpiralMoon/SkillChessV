using System.Collections.Generic;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Pawn의 모델.
    /// </summary>
    public class Pawn : Piece
    {
        public Pawn(string color) : base(color)
        {
            this.PieceName = "Bishop";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = true;
        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            if (Color == Support.Color.WHITE)
            {
               
            }

            if (Color == Support.Color.BLACK)
            {

            }
        }
    }
}
