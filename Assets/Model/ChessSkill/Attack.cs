using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.SkillChessPiece;
using Assets.Service;
using Assets.Support;

namespace Assets.Model.ChessSkill
{
    public class Attack : MonoBehaviour
    {
        protected GameObject Obj;

        protected EffectManager _effectManager;

        /// <summary>
        /// 이 공격의 주인 기물
        /// </summary>
        protected SkillPiece Owner;

        /// <summary>
        /// 속성
        /// </summary>
        protected Element Element;

        public Attack(SkillPiece owner)
        {
            _effectManager = EffectManager.GetInstance();

            Owner = owner;
        }

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
        /// 이번 시도가 일반공격이 아니라 확정킬로 판정되는지 검사.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="targetLocation">적 기물의 위치</param>
        /// <returns>확정킬 여부</returns>
        public bool CheckFinalizeAttack(List<Board[]> board, Location targetLocation)
        {
            // 체력이 남으면 확정킬이 아니므로 false
            // 체력이 남지 않으면 확정킬이므로 true
            var target = board[targetLocation.X][targetLocation.Y].Piece as SkillPiece;
            return target.CurrentHp <= Owner.Power;
        }

        /// <summary>
        /// 적이 공격범위에 있는지 검사.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="targetLocation">적 기물의 위치</param>
        /// <returns></returns>
        public bool CheckAdjoinEnemy(List<Board[]> board, Location targetLocation)
        {
            return false;
        }
    }
}
