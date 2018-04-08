namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Bishop의 모델.
    /// </summary>
    public class Bishop : Piece
    {
        public Bishop(string color) : base(color)
        {
            this.PieceName = "Bishop";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }
    }
}
