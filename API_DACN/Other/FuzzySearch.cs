using API_DACN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API_DACN.Other
{
    public class FuzzySearch
    {
        private readonly food_location_dbContext db;

        public FuzzySearch(food_location_dbContext db)
        {
            this.db = db;
        }

        public List<Database.Restaurant> SearchFood(string word, List<Database.Food> wordList, double fuzzyness)
        {
            // Tests have prove that the !LINQ-variant is about 3 times
            // faster!
            List<Database.Restaurant> foundWords =
                (
                    from s in wordList
                    let levenshteinDistance = LevenshteinDistance(word, s.KeyWord)
                    let length = Math.Max(s.KeyWord.Length, word.Length)
                    let score = 1.0 - (double)levenshteinDistance / length
                    where score > fuzzyness
                    select db.Menus.Find(s.MenuId).Restaurant
                ).ToList();

            return foundWords;
        }

        public List<Database.Restaurant> SearchRes(string word, List<Database.Restaurant> wordList, double fuzzyness)
        {
            // Tests have prove that the !LINQ-variant is about 3 times
            // faster!
            List<Database.Restaurant> foundWords =
                (
                    from s in wordList
                    let levenshteinDistance = LevenshteinDistance(word, s.Name.ToUpper())
                    let length = Math.Max(s.Name.Length, word.Length)
                    let score = 1.0 - (double)levenshteinDistance / length
                    where score > fuzzyness
                    select s
                ).ToList();

            return foundWords;
        }

        public static List<string> Search(string word, List<string> wordList, double fuzzyness)
        {
            List<string> foundWords = new List<string>();

            foreach (string s in wordList)
            {
                // Calculate the Levenshtein-distance:
                int levenshteinDistance =
                    LevenshteinDistance(word, s);

                // Length of the longer string:
                int length = Math.Max(word.Length, s.Length);

                // Calculate the score:
                double score = 1.0 - (double)levenshteinDistance / length;

                // Match?
                if (score > fuzzyness)
                    foundWords.Add(s);
            }
            return foundWords;
        }

        public static int LevenshteinDistance(string src, string dest)
        {
            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {

                    if (str1[i - 1] == str2[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }
    }

}
