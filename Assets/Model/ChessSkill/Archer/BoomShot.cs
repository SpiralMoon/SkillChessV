using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archer
{
    public class BoomShot : Skill
    {
        private readonly string _hitPath;

        public BoomShot(SkillPiece owner) : base(owner)
        {
            this.Code = 1412;
            this.Element = Element.FIRE;
            this.Power = 150;
            this.Mp = 120;
            this._hitPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Hit";

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 좌상
            if (y > 2 && x > 2)
            {
                if (board[x - 3][y - 3].Piece?.Color == enemyColor)
                {
                    board[x - 3][y - 3].IsPossibleSkill = true;
                }
            }

            // 우상
            if (y > 2 && x < 5)
            {
                if (board[x + 3][y - 3].Piece?.Color == enemyColor)
                {
                    board[x + 3][y - 3].IsPossibleSkill = true;
                }
            }

            // 좌하
            if (y < 5 && x > 2)
            {
                if (board[x - 3][y + 3].Piece?.Color == enemyColor)
                {
                    board[x - 3][y + 3].IsPossibleSkill = true;
                }
            }


            // 우하
            if (y < 5 && x < 5)
            {
                if (board[x + 3][y + 3].Piece?.Color == enemyColor)
                {
                    board[x + 3][y + 3].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            // 좌상
            if (y > 2 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y - 3);
            }

            // 우상
            if (y > 2 && x < 5)
            {
                _effectManager.SkillScope(board, x + 3, y - 3);
            }

            // 좌하
            if (y < 5 && x > 2)
            {
                _effectManager.SkillScope(board, x - 3, y + 3);
            }
            
            // 우하
            if (y < 5 && x < 5)
            {
                _effectManager.SkillScope(board, x + 3, y + 3);
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
