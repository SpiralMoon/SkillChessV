using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Assassin
{
    public class Slash : Skill
    {
        public Slash(SkillPiece owner) : base(owner)
        {
            this.Code = 1212;
            this.Element = Element.NORMAL;
            this.Power = 80;
            this.Mp = 120;

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
