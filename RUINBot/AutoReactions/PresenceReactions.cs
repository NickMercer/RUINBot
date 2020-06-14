using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.AutoReactions
{
    public class PresenceReactions
    {
        internal static void ReactToPresences(PresenceUpdateEventArgs e)
        {
            HeatherReaction(e);
            WingerdSistersReaction(e);
        }

        private static async Task WingerdSistersReaction(PresenceUpdateEventArgs e)
        {
            if(e.Member.Id == (ulong)SquadMembers.Jessica || e.Member.Id == (ulong)SquadMembers.Rachael || e.Member.Id == (ulong)SquadMembers.Rebekah)
            {
                if (e.Member.Presence.Status == UserStatus.Online && (e.PresenceBefore.Status == UserStatus.Offline || e.PresenceBefore.Status == UserStatus.Invisible))
                {
                    var guildMembers = await e.Member.Guild.GetAllMembersAsync();
                    List<DiscordMember> onlineMembers = guildMembers.Where(x => (x.Presence.Status == UserStatus.Online || x.Presence.Status == UserStatus.Idle) && x.IsBot == false).ToList();
                    List<ulong> sisters = new List<ulong>() { (ulong)SquadMembers.Jessica, (ulong)SquadMembers.Rachael, (ulong)SquadMembers.Rebekah };
                    List<ulong> userIds = onlineMembers.Select(x => x.Id).ToList();

                    var sistersOnline = sisters.All(userIds.Contains);

                    if(sistersOnline)
                    {
                        await e.Guild.GetDefaultChannel().SendMessageAsync("Woah! It's all the Wingerd sisters in one place!");
                    }
                }
            }
            
        }

        private static void HeatherReaction(PresenceUpdateEventArgs e)
        {
            var rnd = new Random();
            if(rnd.Next(5) == 1)
            {
                if (e.Member.Id == (ulong)SquadMembers.Heather)
                {
                    if ((e.PresenceBefore.Status == UserStatus.Offline || e.PresenceBefore.Status == UserStatus.Invisible) && e.Member.Presence.Status == UserStatus.Online)
                    {
                        e.Guild.GetDefaultChannel().SendMessageAsync("Wait, is that a Hangry...Walrus??");
                    }
                }
            }
        }
    }
}
