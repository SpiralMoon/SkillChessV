using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Model;
using Assets.Model.ChessPiece;

namespace Assets.Support.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// 기물의 위치를 얻음
        /// </summary>
        /// <param name="board"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static Location GetLocation (this List<Board[]> board, Piece piece)
        {
            Location location = null;

            for(int i = 0; i < board.Count; i++)
            {
                for(int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j].Piece == piece)
                    {
                        location = new Location(i, j);
                        break;
                    }
                }
            }

            return location;
        }
    }
}
