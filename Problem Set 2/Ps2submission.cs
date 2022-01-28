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
        static public int lastslope;
        static public int firstslope = 1;
        static public int minvalue = int.MaxValue;
        static public int first;
        static public int secfirst;
       // static public  int addtionalp;
        public static Dictionary<int, int> values = new Dictionary<int, int>();
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
            
            // get the first point and last point only once and add them to the values dictionary 
            first = queryAraay(0);
            secfirst = queryAraay(0 + 1);
            last = queryAraay(arrayLength);
            lastsec = queryAraay(arrayLength - 1);
            values.Add(0, first);
            values.Add(arrayLength, last);
            values.Add(0 + 1, secfirst);
            values.Add(arrayLength - 1, lastsec);
            firstslope = secfirst - first;
            lastslope = last - lastsec;

            // find the min 
            algorithmToFindMin(0, arrayLength);


            // print the poistion in the array of where the minimum is. 
            int min = 0; ;
            foreach(KeyValuePair<int, int> kvp in values)
                if (kvp.Value == minvalue)
                    min = kvp.Key;

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
            int mid = 0, midsec = 0;
            int midcalculation = (start + arrayLength)/2;

            //base case
            if (midcalculation == start || midcalculation == arrayLength)
                return;
            int addtionalp;
            bool right;

            if ((firstslope > 0 && lastslope > 0))
            { addtionalp = midcalculation + 1;}
            else
            { addtionalp = midcalculation  - 1; }

            if (values.ContainsKey(midcalculation))
                values.TryGetValue((int)midcalculation, out mid);
            else
            {
                mid = queryAraay(midcalculation);
                values.Add(midcalculation, mid);
                qCounter++;
            }
            if (values.ContainsKey(addtionalp))
                values.TryGetValue(addtionalp, out midsec);
            else
            {
                midsec = queryAraay(addtionalp);
                values.Add(addtionalp, midsec);
                qCounter++;
            }
            int midslope;
            if (lastslope > 0 && firstslope > 0)
              midslope = midsec - mid;
            else
                midslope = mid - midsec;
            // update the min. 
            if (midsec < minvalue)
                minvalue = midsec;
             if (mid < midsec && mid<minvalue)
                minvalue = mid;

          if(midslope > 0)
            {
                if (firstslope > 0)
                {
                    if (mid > first)
                        algorithmToFindMin(midcalculation, arrayLength);
                    else if (mid < first)
                        algorithmToFindMin(start, midcalculation);
                }
                if (lastslope > 0)
                    algorithmToFindMin(midcalculation, arrayLength);
                else
                    algorithmToFindMin(start, midcalculation);
            }
            if (midslope < 0)
            {
                if (firstslope < 0)
                {
                    if (midcalculation < first)
                        algorithmToFindMin(midcalculation, arrayLength);
                    else if (midcalculation > first)
                        algorithmToFindMin(start, midcalculation);
                }
                else
                    algorithmToFindMin(midcalculation, arrayLength);
            }
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
