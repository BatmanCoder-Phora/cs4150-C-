using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace submission
{
    
    public class submission
    {
        public static Dictionary<string, HashSet<string>> matchedwords = new Dictionary<string, HashSet<string>>();
        static void Main(string[] args)
        {
            // n and k variables and create other needed any other variables
            int numberOfWords = 0;
            int lengthOfWords = 0;
            int result = 0;
            string line;
            // get the numbers of words in the list and their length. 
            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            Int32.TryParse(linespilt[0], out numberOfWords);
            Int32.TryParse(linespilt[1], out lengthOfWords);

            // grab the rest of the words and pair them. 
            for (int i = 0; i < numberOfWords; i++)
            {
                line = Console.ReadLine();
                anagramAl2(line); 
            }
            // see  how many groups of anagrams are in the dictionary 
            foreach (KeyValuePair<string, HashSet<string>> text in matchedwords.ToArray())
            {
                if (text.Value.Count > 0)
                    result++;
            }
            //print the number of groups that are anagrams 
            Console.WriteLine("\n" + result);
    //        Console.ReadLine();
        }
        /// <summary>
        /// A method that checks if a word is an anagram for an of the words in a list. 
        /// </summary>
        /// <param name="line">The word we are check is an anagram</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void anagramAl2(string line)
        {
            bool wasadded = false;
            foreach (KeyValuePair<string, HashSet<string>> text in matchedwords.ToHashSet())
            {
                char[] first = text.Key.ToCharArray();
                char[] second = line.ToCharArray();
                Array.Sort(first);
                Array.Sort(second); 
                if (first.SequenceEqual(second)) // compare the two strings
                {
                    matchedwords[text.Key].Add(line);
                    wasadded = true;
                }
            }
            if(!wasadded)
               matchedwords.Add(line,new HashSet<string>() { });
        }
    }
}
