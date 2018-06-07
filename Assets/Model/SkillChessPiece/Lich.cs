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
    public class Lich : SkillQueen
    {
        public Lich(string color) : base(color)
        {
            this.Power = 150;
            this.Exp = 175;

            this.MaxHp = 650;
            this.MaxMp = 1000;
            this.MaxExp = new int[] { 150, 275 };

            this.ClassCode = Support.ClassCode.LICH;
            this.Element = Element.POISON;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
