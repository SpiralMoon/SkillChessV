using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Support;

namespace Assets.Service
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager _instance;

        private static string _color;

        public GameObject CameraParent;

        public Camera Camera;

        /// <summary>
        /// 초기 줌인값
        /// </summary>
        private float _defaultZoom;

        /// <summary>
        /// 초기 위치
        /// </summary>
        private Vector3 _defaultPosition;

        /// <summary>
        /// 초기 각도
        /// </summary>
        private Quaternion _defaultRotation;

        private void Start()
        {
            // 기본값 세팅
            _defaultZoom = Camera.fieldOfView;
            _defaultPosition = Camera.transform.position;
            if (_color == Support.Color.WHITE)
            {
                _defaultRotation = CameraParent.transform.rotation;
            }
           else
            {
                _defaultRotation =  new Quaternion(0, -180, 0, 0);
            }
            CameraParent.transform.rotation = _defaultRotation;
        }

        private void Update()
        {
            // 마우스 우클릭 드래그
            if (Input.GetMouseButton(1))
            {
                // x, z축은 고정  -> y축만을 기준으로 화면 회전
                CameraParent.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 7, 0);
            }

            // 휠로 마우스 줌인 조정
            Camera.fieldOfView += (20 * Input.GetAxis("Mouse ScrollWheel"));

            // 카메라 확대 제한
            if (Camera.fieldOfView < 20)
            {
                Camera.fieldOfView = 20;
            }
            // 카메라 축소 제한
            if (Camera.fieldOfView > 90)
            {
                Camera.fieldOfView = 90;
            }

            // 마우스 휠 클릭
            if (Input.GetMouseButton(2))
            {
                // 카메라 위치 초기화
                Camera.fieldOfView = _defaultZoom;

                if (_color == Support.Color.WHITE)
                {
                    Camera.transform.position = _defaultPosition;
                    CameraParent.transform.rotation = _defaultRotation;
                }
                else
                {
                    Camera.transform.position.Set(0, 80, -45);
                    CameraParent.transform.rotation = _defaultRotation;
                }
            }
        }

        public void Run(GameObject cameraService, string color)
        {
            _color = color;

            cameraService.SetActive(true);
        }

        public static CameraManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CameraManager();
            }

            return _instance;
        }
    }
}
