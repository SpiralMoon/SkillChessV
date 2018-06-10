using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        /// Display name
        /// </summary>
        protected string Name;

        /// <summary>
        /// Display explain
        /// </summary>
        protected string Explain;

        /// <summary>
        /// 속성
        /// </summary>
        protected Element Element;

        /// <summary>
        /// 기본 데미지
        /// </summary>
        protected int Power;

        /// <summary>
        /// 기술 사용 마나량
        /// </summary>
        protected int Mp;

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
    }
}
