using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using RUINBot.Core.Models;
using RUINBot.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class APICommands : BaseCommandModule
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        [Command("joke")]
        [Description("RUINBot attempts a 'dad joke'")]
        [Aliases("dadjoke", "groaner", "icanhazdadjoke")]
        public async Task Joke(CommandContext context)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://icanhazdadjoke.com/slack");

            if (response.IsSuccessStatusCode)
            {
                string jokeJson = await response.Content.ReadAsStringAsync();
                var joke = JsonConvert.DeserializeObject<Joke>(jokeJson);

                await context.Channel.SendMessageAsync(joke.Attachments[0].Text);
            }
            else
            {
                await context.Channel.SendMessageAsync("Sorry, I'm fresh out of dad jokes. I was going to tell you the one about the garbage truck... but it stinks.");
            }
        }
    }
}
