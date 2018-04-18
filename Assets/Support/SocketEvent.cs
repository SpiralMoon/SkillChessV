using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Support
{
    public class SocketEvent
    {
        /// <summary>
        /// 방 만들기 (Owner)
        /// </summary>
        public const string CREATE = "create";

        /// <summary>
        /// 방 삭제하기 (Owner)
        /// </summary>
        public const string CLOSE = "close";

        /// <summary>
        /// 방 입장하기 (Other)
        /// </summary>
        public const string JOIN = "join";

        /// <summary>
        /// 방 목록 불러오기
        /// </summary>
        public const string LIST = "list";

        /// <summary>
        /// 채팅
        /// </summary>
        public const string CHATTING = "chatting";

        /// <summary>
        /// Player 1, 2가 모두 방에 입장 했을 때 받는 이벤트
        /// </summary>
        public const string MATCH = "match";

        /// <summary>
        /// 게임화면에 로딩이 되었을 때 보내는 이벤트
        /// </summary>
        public const string READY = "ready";

        /// <summary>
        /// 게임이 시작되었을 때 받는 이벤트 (이전까지는 움직일 수 없음)
        /// </summary>
        public const string START = "start";

        /// <summary>
        /// 게임 내용 진행
        /// </summary>
        public const string RELAY = "relay";

        /// <summary>
        /// 
        /// </summary>
        public const string DISCONNECT = "disconnect";
    }
}
