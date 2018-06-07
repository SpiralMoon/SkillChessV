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
    public class Priest : SkillBishop
    {
        public Priest(string color) : base(color)
        {
            this.Power = 100;
            this.Exp = 100;

            this.MaxHp = 500;
            this.MaxMp = 750;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = Support.ClassCode.PRIEST;
            this.Element = Element.HOLY;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
