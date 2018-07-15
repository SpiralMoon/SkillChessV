using System;
using System.Collections.Generic;

namespace Assets.Model.SkillChessPiece
{
    public class SkillKing : SkillPiece
    {
        public SkillKing(string color) : base(color)
        {
            this.PieceName = "King";
            this.Color = color;
            this.IsPossibleCastling = true;
            this.IsPossibleFirstChance = false;
        }

        public override void SetAttackStatus(List<Board[]> board, Location location)
        {

        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // 현재 기물의 위치가 최전방이 아닌 경우
            if (y > 0)
            {
                // Top Left
                if (x > 0)
                {
                    if (board[x - 1][y - 1].Piece?.Color != Color)
                    {
                        board[x - 1][y - 1].IsPossibleMove = true;
                    }
                }

                // Top Right
                if (x < 7)
                {
                    if (board[x + 1][y - 1].Piece?.Color != Color)
                    {
                        board[x + 1][y - 1].IsPossibleMove = true;
                    }
                }

                // Top
                if (board[x][y - 1].Piece?.Color != Color)
                {
                    board[x][y - 1].IsPossibleMove = true;
                }
            }

            // 현재 기물의 위치가 최후방이 아닌 경우
            if (y < 7)
            {
                // Bottom Left
                if (x > 0)
                {
                    if (board[x - 1][y + 1].Piece?.Color != Color)
                    {
                        board[x - 1][y + 1].IsPossibleMove = true;
                    }
                }

                // Bottom Right
                if (x < 7)
                {
                    if (board[x + 1][y + 1].Piece?.Color != Color)
                    {
                        board[x + 1][y + 1].IsPossibleMove = true;
                    }
                }

                // Bottom
                if (board[x][y + 1].Piece?.Color != Color)
                {
                    board[x][y + 1].IsPossibleMove = true;
                }
            }

            // Left
            if (x > 0)
            {
                if (board[x - 1][y].Piece?.Color != Color)
                {
                    board[x - 1][y].IsPossibleMove = true;
                }
            }

            // Right
            if (x < 7)
            {
                if (board[x + 1][y].Piece?.Color != Color)
                {
                    board[x + 1][y].IsPossibleMove = true;
                }
            }

            // 캐슬링이 가능한 경우
            if (board[x][y].Piece.IsPossibleCastling)
            {
                // King과 Rook 사이에 장애물이 있는가?
                bool obstacles = false;

                // 왼쪽 Rook과 캐슬링을 할 수 있는 경우
                if (board[0][y].Piece.IsPossibleCastling)
                {
                    for (int i = x - 1; i > 0; i--)
                    {
                        // 장애물 발견
                        if (board[i][y].Piece != null)
                        {
                            obstacles = true;
                            break;
                        }
                    }
                    if (!obstacles)
                    {
                        board[2][y].IsPossibleMove = true;
                    }
                }

                obstacles = false;

                // 오른쪽 Rook과 캐슬링을 할 수 있는 경우
                if (board[7][y].Piece.IsPossibleCastling)
                {
                    for (int i = x + 1; i < 7; i++)
                    {
                        if (board[i][y].Piece != null)
                        {
                            obstacles = true;
                            break;
                        }
                    }
                    if (!obstacles)
                    {
                        board[6][y].IsPossibleMove = true;
                    }
                }
            }
        }

        public override void ShowAttackScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            throw new NotImplementedException();
        }
    }
}
