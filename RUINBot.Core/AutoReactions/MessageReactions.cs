using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot.Core.AutoReactions
{
    public static class MessageReactions
    {
        internal static async void ReactToMessages(MessageCreateEventArgs e)
        {
            var prefix = GlobalConfig.BotConfig.CommandPrefix;

            if (!e.Message.Content.StartsWith(prefix))
            {
                await MarciReactions(e);
                await JonReactions(e);
                await DevinReactions(e);
                await JosiahAndJessicaReactions(e);
                await HeatherReactions(e);
                await KeywordReactions(e);
            }
        }

        private static async Task MarciReactions(MessageCreateEventArgs e)
        {
            var namesToFind = new List<string>
            {
                "Arsenio",
                "Borderpo",
                "Imagimagician"
            };

            var namesToReplace = new List<string>
            {
                "Marci",
                "Marcino",
                "Marseno",
                "Arsenial",
                "Marcino Alvrz",
                "Mrcn lvrz",
                "Arsenic",
                "Arsenic James Alvarararez",
                "Imagimagimagimagiamagician",
                "\"Full disclosure I work at 2K\"-senio",
                "Spoil-senio",
                "The best :)"
            };

            var textToSearch = e.Message.Content.ToLower();
            var foundName = namesToFind.Any(x => textToSearch.Contains(x.ToLower()));

            if (foundName)
            {
                var editedMessage = e.Message.Content;

                var random = new Random();
                
                foreach(var oldName in namesToFind)
                {
                    var newName = namesToReplace[random.Next(0, namesToReplace.Count - 1)];
                    editedMessage = editedMessage.Replace(oldName, newName, true, CultureInfo.InvariantCulture); 
                }

                await e.Message.RespondAsync(editedMessage);
            }
        }

        private static Task JonReactions(MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Task DevinReactions(MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Task JosiahAndJessicaReactions(MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Task KeywordReactions(MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Task HeatherReactions(MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
