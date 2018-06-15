using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Service;
using Assets.Support;

namespace Assets.Model.ChessSkill.Angel
{
    public class HolyArrow : Skill
    {
        public HolyArrow(SkillPiece owner) : base(owner)
        {
            this.Code = 1611;
            this.Element = Element.HOLY;
            this.Power = 125;
            this.Mp = 150;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (_owner.Color == Color.WHITE)
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

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
