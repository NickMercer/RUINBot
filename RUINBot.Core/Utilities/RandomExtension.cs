using System;
using System.Collections.Generic;
using System.Text;

namespace RUINBot.Core.Utilities
{
    public static class RandomExtension
    {
        private static readonly Random rng = new Random();

        public static int Range(int min, int max)
        {
            return rng.Next(min, max);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T Choose<T>(List<T> list)
        {
            var obj = list[Range(0, list.Count - 1)];
            return obj;
        }
    }
}
