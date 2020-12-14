using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Core.Models
{
    public class Joke
    {
        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
