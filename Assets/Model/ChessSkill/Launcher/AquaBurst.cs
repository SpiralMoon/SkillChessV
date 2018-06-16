using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Launcher
{
    public class AquaBurst : Skill
    {
        public AquaBurst(SkillPiece owner) : base(owner)
        {
            this.Code = 1141;
            this.Element = Element.WATER;
            this.Power = 50;
            this.Mp = 150;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 0)
            {
                board[x][y - 1].IsPossibleSkill = true;
            }

            // 하
            if (y < 7)
            {
                board[x][y + 1].IsPossibleSkill = true;
            }

            // 좌
            if (x > 0)
            {
                board[x - 1][y].IsPossibleSkill = true;
            }

            // 우
            if (x < 7)
            {
                board[x + 1][y].IsPossibleSkill = true;
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

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
