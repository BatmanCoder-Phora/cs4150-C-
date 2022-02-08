/*
* Author: Sephora Bateman 
* Class: CS 4150
* Problem Set #3
*/

/*
 * WHAT TO WORK ON FOR CODE:
 *  - work on the timing issue. 
 *     
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_3
{
    public class Ps3submission
    {
        public static long islandNumbers;
        public static long passedDes = 0;
        public static int minisl = int.MaxValue;
        public static long result;
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        public static long islandsWithShops;
        public static Dictionary<int, long> islandsAndFerries = new Dictionary<int, long>();

        /// <summary>
        /// main function to slove for min islands. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int minislands = 0;
            string stringAnser = "";
            // is there a way to get rid of all these loops.

            string line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);

            // grab the number of destinations. // could get rid of this for loop but how. 
              for (int i = 1; i <= numOfDestinations; i++)
                 islandNumbers = (islandNumbers | (1L << i));


            // input all the ferry route data into a data structure. 
            for (int i = 0; i < numOfFerryRoutes; i++)
            {
                inputIslandAndFerryData(line);
            }

            int index = 1;
            // locate the min number of shops 
            findMinNumOfShops(islandsWithShops, passedDes, index, minislands);


            Console.WriteLine(minisl);
            for (int i = 1; i <= numOfDestinations; i++)
            {
                if ((result & (1L << i)) != 0)
                    stringAnser = stringAnser + (i.ToString() + " ");
            }
            Console.WriteLine(stringAnser.Trim());

        }
        /// <summary>
        ///  Stores the islands and the ferry routes in a dictionary also figure out the destination. 
        /// </summary>
        /// <param name="i"></param>
        private static void inputIslandAndFerryData(string line)
        {
            line = Console.ReadLine().Trim();
            string[] spilt = line.Split(' ');
            int island = Int32.Parse(spilt[0]);
            int ferry = Int32.Parse(spilt[1]);

            if (!islandsAndFerries.ContainsKey(island))
                islandsAndFerries.Add(island, (1L << island));
            if (!islandsAndFerries.ContainsKey(ferry))
                islandsAndFerries.Add(ferry, (1L << ferry));

            long value, valueferry;
            islandsAndFerries.TryGetValue(island, out value);
            islandsAndFerries.TryGetValue(ferry, out valueferry);
            islandsAndFerries[island] = (value | (1L << ferry));
            islandsAndFerries[ferry] = (valueferry | (1L << island));
        }

        /// <summary>
        /// Finds the smallest number of shops that need to be built. 
        /// </summary>
        /// <param name="islands"> the list of islands.</param>
        /// <param name="passedDes">The islands we have passed</param>
        /// <param name="index">the island we are checking.</param>
        private static void findMinNumOfShops(long islandsWithShops, long passedDes, int index, int minislands)
        {
            long tempislandswithshops = islandsWithShops;
            long temppassedDes = passedDes;
            int tempminisland = minislands;

            // base case for min soultion
            if (passedDes == islandNumbers)       
            {
                if (minislands < minisl)
                {
                    minisl = minislands;
                    result = islandsWithShops;
                }
                return;
            }
            // base case for no soultion. 
            if (index > numOfDestinations)
                return;

            long value; 
            islandsAndFerries.TryGetValue(index, out value);
            if (value == 0) // orphan cluase. 
            {
                minislands++;
                passedDes = passedDes | (1L << index);
                islandsWithShops = islandsWithShops | (1L << index);
                findMinNumOfShops(islandsWithShops, passedDes, index + 1, minislands);
            }
            // purphas another pruning method here but what??
            else
            {
                for (int i = 1; i <= numOfDestinations; i++)
                {
                    if ((value & (1L << i)) != 0 && (passedDes & (1L << i)) == 0)
                    {
                        passedDes = passedDes | (1L << i);
                        if ((islandsWithShops & (1L << index)) == 0)
                        {
                            minislands++;
                            islandsWithShops = islandsWithShops | (1L << index);
                        }
                    }
                }
                findMinNumOfShops(tempislandswithshops, temppassedDes, index + 1, tempminisland);
                findMinNumOfShops(islandsWithShops, passedDes, index + 1, minislands);
            }


        }
    }
}