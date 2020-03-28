using System;
using System.Collections.Generic;

namespace AuraUWP.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomItem<T>(this List<T> list)
        {
            Random rand = new Random();
            return list[rand.Next(0, list.Count)];
        }
    }
}