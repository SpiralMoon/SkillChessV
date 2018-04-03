using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Assets.Model.Bean
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChatForm
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
