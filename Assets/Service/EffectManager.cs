using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model;
using Assets.Model.ChessPiece;
using Assets.Support.Extension;

namespace Assets.Service
{
    public class EffectManager : MonoBehaviour
    {
        private static EffectManager _instance;

        /// <summary>
        /// 내 기물을 선택했을 때 표시하는 이펙트
        /// </summary>
        private static GameObject _selectMyPiece;

        /// <summary>
        /// 적 기물이나 빈 바닥을 선택했을 때 표시하는 이펙트
        /// </summary>
        private static GameObject _selectAnother;

        private static List<GameObject> _moveScopes;

        private static List<GameObject> _skillScopes;

        public EffectManager()
        {
            _moveScopes = new List<GameObject>();
            _skillScopes = new List<GameObject>();
        }

        /// <summary>
        /// 화면에 표시된 선택 이펙트를 모두 지움
        /// </summary>
        public void Clear()
        {
            if (_selectMyPiece != null)
            {
                Destroy(_selectMyPiece);
            }

            if (_selectAnother != null)
            {
                Destroy(_selectAnother);
            }

            if (_moveScopes.Any())
            {
                foreach(var temp in _moveScopes)
                {
                    Destroy(temp);
                }
            }

            if (_skillScopes.Any())
            {
                foreach (var temp in _skillScopes)
                {
                    Destroy(temp);
                }
            }
        }

        /// <summary>
        /// 선택 이펙트 표시
        /// </summary>
        public void Select(List<Board[]> board, Location location, string myColor)
        {
            if (board[location.X][location.Y].Piece?.Color == myColor)
            {
                _selectMyPiece = Instantiate(Resources.Load<GameObject>("Effect/SelectMe"));
            }
            else
            {
                _selectMyPiece = Instantiate(Resources.Load<GameObject>("Effect/SelectAnother"));
            }

            _selectMyPiece.SetPosition(board, location);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="board"></param>
       /// <param name="x"></param>
       /// <param name="y"></param>
        public void MoveScope(List<Board[]> board, int x ,int y)
        {
            GameObject scope = null;

            if (board[x][y].IsPossibleMove)
            {
                scope = Instantiate(Resources.Load<GameObject>("Effect/EnabledMove"));
            }
            else
            {
                scope = Instantiate(Resources.Load<GameObject>("Effect/DisabledMove"));

            }

            scope.SetPosition(board, new Location(x, y));
            _moveScopes.Add(scope);
        }

        /// <summary>
        /// 기물이 이동할 수 있는 발판과 이동할 수 없는 발판 표시
        /// </summary>
        /// <param name="board"></param>
        public void MoveScope(List<Board[]> board, Location location)
        {
            var piece = board[location.X][location.Y].Piece ?? null;

            if (piece == null)
            {
                return;
            }

            piece.ShowMoveScope(board, location);
        }



        public static EffectManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }
    }
}
