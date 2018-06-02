using Assets.Support;
using Assets.Model;

using Newtonsoft.Json;

namespace Assets.Model.Bean
{
    public class ResultForm
    {
        /// <summary>
        /// 행동 분류
        /// </summary>
        [JsonProperty("pattern")]
        public Pattern Pattern;

        /// <summary>
        /// 이 메시지를 보내는 플레이어의 Id
        /// </summary>
        [JsonProperty("id")]
        public string Id;

        /// <summary>
        /// 이 메시지를 보내는 플레이어의 색
        /// </summary>
        [JsonProperty("color")]
        public string Color;
    }
}
