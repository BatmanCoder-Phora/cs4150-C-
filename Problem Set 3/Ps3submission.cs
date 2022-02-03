/*
 * Author: Sephora Bateman 
 * Class: CS 4150
 * Problem Set #3
 */
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
        public static Dictionary<int,int> islandsAndFerries = new Dictionary<int, int>();
        public static HashSet<int> islands = new HashSet<int>();
        public static long numOfDestinations;
        public static long numOfFerryRoutes;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string line;
            int index = 1;

            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);

            for (int i = 0; i < numOfFerryRoutes; i++)
            {
                inputIslandAndFerryData(line, i);
               
            }
            findMinNumOfShops(islands,passedDes,index);
        }

        private static void inputIslandAndFerryData(string line, int i)
        {
            line = Console.ReadLine();
            string[] spilt = line.Split(' ');
            int island = Int32.Parse(spilt[0]);
            int ferry = Int32.Parse(spilt[1]);
            if (i <= numOfDestinations)
                nofDestinations = nofDestinations | (1 << i);

            if (islandsAndFerries.ContainsKey(island))
                islandsAndFerries.Add(island, 0);
            else
            {
                islandsAndFerries.TryGetValue(island, out int value);
                islandsAndFerries.Remove(island);
                islandsAndFerries.Add(island, (value | (1 << ferry)));
            }
        }

        private static void findMinNumOfShops(HashSet<int> islands, long passedDes, int index)
        {
            if (passedDes == nofDestinations)
                return;


            findMinNumOfShops(islands,passedDes, index + 1);
            islands.Add(index);
            findMinNumOfShops(islands, passedDes, index + 1);
        }
    }
}
