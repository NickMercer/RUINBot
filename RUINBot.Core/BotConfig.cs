using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Core
{
    public struct BotConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string CommandPrefix { get; private set; }
        [JsonProperty("imgurid")]
        public string ImgurId { get; private set; }
    }
}
