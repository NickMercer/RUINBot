using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Core.DiscordExtensions
{
    public static class PresenceExtensions
    {
        public static bool IsOnline(this DiscordPresence presence)
        {
            var isOnline = false;
            if(presence.ClientStatus != null)
            {
                var desktop = presence.ClientStatus.Desktop;
                var mobile = presence.ClientStatus.Mobile;
                var web = presence.ClientStatus.Web;

                if (desktop.HasValue)
                {
                    isOnline = (desktop.Value == UserStatus.Online);

                    if (isOnline)
                    { 
                        return isOnline;
                    }
                }

                if (mobile.HasValue)
                {
                    isOnline = (mobile.Value == UserStatus.Online);
                    
                    if (isOnline)
                    {
                        return isOnline;
                    }
                }

                if (web.HasValue)
                {
                    isOnline = (web.Value == UserStatus.Online);

                    if (isOnline)
                    {
                        return isOnline;
                    }
                }
            }

            if(presence.Status == UserStatus.Online)
            {
                isOnline = true;

                return isOnline;
            }

            return isOnline;
        }
    }
}
