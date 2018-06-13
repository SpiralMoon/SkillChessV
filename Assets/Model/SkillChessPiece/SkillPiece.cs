using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.ChessPiece;
using Assets.Model.ChessSkill;
using Assets.Support;
using Assets.Support.Language;

namespace Assets.Model.SkillChessPiece
{
    public abstract class SkillPiece : Piece
    {
        private TextResource _textResource;

        public GameObject StatusObj;

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

        public ClassCode ClassCode;

        public Element Element;

        /// <summary>
        /// 직업 이름
        /// </summary>
        public string ClassName;

        /// <summary>
        /// 직업 설명
        /// </summary>
        public string ClassExplain;

        /// <summary>
        /// 마지막으로 이 기물에게 데미지를 준 기물
        /// </summary>
        public SkillPiece LastHit;

        public Attack Attack;

        public Skill[] Skill;

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
            this.ClassName = _textResource.GetClassName(this);
            // this.ClassDescription;
        }
    }
}
