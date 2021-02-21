using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using RUINBot.Core.DiscordExtensions;
using RUINBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.AutoReactions
{
    public class PresenceReactions
    {
        internal static async void ReactToPresenceChanges(PresenceUpdateEventArgs e)
        {
            HeatherReaction(e);
            await WingerdSistersReaction(e);
        }

        private static async Task WingerdSistersReaction(PresenceUpdateEventArgs e)
        {
            var wingerdSisters = new List<ulong>() { DiscordIDs.Scrub, DiscordIDs.Rachael, DiscordIDs.Rebekah };

            if(wingerdSisters.Contains(e.User.Id))
            {
                var guildMembers = await e.PresenceAfter.Guild.GetAllMembersAsync();
                var onlineMembers = guildMembers.Where(x => x.Presence.IsOnline() || x.Presence.Status == UserStatus.Idle).Select(x => x.Id);

                var allSistersOnline = wingerdSisters.All(onlineMembers.Contains);

                if (allSistersOnline)
                {
                    await e.PresenceAfter.Guild.GetDefaultChannel().SendMessageAsync("Woah! It's all the Wingerd sisters in one place!");
                }
            }
        }

        private static void HeatherReaction(PresenceUpdateEventArgs e)
        {
            var random = new Random();
            if (random.Next(5) == 1)
            {
                if(e.User.Id == DiscordIDs.Hangry)
                {
                    if(!e.PresenceBefore.IsOnline() && e.PresenceAfter.IsOnline())
                    {
                        e.PresenceAfter.Guild.GetDefaultChannel().SendMessageAsync("Wait, is that a Hangry... Walrus??");
                    }
                }
            }
        }
    }
}
