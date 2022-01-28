using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_2
{
    public class Ps2submission
    {
        static public int qCounter = 2;//TEST TAKE IT OUT 
        static public int last;
        static public int lastsec;
        static public int firstslope = 1;
        static public int Lastslope = 0;
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
            arrayLength -= 1;

            // get the first point and last point only once. 
            int first = queryAraay(0);
            int secfirst = queryAraay(0 + 1);
            last = queryAraay(arrayLength);
            lastsec = queryAraay(arrayLength - 1);
            firstslope = secfirst - first;

            // find the min 
            miniPosition = algorithmToFindMin(0,(arrayLength));
            

            // print the poistion of where the minimum is minimum 
            Console.WriteLine("minimum" + " " + miniPosition);
            Console.WriteLine("number of queries" + " " + qCounter);// TESTS TAKE IT OUT 
        }
        /// <summary>
        /// An algorithm to find 
        /// </summary>
        /// <param name="arrayLength"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static int algorithmToFindMin(int start,int arrayLength)
        {
            // to many q   
            int min = 0;
            int midcal = (start + arrayLength) / 2;
            int mid = queryAraay(midcal);
            int midTwo = queryAraay(((start + arrayLength)/ 2)-1);
            int midslope = (mid - midTwo);

            // how to even check the slope 
            if (midslope < 0)
            {
                if(Lastslope > 0)
                {
                    min = Lastslope;
                }
                else
                {
                    firstslope = midslope;
                    min = algorithmToFindMin(midcal + 1, arrayLength);
                }
            }
            if (midslope > 0)
            {
                    Lastslope = midslope;
                min = algorithmToFindMin(start, midcal);
            }
        //    if (midslope < 0 && Lastslope < 0)
            
          return min;
        }
        /// <summary>
        /// Helpful method to make queries. 
        /// </summary>
        /// <param name="queryNumber"></param>
        private static int queryAraay(int queryNumber)
        {
            Console.WriteLine("query" + " " + queryNumber);
            string line = Console.ReadLine();
            Int32.TryParse(line, out int qn);
            return qn;
        }
    }
}
