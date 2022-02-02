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
        public static int minsandshops;
        public static long nofDestinations;
        public static long passedDes;
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
                findMinNumOfShops(line);
            }

            Console.WriteLine(minsandshops);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public  static void findMinNumOfShops(string line)
        {

        }
    }
}
