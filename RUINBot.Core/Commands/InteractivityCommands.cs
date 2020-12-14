using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using RUINBot.Core.DiscordExtensions;
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

        [Command("pressf")]
        [Description("Can I get an F in the chat?")]
        [Aliases("payrespects", "respecc")]
        public async Task PressF(CommandContext context)
        {
            var initialMessage = await context.Channel.SendMessageAsync("Can I get an F in the chat?");

            var interactivity = context.Client.GetInteractivity();

            var members = await context.Guild.GetAllMembersAsync();
            var onlineMembers = members.Where(x => x.Presence.IsOnline() && x.IsBot == false).ToList();

            //TODO: Figure out how to gather messages and then respond.

            await context.TriggerTypingAsync();

            var nonRespondingMembers = new List<DiscordMember>(); // This should be replaced by everyone who did not respond with F or f.
            
            if(nonRespondingMembers.Count == 0)
            {
                await context.Channel.SendFileAsync("pack://application:,,,/RUINBot.Core;component/Images/PressF.jpg", "I protecc, I rekk, but mostly, I press F to pay respecc.");
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (var member in nonRespondingMembers)
                {
                    sb.Append($"{member.Username}, ");
                }

                sb.Remove(sb.Length - 2, 2);

                string shameList = sb.ToString();

                await context.Channel.SendMessageAsync($"Wow, {shameList}. Do you even respecc?");
            }
        }
    }
}
