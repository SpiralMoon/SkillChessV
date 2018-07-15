using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Priest
{
    public class Heal : Skill
    {
        public Heal(SkillPiece owner) : base(owner)
        {
            this.Code = 1321;
            this.Element = Element.HOLY;
            this.Power = 160;
            this.Mp = 100;

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var myColor = Owner.Color;
            Board targetCell = null;
            SkillPiece target = null;

            // 상
            if (y > 0)
            {
                targetCell = board[x][y - 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 하
            if (y < 7)
            {
                targetCell = board[x][y + 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 좌
            if (x > 0)
            {
                targetCell = board[x - 1][y];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 우
            if (x < 7)
            {
                targetCell = board[x + 1][y];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 좌상
            if (y > 0 && x > 0)
            {
                targetCell = board[x - 1][y - 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 우상
            if (y > 0 && x < 7)
            {
                targetCell = board[x + 1][y - 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 좌하
            if (y < 7 && x > 0)
            {
                targetCell = board[x - 1][y + 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                }
            }

            // 우하
            if (y < 7 && x < 7)
            {
                targetCell = board[x + 1][y + 1];
                target = targetCell.Piece as SkillPiece;

                if (target != null)
                {
                    if (target.Color == myColor)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
                    else if (target.Element == Element.DARK)
                    {
                        targetCell.IsPossibleSkill = true;
                    }
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

        protected override IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            throw new NotImplementedException();

            finishCallback?.Invoke();
        }
    }
}
