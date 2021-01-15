using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RUINBot.AutoReactions
{
    public class MessageReactions
    {
        internal static void ReactToMessages(MessageCreateEventArgs e)
        {
            if (!e.Message.Content.StartsWith("/"))
            {
                MarciReaction(e);
                JonReaction(e);
                DevinReaction(e);
                JosiahAndJessicaReaction(e);
                HeatherReaction(e);
                KeywordReactions(e);
            }
        }

        private static async Task MarciReaction(MessageCreateEventArgs e)
        {
            List<string> arsenioList = new List<string>
            {
                "Arsenio",
                "Borderpo",
            };

            var q = arsenioList.Any(w => e.Message.Content.ToLower().Contains(w, StringComparison.OrdinalIgnoreCase));

            if (q)
            {
                var responseString = e.Message.Content;

                List<string> marciList = new List<string>
                {
                    "Marci",
                    "Marcino",
                    "Marseno",
                    "Arsenial",
                    "Marcino Alvrz",
                    "Mrcn lvrz",
                    "Arsenic",
                    "Arsenic James Alvarararez"
                };

                var rnd = new Random();
                var marci = marciList[rnd.Next(0, marciList.Count - 1)];

                foreach (string arsenio in arsenioList)
                {
                    responseString = responseString.Replace(arsenio, marci, true, CultureInfo.InvariantCulture);
                }

                await e.Message.RespondAsync(responseString).ConfigureAwait(false);
            }
        }
        private static async Task JonReaction(MessageCreateEventArgs e)
        {
            //Jon's ID 297411723839930379
            if (e.Author.Id == (ulong)SquadMembers.Jon)
            {
                var rnd = new Random();

                if (rnd.Next(25) == 1)
                {
                    string responseString = "It's okay, Jon. Words are bad.";

                    await e.Message.RespondAsync(responseString).ConfigureAwait(false);
                }
            }
        }
        private static async Task DevinReaction(MessageCreateEventArgs e)
        {
            //Devin's ID 208824363137368066
            if (e.Author.Id == (ulong)SquadMembers.Devin)
            {
                var rnd = new Random();

                if (rnd.Next(25) == 1)
                {
                    string responseString = "Devin, that beard is majestic. I'm jealous.";

                    await e.Message.RespondAsync(responseString).ConfigureAwait(false);
                }
            }
        }
        private static async Task JosiahAndJessicaReaction(MessageCreateEventArgs e)
        {
            if (e.Author.Id == (ulong)SquadMembers.Josiah && (e.Message.Content.Contains("what") || e.Message.Content.Contains("wat")))
            {
                await e.Message.RespondAsync("Wat WAT what wat wat WAAAT?!?!!?").ConfigureAwait(false);
            }
            else if (e.Author.Id == (ulong)SquadMembers.Josiah || e.Author.Id == (ulong)SquadMembers.Jessica)
            {
                var rnd = new Random();

                if (rnd.Next(25) == 1)
                {
                    string responseString = "Uh, I was going to say something, but I forgot. Whoops. 🙃";
                    rnd = new Random();

                    switch (rnd.Next(4))
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
                            if (e.Author.Id == (ulong)SquadMembers.Josiah)
                                responseString = "🎤 Jooosiah Laicaaans, uh yeaaaah 🎤";
                            else
                            {
                                responseString = "🐅 Yo, Jessica, give me a fun fact about Tigers. 🐅";
                            }
                            break;
                    }

                    await e.Message.RespondAsync(responseString).ConfigureAwait(false);
                }
            }
        }
        private static async Task HeatherReaction(MessageCreateEventArgs e)
        {
            if (e.Author.Id == (ulong)SquadMembers.Heather)
            {
                var rnd = new Random();

                if (rnd.Next(25) == 1)
                {
                    string responseString = "Wow, Heather, you're looking younger than I remember! I guess I should stop calling you Hangrygranny.";

                    await e.Message.RespondAsync(responseString).ConfigureAwait(false);
                }
            }
        }
        private static async Task KeywordReactions(MessageCreateEventArgs e)
        {
            if (e.Author.Id != Program.botId)
            {
                List<string> findList = new List<string>
                {
                    "hello",
                    "hi",
                    "hey",
                    "feelings",
                    "josiah"
                };

                var q = findList.Any(w => e.Message.Content.ToLower().Contains(w, StringComparison.OrdinalIgnoreCase));

                if (q)
                {
                    Dictionary<string, int> responseList = new Dictionary<string, int>();
                    responseList.Add("Hello, Hoomans!", 1);
                    responseList.Add("Hi, how are ya?", 1);
                    responseList.Add($"Hey there, { e.Author }", 1);
                    responseList.Add("Oh no, Devin's feelings. 🙁", 2);
                    responseList.Add("Jo - SI -Ahhh", 3);

                    string responseString = "";
                    var rnd = new Random();
                    for (int i = 0; i <= findList.Count - 1; i++)
                    {
                        if (e.Message.Content.ToLower().Contains($"{findList[i]} ") || e.Message.Content.ToLower().Equals(findList[i]))
                        { 
                            int result = rnd.Next(0, responseList.Values.ElementAt(i));
                            if(result == 0)
                            {
                                responseString = responseList.Keys.ElementAt(i);
                                i = findList.Count + 1;
                            }
                        }
                    }
                    

                    if (responseString != "")
                    {
                        await e.Message.RespondAsync(responseString).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
