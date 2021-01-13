using DSharpPlus;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using RUINBot.Core.Commands;
using RUINBot.Core;
using DSharpPlus.Interactivity;
using DSharpPlus.Net.WebSocket;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;

namespace RUINBot.Core
{
    public class Bot
    {
        public readonly EventId BotEventId = new EventId(42, "RUINBot");

        public DiscordClient Client { get; set; }
        public CommandsNextExtension Commands { get; set; }
        public InteractivityExtension Interactivity { get; set; }

        public async Task RunBotAsync()
        {
            BotConfig botConfig = await InitializeClient();
            GlobalConfig.ImgurId = botConfig.ImgurId;

            InitializeInteractivity();
            InitializeCommands(botConfig);

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        #region Initialize
        private async Task<BotConfig> InitializeClient()
        {
            var botConfig = await CreateBotConfig();
            var discordConfig = CreateDiscordConfiguration(botConfig);

            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;
            Client.GuildAvailable += Client_GuildAvailable;
            Client.ClientErrored += Client_ClientError;
            return botConfig;
        }

        private async Task<BotConfig> CreateBotConfig()
        {
            var configJson = await ReadConfigFile();

            return JsonConvert.DeserializeObject<BotConfig>(configJson);
        }

        private static async Task<string> ReadConfigFile()
        {
            var json = "";
            using (var fileStream = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "config.json")))
            using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
            {
                json = await streamReader.ReadToEndAsync();
            }

            return json;
        }

        private DiscordConfiguration CreateDiscordConfiguration(BotConfig botConfig)
        {
            var discordConfig = new DiscordConfiguration
            {
                Token = botConfig.Token,
                TokenType = TokenType.Bot,

                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };

            return discordConfig;
        }

        #endregion

        #region Event Hooks

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, "Client is ready to process events.");

            return Task.CompletedTask;
        }

        private Task Client_GuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");

            return Task.CompletedTask;
        }

        private Task Client_ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            sender.Logger.LogError(BotEventId, e.Exception, "Exception occured");

            return Task.CompletedTask;
        }

        #endregion

        #region Commmands

        private void InitializeCommands(BotConfig botConfig)
        {
            var commandsConfig = CreateCommandsConfig(botConfig);
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.CommandExecuted += Commands_CommandExecuted;
            Commands.CommandErrored += Commands_CommandErrored;

            Commands.RegisterCommands<BasicCommands>();
            Commands.RegisterCommands<AdminCommands>();
            Commands.RegisterCommands<InteractivityCommands>();
            Commands.RegisterCommands<APICommands>();
        }

        private CommandsNextConfiguration CreateCommandsConfig(BotConfig botConfig)
        {
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { botConfig.CommandPrefix },

                EnableDms = true,

                EnableMentionPrefix = true
            };

            return commandsConfig;
        }

        private Task Commands_CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            e.Context.Client.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'");

            return Task.CompletedTask;
        }

        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            if(e.Exception is ChecksFailedException exception)
            {
                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} Sorry {e.Context.User.Username}, You do not have the permissions required to execute this command.",
                    Color = new DiscordColor(0xFF0000)
                };

                await e.Context.RespondAsync("", embed: embed);
            }
        }

        #endregion

        #region Interactivity

        private void InitializeInteractivity()
        {
            var interactivityConfig = new InteractivityConfiguration
            {
                PaginationBehaviour = PaginationBehaviour.Ignore,
                Timeout = TimeSpan.FromMinutes(2)
            };

            Client.UseInteractivity(interactivityConfig);
        }

        #endregion
    }
}
