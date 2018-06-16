using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.BlackMagician
{
    public class DeathPenalty : Skill
    {
        public DeathPenalty(SkillPiece owner) : base(owner)
        {
            this.Code = 1512;
            this.Element = Element.DARK;
            this.Power = 0;
            this.Mp = 750;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (_owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 상
            if (y > 0)
            {
                if (board[x][y - 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x][y - 1].Piece is SkillKing))
                    {
                        board[x][y - 1].IsPossibleSkill = true;
                    }
                }
            }

            // 하
            if (y < 7)
            {
                if (board[x][y + 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x][y + 1].Piece is SkillKing))
                    {
                        board[x][y + 1].IsPossibleSkill = true;
                    }
                }
            }
            
            // 좌
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color == enemyColor)
                {
                    if (!(board[x - 1][y].Piece is SkillKing))
                    {
                        board[x - 1][y].IsPossibleSkill = true;
                    }
                }
            }

            // 우
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color == enemyColor)
                {
                    if (!(board[x + 1][y].Piece is SkillKing))
                    {
                        board[x + 1][y].IsPossibleSkill = true;
                    }
                }
            }

            // 좌상
            if (y > 0 && x > 0)
            {
                if (board[x - 1][y - 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x - 1][y - 1].Piece is SkillKing))
                    {
                        board[x - 1][y - 1].IsPossibleSkill = true;
                    }
                }
            }

            // 우상
            if (y > 0 && x < 7)
            {
                if (board[x + 1][y - 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x + 1][y - 1].Piece is SkillKing))
                    {
                        board[x + 1][y - 1].IsPossibleSkill = true;
                    }
                }
            }
            
            // 좌하
            if (y < 7 && x > 0)
            {
                if (board[x - 1][y + 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x - 1][y + 1].Piece is SkillKing))
                    {
                        board[x - 1][y + 1].IsPossibleSkill = true;
                    }
                }
            }

            // 우하
            if (y < 7 && x < 7)
            {
                if (board[x + 1][y + 1].Piece?.Color == enemyColor)
                {
                    if (!(board[x + 1][y + 1].Piece is SkillKing))
                    {
                        board[x + 1][y + 1].IsPossibleSkill = true;
                    }
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (_owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

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

            // 좌상
            if (y > 0 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y - 1);
            }

            // 우상
            if (y > 0 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y - 1);
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y + 1);
            }

            // 우하
            if (y < 7 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y + 1);
            }
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
