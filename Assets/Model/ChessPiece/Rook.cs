using System.Collections.Generic;

using Assets.Service;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Rook의 모델.
    /// </summary>
    public class Rook : Piece
    {
        public Rook(string color) : base(color)
        {
            this.PieceName = "Rook";
            this.Color = color;
            this.IsPossibleCastling = true;
            this.IsPossibleFirstChance = false;
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // Left
            for (int i = x - 1; i >= 0; i--)
            {
                // 내 말이 아닌 경우
                if (board[i][y].Piece?.Color != Color)
                {
                    board[i][y].IsPossibleMove = true;
                }

                // 기물이 있는 경우
                if (board[i][y].Piece != null)
                {
                    break;
                }
            }

            //Right
            for (int i = x + 1; i < 8; i++)
            {
                if (board[i][y].Piece?.Color != Color)
                {
                    board[i][y].IsPossibleMove = true;
                }
                
                if (board[i][y].Piece != null)
                {
                    break;
                }
            }
            
            // Top
            for (int j = y - 1; j >= 0; j--)
            {
                if (board[x][j].Piece?.Color != Color)
                {
                    board[x][j].IsPossibleMove = true;
                }

                if (board[x][j].Piece != null)
                {
                    break;
                }
            }

            // Bottom
            for (int j = y + 1; j < 8; j++)
            {
                if (board[x][j].Piece?.Color != Color)
                {
                    board[x][j].IsPossibleMove = true;
                }

                if (board[x][j].Piece != null)
                {
                    break;
                }
            }
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            var effectManager = EffectManager.GetInstance();
            var x = location.X;
            var y = location.Y;
            
            for (int i = x - 1; i >= 0; i--)
            {
                effectManager.MoveScope(board, i, y);

                if (board[i][y].Piece != null)
                {
                    break;
                }
            }
            
            for (int i = x + 1; i < 8; i++)
            {
                effectManager.MoveScope(board, i, y);

                if (board[i][y].Piece != null)
                {
                    break;
                }
            }
            
            for (int j = y - 1; j >= 0; j--)
            {
                effectManager.MoveScope(board, x, j);

                if (board[x][j].Piece != null)
                {
                    break;
                }
            }
            
            for (int j = y + 1; j < 8; j++)
            {
                effectManager.MoveScope(board, x, j);

                if (board[x][j].Piece != null)
                {
                    break;
                }
            }
        }
    }
}
