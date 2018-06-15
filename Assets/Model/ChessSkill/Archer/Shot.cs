using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archer
{
    public class Shot : Skill
    {
        public Shot(SkillPiece owner) : base(owner)
        {
            this.Code = 1411;
            this.Element = Element.NORMAL;
            this.Power = 100;
            this.Mp = 80;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var myColor = _owner.Color;

            // 상
            if (y > 0)
            {
                if (board[x][y - 1].Piece?.Color != myColor)
                {
                    board[x][y - 1].IsPossibleSkill = true;
                }
            }

            // 하
            if (y < 7)
            {
                if (board[x][y + 1].Piece?.Color != myColor)
                {
                    board[x][y + 1].IsPossibleSkill = true;
                }
            }

            // 좌
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color != myColor)
                {
                    board[x - 1][y].IsPossibleSkill = true;
                }
            }

            // 우
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color != myColor)
                {
                    board[x + 1][y].IsPossibleSkill = true;
                }
            }
            
            // 좌상
            if (y > 0 && x > 0)
            {
                if (board[x - 1][y - 1].Piece?.Color != myColor)
                {
                    board[x - 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 우상
            if (y > 0 && x < 7)
            {
                if (board[x + 1][y - 1].Piece?.Color != myColor)
                {
                    board[x + 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                if (board[x - 1][y + 1].Piece?.Color != myColor)
                {
                    board[x - 1][y + 1].IsPossibleSkill = true;
                }
            }

            // 우하
            if (y < 7 && x < 7)
            {
                if (board[x + 1][y + 1].Piece?.Color != myColor)
                {
                    board[x + 1][y + 1].IsPossibleSkill = true;
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
