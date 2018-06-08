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
    public class Skill : MonoBehaviour
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
    }
}
