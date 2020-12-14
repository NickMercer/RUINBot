using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.Commands
{
    public class AdminCommands : BaseCommandModule
    {
        [Command("sudo")]
        [Description("Executes a command as another user.")]
        [Hidden]
        [RequireOwner]
        public async Task Sudo(CommandContext context, [Description("Member to execute as.")] DiscordMember member, [RemainingText, Description("Command text to execute.")] string commandString)
        {
            await context.TriggerTypingAsync();

            var commands = context.CommandsNext;
            var command = commands.FindCommand(commandString, out var customArguments);

            var fakeContext = commands.CreateFakeContext(member, context.Channel, commandString, context.Prefix, command, customArguments);

            await commands.ExecuteCommandAsync(fakeContext);
        }


        [Command("nick")]
        [Description("Gives someone a new nickname.")]
        [RequirePermissions(DSharpPlus.Permissions.ManageNicknames)]
        public async Task ChangeNickname(CommandContext context, [Description("Member to change the nickname for.")] DiscordMember member, [RemainingText, Description("The nickname to give to that user")] string newNickname)
        {
            await context.TriggerTypingAsync();

            try
            {
                await member.ModifyAsync(x =>
                {
                    x.Nickname = newNickname;
                    x.AuditLogReason = $"Changed by {context.User.Username} ({context.User.Id}).";
                });

                var emoji = DiscordEmoji.FromName(context.Client, ":+1:");

                await context.RespondAsync(emoji);
            }
            catch(Exception)
            {
                var emoji = DiscordEmoji.FromName(context.Client, ":-1:");

                await context.RespondAsync($"I made a boo boo :( {emoji}");
            }
        }

        [Command("wipechat")]
        [Description("Wipes the chat history")]
        [Hidden]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task WipeChat(CommandContext context, int messageLimit = 1)
        {
            var messageList = await context.Channel.GetMessagesAsync(messageLimit);

            await context.Channel.DeleteMessagesAsync(messageList);

            await context.Channel.SendMessageAsync($"Channel Wiped: { messageList.Count} messages cleaned.");
        }
    }
}
