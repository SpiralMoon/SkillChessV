using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// 공격.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="startLocation"></param>
        /// <param name="endLocation"></param>
        /// <returns></returns>
        public Task Trigger(List<Board[]> board, Location startLocation, Location endLocation)
        {
            return null;
        }

        /// <summary>
        /// 기물이 공격할 수 있는 발판의 IsPossibleAttack를 true로 변경
        /// </summary>
        /// <param name="board"></param>
        /// /// <param name="location"></param>
        public void SetAttackStatus(List<Board> board, Location location)
        {

        }

        /// <summary>
        /// 기물이 공격할 수 있는 발판과 공격할 수 없는 발판 표시
        /// </summary>
        /// <param name="board"></param>
        /// /// <param name="location"></param>
        public void ShowAttackScope(List<Board> board, Location location)
        {

        }
    }
}
