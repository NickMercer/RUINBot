using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Core.Models
{
    public class ImgurMeme
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ImgurMemeGallery
    {
        [JsonProperty("data")]
        public IList<ImgurMeme> Memes { get; set; }

        [JsonIgnore]
        public DateTime TimeDownloaded { get; set; }
    }
}
