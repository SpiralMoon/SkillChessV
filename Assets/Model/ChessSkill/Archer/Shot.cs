using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archer
{
    public class Shot : Skill
    {
        private readonly string _hitPath;

        public Shot(SkillPiece owner) : base(owner)
        {
            this.Code = 1411;
            this.Element = Element.NORMAL;
            this.Power = 100;
            this.Mp = 80;
            this._hitPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Hit";

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var myColor = Owner.Color;

            // 상
            if (y > 0)
            {
                if (board[x][y - 1].Piece?.Color != myColor)
                {
                    board[x][y - 1].IsPossibleSkill = true;
                }
            }

            // 하
            if (y < 7)
            {
                if (board[x][y + 1].Piece?.Color != myColor)
                {
                    board[x][y + 1].IsPossibleSkill = true;
                }
            }

            // 좌
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color != myColor)
                {
                    board[x - 1][y].IsPossibleSkill = true;
                }
            }

            // 우
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color != myColor)
                {
                    board[x + 1][y].IsPossibleSkill = true;
                }
            }
            
            // 좌상
            if (y > 0 && x > 0)
            {
                if (board[x - 1][y - 1].Piece?.Color != myColor)
                {
                    board[x - 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 우상
            if (y > 0 && x < 7)
            {
                if (board[x + 1][y - 1].Piece?.Color != myColor)
                {
                    board[x + 1][y - 1].IsPossibleSkill = true;
                }
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                if (board[x - 1][y + 1].Piece?.Color != myColor)
                {
                    board[x - 1][y + 1].IsPossibleSkill = true;
                }
            }

            // 우하
            if (y < 7 && x < 7)
            {
                if (board[x + 1][y + 1].Piece?.Color != myColor)
                {
                    board[x + 1][y + 1].IsPossibleSkill = true;
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 상
            if (y > 0)
            {
                _effectManager.SkillScope(board, x, y - 1);
            }

            // 하
            if (y < 7)
            {
                _effectManager.SkillScope(board, x, y + 1);
            }

            // 좌
            if (x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y);
            }

            // 우
            if (x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y);
            }

            // 좌상
            if (y > 0 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y - 1);
            }

            // 우상
            if (y > 0 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y - 1);
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                _effectManager.SkillScope(board, x - 1, y + 1);
            }

            // 우하
            if (y < 7 && x < 7)
            {
                _effectManager.SkillScope(board, x + 1, y + 1);
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
