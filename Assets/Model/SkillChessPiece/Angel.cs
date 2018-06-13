using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.Angel;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Angel : SkillKing
    {
        public Angel(string color) : base(color)
        {
            this.Power = 200;
            this.Exp = 200;

            this.MaxHp = 800;
            this.MaxMp = 800;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = ClassCode.Angel;
            this.Element = Element.HOLY;

            this.Skill = new Skill[]
            {
                new HolyArrow(this),
                new Dispel(this),
                new Genesis(this)
            };
            
            Init();
        }
    }
}
