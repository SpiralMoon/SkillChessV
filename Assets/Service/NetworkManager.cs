using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocket4Net;
using Quobject.SocketIoClientDotNet.Client;

using Assets.Model;
using Assets.Model.Bean;
using Assets.Support;

namespace Assets.Service
{
    public class NetworkManager
    {
        private Setting _setting;

        private string _baseUrl;

        private string _subUrl;

        private static NetworkManager _instance;

        private static HttpWebRequest _request;

        private static HttpWebResponse _response;

        private static Socket _io;

        public EventHandler<RoomForm> OnCreateRoom;

        public EventHandler<RoomForm> OnCloseRoom;

        public EventHandler<List<RoomForm>> OnLoadRoomList;

        public EventHandler<MatchForm> OnMatchBattle;

        public EventHandler OnStartBattle;

        public EventHandler<RelayForm> OnRelayBattle;

        public EventHandler<ChatForm> OnChat;

        public NetworkManager()
        {
            _baseUrl = "http://192.168.0.185:21214";
            _setting = Setting.GetInstance();
        }

        public static NetworkManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NetworkManager();
            }

            return _instance;
        }

        /// <summary>
        /// Socket.io를 킴
        /// </summary>
        public void SocketOpen()
        {
            if (_io == null)
            {
                _io = IO.Socket("ws://localhost:21214/");

                _io.On(SocketEvent.LIST, (data) =>
                {
                    if (data != null)
                    {
                        data = JsonConvert.DeserializeObject<List<RoomForm>>(data.ToString());
                    }

                    OnLoadRoomList?.Invoke(this, data as List<RoomForm>);
                });
                _io.On(SocketEvent.CREATE, (data) =>
                {
                    if (data != null)
                    {
                        data = JsonConvert.DeserializeObject<RoomForm>(data.ToString());
                    }

                    OnCreateRoom?.Invoke(this, data as RoomForm);
                });
                _io.On(SocketEvent.CLOSE, (data) =>
                {
                    if (data != null)
                    {
                        data = JsonConvert.DeserializeObject<RoomForm>(data.ToString());
                    }

                    OnCreateRoom?.Invoke(this, data as RoomForm);
                });
                _io.On(SocketEvent.MATCH, (data) =>
                {
                    if (data != null)
                    {
                        data = JsonConvert.DeserializeObject<MatchForm>(data.ToString());
                    }

                    OnMatchBattle?.Invoke(this, data as MatchForm);
                });
                _io.On(SocketEvent.START, (data) =>
                {
                    OnStartBattle?.Invoke(this, null);
                });
                _io.On(SocketEvent.CHATTING, (data) =>
                {
                    if (data != null)
                    {
                        data = JsonConvert.DeserializeObject<ChatForm>(data.ToString());
                    }

                    OnChat?.Invoke(this, data as ChatForm);
                });
            }
        }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="email">계정</param>
        /// <param name="password">비밀번호</param>
        /// <returns></returns>
        public async Task<HttpStatus> SignIn(string email, string password)
        {
            //파라미터 준비
            var postParameters = new Dictionary<string, string>();
            var postData = "";
            _subUrl = "/API/signin";

            postParameters.Add("email", email);
            postParameters.Add("password", password);

            //postData에 파라미터 삽입
            foreach (string key in postParameters.Keys)
            {
                postData += key + "=" +
                    postParameters[key] + "&";
            }

            var data = Encoding.UTF8.GetBytes(postData);

            _request = (HttpWebRequest)WebRequest.Create(_baseUrl + _subUrl);
            _request.Method = "POST";
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.ContentLength = data.Length;

            //로그인 데이터 전송
            var requestStream = _request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            //로그인 요청값 승인
            _response = (HttpWebResponse)_request.GetResponse();
            var reader = new StreamReader(_response.GetResponseStream(), Encoding.GetEncoding(_response.CharacterSet));
            var httpStatus = JsonConvert.DeserializeObject<HttpStatus>(reader.ReadToEnd());

            return httpStatus;
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <param name="email">계정</param>
        /// <param name="password">비밀번호</param>
        /// <param name="nickname">닉네임</param>
        /// <returns></returns>
        public async Task<HttpStatus> SignUp(string email, string password, string nickname)
        {
            //파라미터 준비
            var postParameters = new Dictionary<string, string>();
            var postData = "";
            _subUrl = "/API/signup";

            postParameters.Add("email", email);
            postParameters.Add("password", password);
            postParameters.Add("nickname", nickname);

            //postData에 파라미터 삽입
            foreach (string key in postParameters.Keys)
            {
                postData += key + "=" +
                    postParameters[key] + "&";
            }

            var data = Encoding.UTF8.GetBytes(postData);

            _request = (HttpWebRequest)WebRequest.Create(_baseUrl + _subUrl);
            _request.Method = "POST";
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.ContentLength = data.Length;

            //회원가입 데이터 전송
            var requestStream = _request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            //회원가입 요청값 승인
            _response = (HttpWebResponse)_request.GetResponse();
            var reader = new StreamReader(_response.GetResponseStream(), Encoding.GetEncoding(_response.CharacterSet));
            var httpStatus = JsonConvert.DeserializeObject<HttpStatus>(reader.ReadToEnd());

            return httpStatus;
        }
        
        /// <summary>
        /// 랭킹 목록
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<List<Rank>> Rank(int pageNumber)
        {
            //파라미터 준비
            var postParameters = new Dictionary<string, string>();
            _subUrl = "/API/ranklist/" + pageNumber;

            _request = (HttpWebRequest)WebRequest.Create(_baseUrl + _subUrl);
            _request.Method = "GET";
            _request.ContentType = "application/x-www-form-urlencoded";

            //요청값 승인
            _response = (HttpWebResponse)_request.GetResponse();
            var reader = new StreamReader(_response.GetResponseStream(), Encoding.GetEncoding(_response.CharacterSet));
            var rankList = JsonConvert.DeserializeObject<List<Rank>>(reader.ReadToEnd());

            return rankList;
        }

        /// <summary>
        /// 내 랭킹
        /// </summary>
        /// <returns></returns>
        public async Task<Rank> Rank()
        {
            //파라미터 준비
            var postParameters = new Dictionary<string, string>();
            _subUrl = "/API/rank/" + _setting.Email;

            _request = (HttpWebRequest)WebRequest.Create(_baseUrl + _subUrl);
            _request.Method = "GET";
            _request.ContentType = "application/x-www-form-urlencoded";

            //요청값 승인
            _response = (HttpWebResponse)_request.GetResponse();
            var reader = new StreamReader(_response.GetResponseStream(), Encoding.GetEncoding(_response.CharacterSet));
            var rank = JsonConvert.DeserializeObject<Rank>(reader.ReadToEnd());

            return rank;
        }

        /// <summary>
        /// 방 만들기 요청
        /// </summary>
        /// <param name="room"></param>
        public void CreateRoom(RoomForm room)
        {
            var json = JsonConvert.SerializeObject(room);

            _io.Emit(SocketEvent.CREATE, json);
        }

        /// <summary>
        /// 방 입장 요청
        /// </summary>
        /// <param name="room"></param>
        public void JoinRoom(RoomForm room)
        {
            var json = JsonConvert.SerializeObject(room);

            _io.Emit(SocketEvent.JOIN, json);
        }

        /// <summary>
        /// 방 삭제 요청
        /// </summary>
        /// <param name="room"></param>
        public void CloseRoom(RoomForm room)
        {
            var json = JsonConvert.SerializeObject(room);

            _io.Emit(SocketEvent.CLOSE, json);
        }

        /// <summary>
        /// 방 목록 요청
        /// </summary>
        public void LoadRoomList()
        {
            _io.Emit(SocketEvent.LIST);
        }

        /// <summary>
        /// 게임화면 로딩이 완료(시작준비)되었음을 알림.
        /// </summary>
        public void ReadyGame()
        {
            _io.Emit(SocketEvent.READY);
        }

        /// <summary>
        /// 채팅 메시지 보내기
        /// </summary>
        /// <param name="chat"></param>
        public void Chat(ChatForm chat)
        {
            var json = JsonConvert.SerializeObject(chat);

            _io.Emit(SocketEvent.CHATTING, json);
        }
    }
}