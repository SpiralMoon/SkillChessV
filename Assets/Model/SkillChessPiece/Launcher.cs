using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.Launcher;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Launcher : SkillPawn
    {
        public Launcher(string color) : base(color)
        {
            this.Power = 100;
            this.Exp = 50;

            this.MaxHp = 350;
            this.MaxMp = 300;
            this.MaxExp = new int[] { 125, 200 };

            this.ClassCode = ClassCode.Launcher;
            this.Element = Element.WATER;

            this.Skill = new Skill[]
            {
                new AquaBurst(this),
                new BioChemistryMissile(this),
                new AquaCannon(this)
            };

            Init();
        }
    }
}
