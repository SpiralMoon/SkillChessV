using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.FireBat
{
    public class FireWall : Skill
    {
        private readonly string _ringPath;

        public FireWall(SkillPiece owner) : base(owner)
        {
            this.Code = 1221;
            this.Element = Element.FIRE;
            this.Power = 0;
            this.Mp = 150;
            this._ringPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Ring";

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
