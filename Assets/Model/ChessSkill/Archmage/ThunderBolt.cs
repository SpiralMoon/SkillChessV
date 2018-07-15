using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archmage
{
    public class ThunderBolt : Skill
    {
        public ThunderBolt(SkillPiece owner) : base(owner)
        {
            this.Code = 1312;
            this.Element = Element.LIGHTNING;
            this.Power = 150;
            this.Mp = 200;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 상 3
            if (y - 2 > 0)
            {
                if (board[x][y - 3].Piece?.Color == enemyColor)
                {
                    board[x][y - 3].IsPossibleSkill = true;
                }
            }

            // 상 2, 좌 1
            if (y - 1 > 0 && x > 0)
            {
                if (board[x - 1][y - 2].Piece?.Color == enemyColor)
                {
                    board[x - 1][y - 2].IsPossibleSkill = true;
                }
            }

            // 상 2
            if (y - 1 > 0)
            {
                if (board[x][y - 2].Piece?.Color == enemyColor)
                {
                    board[x][y - 2].IsPossibleSkill = true;
                }
            }

            // 상 2, 우 1
            if (y - 1 > 0 && x < 7)
            {
                if (board[x + 1][y - 2].Piece?.Color == enemyColor)
                {
                    board[x + 1][y - 2].IsPossibleSkill = true;
                }
            }
            
            // 상 1, 좌 2
            if (y > 0 && x - 1 > 0)
            {
                if (board[x - 2][y - 1].Piece?.Color == enemyColor)
                {
                    board[x - 2][y - 1].IsPossibleSkill = true;
                }
            }

            // 상 1, 좌 1
            if (y > 0 && x > 0)
            {
                if (board[x - 1][y - 1].Piece?.Color == enemyColor)
                {
                    board[x - 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 상 1
            if (y > 0)
            {
                if (board[x][y - 1].Piece?.Color == enemyColor)
                {
                    board[x][y - 1].IsPossibleSkill = true;
                }
            }
            
            // 상 1, 우 1
            if (y > 0 && x < 7)
            {
                if (board[x + 1][y - 1].Piece?.Color == enemyColor)
                {
                    board[x + 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 상 1, 우 2
            if (y > 0 && x + 1 < 7)
            {
                if (board[x + 2][y - 1].Piece?.Color == enemyColor)
                {
                    board[x + 2][y - 1].IsPossibleSkill = true;
                }
            }
            
            // 좌 3
            if (x - 2 > 0)
            {
                if (board[x - 3][y].Piece?.Color == enemyColor)
                {
                    board[x - 3][y].IsPossibleSkill = true;
                }
            }

            // 좌 2
            if (x - 1 > 0)
            {
                if (board[x - 2][y].Piece?.Color == enemyColor)
                {
                    board[x - 2][y].IsPossibleSkill = true;
                }
            }

            // 좌 1
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color == enemyColor)
                {
                    board[x - 1][y].IsPossibleSkill = true;
                }
            }

            // 우 1
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color == enemyColor)
                {
                    board[x + 1][y].IsPossibleSkill = true;
                }
            }

            // 우 2
            if (x + 1 < 7)
            {
                if (board[x + 2][y].Piece?.Color == enemyColor)
                {
                    board[x + 2][y].IsPossibleSkill = true;
                }
            }

            // 우 3
            if (x + 2 < 7)
            {
                if (board[x + 3][y].Piece?.Color == enemyColor)
                {
                    board[x + 3][y].IsPossibleSkill = true;
                }
            }
            
            // 하 1, 좌 2
            if (y < 7 && x - 1 > 0)
            {
                if (board[x - 2][y + 1].Piece?.Color == enemyColor)
                {
                    board[x - 2][y + 1].IsPossibleSkill = true;
                }
            }

            // 하 1 , 좌 1
            if (y < 7 && x > 0)
            {
                if (board[x - 1][y + 1].Piece?.Color == enemyColor)
                {
                    board[x - 1][y + 1].IsPossibleSkill = true;
                }
            }

            // 하 1
            if (y < 7)
            {
                if (board[x][y + 1].Piece?.Color == enemyColor)
                {
                    board[x][y + 1].IsPossibleSkill = true;
                }
            }
            
            // 하 1, 우 1
            if (y < 7 && x < 7)
            {
                if (board[x + 1][y + 1].Piece?.Color == enemyColor)
                {
                    board[x + 1][y + 1].IsPossibleSkill = true;
                }
            }

            // 하 1, 우 2
            if (y < 7 && x + 1 < 7)
            {
                if (board[x + 2][y + 1].Piece?.Color == enemyColor)
                {
                    board[x + 2][y + 1].IsPossibleSkill = true;
                }
            }
            
            // 하 2, 좌 1
            if (y + 1 < 7 && x > 0)
            {
                if (board[x - 1][y + 2].Piece?.Color == enemyColor)
                {
                    board[x - 1][y + 2].IsPossibleSkill = true;
                }
            }
            
            // 하 2
            if (y + 1 < 7)
            {
                if (board[x][y + 2].Piece?.Color == enemyColor)
                {
                    board[x][y + 2].IsPossibleSkill = true;
                }
            }

            // 하 2, 우 1
            if (y + 1 < 7 && x < 7)
            {
                if (board[x + 1][y + 2].Piece?.Color == enemyColor)
                {
                    board[x + 1][y + 2].IsPossibleSkill = true;
                }
            }

            // 하 3
            if (y + 2 < 7)
            {
                if (board[x][y + 3].Piece?.Color == enemyColor)
                {
                    board[x][y + 3].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상 3
            if (y - 2 > 0)
            {
                _effectManager.SkillScope(board, x, y - 3);
            }

            // 상 2, 좌 1
            if (y - 1 > 0 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y - 2);
            }

            // 상 2
            if (y - 1 > 0)
            {
                _effectManager.SkillScope(board, x, y - 2);
            }

            // 상 2, 우 1
            if (y - 1 > 0 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y - 2);
            }

            // 상 1, 좌 2
            if (y > 0 && x - 1 > 0)
            {
                _effectManager.SkillScope(board, x - 2, y - 1);
            }

            // 상 1, 좌 1
            if (y > 0 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y - 1);
            }

            // 상 1
            if (y > 0)
            {
                _effectManager.SkillScope(board, x, y - 1);
            }

            // 상 1, 우 1
            if (y > 0 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y - 1);
            }

            // 상 1, 우 2
            if (y > 0 && x + 1 < 7)
            {
                _effectManager.SkillScope(board, x + 2, y - 1);
            }

            // 좌 3
            if (x - 2 > 0)
            {
                _effectManager.SkillScope(board, x - 3, y);
            }

            // 좌 2
            if (x - 1 > 0)
            {
                _effectManager.SkillScope(board, x - 2, y);
            }

            // 좌 1
            if (x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y);
            }

            // 우 1
            if (x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y);
            }

            // 우 2
            if (x + 1 < 7)
            {
                _effectManager.SkillScope(board, x + 2, y);
            }

            // 우 3
            if (x + 2 < 7)
            {
                _effectManager.SkillScope(board, x + 3, y);
            }

            // 하 1, 좌 2
            if (y < 7 && x - 1 > 0)
            {
                _effectManager.SkillScope(board, x - 2, y + 1);
            }

            // 하 1 , 좌 1
            if (y < 7 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y + 1);
            }

            // 하 1
            if (y < 7)
            {
                _effectManager.SkillScope(board, x, y + 1);
            }

            // 하 1, 우 1
            if (y < 7 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y + 1);
            }

            // 하 1, 우 2
            if (y < 7 && x + 1 < 7)
            {
                _effectManager.SkillScope(board, x + 2, y + 1);
            }

            // 하 2, 좌 1
            if (y + 1 < 7 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y + 2);
            }

            // 하 2
            if (y + 1 < 7)
            {
                _effectManager.SkillScope(board, x, y + 2);
            }

            // 하 2, 우 1
            if (y + 1 < 7 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y + 2);
            }

            // 하 3
            if (y + 2 < 7)
            {
                _effectManager.SkillScope(board, x, y + 3);
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
