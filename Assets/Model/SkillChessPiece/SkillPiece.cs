using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Support;

namespace Assets.Model.SkillChessPiece
{
    public class SkillPiece : Piece
    {
        public int Power;

        public int MaxHp;

        public int MaxMp;

        public int[] MaxExp;

        public int CurrentHp;

        public int CurrentMp;

        public int CurrentExp;

        public int Exp;

        public int Level;

        public Status Status;

        public int StatusCount;

        public Element Element;

        public SkillPiece(string color) : base(color)
        {

        }
    }
}
