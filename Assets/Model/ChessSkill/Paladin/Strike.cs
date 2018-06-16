using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Paladin
{
    public class Strike : Skill
    {
        public Strike(SkillPiece owner) : base(owner)
        {
            this.Code = 1421;
            this.Element = Element.LIGHTNING;
            this.Power = 220;
            this.Mp = 125;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (_owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 좌상
            if (y > 0 && x > 0)
            {
                if (board[x - 1][y - 1].Piece?.Color == enemyColor)
                {
                    board[x - 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 우상
            if (y > 0 && x < 7)
            {
                if (board[x + 1][y - 1].Piece?.Color == enemyColor)
                {
                    board[x + 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                if (board[x - 1][y + 1].Piece?.Color == enemyColor)
                {
                    board[x - 1][y + 1].IsPossibleSkill = true;
                }
            }

            // 우하
            if (y < 7 && x < 7)
            {
                if (board[x + 1][y + 1].Piece?.Color == enemyColor)
                {
                    board[x + 1][y + 1].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var myColor = _owner.Color;

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
