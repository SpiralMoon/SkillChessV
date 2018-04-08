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
    }
}
