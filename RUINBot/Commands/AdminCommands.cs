using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Commands
{
    public class AdminCommands
    {
        [Command("wipechat")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        [Hidden]
        [Description("Wipes the chat history")]
        public async Task WipeChat(CommandContext context, int messageLimit = 1)
        {
            if (context.Member.IsOwner)
            {
                int messageCount = 0;

                var messageList = await context.Channel.GetMessagesAsync(messageLimit, context.Message.Id);

                messageCount = messageList.Count;

                await context.Channel.DeleteMessagesAsync(messageList);

                await context.Channel.SendMessageAsync($"Channel Wiped: { messageCount} messages cleaned.");
            }
            else
            {
                await context.Channel.SendMessageAsync("I'm not your mother, human.");
            }
        }
    }
}
