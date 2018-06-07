using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillQueen : SkillPiece
    {
        public SkillQueen(string color) : base(color)
        {
            this.PieceName = "Queen";
            this.Color = color;
            this.IsPossibleCastling = false;
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
