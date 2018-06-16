using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.BlackMagician
{
    public class MeteorStrike : Skill
    {
        public MeteorStrike(SkillPiece owner) : base(owner)
        {
            this.Code = 1513;
            this.Element = Element.FIRE;
            this.Power = 200;
            this.Mp = 750;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 좌상
            if (y > 2 && x > 2)
            {
                board[x - 3][y - 3].IsPossibleSkill = true;
            }

            // 우상
            if (y > 2 && x < 5)
            {
                board[x + 3][y - 3].IsPossibleSkill = true;
            }

            // 좌하
            if (y < 5 && x > 2)
            {
                board[x - 3][y + 3].IsPossibleSkill = true;
            }

            // 우하
            if (y < 5 && x < 5)
            {
                board[x + 3][y + 3].IsPossibleSkill = true;
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 좌상
            if (y > 2 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y - 3);
            }

            // 우상
            if (y > 2 && x < 5)
            {
                _effectManager.SkillScope(board, x + 3, y - 3);
            }

            // 좌하
            if (y < 5 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y + 3);
            }

            // 우하
            if (y < 5 && x < 5)
            {
                _effectManager.SkillScope(board, x + 3, y + 3);
            }
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
