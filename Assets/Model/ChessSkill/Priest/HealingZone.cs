using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Priest
{
    public class HealingZone : Skill
    {
        public HealingZone(SkillPiece owner) : base(owner)
        {
            this.Code = 1322;
            this.Element = Element.HOLY;
            this.Power = 75;
            this.Mp = 100;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            board[x][y].IsPossibleSkill = true;
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            _effectManager.SkillScopeSelf(board, x, y);
        }

        protected override IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
