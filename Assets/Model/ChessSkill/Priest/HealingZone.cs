using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
