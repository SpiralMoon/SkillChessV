using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Archer : SkillKnight
    {
        public Archer(string color) : base(color)
        {
            this.Power = 125;
            this.Exp = 125;

            this.MaxHp = 450;
            this.MaxMp = 300;
            this.MaxExp = new int[] { 225, 250 };

            this.ClassCode = ClassCode.Archer;
            this.Element = Element.NORMAL;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
