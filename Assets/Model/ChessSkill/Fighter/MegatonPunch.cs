using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Fighter
{
    public class MegatonPunch : Skill
    {
        public MegatonPunch(SkillPiece owner) : base(owner)
        {
            this.Code = 1133;
            this.Element = Element.NORMAL;
            this.Power = 200;
            this.Mp = 125;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
