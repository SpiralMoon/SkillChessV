using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillRook : SkillPiece
    {
        public SkillRook(string color) : base(color)
        {
            this.PieceName = "Rook";
            this.Color = color;
            this.IsPossibleCastling = true;
            this.IsPossibleFirstChance = false;
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
