using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Bomber
{
    public class RemoteExplosion : Skill
    {
        private readonly string _hitPath;

        public RemoteExplosion(SkillPiece owner) : base(owner)
        {
            this.Code = 1121;
            this.Element = Element.FIRE;
            this.Power = 100;
            this.Mp = 50;
            this._hitPath = $"Effect/Skill/{Owner.GetType().Name}/{GetType().Name}Hit";

            Init();
        }

        public override void SetSkillStatus(List<Board[]> board, Location location)
        {
            var myColor = Owner.Color;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i][j].Piece?.Color == enemyColor)
                    {
                        // 내 기물과 근접해있는가?
                        var isAdjoinMyPiece = false;

                        //상
                        if (j > 0)
                        {
                            if (board[i][j - 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 하
                        if (j < 7)
                        {
                            if (board[i][j + 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 좌
                        if (i > 0)
                        {
                            if (board[i - 1][j].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 우
                        if (i < 7)
                        {
                            if (board[i + 1][j].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        //좌상
                        if (i > 0 && j > 0)
                        {
                            if (board[i - 1][j - 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 좌하
                        if (i > 0 && j < 7)
                        {
                            if (board[i - 1][j + 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 우상
                        if (i < 7 && j > 0)
                        {
                            if (board[i + 1][j - 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        // 우하
                        if (i < 7 && j < 7)
                        {
                            if (board[i + 1][j + 1].Piece?.Color == myColor)
                            {
                                isAdjoinMyPiece = true;
                            }
                        }

                        board[i][j].IsPossibleSkill = !isAdjoinMyPiece;
                    }
                }
            }
        }

        public override void ShowSkillScope(List<Board[]> board, Location location)
        {
            var myColor = Owner.Color;
            var enemyColor = (Owner.Color == Color.WHITE)
                ? Color.BLACK
                : Color.WHITE;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i][j].Piece?.Color == enemyColor)
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
