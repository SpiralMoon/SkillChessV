using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Assets.Model.Bean
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MatchForm
    {
        [JsonProperty("enemy")]
        public Rank Enemy;

        [JsonProperty("myColor")]
        public string Color;

        [JsonProperty("enemyLineUp")]
        public object EnemyLineUp;

        [JsonProperty("myLineUp")]
        public object MyLineUp;
    }
}
