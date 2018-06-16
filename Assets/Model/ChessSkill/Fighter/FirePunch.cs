using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Fighter
{
    public class FirePunch : Skill
    {
        public FirePunch(SkillPiece owner) : base(owner)
        {
            this.Code = 1132;
            this.Element = Element.FIRE;
            this.Power = 80;
            this.Mp = 50;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var myColor = _owner.Color;

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
