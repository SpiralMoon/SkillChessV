using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillKnight : SkillPiece
    {
        public SkillKnight(string color) : base(color)
        {
            this.PieceName = "Knight";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }

        public override void SetAttackStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowAttackScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
