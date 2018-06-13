using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.Assassin;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Assassin : SkillRook
    {
        public Assassin(string color) : base(color)
        {
            this.Power = 150;
            this.Exp = 100;

            this.MaxHp = 800;
            this.MaxMp = 300;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = ClassCode.Assassin;
            this.Element = Element.NORMAL;

            this.Skill = new Skill[]
            {
                new Assaulter(this),
                new Slash(this),
                new Shuriken(this)
            };

            Init();
        }
    }
}
