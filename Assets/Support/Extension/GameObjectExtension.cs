using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model;

namespace Assets.Support.Extension
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 객체의 위치를 발판 바로 위로 설정.
        /// </summary>
        /// <param name="gameObject">위치를 설정할 객체</param>
        /// <param name="board"></param>
        /// <param name="boardLocation">board의 좌표</param>
        public static void SetPosition(this GameObject gameObject, List<Board[]> board, Location boardLocation)
        {
            var x = boardLocation.X;
            var y = boardLocation.Y;

            var target = new
            {
                x = board[x][y].BoardObj.transform.position.x,
                y = board[x][y].BoardObj.transform.position.y + 1,
                z = board[x][y].BoardObj.transform.position.z
            };

            gameObject.transform.position = new Vector3(target.x, target.y, target.z);
        }
    }
}
