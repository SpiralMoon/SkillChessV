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
    public class BlackMagician : SkillQueen
    {
        public BlackMagician(string color) : base(color)
        {
            this.Power = 125;
            this.Exp = 175;

            this.MaxHp = 500;
            this.MaxMp = 1000;
            this.MaxExp = new int[] { 150, 275 };

            this.ClassCode = ClassCode.BlackMagician;
            this.Element = Element.DARK;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
