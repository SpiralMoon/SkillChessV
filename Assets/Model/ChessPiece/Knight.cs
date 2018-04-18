using System.Collections.Generic;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Knight의 모델.
    /// </summary>
    public class Knight : Piece
    {
        public Knight(string color) : base(color)
        {
            this.PieceName = "Knight";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            throw new System.NotImplementedException();
        }
    }
}
