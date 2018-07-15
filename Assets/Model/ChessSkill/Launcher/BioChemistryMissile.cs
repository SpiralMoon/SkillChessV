using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Launcher
{
    public class BioChemistryMissile : Skill
    {
        private readonly string _aimPath;

        public BioChemistryMissile(SkillPiece owner) : base(owner)
        {
            this.Code = 1142;
            this.Element = Element.POISON;
            this.Power = 110;
            this.Mp = 85;
            this._aimPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Aim";

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((x > i + 2 || x < i - 2) || (y > j + 2 || y < j - 2))
                    {
                        if (board[i][j].Piece?.Color == enemyColor)
                        {
                            board[i][j].IsPossibleSkill = true;
                        }
                    }
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((x > i + 2 || x < i - 2) || (y > j + 2 || y < j - 2))
                    {
                        _effectManager.SkillScope(board, i, j);
                    }
                }
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location startLocation, Location endLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
