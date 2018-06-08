using UnityEngine;

using Assets.Service;
using Assets.Support;

namespace Assets.Model.ChessSkill
{
    public class Attack : MonoBehaviour
    {
        protected GameObject Obj;

        protected EffectManager _effectManager;

        /// <summary>
        /// 속성
        /// </summary>
        protected Element Element;

        /// <summary>
        /// 기본 데미지
        /// </summary>
        protected int Power;
    }
}
