using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archmage
{
    public class IceField : Skill
    {
        public IceField(SkillPiece owner) : base(owner)
        {
            this.Code = 1311;
            this.Element = Element.ICE;
            this.Power = 150;
            this.Mp = 200;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 1)
            {
                board[x][y - 2].IsPossibleSkill = true;
            }
        
            // 하
            if (y < 6)
            {
                board[x][y + 2].IsPossibleSkill = true;
            }
            
            // 좌
            if (x > 1)
            {
                board[x - 2][y].IsPossibleSkill = true;
            }

            // 우
            if (x < 6)
            {
                board[x + 2][y].IsPossibleSkill = true;
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 1)
            {
                _effectManager.SkillScope(board, x, y - 2);
            }

            // 하
            if (y < 6)
            {
                _effectManager.SkillScope(board, x, y + 2);
            }

            // 좌
            if (x > 1)
            {
                _effectManager.SkillScope(board, x - 2, y);
            }

            // 우
            if (x < 6)
            {
                _effectManager.SkillScope(board, x + 2, y);
            }
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
