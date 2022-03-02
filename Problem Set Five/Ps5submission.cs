using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tests: taking to long

namespace Problem_Set_Five
{
    public class Ps5submission
    {
        // The input table sotres the data from the console and the markedTable keeps track of visited sqaures 
        public static string[,] inputTable;
        public static bool[,] markedtable;
        public static int numberOfRows;
        public static int numberOfCols;


        // The "bag"  
        public static Stack<Tuple<int,int>> Bag;

        // Some global variables two get and store infomation.  
        public static Tuple<int, int> playerStarts;
        public static int ending = 0;
        public static int endMid = int.MaxValue;

        static void Main(string[] args)
        {
            // spilt the string to get the number of rows and columns. 
            string line = Console.ReadLine();
            string[] spiltline = line.Split(' ');
            numberOfRows = int.Parse(spiltline[0]);
            numberOfCols = int.Parse(spiltline[1]);

            // input data
            inputTable = new string[numberOfRows, numberOfCols];
            Bag = new Stack<Tuple<int, int>>();
            for (int i = 0; i < numberOfRows; i++)
                inputGraphData(i, numberOfCols);

            // Variable to store soultions. 
            string currentRow = "";

            // Loop through every empty space that the player can reach and add a monster. 
            for (int i = 1; i < numberOfRows - 1; i++)
                for (int j = 1; j < numberOfCols - 1; j++)
                {
                    // start inside the border and make sure no monster is placed adjcant to the player.  
                    if (inputTable[i, j] == "." && (CheckForUnwatedNeighbor(i, j, "p") != true))
                    {
                        inputTable[i, j] = "m";

                        WhataeverFirstSearchAdapted(playerStarts);// finds the soultions. 

                        if (ending < endMid)
                        {
                            endMid = ending;
                            currentRow = i + " " + j;
                        }
                        inputTable[i, j] = ".";
                    }
                }
            // After a monster has been put in each spot print the best location for the player to get minimum treasure. 
            Console.Write(currentRow);
            Console.WriteLine();
            Console.Write(endMid);
        }
        
        /// <summary>
        /// Our search through every position the player can go to. Applies a stack depth-first search. 
        /// </summary>
        /// <param name="PlayersStartingPosition">The players starting position</param>
        private static void WhataeverFirstSearchAdapted(Tuple<int, int> PlayersStartingPosition)
        {
            int treasure = 0;
            ending = 0;

            /* create bag to input verticies*/
            Bag.Clear();
            markedtable = new bool[numberOfRows - 1, numberOfCols - 1];

            // push first vertex
            Bag.Push(PlayersStartingPosition);
            
            // while the bag is not empty, check the next vertex.      
            while (Bag.Count > 0)
            {
                Tuple<int, int> tuple = Bag.Pop();
                int row = tuple.Item1;
                int col = tuple.Item2;
                string ContentsForSquare = inputTable[row, col];

                // Check to see if the position had been visited, if it hasn't mark it and add it's neighbors.
                if (markedtable[row, col] != true)
                {
                    markedtable[row, col] = true;
                    bool thereIsAMonster = CheckForUnwatedNeighbor(row, col, "m");
                    if (!thereIsAMonster)
                        addNeighbors(row, col);

                    Int32.TryParse(ContentsForSquare, out treasure);
                    ending += treasure;
                }
                // the the amount of treasure is already larger than the stored min, stop calculating treasure. 
                if (ending >= endMid)
                    return;
            }
        }
        /// <summary>
        /// Add the neighbors that are not a wall or have not been visited onto the stack.  
        /// </summary>2
        /// <param name="currentRow">Row of the position we are checking the neighbors for</param>
        /// <param name="currentCol">Col of the position we are checking the neighbors for</param>
        private static void addNeighbors(int currentRow, int currentCol)
        {
            if (inputTable[currentRow + 1, currentCol].Trim() != "#" && markedtable[currentRow + 1, currentCol] != true)
                Bag.Push(Tuple.Create((currentRow + 1),currentCol));
            if (inputTable[currentRow - 1, currentCol].Trim() != "#" && markedtable[currentRow - 1, currentCol] != true)
                Bag.Push(Tuple.Create((currentRow - 1), currentCol));
            if (inputTable[currentRow, currentCol + 1].Trim() != "#" && markedtable[currentRow, currentCol + 1] != true)
                Bag.Push(Tuple.Create(currentRow, (currentCol + 1)));
            if (inputTable[currentRow, currentCol - 1].Trim() != "#" && markedtable[currentRow, currentCol - 1] != true)
                Bag.Push(Tuple.Create(currentRow, (currentCol-1)));
        }
        /// <summary>
        /// Checks to see if any of the neighbors are a character that we don't want. 
        /// </summary>
        /// <param name="posRow">The row of the position</param>
        /// <param name="posCol">The current col of the position</param>
        /// <param name="lookFor">The character we want to make sure isn't there</param>
        /// <returns></returns>
        private static bool CheckForUnwatedNeighbor(int posRow, int posCol, string lookFor)
        {
            bool result = false;
            if (inputTable[posRow + 1, posCol].Trim() == lookFor)
                result = true;
            if (inputTable[posRow - 1, posCol].Trim() == lookFor)
                result = true;
            if (inputTable[posRow, posCol + 1].Trim() == lookFor)
                result = true;
            if (inputTable[posRow, posCol - 1].Trim() == lookFor)
                result = true;

            return result;
        }
        /// <summary>
        /// Stores all the information from the console. 
        /// </summary>
        /// <param name="row">The rows from the input</param>
        /// <param name="col">The cols from the input</param>
        private static void inputGraphData(int row, int col)
        {
            string line = Console.ReadLine();
            char[] tester = line.ToArray();
            for (int i = 0; i < col; i++)
            {
                if (tester[i] == 'p')
                    playerStarts = Tuple.Create(row,i);

                inputTable[row, i] = tester[i].ToString();
            }
        }
    }

}