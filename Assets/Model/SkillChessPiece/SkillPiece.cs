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

        /// <summary>
        /// 마지막으로 이 기물에게 상태이상 데미지를 준 기물
        /// </summary>
        public SkillPiece LastStatusHit;

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

            this.Attack = new Attack(this);
        }

        /// <summary>
        /// 기물의 상태이상 설정.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="count"></param>
        public void SetStatus(Status status, int count)
        {
            GameObject obj = null;

            // 무적상태이면 새로운 상태이상에 걸리지 않음
            if (this.Status == Status.INVINCIBLE && this.StatusCount >= 1)
            {
                return;
            }
            
            // 기존 상태이상과 동일한 경우 남은 턴 수를 증가시킴
            if (this.Status == status)
            {
                this.StatusCount += count;
            }

            switch (this.Status)
            {
                case Status.NONE:
                    break;
                case Status.FREEZING:
                    break;
                case Status.BURN:
                    break;
                case Status.INVINCIBLE:
                    break;
                case Status.STUN:
                    break;
                case Status.POISONING:
                    break;
            }
            // "Effect/Status/Burn"
            this.Status = status;
        }

        /// <summary>
        /// 기물이 공격할 수 있는 발판의 IsPossibleAttack를 true로 변경
        /// </summary>
        /// <param name="board"></param>
        /// /// <param name="location"></param>
        public abstract void SetAttackStatus(List<Board[]> board, Location location);

        /// <summary>
        /// 기물이 공격할 수 있는 발판과 공격할 수 없는 발판 표시
        /// </summary>
        /// <param name="board"></param>
        /// /// <param name="location"></param>
        public abstract void ShowAttackScope(List<Board[]> board, Location location);
    }
}
