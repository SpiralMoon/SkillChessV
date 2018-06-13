using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.Iuppiter;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Iuppiter : SkillKing
    {
        public Iuppiter(string color) : base(color)
        {
            this.Power = 175;
            this.Exp = 200;

            this.MaxHp = 680;
            this.MaxMp = 1000;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = ClassCode.Iuppiter;
            this.Element = Element.LIGHTNING;

            this.Skill = new Skill[]
            {
                new AuraSphere(this),
                new ElectricSphere(this),
                new Judgment(this)
            };

            Init();
        }
    }
}
