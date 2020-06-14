using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Commands
{
    public class ImageCommands
    {
        [Command("meme")]
        [Description("Returns a meme")]
        public async Task Meme(CommandContext context)
        {
            var imgurClient = new ImgurClient("ebbe87bca3496b9");
            var endpoint = new GalleryEndpoint(imgurClient);
            IEnumerable<IGalleryImage> images = await endpoint.GetSubredditGalleryAsync("memes");

            int imageIndex = RandomExtension.Range(0, images.Count() - 1);

            var meme = images.ElementAt(imageIndex);

            var memeEmbed = new DiscordEmbedBuilder
            {
                Title = meme.Title,
                ImageUrl = meme.Link
            };

            await context.Channel.SendMessageAsync(embed: memeEmbed);
        }

        [Command("siege")]
        [Description("Posts the siege meme")]
        public async Task Siege(CommandContext context)
        {
            await context.Channel.SendFileAsync("Images\\siegememe.JPG", "Devin's Magnum Opus:");
        }

        [Command("poopfuel")]
        [Description("Let everyone know you're making coffee.")]
        public async Task PoopFuel(CommandContext context)
        {
            await context.Channel.SendFileAsync("Images\\coffee.jpeg", $"{context.User.Username} is brewing up some of that sweet sweet poopfuel. BRB");
        }

        [Command("dinkleberg")]
        [Description("Dinkleberg...")]
        public async Task Dinkleberg(CommandContext context)
        {
            await context.Channel.SendFileAsync("Images\\Dinkleberg.jpg", $"Dinkleberg...");
        }

        [Command("disappointed")]
        [Description("Posts the disappointed meme")]
        public async Task Disappointed(CommandContext context)
        {
            await context.Channel.SendFileAsync("Images\\Disappointed.gif", "DisaPPOINTED!!!");
        }

        [Command("csimiami")]
        [Description("YEAAAAAAAAAAAAAAAAAAA")]
        public async Task CsiMiami(CommandContext context)
        {
            string csiLine = RandomExtension.Choose(new List<string> { "Looks like you aren't letting your memes... be dreams.", "Looking at the scene I'm convinced our suspect is will smith. He left some... fresh prints", "Fool me once, shame on you. Fool me twice...", "I don't always listen to the Who, but when I do I like to scream...", "CSI Miami was cancelled. I guess it was time for miami to go into... retirement." });
            await context.Channel.SendFileAsync("Images\\csimiami.png", csiLine);
        }

        [Command("shiadoit")]
        [Description("Just... DO IT!")]
        public async Task ShiaDoIt(CommandContext context)
        {
            string shiaLine = RandomExtension.Choose(new List<string> { "Yesterday, you said tomorrow... So just... DO IT", "Just... DO IT!", "Don't let your dreams be dreams!", "Some people dream success, but you're going to get out there, and you're going to work HARD AT IT!", "NOTHING IS IMPOSSIBLE" });
            await context.Channel.SendFileAsync("Images\\shiadoit.gif", shiaLine);
        }
        [Command("shiaflex")]
        [Description("Just... DO IT!")]
        public async Task ShiaFlex(CommandContext context)
        {
            await context.Channel.SendFileAsync("Images\\shiaflex.gif", "I... don't know what this is.");
        }
    }
}
