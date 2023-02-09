using System;

namespace GamesClub.Code.Extensions
{
    public static class GameplayExtensions
    {
        private static readonly Random _random = new Random();  
        
        public static void Shuffle<T> (this T[] array)
        {
            int n = array.Length;
            while (n > 1) 
            {
                int k = _random.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }
}