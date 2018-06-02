using System.Collections.Generic;

using Assets.Service;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Pawn의 모델.
    /// </summary>
    public class Pawn : Piece
    {
        public Pawn(string color) : base(color)
        {
            this.PieceName = "Pawn";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = true;
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            if (Color == Support.Color.WHITE)
            {
                if (y > 0)
                {
                    // 앞에 적이 없는 경우
                    if (board[x] [y - 1].Piece == null)
                    {
                        board[x][y - 1].IsPossibleMove = true;
                    }
                }
                
                if (x > 0)
                {
                    // 왼쪽 대각선에 공격할 수 있는 적이 있는 경우
                    if (board[x - 1][y - 1].Piece?.Color == Support.Color.BLACK)
                    {
                        board[x - 1][y - 1].IsPossibleMove = true;
                    }
                }

                if (x < 7)
                {
                    // 오른쪽 대각선에 공격할 수 있는 적이 있는 경우
                    if (board[x + 1][y - 1].Piece?.Color == Support.Color.BLACK)
                    {
                        board[x + 1][y - 1].IsPossibleMove = true;
                    }
                }

                // 첫 이동 찬스를 사용할 수 있는 경우 (2칸 전진)
                if (board[x][y - 1].Piece == null && IsPossibleFirstChance)
                {
                    if (board[x][y - 2].Piece == null)
                    {
                        board[x][y - 2].IsPossibleMove = true;
                    }
                }
            }

            if (Color == Support.Color.BLACK)
            {
                if (y < 7)
                {
                    if (board[x][y + 1].Piece == null)
                    {
                        board[x][y + 1].IsPossibleMove = true;
                    }
                }

                if (x > 0)
                {
                    if (board[x - 1][y + 1].Piece?.Color == Support.Color.WHITE)
                    {
                        board[x - 1][y + 1].IsPossibleMove = true;
                    }
                }

                if (x < 7)
                {
                    if (board[x + 1][y + 1].Piece?.Color == Support.Color.WHITE)
                    {
                        board[x + 1][y + 1].IsPossibleMove = true;
                    }
                }
                
                if (board[x][y + 1].Piece == null && IsPossibleFirstChance)
                {
                    if (board[x][y + 2].Piece == null)
                    {
                        board[x][y + 2].IsPossibleMove = true;
                    }
                }
            }
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            var effectManager = EffectManager.GetInstance();
            var x = location.X;
            var y = location.Y;
            
            if (Color == Support.Color.WHITE)
            {
                if (y > 0)
                {
                    effectManager.MoveScope(board, x, y - 1);
                }
                if (x > 0)
                {
                    effectManager.MoveScope(board, x - 1, y - 1);
                }
                if (x < 7)
                {
                    effectManager.MoveScope(board, x + 1, y - 1);
                }

                if (board[x][y - 1].Piece == null && IsPossibleFirstChance)
                {
                    effectManager.MoveScope(board, x, y - 2);
                }
            }

            if (Color == Support.Color.BLACK)
            {
                if (y < 7)
                {
                    effectManager.MoveScope(board, x, y + 1);
                }

                if (x > 0)
                {
                    effectManager.MoveScope(board, x - 1, y + 1);
                }

                if (x < 7)
                {
                    effectManager.MoveScope(board, x + 1, y + 1);

                }

                if (board[x][y + 1].Piece == null && IsPossibleFirstChance)
                {
                    effectManager.MoveScope(board, x, y + 2);
                }
            }
        }
    }
}
