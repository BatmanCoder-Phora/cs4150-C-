/*
 * Author: Sephora Bateman 
 * Class: CS 4150
 * Problem Set #3
 */

/*
 * WHAT TO WORK ON FOR CODE:
 *  - work on the fidnminisland function. 
 *       passing some test, failing more. orphan clause works. some test beacuse of time, other beacuse of wrong output. 
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
        public static int minisl;
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        public static long islandsWithShops;
        public static Dictionary<int, long> islandsAndFerries = new Dictionary<int, long>();
        public static HashSet<long> results = new HashSet<long>();

        /// <summary>
        /// main function to slove for min islands. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int index = 1;
            int minislands = 0;
            // is there a way to get rid of all these loops.

            string line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);

            // grab the number of destinations. 
            for (int i = 1; i <= numOfDestinations; i++)
                islandNumbers = islandNumbers | (1 << i);

            // input all the ferry route data into a data structure. 
            for (int i = 0; i < numOfFerryRoutes; i++)
                inputIslandAndFerryData(line);


            // locate the min number of shops 
            findMinNumOfShops(islandsWithShops, passedDes, index, minislands);

            // Print the min number of islands and the islands it takes. 
            Console.WriteLine(minisl);
            long answer = results.Min();
            string stringAnser = "";
            for (int i = 1; i <= numOfDestinations; i++)
            {
                if ((answer & (1L << i)) != 0)
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

          //  islandsAndFerries.TryGetValue(island, out long value);
            long value = getislandRoutes(island);
            //  islandsAndFerries.TryGetValue(ferry, out long valueferry);
            long valueferry = getislandRoutes(ferry);
            islandsAndFerries[island] = (value | (1L << ferry));
            islandsAndFerries[ferry] = (valueferry | (1L << island));
        }

        private static long getislandRoutes(int island)
        {
            foreach (KeyValuePair<int, long> kvp in islandsAndFerries)
                if (kvp.Key == island)
                    return kvp.Value;
            return 0;    
        }

        /// <summary>
        /// Finds the smallest number of shops that need to be built. 
        /// </summary>
        /// <param name="islands"> the list of islands.</param>
        /// <param name="passedDes">The islands we have passed</param>
        /// <param name="index">the island we are checking.</param>
        private static bool findMinNumOfShops(long islandsWithShops, long passedDes, int index, int minislands)
        {
          
            // base case for min soultion
            if (passedDes == islandNumbers)
            {
                results.Add(islandsWithShops);
                minisl = minislands;
                return true;
            }
            // base case for no soultion. 
            if (index > numOfDestinations)
                return false;

          //  islandsAndFerries.TryGetValue(index, out long value);
            long value = getislandRoutes(index);
            // temps to roll back. 
            long tempislandswithshops = islandsWithShops;
            long temppassedDes = passedDes;
            int tempminisland = minislands;
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
            
            // find out how to store the minnumber of islands. still some twerks to do. 
            // still getting too many options for a larger set. 
            return findMinNumOfShops(tempislandswithshops, temppassedDes, index + 1, tempminisland) ||
                findMinNumOfShops(islandsWithShops, passedDes, index + 1, minislands);
               


        }
    }
}
