using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Assets.Model.Bean
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LineUp
    {
        [JsonProperty("king")]
        public int King;

        [JsonProperty("queen")]
        public int Queen;

        [JsonProperty("knight")]
        public int[] Knight;

        [JsonProperty("bishop")]
        public int[] Bishop;

        [JsonProperty("rook")]
        public int[] Rook;

        [JsonProperty("pawn")]
        public int[] Pawn;
    }
}
