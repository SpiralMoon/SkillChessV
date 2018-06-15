﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Iuppiter
{
    public class ElectricSphere : Skill
    {
        public ElectricSphere(SkillPiece owner) : base(owner)
        {
            this.Code = 1622;
            this.Element = Element.LIGHTNING;
            this.Power = 150;
            this.Mp = 300;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}