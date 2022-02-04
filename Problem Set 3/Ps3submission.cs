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
        public static long nofDestinations;
        public static long passedDes = 0;
        public static int minislands;
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        public static long islandsWithShops;
        public static Dictionary<int,int> islandsAndFerries = new Dictionary<int, int>();

        /// <summary>
        /// main function to slove for min islands. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string line;
            int index = 1;
            // is there a way to get rid of all these loops.

            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);


            for (int i = 1; i <= numOfDestinations; i++)
                if (i <= numOfDestinations)
                    nofDestinations = nofDestinations | (1 << i);

            int number = 1;
            while (true)// store the islands and their data into some data sturctures. 
            {
                line = Console.ReadLine();
                if (number >= numOfFerryRoutes)
                    break;
                else
                    inputIslandAndFerryData(line);
                number++;
            }


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
            string[] spilt = line.Split(' ');
            int island = Int32.Parse(spilt[0]);
            int ferry = Int32.Parse(spilt[1]);

            if (!islandsAndFerries.ContainsKey(island))
                islandsAndFerries.Add(island, (1 << island));     
            if(!islandsAndFerries.ContainsKey(ferry))
                islandsAndFerries.Add(ferry, (1 << ferry));

            islandsAndFerries.TryGetValue(island, out int value);
            islandsAndFerries.TryGetValue(ferry, out int valueferry);
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
            if (passedDes == nofDestinations)
                return;
            // base case for no soultion. 
            if (index > numOfDestinations)
                return;
            // Look for orphan 
            islandsAndFerries.TryGetValue(index, out int value);
            if (value == 0)
            {
                minislands++;
                passedDes = passedDes | (1 << index);
                islandsWithShops = islandsWithShops | (1 << index);
                findMinNumOfShops(islandsWithShops, passedDes, index + 1,minislands);
            }
            /*  else if()// looks for  macthing island numbers. 
              {
                  findMinNumOfShops(islandsWithShops, passedDes, index + 1);
              } */
            else // normal branching. 
            {

                long tempislandswithshops = islandsWithShops;

                islandsWithShops = islandsWithShops | (1 << index);
                for (int i = 1; i <= numOfDestinations; i++)
                    if ((value & (1 << i)) != 0 && (passedDes & (1 << i)) != 1) 
                      passedDes = passedDes | (1 << i);

                findMinNumOfShops(islandsWithShops, passedDes, index + 1, minislands++);
                findMinNumOfShops(tempislandswithshops, 0, index + 1, minislands);
            }
        }
    }
}
