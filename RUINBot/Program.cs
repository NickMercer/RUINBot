using System;

namespace RUINBot
{
    class Program
    {
        public static ulong botId = 703793988087447632;

        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
