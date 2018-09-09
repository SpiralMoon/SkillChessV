using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Iuppiter
{
    public class ElectricSphere : Skill
    {
        private readonly string _hitPath;

        public ElectricSphere(SkillPiece owner) : base(owner)
        {
            this.Code = 1622;
            this.Element = Element.LIGHTNING;
            this.Power = 150;
            this.Mp = 300;
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

            // 1시
            if (y > 2 && x < 7)
            {
                if (board[x + 1][y - 3].Piece?.Color == enemyColor)
                {
                    board[x + 1][y - 3].IsPossibleSkill = true;
                }
            }

            // 2시
            if (y > 0 && x < 5)
            {
                if (board[x + 3][y - 1].Piece?.Color == enemyColor)
                {
                    board[x + 3][y - 1].IsPossibleSkill = true;
                }
            }

            // 4시
            if (y < 7 && x < 5)
            {
                if (board[x + 3][y + 1].Piece?.Color == enemyColor)
                {
                    board[x - 3][y + 1].IsPossibleSkill = true;
                }
            }

            // 5시
            if (y < 5 && x < 7)
            {
                if (board[x + 1][y + 3].Piece?.Color == enemyColor)
                {
                    board[x + 1][y + 3].IsPossibleSkill = true;
                }
            }

            // 11시
            if (y > 2 && x > 0)
            {
                if (board[x - 1][y - 3].Piece?.Color == enemyColor)
                {
                    board[x - 1][y - 3].IsPossibleSkill = true;
                }
            }

            // 10시
            if (y > 0 && x > 2)
            {
                if (board[x - 3][y - 1].Piece?.Color == enemyColor)
                {
                    board[x - 3][y - 1].IsPossibleSkill = true;
                }
            }

            // 8시
            if (y < 7 && x > 2)
            {
                if (board[x - 3][y + 1].Piece?.Color == enemyColor)
                {
                    board[x - 3][y + 1].IsPossibleSkill = true;
                }
            }

            // 7시
            if (y < 5 && x > 0)
            {
                if (board[x - 1][y + 3].Piece?.Color == enemyColor)
                {
                    board[x - 1][y + 3].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 1시
            if (y > 2 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y - 3);
            }

            // 2시
            if (y > 0 && x < 5)
            {
                _effectManager.SkillScope(board, x + 3, y - 1);
            }

            // 4시
            if (y < 7 && x < 5)
            {
                _effectManager.SkillScope(board, x - 3, y + 1);
            }

            // 5시
            if (y < 5 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y + 3);
            }

            // 11시
            if (y > 2 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y - 3);
            }

            // 10시
            if (y > 0 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y - 1);
            }

            // 8시
            if (y < 7 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y + 1);
            }

            // 7시
            if (y < 5 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y + 3);
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
