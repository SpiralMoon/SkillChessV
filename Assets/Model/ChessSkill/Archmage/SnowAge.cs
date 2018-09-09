﻿using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Model.ChessSkill.Archmage
{
    public class SnowAge : Skill
    {
        private readonly string _hitPath;

        public SnowAge(SkillPiece owner) : base(owner)
        {
            this.Code = 1313;
            this.Element = Element.ICE;
            this.Power = 300;
            this.Mp = 400;
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

            // 좌
            for (int i = x - 1; i >= 0; i--)
            {
                if (board[i][y].Piece != null)
                {
                    if (board[i][y].Piece.Color == enemyColor)
                    {
                        board[i][y].IsPossibleSkill = true;
                    }

                    break;
                }
            }
                
            // 우
            for (int i = x + 1; i < 8; i++)
            {
                if (board[i][y].Piece != null)
                {
                    if (board[i][y].Piece.Color == enemyColor)
                    {
                        board[i][y].IsPossibleSkill = true;
                    }

                    break;
                }
            }

            // 상
            for (int i = y - 1; i >= 0; i--)
            {
                if (board[x][i].Piece != null)
                {
                    if (board[x][i].Piece.Color == enemyColor)
                    {
                        board[x][i].IsPossibleSkill = true;
                    }

                    break;
                }
            }
                
            // 하
            for (int i = y + 1; i < 8; i++)
            {
                if (board[x][i].Piece != null)
                {
                    if (board[x][i].Piece.Color == enemyColor)
                    {
                        board[x][i].IsPossibleSkill = true;
                    }

                    break;
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

            // 좌
            for (int i = x - 1; i >= 0; i--)
            {
                if (board[i][y].Piece != null)
                {
                    _effectManager.SkillScope(board, i, y);
                    break;
                }
            }

            // 우
            for (int i = x + 1; i < 8; i++)
            {
                if (board[i][y].Piece != null)
                {
                    _effectManager.SkillScope(board, i, y);
                    break;
                }
            }

            // 상
            for (int i = y - 1; i >= 0; i--)
            {
                if (board[x][i].Piece != null)
                {
                    _effectManager.SkillScope(board, x, i);
                    break;
                }
            }

            // 하
            for (int i = y + 1; i < 8; i++)
            {
                if (board[x][i].Piece != null)
                {
                    _effectManager.SkillScope(board, x, i);
                    break;
                }
            }
        }

        protected override IEnumerator Active(List<Board[]> board, Location targetLocation, Action finishCallback)
        {
            throw new NotImplementedException();
        }
    }
}
