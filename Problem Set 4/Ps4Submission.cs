using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_4
{
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
            productionLineInformation = new int[numOfProductionLines, numOfProductionSteps];
            switchcost = new int[numOfProductionSteps - 1];

            // store data 
            inputData(numOfProductionLines, numOfProductionSteps);

            //create soultion table and track switches
            int[,] soultionTable = new int[numOfProductionLines, numOfProductionSteps];
            int[,] trackSwitchs = new int[numOfProductionLines, numOfProductionSteps];

            for (int firstCol = 0; firstCol < numOfProductionLines; firstCol++)
                soultionTable[firstCol, 0] = productionLineInformation[firstCol, 0];

            int switchC = 0;


            // trying to fill in the rest of the table 
            for (int col = 1; col < numOfProductionSteps; col++)
            {
                for (int row = 0; row < numOfProductionLines; row++)
                {
                    min = soultionTable[row, col - 1] + productionLineInformation[row, col];
                    // loop through all the possiable costs. 
                    for (int rowT = 1; rowT <= numOfProductionLines; rowT++)
                    {
                        int tempmin = soultionTable[rowT-1, col - 1] + productionLineInformation[row, col] + switchcost[switchC];
                        if (tempmin < min)
                        {
                            min = tempmin;
                            trackSwitchs[row, col] = rowT;
                        }
                    }
                    soultionTable[row, col] = min;
                }
                // keep track of the switch costs. 
               if(switchC+1 < numOfProductionSteps-1)
                  switchC++;
            }

            //// FINISH PRINT OUT THE MIN AND THE TRACKING///////
            int finalmin = int.MaxValue;
            // find the minimum line and print that value. 
            for (int finalrow = 0; finalrow < numOfProductionLines; finalrow++)
            {
                int  tempmin = soultionTable[finalrow, numOfProductionSteps - 1];
                 if(tempmin < finalmin)
                    finalmin = tempmin;
            }
            Console.Write(finalmin);
            
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
