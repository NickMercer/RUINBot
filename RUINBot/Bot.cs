using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using RUINBot.AutoReactions;
using RUINBot.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RUINBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextModule Commands { get; private set; }    
        public static InteractivityModule Interactivity { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using(var fs = File.OpenRead("config.json"))
            using(var sr = new StreamReader(fs, new UTF8Encoding(false)))
            {
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            //DISCORD
            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Info,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;

            //INTERACTIVITY
            Interactivity = Client.UseInteractivity(new InteractivityConfiguration());

            //COMMANDS
            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {
                StringPrefix = configJson.Prefix,
                EnableMentionPrefix = true,
                EnableDms = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<GameCommands>();
            Commands.RegisterCommands<ImageCommands>();
            Commands.RegisterCommands<AdminCommands>();

            Client.MessageCreated += OnMessageCreated;

            Client.PresenceUpdated += OnPresenceUpdated;

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnPresenceUpdated(PresenceUpdateEventArgs e)
        {
            PresenceReactions.ReactToPresences(e);
            return Task.CompletedTask;
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private Task OnMessageCreated(MessageCreateEventArgs e)
        {
            MessageReactions.ReactToMessages(e);
            return Task.CompletedTask;
        }
    }
}
