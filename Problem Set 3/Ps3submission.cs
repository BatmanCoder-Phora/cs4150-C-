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
        public static long passedDes;
        public static Dictionary<int,int> islandsAndFerries = new Dictionary<int, int>();
        public static HashSet<int> islands = new HashSet<int>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            long numOfDestinations;
            long numOfFerryRoutes;
            string line;

            line = Console.ReadLine();
            string[] linespilt = line.Split(' ');
            numOfDestinations = Int32.Parse(linespilt[0]);
            numOfFerryRoutes = Int32.Parse(linespilt[1]);

            for (int i = 0; i < numOfFerryRoutes; i++)
            {
                line = Console.ReadLine();
                string[] spilt = line.Split(' ');
                int island = Int32.Parse(spilt[0]);
                int ferry = Int32.Parse(spilt[1]);
                if (i <= numOfDestinations)
                    islands.Add(i);
                        
                if(islandsAndFerries.ContainsKey(island))
                   islandsAndFerries.Add(island, 0);
                else
                {
                    islandsAndFerries.TryGetValue(island, out int value);
                    islandsAndFerries.Remove(island);
                    islandsAndFerries.Add(island, (value | (1 << ferry)));
                }
            }
            findMinNumOfShops();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public  static void findMinNumOfShops()
        {

        }
    }
}
