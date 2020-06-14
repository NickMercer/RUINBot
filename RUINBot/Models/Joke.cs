using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Models
{
    public class Attachment
    {
        public string text { get; set; }
    }

    public class Joke
    {
        public IList<Attachment> attachments { get; set; }
    }
}
