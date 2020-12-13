using RUINBot.Core;

namespace RUINBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunBotAsync().GetAwaiter().GetResult();
        }
    }
}
