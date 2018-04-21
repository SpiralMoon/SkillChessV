using System.Collections.Generic;

using Assets.Service;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Bishop의 모델.
    /// </summary>
    public class Bishop : Piece
    {
        public Bishop(string color) : base(color)
        {
            this.PieceName = "Bishop";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // Top Left
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i][j].Piece?.Color != Color)
                {
                    board[i][j].IsPossibleMove = true;
                }
                   
                if (board[i][j].Piece != null)
                {
                    break;
                }
            }

            // Top Right
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (board[i][j].Piece?.Color != Color)
                {
                    board[i][j].IsPossibleMove = true;
                }

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }

            // Bottom Left
            for (int i = x - 1, j = y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (board[i][j].Piece?.Color != Color)
                {
                    board[i][j].IsPossibleMove = true;
                }

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }

            // Bottom Right
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
            {
                if (board[i][j].Piece?.Color != Color)
                {
                    board[i][j].IsPossibleMove = true;
                }

                if (board[i][j].Piece != null)
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
            
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                effectManager.MoveScope(board, i, j);

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }
            
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                effectManager.MoveScope(board, i, j);

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }
            
            for (int i = x - 1, j = y + 1; i >= 0 && j < 8; i--, j++)
            {
                effectManager.MoveScope(board, i, j);

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }
            
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
            {
                effectManager.MoveScope(board, i, j);

                if (board[i][j].Piece != null)
                {
                    break;
                }
            }
        }
    }
}
