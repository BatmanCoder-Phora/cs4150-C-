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
        //Dictionary were all the words and their matches are put. 
        public static Dictionary<string, HashSet<string>> matchedwords = new Dictionary<string, HashSet<string>>();
        static void Main(string[] args)
        {
            int result = 0;
            string readline;

            // get the numbers of words in the list. 
            readline = Console.ReadLine();
            string[] linespilt = readline.Split(' ');
            Int32.TryParse(linespilt[0], out int numberOfWords);

            // grab the rest of the words and pair them in the dictionary. 
            for (int i = 0; i < numberOfWords; i++)
            {
                readline = Console.ReadLine();
                anagramAl2(readline); 
            }
            // see how many groups of anagrams are in the dictionary 
            foreach (KeyValuePair<string, HashSet<string>> text in matchedwords.ToArray())
            {
                if (text.Value.Count > 0)
                    result++;
            }
            // print the number of groups that are anagrams 
            Console.WriteLine("\n" + result);
        }
        /// <summary>
        /// A method that checks if a word is an anagram with any words already in the list. 
        /// </summary>
        /// <param name="word">The word being checked</param>
        public static void anagramAl2(string word)
        {
            bool wasadded = false;
            foreach (KeyValuePair<string, HashSet<string>> text in matchedwords.ToArray())
            {
                // spilt the key and input word into char array, sort them and then compare their sequence.  
                char[] first = text.Key.ToCharArray();
                char[] second = word.ToCharArray();
                Array.Sort(first);
                Array.Sort(second); 
                if (first.SequenceEqual(second)) // compare the char arrays
                {
                    matchedwords[text.Key].Add(word);
                    wasadded = true;
                }
            }
            if(!wasadded)
               matchedwords.Add(word,new HashSet<string>() { });
        }
    }
}
