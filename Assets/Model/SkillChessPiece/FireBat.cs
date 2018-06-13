using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.FireBat;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class FireBat : SkillRook
    {
        public FireBat(string color) : base(color)
        {
            this.Power = 200;
            this.Exp = 100;

            this.MaxHp = 500;
            this.MaxMp = 500;
            this.MaxExp = new int[] { 200, 300 };

            this.ClassCode = ClassCode.FireBat;
            this.Element = Element.FIRE;

            this.Skill = new Skill[]
            {
                new FireWall(this),
                new Explosion(this),
                new FlameRadiation(this)
            };

            Init();
        }
    }
}
