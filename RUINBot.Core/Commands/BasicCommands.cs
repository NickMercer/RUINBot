using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("ping RUINBot")]
        public async Task Ping(CommandContext context)
        {
            await context.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(context.Client, ":ping_pong:");

            await context.RespondAsync($"{emoji} Pong! Ping: {context.Client.Ping}ms");
        }

        [Command("greet")]
        [Description("Says hi to specified user.")]
        [Aliases("hi", "hello", "sayhi", "say_hi", "say_hello", "hello!", "hey", "hey!")]
        public async Task Greet(CommandContext context, [Description("the user to say hi to.")] DiscordMember member = null)
        {
            await context.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(context.Client, ":wave:");

            if(member == null)
            {
                member = context.User as DiscordMember;
            }

            await context.RespondAsync($"{emoji} Hello, {member.Mention}!");
        }

        [Command("disappointedmarci")]
        [Description("Sighs at Jon's life decisions")]
        public async Task DisappointedMarci(CommandContext context)
        {
            await context.Channel.SendMessageAsync("*Sigh* Jooonnnn");
        }

        [Command("overwatch")]
        [Description("How long does it take to matchmake?")]
        public async Task Overwatch(CommandContext context)
        {
            await context.Channel.SendMessageAsync("What is this, Overwatch?");
        }

        [Command("random")]
        [Description("Returns a random number between two values")]
        public async Task Random(CommandContext context, [Description("Minimum Number")] int min, [Description("Maximum Number")] int max)
        {
            await context.TriggerTypingAsync();

            var rnd = new Random();

            var dieEmoji = DiscordEmoji.FromName(context.Client, ":game_die:");

            await context.RespondAsync($"{dieEmoji} Your random number is: {rnd.Next(min, max)}");
        }
    }
}
