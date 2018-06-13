﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Model.ChessSkill.Paladin;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class Paladin : SkillKnight
    {
        public Paladin(string color) : base(color)
        {
            this.Power = 250;
            this.Exp = 125;

            this.MaxHp = 1000;
            this.MaxMp = 250;
            this.MaxExp = new int[] { 225, 250 };

            this.ClassCode = ClassCode.Paladin;
            this.Element = Element.HOLY;

            this.Skill = new Skill[]
            {
                new Strike(this),
                new Shield(this),
                new IronBody(this)
            };

            Init();
        }
    }
}
