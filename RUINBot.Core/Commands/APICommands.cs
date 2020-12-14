using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RUINBot.Core.Models;
using RUINBot.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class APICommands : BaseCommandModule
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static ImgurMemeGallery MemeGallery;

        [Command("joke")]
        [Description("RUINBot attempts a 'dad joke'")]
        [Aliases("dadjoke", "groaner", "icanhazdadjoke")]
        public async Task Joke(CommandContext context)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://icanhazdadjoke.com/slack"))
            {
                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

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


        [Command("meme")]
        [Description("Returns a meme")]
        public async Task Meme(CommandContext context)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.imgur.com/3/gallery/r/memes"))
            {
                if(MemeGallery == null || MemeGallery.TimeDownloaded < DateTime.Now.AddHours(-12))
                { 
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Client-ID", $"{GlobalConfig.ImgurId}");
                    HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var memeGalleryString = await response.Content.ReadAsStringAsync();
                        MemeGallery = JsonConvert.DeserializeObject<ImgurMemeGallery>(memeGalleryString);
                        MemeGallery.TimeDownloaded = DateTime.Now;
                        MemeGallery.Memes = MemeGallery.Memes.Where(x => x.Type != null && x.Type.StartsWith("image")).ToList();
                        context.Client.Logger.Log(LogLevel.Information, "RUINBot just fetched fresh memes from Imgur.");
                    }
                    else
                    {
                        await context.Channel.SendMessageAsync("Hmmm, for some reason the dank memes have temporarily run dry.");
                        context.Client.Logger.Log(LogLevel.Information, $"ERROR: RUINBot was unable to collect memes from Imgur. response code was {response.StatusCode}");
                        return;
                    }
                }
                
                var randomIndex = RandomExtension.Range(0, MemeGallery.Memes.Count - 1);
                var meme = MemeGallery.Memes[randomIndex];

                var memeEmbed = new DiscordEmbedBuilder
                {
                    Title = meme.Title,
                    Color = new DiscordColor(52, 201, 189),
                    ImageUrl = meme.Link
                };

                await context.Channel.SendMessageAsync(embed: memeEmbed);
            }
        }
    }
}
