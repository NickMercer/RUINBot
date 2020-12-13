﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class InteractivityCommands : BaseCommandModule
    {
        [Command("poll")]
        [Description("Run a poll with reactions.")]
        public async Task Poll(CommandContext context, [Description("How long should the poll last?")] TimeSpan duration, [Description("What emoji options should people have?")] params DiscordEmoji[] options)
        {
            var interactivity = context.Client.GetInteractivity();
            var pollOptions = options.Select(x => x.ToString());

            var embed = new DiscordEmbedBuilder
            {
                Title = "Poll time!",
                Description = string.Join(" ", pollOptions)
            };

            var message = await context.RespondAsync(embed: embed);

            for(var i = 0; i < options.Length; i++)
            {
                await message.CreateReactionAsync(options[i]);
            }

            var pollResult = await interactivity.CollectReactionsAsync(message, duration);
            
            if(pollResult.All(x => x.Total == pollResult[0].Total))
            {
                await context.RespondAsync("Looks like we have a tie.");
            }
            else
            {
                var results = pollResult.Where(x => options.Contains(x.Emoji)).Select(x => $"{x.Emoji}: {x.Total}");

                await context.RespondAsync("Here are the results! \n" + string.Join("\n", results));
            }
        }

        [Command("quote")]
        [Description("Tell RUINBot to quote the next message you give a reaction to.")]
        public async Task Quote(CommandContext context)
        {
            var interactivity = context.Client.GetInteractivity();

            await context.TriggerTypingAsync();

            var result = await interactivity.WaitForReactionAsync(x => x != null, context.User, TimeSpan.FromSeconds(60));

            if(result.TimedOut == false)
            {
                var message = await context.Guild.GetChannel(result.Result.Channel.Id).GetMessageAsync(result.Result.Message.Id);

                await context.RespondAsync("And I quote:");

                var embed = new DiscordEmbedBuilder
                {
                    Color = message.Author is DiscordMember m ? m.Color : new DiscordColor(0xFF00FF),
                    Description = $"{message.Timestamp.DateTime}\n{message.Content}",
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        Name = message.Author is DiscordMember mx ? mx.DisplayName : message.Author.Username,
                        IconUrl = message.Author.AvatarUrl
                    }
                };
                await context.RespondAsync(embed: embed);
            }
            else
            {
                await context.RespondAsync($"Umm {context.User}, didn't you want a quote?");
            }
        }
    }
}
