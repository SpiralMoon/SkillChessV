using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Model.SkillChessPiece;
using Assets.Support;
using Assets.Support.Extension;

namespace Assets.Model.ChessSkill.Angel
{
    public class Dispel : Skill
    {
        public Dispel(SkillPiece owner) : base(owner)
        {
            this.Code = 1612;
            this.Element = Element.HOLY;
            this.Power = 0;
            this.Mp = 220;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            board[x][y].IsPossibleSkill = true;
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            _effectManager.SkillScopeSelf(board, x, y);
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            var x = board.GetLocation(Owner).X;
            var y = board.GetLocation(Owner).Y;
            var targetX = targetLocation.X;
            var targetY = targetLocation.Y;
            var enemyColor = (Owner.Color == Support.Color.WHITE) ?
                Support.Color.BLACK :
                Support.Color.WHITE;

            Obj = Load(_path);

            // 스킬 시전 플레이어의 색에 따라 십자가의 방향 전환
            if (Owner.Color == Support.Color.BLACK)
            {
                Obj.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
            yield return new WaitForSeconds(2);
            // 상태이상 해제 시작
            // 상 2

        }
    }
}
