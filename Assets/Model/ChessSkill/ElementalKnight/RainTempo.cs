using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.ElementalKnight
{
    public class RainTempo : Skill
    {
        public RainTempo(SkillPiece owner) : base(owner)
        {
            this.Code = 1111;
            this.Element = Element.WATER;
            this.Power = 75;
            this.Mp = 100;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 좌상
            if (x > 1 && y > 1)
            {
                board[x - 2][y - 2].IsPossibleSkill = true;
            }

            // 우상
            if (x < 6 && y > 1)
            {
                board[x + 2][y - 2].IsPossibleSkill = true;
            }

            // 좌하
            if (x > 1 && y < 6) 
            {
                board[x - 2][y + 2].IsPossibleSkill = true;
            }

            // 우하
            if (x < 6 && y < 6) 
            {
                board[x + 2][y + 2].IsPossibleSkill = true;
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 좌상
            if (x > 1 && y > 1)
            {
                _effectManager.SkillScope(board, x - 2, y - 2);
            }

            // 우상
            if (x < 6 && y > 1)
            {
                _effectManager.SkillScope(board, x + 2, y - 2);
            }

            // 좌하
            if (x > 1 && y < 6)
            {
                _effectManager.SkillScope(board, x - 2, y + 2);
            }

            // 우하
            if (x < 6 && y < 6)
            {
                _effectManager.SkillScope(board, x + 2, y + 2);
            }
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
