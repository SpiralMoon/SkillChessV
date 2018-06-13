using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.BlackMagician
{
    public class DarkHole : Skill
    {
        public DarkHole(SkillPiece owner) : base(owner)
        {
            this.Code = 1511;
            this.Element = Element.DARK;
            this.Power = 0;
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
