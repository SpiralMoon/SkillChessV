namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Queen의 모델.
    /// </summary>
    public class Queen : Piece
    {
        public Queen(string color) : base(color)
        {
            this.PieceName = "Queen";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }
    }
}
