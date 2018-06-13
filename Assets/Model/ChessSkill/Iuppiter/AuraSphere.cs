using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Iuppiter
{
    public class AuraSphere : Skill
    {
        public AuraSphere(SkillPiece owner) : base(owner)
        {
            this.Code = 1621;
            this.Element = Element.LIGHTNING;
            this.Power = 100;
            this.Mp = 150;

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
