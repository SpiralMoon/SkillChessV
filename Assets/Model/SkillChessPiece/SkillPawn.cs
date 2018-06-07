using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillPawn : SkillPiece
    {
        public SkillPawn(string color) : base(color)
        {
            this.PieceName = "Pawn";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = true;
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
