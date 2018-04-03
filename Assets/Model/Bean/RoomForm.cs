using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Assets.Support;

namespace Assets.Model.Bean
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RoomForm
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string OwnerId { get; set; }

        [JsonProperty("other")]
        public string OtherId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("gameMode")]
        public GameMode GameMode { get; set; }
    }
}
