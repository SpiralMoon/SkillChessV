using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.ElementalKnight
{
    public class ElectricField : Skill
    {
        public ElectricField(SkillPiece owner) : base(owner)
        {
            this.Code = 1112;
            this.Element = Element.LIGHTNING;
            this.Power = 90;
            this.Mp = 125;

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
