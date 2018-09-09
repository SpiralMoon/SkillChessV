using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Assassin
{
    public class Assaulter : Skill
    {
        public Assaulter(SkillPiece owner) : base(owner)
        {
            this.Code = 1211;
            this.Element = Element.NORMAL;
            this.Power = 100;
            this.Mp = 100;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var lastDestination = 0; // 마지막 목적지 인덱스

            // 좌
            for (int i = x - 1, count = 0; i >= 0 && count < 4; i--, count++)
            {
                if (board[i][y].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (x > 0)
            {
                if (lastDestination > 1)
                {
                    board[x - 1][y].IsPossibleSkill = true;
                }
            }

            // 우
            lastDestination = 0;
            for (int i = x + 1, count = 0; i < 8 && count < 4; i++, count++)
            {
                if (board[i][y].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (x < 7)
            {
                if (lastDestination > 1)
                {
                    board[x + 1][y].IsPossibleSkill = true;
                }
            }
            
            // 위
            lastDestination = 0;
            for (int i = y - 1, count = 0; i >= 0 && count < 4; i--, count++)
            {
                if (board[x][i].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (y > 0)
            {
                if (lastDestination > 1)
                {
                    board[x][y - 1].IsPossibleSkill = true;
                }
            }

            // 아래
            lastDestination = 0;
            for (int i = y + 1, count = 0; i < 8 && count < 4; i++, count++)
            {
                if (board[x][i].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (y < 7)
            {
                if (lastDestination > 1)
                {
                    board[x][y + 1].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var lastDestination = 0; // 마지막 목적지 인덱스

            // 좌
            for (int i = x - 1, count = 0; i >= 0 && count < 4; i--, count++)
            {
                if (board[i][y].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (x > 0)
            {
                if (lastDestination > 1)
                {
                    _effectManager.SkillScope(board, x - 1, y);
                }
            }

            // 우
            lastDestination = 0;
            for (int i = x + 1, count = 0; i < 8 && count < 4; i++, count++)
            {
                if (board[i][y].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (x < 7)
            {
                if (lastDestination > 1)
                {
                    _effectManager.SkillScope(board, x + 1, y);
                }
            }

            // 상
            lastDestination = 0;
            for (int i = y - 1, count = 0; i >= 0 && count < 4; i--, count++)
            {
                if (board[x][i].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (y > 0)
            {
                if (lastDestination > 1)
                {
                    _effectManager.SkillScope(board, x, y - 1);
                }
            }

            // 하
            lastDestination = 0;
            for (int i = y + 1, count = 0; i < 8 && count < 4; i++, count++)
            {
                if (board[x][i].Piece == null)
                {
                    lastDestination = count;
                }
            }
            if (y < 7)
            {
                if (lastDestination > 1)
                {
                    _effectManager.SkillScope(board, x, y + 1);
                }
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
