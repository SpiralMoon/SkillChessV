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
    public class ElementalKnight : SkillPawn
    {
        public ElementalKnight (string color) : base (color)
        {
            this.Power = 100;
            this.Exp = 50;

            this.MaxHp = 400;
            this.MaxMp = 450;
            this.MaxExp = new int[] { 125, 200 };

            this.ClassCode = Support.ClassCode.ELEMENTALKNIGHT;
            this.Element = Element.NORMAL;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
