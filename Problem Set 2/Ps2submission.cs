using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_2
{
    public class Ps2submission
    {
        static public int qCounter = 4;//TEST TAKE IT OUT 
        static public int last;
        static public int lastsec;
        static public int firstslope = 1;
        static public int Lastslope = 0;
        static public int min = int.MaxValue;
        static public int first;
        static public int secfirst;
        /// <summary>
        /// The main function for a program that finds the minium vales in a circle array. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // read the array size
            string line = Console.ReadLine();
            Int32.TryParse(line, out int arrayLength);
            arrayLength -= 1;

            // get the first point and last point only once. 
             first = queryAraay(0);
             secfirst = queryAraay(0 + 1);
             last = queryAraay(arrayLength);
             lastsec = queryAraay(arrayLength - 1);
             firstslope = secfirst - first;

            // find the min 
            algorithmToFindMin(0, (arrayLength));


            // print the poistion of where the minimum is minimum 
            Console.WriteLine("minimum" + " " + min);
            Console.WriteLine("number of queries" + " " + qCounter);// TESTS TAKE IT OUT 
        }
        /// <summary>
        /// An algorithm to find the position of the minimum value.
        /// </summary>
        /// <param name="arrayLength"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void algorithmToFindMin(int start, int arrayLength)
        {
            // to many q  
            int mid, midTwo;
            int midcal = (start + arrayLength) / 2;

            if (midcal == start || (midcal+1) == start+1)
            { mid = first; midTwo = secfirst; }
            else if (midcal == arrayLength || (midcal+1) == arrayLength-1)
            { mid = last; midTwo = lastsec; }
            else
            {
                mid = queryAraay(midcal);
                midTwo = queryAraay(midcal - 1);
                qCounter += 2;
            }

            if (midcal == start || midcal == arrayLength)
                return;

            int midslope = (mid - midTwo);

            // how to even check the slope 
            if (midslope < 0)
            {
                if(!(Lastslope < 0))
                {
                    firstslope = midslope;
                    if (midTwo < mid && midTwo < min)
                        min = midcal-1;
                    else if (mid < min)
                        min = midcal;
                    algorithmToFindMin(midcal, arrayLength);                  
                }
                
            }
            if (midslope > 0)
            {
                if (!(firstslope > 0))
                {
                    Lastslope = midslope;
                    if (midTwo < min && midTwo<mid)
                        min = midcal - 1;
                    else 
                        min = midcal;
                    algorithmToFindMin(start , midcal);
                }
                
            }
            // spepical edge cases. 
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
