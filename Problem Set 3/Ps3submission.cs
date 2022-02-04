/*
 * Author: Sephora Bateman 
 * Class: CS 4150
 * Problem Set #3
 */

/*
 * WHAT TO WORK ON FOR CODE:
 *  - work on the fidnminisland function. 
 *     - ophan clause is working 
 *       - fix the else caluse and the matching island clause. 
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
        public static int minislands;
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        public static long islandsWithShops;
        public static Dictionary<int,long> islandsAndFerries = new Dictionary<int, long>();

        /// <summary>
        /// main function to slove for min islands. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int index = 1;
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

            findMinNumOfShops(islandsWithShops, passedDes,index,minislands);

            // Print the min number of islands and the islands it takes. 
            Console.WriteLine(minislands);
            for (int i = 1; i <= numOfDestinations; i++)
            {
                long tester = (islandsWithShops & (1 << i));
                if ((islandsWithShops & (1 << i)) != 0)
                    Console.WriteLine(i + ' ');
            }
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
                islandsAndFerries.Add(island, (1 << island));     
            if(!islandsAndFerries.ContainsKey(ferry))
                islandsAndFerries.Add(ferry, (1 << ferry));

            islandsAndFerries.TryGetValue(island, out long value);
            islandsAndFerries.TryGetValue(ferry, out long valueferry);
            islandsAndFerries[island] = (value | (1 << ferry));
            islandsAndFerries[ferry] = (valueferry | (1 << island));
        }

        /// <summary>
        /// Finds the smallest number of shops that need to be built. 
        /// </summary>
        /// <param name="islands"> the list of islands.</param>
        /// <param name="passedDes">The islands we have passed</param>
        /// <param name="index">the island we are checking.</param>
        private static void findMinNumOfShops(long islandsWithShops, long passedDes, int index, int minislands)
        {
            // base case for min soultion
            if (passedDes == islandNumbers)
            {
                return;
            }
            // base case for no soultion. 
            if (index > numOfDestinations)
                return;

            islandsAndFerries.TryGetValue(index, out long value);
            long tempislandswithshops = islandsWithShops;
            for (int i = 1; i <= numOfDestinations; i++)
                if ((value & (1 << i)) != 0 && (passedDes & (1 << i)) != 1)
                {
                    passedDes = passedDes | (1 << i);
                }
            islandsWithShops = islandsWithShops | (1 << index);

            findMinNumOfShops(islandsWithShops, passedDes, index + 1, minislands++);
            findMinNumOfShops(tempislandswithshops, 0, index + 1, minislands);
        }
 
    }
}
