using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* Author: Sephora Bateman 
* Class: CS 4150
* Problem Set #4
*/
/// Work on the tracking.///
namespace Problem_Set_4
{
    /// <summary>
    /// Class to find the lowest cost of producing an item. 
    /// </summary>
    public class Ps4Submission
    {
        public static int[,] productionLineInformation;
        public static int[] switchcost;
        static void Main(string[] args)
        {
            // grab the number of production steps and lines.
            int numOfProductionLines = 0;
            int numOfProductionSteps = 0;
            int min; 
            string line = Console.ReadLine();
            string[] vs = line.Split(' ');
            numOfProductionLines = int.Parse(vs[0]);
            numOfProductionSteps = int.Parse(vs[1]);

            // inialize the information array and the switch array.
            productionLineInformation = new int[numOfProductionLines, numOfProductionSteps];
            switchcost = new int[numOfProductionSteps - 1];

            // store data 
            inputData(numOfProductionLines, numOfProductionSteps);

            //create soultion table and table to track switches
            int[,] soultionTable = new int[numOfProductionLines, numOfProductionSteps];
            string[,] trackSwitchs = new string[numOfProductionLines, numOfProductionSteps];
            int switchC = 0;
            // insert first table. 
            for (int firstCol = 0; firstCol < numOfProductionLines; firstCol++)
            {
                soultionTable[firstCol, 0] = productionLineInformation[firstCol, 0];
                trackSwitchs[firstCol, 0] = (firstCol+1).ToString();
            }

            // Fill in the rest of the table 
            for (int col = 1; col < numOfProductionSteps; col++)
            {
                for (int row = 0; row < numOfProductionLines; row++)
                {
                    min = soultionTable[row, col - 1] + productionLineInformation[row, col];
                    int oldLine = row + 1;
                    // another loop though loop through each row again.
                    for (int rowT = 1; rowT <= numOfProductionLines; rowT++)
                    {
                        int tempmin = soultionTable[rowT - 1, col - 1] + productionLineInformation[row, col] + switchcost[switchC];
                        if (tempmin < min)
                        {
                            min = tempmin;
                            oldLine = rowT;
                        }
                    }
                    // put the min in the osultion table and add that path to the pathway. 
                    soultionTable[row, col] = min;
                    trackSwitchs[row, col] = trackSwitchs[oldLine-1,col-1] + " " + (row + 1);
                }
                // keep track of the switch costs. 
                if (switchC + 1 < numOfProductionSteps - 1)
                    switchC++;
            }

            //// FINISH PRINT OUT THE MIN AND THE TRACKING///////
            int finalmin = int.MaxValue;
            int finalIndex = 0;
            // find the minimum line and print that value. 
            for (int finalrow = 0; finalrow < numOfProductionLines; finalrow++)
            {
                int  tempmin = soultionTable[finalrow, numOfProductionSteps - 1];
                if (tempmin < finalmin)
                {
                    finalIndex = finalrow;
                    finalmin = tempmin;
                }
            }
           // print min and path to min.  
            Console.Write(finalmin);
            Console.WriteLine();
            Console.Write(trackSwitchs.GetValue(finalIndex, numOfProductionSteps - 1));
            }
        
              
        /// <summary>
        /// Stores all the production lines and their costs in a double sided array. 
        /// </summary>
        /// <param name="numOfProductionLines"></param>
        /// <param name="numOfProductionSteps"></param>
        public static void inputData(int numOfProductionLines, int numOfProductionSteps)
        {
            string line;
            for (int i = 0; i < numOfProductionLines; i++)
            {
                line = Console.ReadLine();
                string[] steps = line.Split(' ');
                for (int j = 0; j < numOfProductionSteps; j++)
                {
                    productionLineInformation[i, j] = int.Parse(steps[j]);
                }
            }

            line = Console.ReadLine();
            string[] switchsteps = line.Split(' ');
            for (int i = 0; i < numOfProductionSteps - 1; i++)
                switchcost[i] = int.Parse(switchsteps[i]);
        }

        /// <summary>
        /// a little to min. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int min(int x, int y, int z)
        {
            if (x < y && x < z)
            {
                return x;
            }
            if (y < z && y < x)
                return y;

            return z;
        }

    }
}
