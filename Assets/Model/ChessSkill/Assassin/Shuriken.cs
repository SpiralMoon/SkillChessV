using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Assassin
{
    public class Shuriken : Skill
    {
        private readonly string _hitPath;

        public Shuriken(SkillPiece owner) : base(owner)
        {
            this.Code = 1213;
            this.Element = Element.NORMAL;
            this.Power = 200;
            this.Mp = 120;
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

            // 좌
            for (int i = x - 1, count = 3; i >= 0 && count >= 0; i--, count--)
            {
                if (board[i][y].Piece?.Color == enemyColor)
                {
                    board[i][y].IsPossibleSkill = true;
                    break;
                }
            }

            // 우
            for (int i = x + 1, count = 3; i < 8 && count >= 0; i++, count--)
            {
                if (board[i][y].Piece?.Color == enemyColor)
                {
                    board[i][y].IsPossibleSkill = true;
                    break;
                }
            }

            // 상
            for (int i = y - 1, count = 3; i >= 0 && count >= 0; i--, count--)
            {
                if (board[x][i].Piece?.Color == enemyColor)
                {
                    board[x][i].IsPossibleSkill = true;
                    break;
                }
            }

            // 하
            for (int i = y + 1, count = 3; i < 8 && count >= 0; i++, count--)
            {
                if (board[x][i].Piece?.Color == enemyColor)
                {
                    board[x][i].IsPossibleSkill = true;
                    break;
                }
            }

            // 좌상
            for (int i = x - 1, j = y - 1, count = 2; i >= 0 && j >= 0 && count >= 0; i--, j--, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    board[i][j].IsPossibleSkill = true;
                    break;
                }
            }

            // 우상
            for (int i = x + 1, j = y - 1, count = 2; i < 8 && j >= 0 && count >= 0; i++, j--, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    board[i][j].IsPossibleSkill = true;
                    break;
                }
            }

            // 좌하
            for (int i = x - 1, j = y + 1, count = 2; i >= 0 && j < 8 && count >= 0; i--, j++, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    board[i][j].IsPossibleSkill = true;
                    break;
                }
            }

            // 우하
            for (int i = x + 1, j = y + 1, count = 2; i < 8 && j < 8 && count >= 0; i++, j++, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    board[i][j].IsPossibleSkill = true;
                    break;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 좌
            for (int i = x - 1, count = 3; i >= 0 && count >= 0; i--, count--)
            {
                if (board[i][y].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, y);
                }
            }

            // 우
            for (int i = x + 1, count = 3; i < 8 && count >= 0; i++, count--)
            {
                if (board[i][y].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, y);
                }
            }

            // 상
            for (int i = y - 1, count = 3; i >= 0 && count >= 0; i--, count--)
            {
                if (board[x][i].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, x, i);
                }
            }

            // 하
            for (int i = y + 1, count = 3; i < 8 && count >= 0; i++, count--)
            {
                if (board[x][i].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, x, i);
                }
            }

            // 좌상
            for (int i = x - 1, j = y - 1, count = 2; i >= 0 && j >= 0 && count >= 0; i--, j--, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, j);
                }
            }

            // 우상
            for (int i = x + 1, j = y - 1, count = 2; i < 8 && j >= 0 && count >= 0; i++, j--, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, j);
                }
            }

            // 좌하
            for (int i = x - 1, j = y + 1, count = 2; i >= 0 && j < 8 && count >= 0; i--, j++, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, j);
                }
            }

            // 우하
            for (int i = x + 1, j = y + 1, count = 2; i < 8 && j < 8 && count >= 0; i++, j++, count--)
            {
                if (board[i][j].Piece?.Color == enemyColor)
                {
                    _effectManager.SkillScope(board, i, j);
                }
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
