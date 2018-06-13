using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.BlackMagician
{
    public class MeteorStrike : Skill
    {
        public MeteorStrike(SkillPiece owner) : base(owner)
        {
            this.Code = 1513;
            this.Element = Element.FIRE;
            this.Power = 200;
            this.Mp = 750;

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
