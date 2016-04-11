using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Extensions
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