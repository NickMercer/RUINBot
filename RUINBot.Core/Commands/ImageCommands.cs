using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RUINBot.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class ImageCommands : BaseCommandModule
    {
        [Command("siege")]
        [Description("Posts the siege meme")]
        public async Task Siege(CommandContext context)
        {
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}siegememe.JPG"), "Devin's Magnum Opus:");
        }

        [Command("poopfuel")]
        [Description("Let everyone know you're making coffee.")]
        public async Task PoopFuel(CommandContext context)
        {
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}coffee.jpeg"), $"{context.User.Username} is brewing up some of that sweet sweet poopfuel. BRB");
        }

        [Command("dinkleberg")]
        [Description("Dinkleberg...")]
        public async Task Dinkleberg(CommandContext context)
        {
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}Dinkleberg.jpg"), $"Dinkleberg...");
        }

        [Command("disappointed")]
        [Description("Posts the disappointed meme")]
        public async Task Disappointed(CommandContext context)
        {
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}Disappointed.gif"), "DisaPPOINTED!!!");
        }

        [Command("csimiami")]
        [Description("YEAAAAAAAAAAAAAAAAAAA")]
        public async Task CsiMiami(CommandContext context)
        {
            string csiLine = RandomExtension.Choose(new List<string> { "Looks like you aren't letting your memes... be dreams.", "Looking at the scene I'm convinced our suspect is will smith. He left some... fresh prints", "Fool me once, shame on you. Fool me twice...", "I don't always listen to the Who, but when I do I like to scream...", "CSI Miami was cancelled. I guess it was time for miami to go into... retirement." });
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}csimiami.png"), csiLine);
        }

        [Command("shiadoit")]
        [Description("Just... DO IT!")]
        public async Task ShiaDoIt(CommandContext context)
        {
            string shiaLine = RandomExtension.Choose(new List<string> { "Yesterday, you said tomorrow... So just... DO IT", "Just... DO IT!", "Don't let your dreams be dreams!", "Some people dream success, but you're going to get out there, and you're going to work HARD AT IT!", "NOTHING IS IMPOSSIBLE" });
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}shiadoit.gif"), shiaLine);
        }

        [Command("shiaflex")]
        [Description("Just... don't ask.")]
        public async Task ShiaFlex(CommandContext context)
        {
            await context.Channel.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), $"Images{Path.DirectorySeparatorChar}shiaflex.gif"), "I... don't know what this is.");
        }
    }
}
