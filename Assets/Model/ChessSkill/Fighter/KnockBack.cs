using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Fighter
{
    public class KnockBack : Skill
    {
        private readonly string _hitPath;

        public KnockBack(SkillPiece owner) : base(owner)
        {
            this.Code = 1131;
            this.Element = Element.NORMAL;
            this.Power = 110;
            this.Mp = 20;
            this._hitPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Hit";

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 상
            if (y > 0)
            {
                if (board[x][y - 1].Piece?.Color == enemyColor)
                {
                    board[x][y - 1].IsPossibleSkill = true;
                }
            }

            // 하
            if (y < 7)
            {
                if (board[x][y + 1].Piece?.Color == enemyColor)
                {
                    board[x][y + 1].IsPossibleSkill = true;
                }
            }

            // 좌
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color == enemyColor)
                {
                    board[x - 1][y].IsPossibleSkill = true;
                }
            }

            // 우
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color == enemyColor)
                {
                    board[x + 1][y].IsPossibleSkill = true;
                }
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

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
