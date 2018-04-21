using System.Collections.Generic;

using Assets.Service;

namespace Assets.Model.ChessPiece
{
    /// <summary>
    /// Knight의 모델.
    /// </summary>
    public class Knight : Piece
    {
        public Knight(string color) : base(color)
        {
            this.PieceName = "Knight";
            this.Color = color;
            this.IsPossibleCastling = false;
            this.IsPossibleFirstChance = false;
        }

        public override void SetMoveStatus(List<Board[]> board, Location location)
        {
            var x = location.X;
            var y = location.Y;

            // Top[2], Right[1]
            if (x < 7 && y > 1)
            {
                if (board[x + 1][y - 2].Piece?.Color != Color)
                {
                    board[x + 1][y - 2].IsPossibleMove = true;
                }
            }

            // Top[1], Right[2]
            if (x < 6 && y > 0)
            {
                if (board[x + 2][y - 1].Piece?.Color != Color)
                {
                    board[x + 2][y - 1].IsPossibleMove = true;
                }
            }

            // Bottom[1], Right[2]
            if (x < 6 && y < 7)
            {
                if (board[x + 2][y + 1].Piece?.Color != Color)
                {
                    board[x + 2][y + 1].IsPossibleMove = true;
                }
            }

            // Bottom[2], Right[1]
            if (x < 7 && y < 6)
            {
                if (board[x + 1][y + 2].Piece?.Color != Color)
                {
                    board[x + 1][y + 2].IsPossibleMove = true;
                }
            }

            // Bottom[2], Left[1]
            if (x > 0 && y < 6)
            {
                if (board[x - 1][y + 2].Piece?.Color != Color)
                {
                    board[x - 1][y + 2].IsPossibleMove = true;
                }
            }

            // Bottom[1], Left[2]
            if (x > 1 && y < 7)
            {
                if (board[x - 2][y + 1].Piece?.Color != Color)
                {
                    board[x - 2][y + 1].IsPossibleMove = true;
                }
            }

            // Top[1], Left[2]
            if (x > 1 && y > 0)
            {
                if (board[x - 2][y - 1].Piece?.Color != Color)
                {
                    board[x - 2][y - 1].IsPossibleMove = true;
                }
            }

            // Top[2], Left[1]
            if (x > 0 && y > 1)
            {
                if (board[x - 1][y - 2].Piece?.Color != Color)
                {
                    board[x - 1][y - 2].IsPossibleMove = true;
                }
            }    
        }

        public override void ShowMoveScope(List<Board[]> board, Location location)
        {
            var effectManager = EffectManager.GetInstance();
            var x = location.X;
            var y = location.Y;
            
            if (x < 7 && y > 1)
            {
                effectManager.MoveScope(board, x + 1, y - 2);
            }
            
            if (x < 6 && y > 0)
            {
                effectManager.MoveScope(board, x + 2, y - 1);
            }
            
            if (x < 6 && y < 7)
            {
                effectManager.MoveScope(board, x + 2 , y + 1);
            }
            
            if (x < 7 && y < 6)
            {
                effectManager.MoveScope(board, x + 1, y + 2);
            }
            
            if (x > 0 && y < 6)
            {
                effectManager.MoveScope(board, x - 1, y + 2);
            }
            
            if (x > 1 && y < 7)
            {
                effectManager.MoveScope(board, x - 2, y + 1);
            }
            
            if (x > 1 && y > 0)
            {
                effectManager.MoveScope(board, x - 2, y - 1);
            }
            
            if (x > 0 && y > 1)
            {
                effectManager.MoveScope(board, x - 1, y - 2);
            }
        }
    }
}
