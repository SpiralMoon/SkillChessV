using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Launcher
{
    public class AquaCannon : Skill
    {
        public AquaCannon(SkillPiece owner) : base(owner)
        {
            this.Code = 1143;
            this.Element = Element.WATER;
            this.Power = 230;
            this.Mp = 230;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (_owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((x > i + 2 || x < i - 2) || (y > j + 2 || y < j - 2))
                    {
                        if (board[i][j].Piece?.Color == enemyColor)
                        {
                            board[i][j].IsPossibleSkill = true;
                        }
                    }
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((x > i + 2 || x < i - 2) || (y > j + 2 || y < j - 2))
                    {
                        _effectManager.SkillScope(board, i, j);
                    }
                }
            }
        }

        public override Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            throw new NotImplementedException();
        }
    }
}
