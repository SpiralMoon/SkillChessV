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
    public class HttpStatus
    {
        [JsonProperty("HttpCode")]
        public HttpCode HttpCode;

        [JsonProperty("Message")]
        public string Message;
    }
}
