using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_Five
{
    public class Ps5submission
    {
        public static string[,] inputTable;
        public static Stack<string> Bag;
        public static int[,] markedtable;
        public static string playerStarts;
        public static int ending = 0;

        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            string[] spiltline = line.Split(' ');
            int row = int.Parse(spiltline[0]);
            int col = int.Parse(spiltline[1]);

            inputTable = new string[row, col];
            markedtable = new int[row, col];
            Bag = new Stack<string>();

            string currentRow = "";
            int endMid = int.MaxValue;

            for (int i = 0; i < row; i++)
               inputGraphData(i,col);

            for (int i = 1; i < row; i++)
                for (int j = 1; j < col; j++)
                {
                    if (inputTable[i, j] == "." && (CheckForUnwatedNeighbor(i, j, "p") != true))
                    {
                        inputTable[i, j] = "m";
                        WhataeverFirstSearchAdapted(playerStarts);
                        if (ending < endMid)
                        {
                            endMid = ending;
                            currentRow = i + " " + j;
                        }
                    }
                }
            Console.Write(currentRow);
            Console.WriteLine();
            Console.Write(endMid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Tuple<int,int> stringTotuple(string input)
        {
            string[] spiltstring = input.Split(' ');
            int row = int.Parse(spiltstring[0]);
            int col = int.Parse(spiltstring[1]);
            return Tuple.Create(row, col) ; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private static void WhataeverFirstSearchAdapted(string input)
        {
            int treasure = 0;

            Bag.Push(input);
            while(Bag.Count > 0)
            {
                string test = Bag.Pop();
                Tuple<int,int> tuple = stringTotuple(test);
                int row = tuple.Item1;
                int col = tuple.Item2;
                string stringIamon = inputTable[row, col];
                Int32.TryParse(stringIamon, out treasure);
                ending += treasure;
                if (markedtable[tuple.Item1, tuple.Item2] != 1)
                {
                    markedtable[tuple.Item1, tuple.Item2] = 1;
                //    bool thereIsAMonster = CheckForUnwatedNeighbor(tuple.Item1, tuple.Item2, "m");
              //    if(!thereIsAMonster)
                    addNeighbors(row, col);
                }

            }   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item11"></param>
        /// <param name="item12"></param>
        private static void addNeighbors(int item11, int item12)
        {

          bool thereIsAMonster = CheckForUnwatedNeighbor(item11, item12, "m");
            if (thereIsAMonster == false)
            {
                if (inputTable[item11 + 1, item12].Trim() != "m" && inputTable[item11 + 1, item12].Trim() != "#")
                    Bag.Push((item11+1) + " " + item12);
                if (inputTable[item11 - 1, item12].Trim() != "m" && inputTable[item11 - 1, item12].Trim() != "#")
                    Bag.Push((item11-1) + " " + item12);
                if (inputTable[item11, item12 + 1].Trim() != "m" && inputTable[item11, item12 + 1].Trim() != "#")
                    Bag.Push(item11 + " " + (item12+1));
                if (inputTable[item11, item12 - 1].Trim() != "m" && inputTable[item11, item12 - 1].Trim() != "#")
                    Bag.Push(item11 + " " + (item12-1));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item11"></param>
        /// <param name="item12"></param>
        /// <param name="lookFor"></param>
        /// <returns></returns>
        private static bool CheckForUnwatedNeighbor(int item11, int item12,string lookFor)
        {
            bool result = false;
            if (inputTable[item11 + 1, item12].Trim() == lookFor)
                result =  true;
             if (inputTable[item11 - 1, item12].Trim() == lookFor)
                result = true;
             if (inputTable[item11, item12 + 1].Trim() == lookFor)
                result = true;
             if (inputTable[item11, item12 - 1].Trim() == lookFor)
                result = true;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void inputGraphData(int row, int col)
        {
            string line = Console.ReadLine();
            char[] tester = line.ToArray();
            for (int i = 0; i < col; i++)
            {
                if (tester[i] == 'p')
                    playerStarts = row.ToString() + " " + i.ToString();

                inputTable[row, i] = tester[i].ToString();
                markedtable[row, i] = 0;
            }
        }
    }

}
