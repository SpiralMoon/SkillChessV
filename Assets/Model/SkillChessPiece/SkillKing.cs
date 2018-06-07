using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillKing : SkillPiece
    {
        public SkillKing(string color) : base(color)
        {
            this.PieceName = "King";
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
