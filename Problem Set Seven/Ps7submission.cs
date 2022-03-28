using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_Seven
{
    public class Ps7submission
    {
        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            string[] lines = line.Split(' ');
            string algorhim = lines[5];
            algorhim = algorhim.Trim();

            if (algorhim == "Jarnik")
            {
                string startingVertex = lines[6];
                JarníksAlgorithm();
            }
            else if (algorhim == "Kruskal")
            {
                KruskalsAlgorithm();
            }
            else
            {
                BorůvkasAlgorithm();
            }

        }

        public static void JarníksAlgorithm()
        {

        }
        public static void KruskalsAlgorithm()
        {

        }
        public static void BorůvkasAlgorithm()
        {

        }
    }
}
