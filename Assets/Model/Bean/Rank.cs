using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Assets.Model.Bean
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Rank
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
