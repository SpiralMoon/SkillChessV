using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.SkillChessPiece;
using Assets.Service;
using Assets.Support;
using Assets.Support.Language;
using System.Collections;

namespace Assets.Model.ChessSkill
{
    public abstract class Skill : MonoBehaviour
    {
        protected GameObject Obj;

        protected EffectManager _effectManager;

        protected TextResource _textResource;

        /// <summary>
        /// 이 기술의 기본 이펙트
        /// </summary>
        protected string _path;

        /// <summary>
        /// 이 기술의 주인 기물
        /// </summary>
        protected SkillPiece Owner;

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

            Owner = owner;
        }

        protected void Init()
        {
            var nameAndExplain = _textResource.GetSkillInfo(this);

            this.Name = nameAndExplain["Name"];
            this.Explain = nameAndExplain["Explain"];
            this._path = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}";
        }
        
        /// <summary>
        /// 잔여 MP와 Stun 검사. 다른 조건으로 기술을 사용하는 경우 오버라이딩.
        /// </summary>
        /// <param name="owner">기술을 보유한 기물</param>
        /// <returns>기술 발동 가능 여부</returns>
        public bool CheckCondition()
        {
            return Owner.CurrentMp >= Mp && Owner.Status != Status.STUN ;
        }

        /// <summary>
        /// 기술 사용 후 상태변화. MP 감소 등.
        /// </summary>
        /// <param name="owner"></param>
        public void Cost()
        {
            Owner.CurrentMp -= this.Mp;
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
        /// <param name="finishCallback"></param>
        /// <returns></returns>
        public void Trigger(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            StartCoroutine(Active(board, startLocation, endLocation, finishCallback));
        }

        /// <summary>
        /// 기술의 상세 로직. Override
        /// 기술이 끝나면 finish callback을 실행함.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        protected abstract IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback);

        /// <summary>
        /// 기술 오브젝트를 path로부터 불러옴
        /// </summary>
        /// <param name="path">불러올 오브젝트의 경로</param>
        /// <returns></returns>
        protected GameObject Load(string path)
        {
            return Instantiate(Resources.Load<GameObject>(path));
        }
    }
}
