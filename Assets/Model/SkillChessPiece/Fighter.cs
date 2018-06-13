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
    public class Fighter : SkillPawn
    {
        public Fighter(string color) : base(color)
        {
            this.Power = 180;
            this.Exp = 50;

            this.MaxHp = 700;
            this.MaxMp = 150;
            this.MaxExp = new int[] { 125, 200 };

            this.ClassCode = ClassCode.Fighter;
            this.Element = Element.NORMAL;

            this.Skill = new Skill[]
            {
                // TODO
            };

            Init();
        }
    }
}
