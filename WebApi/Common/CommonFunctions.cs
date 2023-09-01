using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace TGC_Game.Web
{
    internal static class CommonFunctions
    {
        private static Random rng = new Random();

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

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        enum Game
        {
            draganddropgame = 1,
            anagramgame = 2,
            angrybirdgame = 3,
            matchthetilegame = 4,
            truckdrivinggame = 5,
            questionquizgame = 6
        }

        enum LoginType
        {
            //Registration = 1,
            SocialGmail = 2,  //Social login with Gmail
            SocialFacebook = 4, //Social login with FaceBook
            Phone = 3,  //Phone Login
            Gmail = 5, //Email Login
            SSOLogin = 6,
            EmailDomainLogin = 7
        }

    }
}