using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model.SkillChessPiece
{
    public class Assassin : SkillPiece
    {
        public Assassin(string color) : base(color)
        {

        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            throw new NotImplementedException();
        }
    }
}
