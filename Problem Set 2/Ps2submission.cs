﻿using System;
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
            int first = queryAraay(start);
            int secfirst = queryAraay(start + 1);
            int mid = queryAraay((start + arrayLength)/2);
            int midTwo = queryAraay(((start + arrayLength)/ 2)-1);
            last = queryAraay(arrayLength);
            lastsec = queryAraay(arrayLength - 1);
            int midslope = (mid - midTwo);
            int Lastslope = (last - lastsec);
            firstslope = secfirst - first;

            // how to even check the slope 
            if(midslope < 0 && Lastslope > 0)
                 min = algorithmToFindMin((start + arrayLength) / 2, arrayLength);
            if(midslope > 0 && firstslope < 0)
                min = algorithmToFindMin(start, (start + arrayLength) / 2);
        //    if (midslope < 0 && Lastslope < 0)
            { }
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