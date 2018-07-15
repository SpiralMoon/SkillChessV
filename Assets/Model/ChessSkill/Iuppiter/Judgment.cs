using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Iuppiter
{
    public class Judgment : Skill
    {
        private readonly string _crossPath;

        public Judgment(SkillPiece owner) : base(owner)
        {
            this.Code = 1623;
            this.Element = Element.LIGHTNING;
            this.Power = 115;
            this.Mp = 350;
            this._crossPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Cross";

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
