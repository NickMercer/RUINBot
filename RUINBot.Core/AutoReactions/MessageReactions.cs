using DSharpPlus.EventArgs;
using RUINBot.Core.Models;
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

        private static async Task JonReactions(MessageCreateEventArgs e)
        {
            if (e.Author.Id == DiscordIDs.Sloth)
            {
                var random = new Random();

                if (random.Next(25) == 1)
                {
                    string responseString = "It's okay, Jon. Words are bad.";

                    await e.Message.RespondAsync(responseString);
                }
            }
        }

        private static async Task DevinReactions(MessageCreateEventArgs e)
        {
            if (e.Author.Id == DiscordIDs.DandyLion)
            {
                var random = new Random();

                if (random.Next(25) == 1)
                {
                    string responseString = "Devin, that beard is majestic. I'm jealous.";

                    await e.Message.RespondAsync(responseString);
                }
            }
        }

        private static async Task JosiahAndJessicaReactions(MessageCreateEventArgs e)
        {
            if (e.Author.Id == DiscordIDs.Arcaerus && 
                (e.Message.Content.ToLower().Contains(" what ") 
                || e.Message.Content.ToLower().Contains(" wat ")))
            {
                await e.Message.RespondAsync("Wat WAT what wat wat WAAAT?!?!!?");
                return;
            }

            if (e.Author.Id == DiscordIDs.Arcaerus || e.Author.Id == DiscordIDs.Scrub)
            {
                var random = new Random();

                if (random.Next(25) == 1)
                {
                    string responseString = "Uh, I was going to say something, but I forgot. Whoops. 🙃";

                    switch (random.Next(4))
                    {
                        case 0:
                            responseString = "👑 Freya asked me to tell you she wants the peasants removed from her kingdom, promptly.";
                            break;

                        case 1:
                            responseString = $"Hey { e.Author.Username }, 🐈 Percy is hungry. 🍲";
                            break;

                        case 2:
                            responseString = "😻 Baby is so swee--- 😾 OOHH OW MY CIRCUTS!!";
                            break;

                        case 3:
                            if (e.Author.Id == DiscordIDs.Arcaerus)
                                responseString = "🎤 Jooosiah Laicaaans, uh yeaaaah 🎤";
                            else
                            {
                                responseString = "🐅 Yo, Jessica, give me a fun fact about Tigers. 🐅";
                            }
                            break;
                    }

                    await e.Message.RespondAsync(responseString);
                }
            }
        }

        private static async Task HeatherReactions(MessageCreateEventArgs e)
        {
            if (e.Author.Id == DiscordIDs.Hangry)
            {
                var random = new Random();

                if (random.Next(25) == 1)
                {
                    string responseString = "Wow, Heather, you're looking younger than I remember! I guess I should stop calling you Hangrygranny.";

                    await e.Message.RespondAsync(responseString);
                }
            }
        }

        private static async Task KeywordReactions(MessageCreateEventArgs e)
        {
            if (!e.Author.IsBot)
            {
                List<string> wordsToFind = new List<string>
                {
                    "hello",
                    "hi",
                    "hey",
                    "feelings",
                    "josiah"
                };

                var foundWords = wordsToFind.Any(w => e.Message.Content.ToLower().Contains(w, StringComparison.OrdinalIgnoreCase));

                if (foundWords)
                {
                    Dictionary<string, int> wordReplacements = new Dictionary<string, int>();
                    wordReplacements.Add("Hello, Hoomans!", 1);
                    wordReplacements.Add("Hi, how are ya?", 1);
                    wordReplacements.Add($"Hey there, { e.Author.Username }", 1);
                    wordReplacements.Add("Oh no, Devin's feelings. 🙁", 2);
                    wordReplacements.Add("Jo - SI -Ahhh", 3);

                    string responseString = "";
                    var random = new Random();
                    for (int i = 0; i <= wordsToFind.Count - 1; i++)
                    {
                        if (e.Message.Content.ToLower().Contains($"{wordsToFind[i]} ") || e.Message.Content.ToLower().Equals(wordsToFind[i]))
                        {
                            int result = random.Next(0, wordReplacements.Values.ElementAt(i));
                            if (result == 0)
                            {
                                responseString = wordReplacements.Keys.ElementAt(i);
                                i = wordsToFind.Count + 1;
                            }
                        }
                    }

                    if (responseString != "")
                    {
                        await e.Message.RespondAsync(responseString);
                    }
                }
            }
        }

    }
}
