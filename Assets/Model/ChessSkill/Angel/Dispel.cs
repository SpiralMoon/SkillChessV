﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Service;
using Assets.Support;

namespace Assets.Model.ChessSkill.Angel
{
    public class Dispel : Skill
    {
        public Dispel(SkillPiece owner) : base(owner)
        {
            this.Code = 1612;
            this.Element = Element.HOLY;
            this.Power = 0;
            this.Mp = 220;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            board[x][y].IsPossibleSkill = true;
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            _effectManager.SkillScopeSelf(board, x, y);
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}