using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.ElementalKnight;
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

            this.ClassCode = ClassCode.ElementalKnight;
            this.Element = Element.NORMAL;

            this.Skill = new Skill[]
            {
                new RainTempo(this),
                new ElectricField(this),
                new BlustBurn(this)
            };

            Init();
        }
    }
}
