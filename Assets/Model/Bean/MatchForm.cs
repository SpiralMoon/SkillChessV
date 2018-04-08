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
    public class MatchForm
    {
        [JsonProperty("gameMode")]
        public GameMode GameMode;

        [JsonProperty("enemy")]
        public Rank Enemy;

        [JsonProperty("myColor")]
        public string Color;

        [JsonProperty("enemyLineUp")]
        public LineUp EnemyLineUp;

        [JsonProperty("myLineUp")]
        public LineUp MyLineUp;
    }
}
