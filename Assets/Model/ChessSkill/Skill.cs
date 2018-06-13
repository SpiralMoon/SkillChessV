﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.SkillChessPiece;
using Assets.Service;
using Assets.Support;
using Assets.Support.Language;

namespace Assets.Model.ChessSkill
{
    public abstract class Skill : MonoBehaviour
    {
        protected GameObject Obj;

        protected EffectManager _effectManager;

        protected TextResource _textResource;

        /// <summary>
        /// 이 기술의 주인 기물
        /// </summary>
        protected SkillPiece _owner;

        /// <summary>
        /// Display name
        /// </summary>
        public string Name;

        /// <summary>
        /// Display explain
        /// </summary>
        public string Explain;

        /// <summary>
        /// 속성
        /// </summary>
        public Element Element;

        /// <summary>
        /// 기술 번호
        /// </summary>
        public int Code;

        /// <summary>
        /// 기본 데미지
        /// </summary>
        public int Power;

        /// <summary>
        /// 기술 사용 마나량
        /// </summary>
        public int Mp;

        public Skill(SkillPiece owner)
        {
            _effectManager = EffectManager.GetInstance();
            _textResource = TextResource.GetInstance();

            _owner = owner;
        }

        protected void Init()
        {
            var nameAndExplain = _textResource.GetSkillInfo(this);

            this.Name = nameAndExplain["Name"];
            this.Explain = nameAndExplain["Explain"];
        }
        
        /// <summary>
        /// 잔여 MP 검사. 다른 조건으로 기술을 사용하는 경우 오버라이딩.
        /// </summary>
        /// <param name="owner">기술을 보유한 기물</param>
        /// <returns>기술 발동 가능 여부</returns>
        public bool CheckCondition()
        {
            return _owner.CurrentMp >= Mp;
        }

        /// <summary>
        /// 기술 사용 후 상태변화. MP 감소 등.
        /// </summary>
        /// <param name="owner"></param>
        public void Cost()
        {
            _owner.CurrentMp -= this.Mp;
        }

        /// <summary>
        /// 데미지를 입히는 함수.
        /// 단일 타겟 기준이지만 오버라이딩하여 사용 가능.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="target"></param>
        public void Damage(SkillPiece target)
        {

        }

        /// <summary>
        /// 기물이 스킬을 사용할 수 있는 발판의 IsPossibleSkill를 true로 변경
        /// </summary>
        /// <param name="board"></param>
        /// <param name="location"></param>
        public abstract void SetSkillStatus(List<Board[]> board, Location location);

        /// <summary>
        /// 기물이 스킬을 사용할 수 있는 발판과 사용할 수 없는 발판 표시
        /// </summary>
        /// <param name="board"></param>
        /// /// <param name="location"></param>
        public abstract void ShowSkillScope(List<Board[]> board, Location location);

        /// <summary>
        /// 기술 사용.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public abstract Task Trigger(List<Board[]> board, Location startLocation, Location endLocation);
    }
}
