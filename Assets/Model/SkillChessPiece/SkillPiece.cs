using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model.ChessPiece;
using Assets.Support;
using Assets.Support.Language;

namespace Assets.Model.SkillChessPiece
{
    public abstract class SkillPiece : Piece
    {
        private TextResource _textResource;

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

        public int ClassCode;

        public Element Element;

        public string ClassName;

        public string ClassDescription;

        public SkillPiece(string color) : base(color)
        {
            _textResource = TextResource.GetInstance();

            this.Color = color;

            this.Level = 1;

            this.Status = Status.NONE;
            this.StatusCount = 0;
        }

        protected void Init()
        {
            this.CurrentHp = MaxHp;
            this.CurrentMp = MaxMp;
            this.CurrentExp = 0;

            // TODO
            // this.ClassName;
            // this.ClassDescription;
        }
    }
}
