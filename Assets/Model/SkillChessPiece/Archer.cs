using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model.SkillChessPiece
{
    public class Archer : SkillPiece
    {
        public Archer(string color) : base(color)
        {

        }

        protected override void SetMoveStatus(List<Board[]> board)
        {
            throw new NotImplementedException();
        }
    }
}
