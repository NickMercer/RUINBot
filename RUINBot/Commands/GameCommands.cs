using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using RUINBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RUINBot;
using System.Threading.Tasks;

namespace RUINBot.Commands
{
    public class GameCommands
    {
        [Command("pickgame")]
        [Description("Picks a game for the squad to play from the list.")]
        public async Task PickGame(CommandContext context, int playTime)
        {
            List<Game> gameList = new List<Game>
            {                
                new Game("Call of Duty: Warzone", 4, 30, 5, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick }),
                new Game("Call of Duty: Multiplayer", 6, 10, 3, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick }), 
                new Game("Sea of Thieves", 4, 120, 5, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick, (ulong)SquadMembers.Jessica, (ulong)SquadMembers.Heather, (ulong)SquadMembers.Rachael, (ulong)SquadMembers.Rebekah }), 
                new Game("Rainbow Six: Siege", 5, 15, 1, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick, (ulong)SquadMembers.Heather }), 
                new Game("Splitgate", 4, 15, 1, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick }), 
                new Game("Paladins", 5, 15, 1, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick }), 
                new Game("Overwatch", 6, 15, 3, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Marci }), 
                new Game("Hunt: Showdown", 3, 20, 5, new List<ulong> { (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci }), 
                new Game("Valorant", 5, 45, 5, new List<ulong> { (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Nick }), 
                new Game("Heroes of the Storm", 5, 20, 1, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick }),
                new Game("Call of Duty: Custom Game", 12, 10, 1, new List<ulong> { (ulong)SquadMembers.Devin, (ulong)SquadMembers.Jon, (ulong)SquadMembers.Josiah, (ulong)SquadMembers.Keith, (ulong)SquadMembers.Marci, (ulong)SquadMembers.Nick })
            };

            var guildMembers = await context.Channel.Guild.GetAllMembersAsync();
            IEnumerable<DiscordMember> onlineMembers = guildMembers.Where(x => (x.Presence != null && (x.Presence.Status == UserStatus.Online || x.Presence.Status == UserStatus.Idle) && x.IsBot == false));

            //Filter games by amount of players
            var filteredGames = gameList.Where(x => x.MaxPlayers >= onlineMembers.Count()).ToList();

            //Filter games by play session
            filteredGames = filteredGames.Where(x => x.AverageSessionInMinutes <= playTime).ToList();

            filteredGames.Shuffle();

            //Filter games by who has what
            //If someone online doesn't have the game, remove it.
            foreach (DiscordMember member in onlineMembers)
            {
                filteredGames.RemoveAll(x => !x.Players.Contains(member.Id));
            }

            //Make recommendations based on that.
            filteredGames = filteredGames.OrderByDescending(x => x.Weight).ToList();

            int gameIndex = RandomExtension.WeightedRandom(filteredGames);

            int topIndex = RandomExtension.Range(0, Math.Min(filteredGames.Count - 1, 2));

            string firstGame = filteredGames[topIndex].Name;
            string secondGame = "";
            if(filteredGames.Count > 3)
            {
                secondGame = filteredGames[topIndex + 1].Name;
            }
            string wildcardGame = filteredGames[gameIndex].Name;
            
            string message = "Something went terribly, terribly wrong. Self-shame sequence initiated. Bad robot :(";
            //Single Recommendation
            if (secondGame == "" && wildcardGame == firstGame)
            {
                message = $"My recommendation is:    {firstGame} \n \n GLHF!";
            }

            //Double or Triple Recommendation
            if (wildcardGame != firstGame)
            {
                if (secondGame == "")
                {
                    message = $"Alright, I've got two recommendations for you: \n \n First Choice:    { firstGame } \n Wildcard:    { wildcardGame } \n \n Rek those n00bs!";
                }
                else if (wildcardGame == secondGame)
                {
                    message = $"Hmmm, I've got two options for ya: \n \n First Choice:    { firstGame } \n Second Choice:    { secondGame } \n \n RUIN 'em!";
                }
                else
                {
                    message = $"So many good games, so little time! I've got THREE options for you! \n \n First Choice:    { firstGame } \n Second Choice:    { secondGame } \n Wildcard:    { wildcardGame }\n \n OH BABY ITS A TRIPLE";
                }
            }

            await context.Channel.SendMessageAsync(message);
        }

        [Command("poll")]
        [Description("Create a Poll for the squad to respond to")]
        public async Task Poll(CommandContext context, string pollQuestion, TimeSpan duration)
        {
            var interactivity = context.Client.GetInteractivityModule();

            var options = pollQuestion.GetEmojis();

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = $"Poll: { pollQuestion }",
                Description = string.Join(" ", options)
            };

            var pollMessage = await context.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in options)
            {
                var emoji = DiscordEmoji.FromUnicode(option);
                await pollMessage.CreateReactionAsync(emoji).ConfigureAwait(false);
            }

            System.Threading.Thread.Sleep(250);


            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);

            var results = result.Reactions;

            var resultStrings = result.Reactions.Select(x => $"{x.Key}: {x.Value}");

            if(results.Count == 0)
            {
                await context.Channel.SendMessageAsync($"Here are the Poll Results: \nNo one voted. Now I'm sad.");
            }
            else
            {
                int winnerCount = results.Values.Max();
                var winner = results.Where(x => x.Value == winnerCount).FirstOrDefault().Key;

                if (winnerCount != results.Values.Min() || results.Count == 1)
                {
                    await context.Channel.SendMessageAsync($"Here are the Poll Results: \n {string.Join("\n", resultStrings)} \n The winner was {winner} with {winnerCount} votes.");
                }
                else
                {
                    await context.Channel.SendMessageAsync($"Here are the Poll Results: \n {string.Join("\n", resultStrings)} \nIt was a tie. Come on people.");
                }
            }
        }
    }
}
