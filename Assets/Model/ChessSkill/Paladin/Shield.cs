using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Paladin
{
    public class Shield : Skill
    {
        private readonly string _barrierPath;

        public Shield(SkillPiece owner) : base(owner)
        {
            this.Code = 1422;
            this.Element = Element.HOLY;
            this.Power = 0;
            this.Mp = 60;
            this._barrierPath = $"Effect/Skill/{Owner.GetType().Name}/Barrier";

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
