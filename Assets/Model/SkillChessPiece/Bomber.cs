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
    public class Bomber : SkillPawn
    {
        public Bomber(string color) : base(color)
        {
            this.Power = 100;
            this.Exp = 50;

            this.MaxHp = 500;
            this.MaxMp = 250;
            this.MaxExp = new int[] { 125, 200 };

            this.ClassCode = Support.ClassCode.BOMBER;
            this.Element = Element.FIRE;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
