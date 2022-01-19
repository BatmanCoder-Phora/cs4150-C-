/*
 * Author: Sephora Bateman 
 * Class: CS 4150
 * Problem Set #1
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace submission
{

    public class submission
    {
        public static Dictionary<string, HashSet<string>> matchedwords = new Dictionary<string, HashSet<string>>();
        public static HashSet<string> mw = new HashSet<string>();
        static void Main(string[] args)
        {
            // n and k variables and create other needed any other variables
            int numberOfWords;
            int result = 0;
            string line;
            // get the numbers of words in the list and their length. 
            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            Int32.TryParse(linespilt[0], out numberOfWords);

            // grab the rest of the words and pair them. 
            for (int i = 0; i < numberOfWords; i++)
            {
                line = Console.ReadLine();
                anagramAl3(line);
            }
            // see  how many groups of anagrams are in the dictionary 
             result = matchedwords.Count;
            //print the number of groups that are anagrams 
            Console.WriteLine(result);
        }
        /// <summary>
        /// A method that checks if a word is an anagram for an of the words in a list. 
        /// </summary>
        /// <param name="line">The word we are check is an anagram</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void anagramAl2(string line)
        {
            bool wasadded = false;
            char[] second = line.ToCharArray();
            Array.Sort(second);
            foreach (KeyValuePair<string, HashSet<string>> text in matchedwords.ToArray())
            {
                char[] first = text.Key.ToCharArray();
                Array.Sort(first);
                if (first.SequenceEqual(second)) // compare the two strings
                {
                    matchedwords[text.Key].Add(line);
                    wasadded = true;
                }
            }
            if (!wasadded)
                matchedwords.Add(line, new HashSet<string>() { });
        }

        /// A method that checks if a word is an anagram for an of the words in a list. 
        /// </summary>
        /// <param name="line">The word we are check is an anagram</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void anagramAl3(string line)
        {
            bool wasadded = false;
            char[] second = line.ToCharArray();
            Array.Sort(second);
            string secondstring = new string(second);

            foreach (string test in mw)
            {
                if (test.Equals(secondstring))
                {
                    wasadded = true;
                    if(matchedwords.ContainsKey(test) == false)
                        matchedwords.Add(secondstring, new HashSet<string>() { });
                    matchedwords[test].Add(secondstring);
                }
            }
            if (!wasadded)
            {
                string input = new string(second);
                mw.Add(input.Trim());
            }
                
        }
    }
}
