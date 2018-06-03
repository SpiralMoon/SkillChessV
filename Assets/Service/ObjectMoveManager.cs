using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Service
{
    public class ObjectMoveManager : MonoBehaviour
    {
        private static ObjectMoveManager _instance;

        /// <summary>
        /// 움직일 기물 객체 목록
        /// </summary>
        private static List<GameObject> _pieces;

        /// <summary>
        /// 도착점 발판 위치 목록
        /// </summary>
        private static List<Vector3> _boardPositions;

        /// <summary>
        /// X축 간격
        /// </summary>
        private float _distanceX;

        /// <summary>
        /// Z축 간격
        /// </summary>
        private float _distanceZ;

        /// <summary>
        /// 이동 속도
        /// </summary>
        private float _moveSpeed;

        public ObjectMoveManager()
        {
            _pieces = new List<GameObject>();
            _boardPositions = new List<Vector3>();
        }

        /// <summary>
        /// 이동이 예약되어있는 객체들을 병렬로 이동시킴
        /// </summary>
        private void Update()
        {
            if (_pieces.Any())
            {
                for(int i = 0; i < _pieces.Count; i++)
                {
                    try
                    {
                        _distanceX = _boardPositions[i].x - _pieces[i].transform.position.x;
                        _distanceZ = _boardPositions[i].z - _pieces[i].transform.position.z;

                        if (_distanceX < 0) _distanceX = _distanceX - _distanceX * 2;
                        if (_distanceZ < 0) _distanceZ = _distanceZ - _distanceZ * 2;

                        _moveSpeed = (_distanceX >= _distanceZ) ? _distanceX : _distanceZ;
                        _moveSpeed = _moveSpeed * 5 * Time.deltaTime;

                        _pieces[i].transform.position = Vector3.MoveTowards(
                            _pieces[i].transform.position,
                            _boardPositions[i],
                            _moveSpeed);

                        // 기물이 도착했을 때
                        if (IsArrived(_pieces[i].transform.position, _boardPositions[i]))
                        {
                            _pieces.RemoveAt(i);
                            _boardPositions.RemoveAt(i);
                            i--;
                        }
                    }
                    catch
                    {
                        _pieces.RemoveAt(i);
                        _boardPositions.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// 객체 이동을 예약함
        /// </summary>
        /// <param name="pieceObj">움직일 기물 객체</param>
        /// <param name="boardObj">도착점 발판 객체</param>
        public void Move(GameObject pieceObj, GameObject boardObj)
        {
            _pieces.Add(pieceObj);
            _boardPositions.Add(new Vector3(
                boardObj.transform.position.x,
                boardObj.transform.position.y + 1,
                boardObj.transform.position.z));
        }

        /// <summary>
        /// 객체가 목적지에 도착했는지 판단
        /// </summary>
        /// <param name="pieceObjPosition"></param>
        /// <param name="boardObjPosition"></param>
        public bool IsArrived(Vector3 pieceObjPosition, Vector3 boardObjPosition)
        {
            var isArrived = false;
            var distance = pieceObjPosition - boardObjPosition;

            // 각 좌표의 오차가 0.2 이하이면 도착으로 간주
            if (Math.Abs(distance.x) <= 0.2 &&
                Math.Abs(distance.y) <= 0.2 &&
                Math.Abs(distance.z) <= 0.2)
            {
                isArrived = true;
            }

            return isArrived;
        }

        public static ObjectMoveManager GetInstance()
        {
            return _instance ?? (_instance = new ObjectMoveManager());
        }
    }
}
