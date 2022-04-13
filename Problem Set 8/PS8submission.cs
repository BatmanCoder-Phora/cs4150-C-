using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_8
{
    public class PS8submission
    {
        static void Main(string[] args)
        {
            // get the road number and intersections 
            string line = Console.ReadLine();
            string[] parts = line.Split(' ');
            int roadSegNumber = int.Parse(parts[0]);
            int intersectionNum = int.Parse(parts[1]);
            string[,] roadMap = new string[roadSegNumber, roadSegNumber];
            // get the driversStarting poistion and ending position
            line = Console.ReadLine();
            parts = line.Split(' ');
            int driversStart = int.Parse(parts[0]);
            int driversEnd = int.Parse(parts[1]);
            //input the rest of the data. 
            int x;
            int y;
            for (int i = 0; i < intersectionNum; i++)
            {
                line = Console.ReadLine();
                parts = line.Split(' ');
                x = int.Parse(parts[0]);
                y = int.Parse(parts[2]);
                roadMap[x, y] = parts[1];           
            }
            FindShortestPathAlgorithm(driversStart);
        }
        /// <summary>
        ///  This is the algorithm that finds the shortest path from any start point to any end point. 
        /// </summary>
        /// <param name="driversStart"></param>
        /// <param name="driversEnd"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void FindShortestPathAlgorithm(int driversStart)
        {
            Queue<int> pathBag = new Queue<int> ();
            Stack<int> otherpathBag = new Stack<int> ();
            pathBag.Enqueue (driversStart);
            otherpathBag.Push (driversStart);

            
        }
    }
}
