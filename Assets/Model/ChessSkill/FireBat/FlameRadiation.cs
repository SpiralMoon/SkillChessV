﻿using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.FireBat
{
    public class FlameRadiation : Skill
    {
        public FlameRadiation(SkillPiece owner) : base(owner)
        {
            this.Code = 1223;
            this.Element = Element.FIRE;
            this.Power = 100;
            this.Mp = 300;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 0)
            {
                board[x][y - 1].IsPossibleSkill = true;
            }

            // 하
            if (y < 7)
            {
                board[x][y + 1].IsPossibleSkill = true;
            }

            // 좌
            if (x > 0)
            {
                board[x - 1][y].IsPossibleSkill = true;
            }

            // 우
            if (x < 7)
            {
                board[x + 1][y].IsPossibleSkill = true;
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 0)
            {
                _effectManager.SkillScope(board, x, y - 1);
            }

            // 하
            if (y < 7)
            {
                _effectManager.SkillScope(board, x, y + 1);
            }

            // 좌
            if (x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y);
            }

            // 우
            if (x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y);
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
