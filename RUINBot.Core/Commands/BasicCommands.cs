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
    }
}
