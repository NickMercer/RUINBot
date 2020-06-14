using RUINBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RUINBot
{
    public static class RandomExtension
    {
        private static readonly Random rng = new Random();

        public static int Range(int min, int max)
        {
            return rng.Next(min, max);
        }

        public static int WeightedRandom(List<Game> weightedItems)
        {
            List<int> intervals = new List<int>();
            int weightTotal = 0;
            for(var i = 0; i < weightedItems.Count; i++)
            {
                intervals.Add(weightedItems[i].Weight + weightTotal);
                weightTotal += weightedItems[i].Weight;
            }

            var number = rng.Next(0, weightTotal);

            var index = intervals.Where(x => x > number).First() - 1;

            return Math.Clamp(index, 0, weightedItems.Count - 1);
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
            var obj = list[Range(0,list.Count - 1)];
            return obj;
        }
    }
}
