using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_2
{
    public class Ps2submission
    {
        /// <summary>
        /// The main function for a program that finds the minium vales in a circle array. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int miniPosition;
            // read the array size 
            string line = Console.ReadLine();
            Int32.TryParse(line, out int arrayLength);

            miniPosition = algorithmToFindMin(0,(arrayLength-1));

             Console.ReadLine();

            // print the poistion of where the minimum is minimum 
            Console.WriteLine("minimum" + " " + miniPosition);
        }
        /// <summary>
        /// An algorithm to find 
        /// </summary>
        /// <param name="arrayLength"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static int algorithmToFindMin(int start,int arrayLength)
        {
            //make quies to find min.
            int min = 0;
            int first = queryAraay(start);
            int last  = queryAraay(arrayLength);
            int mid = queryAraay((start + arrayLength)/2);
            int midTwo = queryAraay(((start + arrayLength)/ 2)-1);
            int slope = (midTwo - first);
            int slopeTwo = (last - mid);

            // if the min hasn't come yet 
            if (slope > 0 && slopeTwo < 0)
                algorithmToFindMin(arrayLength / 2, arrayLength);
            // if the min has passed. 
            if(slope < 0 && slopeTwo > 0)
                algorithmToFindMin(start, arrayLength);

             // special cases
           if(slope > 0 && slopeTwo > 0)
            //  if(slopetwo > slope)
                algorithmToFindMin(start, mid);
            if (slope < 0 && slopeTwo < 0)
                algorithmToFindMin(mid, arrayLength);
            if (slope == 0 && slopeTwo == 0)
                 min = start;
            return min;
        }
        /// <summary>
        /// Helpful method to make queries. 
        /// </summary>
        /// <param name="queryNumber"></param>
        private static int queryAraay(int queryNumber)
        {
            Console.WriteLine(queryNumber);
            string line = Console.ReadLine();
            Int32.TryParse(line, out int qn);
            return qn;
        }
    }
}
