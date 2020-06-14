using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using RUINBot.Models;

namespace RUINBot.Commands
{
    public class FunCommands
    {
        [Command("disappointedmarci")]
        [Description("Sighs at Jon's life decisions")]
        public async Task DisappointedMarci(CommandContext context)
        {
            await context.Channel.SendMessageAsync("*Sigh* Jooonnnn").ConfigureAwait(false);
        }

        [Command("overwatch")]
        [Description("How long does it take to matchmake?")]
        public async Task Overwatch(CommandContext context)
        {
            await context.Channel.SendMessageAsync("What is this, Overwatch?").ConfigureAwait(false);
        }

        [Command("random")]
        [Description("Returns a random number between two values")]
        public async Task Random(CommandContext context, [Description("Minimum Number")] int min, [Description("Maximum Number")] int max)
        {
            var rnd = new Random();
            await context.RespondAsync($"🎲 Your random number is: {rnd.Next(min, max)}");
        }

        [Command("joke")]
        [Description("RUINBot attempts a 'dad joke'")]
        public async Task Joke(CommandContext context)
        {
            if(RandomExtension.Range(0,24) == 1)
            {
                await context.Channel.SendMessageAsync("I'm all out of dad jokes. Keith?");
                return;
            }


            HttpResponseMessage response = await new HttpClient().GetAsync("https://icanhazdadjoke.com/slack");
            Joke joke;

            if(response.IsSuccessStatusCode)
            {
                string jokeString = await response.Content.ReadAsStringAsync();
                joke = JsonConvert.DeserializeObject<Joke>(jokeString);
                await context.Channel.SendMessageAsync(joke.attachments[0].text);
                return;
            }


            await context.Channel.SendMessageAsync("Man, I got nothing.");

        }


        [Command("pressf")]
        [Description("Can we get an F in the chat?")]
        public async Task PressF(CommandContext context)
        {
            //On Command, send "Can I get an F in the chat" message and begin listening
            var initMsg = await context.Channel.SendMessageAsync("Can I get an F in the chat?");
            
            var interactivity = context.Client.GetInteractivityModule();

            var members = await context.Guild.GetAllMembersAsync();
            var onlineMembers = members.Where(x => x.Presence != null && x.Presence.Status == UserStatus.Online && x.IsBot == false).Distinct().ToList();

            Thread.Sleep(TimeSpan.FromSeconds(10));

            var messages = await context.Channel.GetMessagesAsync(100, after: initMsg.Id);
            
            List<DiscordUser> respondingMembers = new List<DiscordUser>();

            foreach(DiscordMessage discordMessage in messages)
            {
                if(discordMessage.Content.ToLower() == "f")
                {
                    respondingMembers.Add(discordMessage.Author);
                }
            }

            List<DiscordMember> noResponseMembers = onlineMembers;
            foreach(DiscordUser user in respondingMembers)
            {
                noResponseMembers.Remove(noResponseMembers.Where(x => x.Id == user.Id).First());
            }

            bool allResponded = (noResponseMembers.Count() == 0);

            //After 30 seconds, if every online member has sent a message that is just "F", show COD meme
            if (allResponded)
            {
                await context.Channel.SendFileAsync("Images\\PressF.jpg", "I protecc, I rekk, but mostly, I press F to pay respecc.");
            }
            else //If not, shame whoever didn't press F
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < noResponseMembers.Count(); i++)
                {
                    if (i == 0) sb.Append($"{noResponseMembers[i].Username}");
                    else
                    {
                        if (i < noResponseMembers.Count - 1) sb.Append($", {noResponseMembers[i].Username}");
                        if (i == noResponseMembers.Count - 1) sb.Append($" and {noResponseMembers[i].Username}");
                    }
                }

                string shameList = sb.ToString();
                await context.Channel.SendMessageAsync($"Wow, {shameList}. Do you even Respecc?");
            }
        }
    }
}
