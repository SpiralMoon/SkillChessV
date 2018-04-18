using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Angel : SkillPiece
    {
        public Angel(string color) : base(color)
        {
            this.Power = 200;

            this.MaxHp = 800;
            this.MaxMp = 800;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = Support.ClassCode.ANGEL;
            this.Element = Element.HOLY;
            
            Init();
        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            throw new NotImplementedException();
        }
    }
}
