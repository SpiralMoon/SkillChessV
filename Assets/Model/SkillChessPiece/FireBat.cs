using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model.SkillChessPiece
{
    public class FireBat : SkillPiece
    {
        public FireBat(string color) : base(color)
        {

        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {

        }
    }
}
