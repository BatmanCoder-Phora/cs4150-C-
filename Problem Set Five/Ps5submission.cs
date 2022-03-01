using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_Set_Five
{
    public class Ps5submission
    {
        // The input table sotres the data from the console and the markedTable keeps track of visited sqaures 
        public static string[,] inputTable;
        public static int[,] markedtable;
        // The "bag" to pull from is in the from of a stack. 
        public static Stack<string> Bag;

        // Some global variables. 
        public static string? playerStarts;
        public static int ending = 0;

        static void Main(string[] args)
        {
            // spilt the string to get the number of rows and columns. 
            string line = Console.ReadLine();
            string[] spiltline = line.Split(' ');
            int numberOfRows = int.Parse(spiltline[0]);
            int numberOfCols = int.Parse(spiltline[1]);
            //input data
            inputTable = new string[numberOfRows, numberOfCols];
            markedtable = new int[numberOfRows, numberOfCols];
            for (int i = 0; i < numberOfRows; i++)
                inputGraphData(i, numberOfCols);

            /* create bag to input verticies*/
            Bag = new Stack<string>();

            // Two variables to store soultions. 
            string currentRow = "";
            int endMid = int.MaxValue;

            // Loop through every empty space in the graph and place a monster. 
            for (int i = 1; i < numberOfRows; i++)
                for (int j = 1; j < numberOfCols; j++)
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
                    }
                }
            // After a monster has been put in each  spot print the best location for the player to get minimum treasure. 
            Console.Write(currentRow);
            Console.WriteLine();
            Console.Write(endMid);
        }
        /// <summary>
        ///  This converts the string on the stcak into a tuple so they two values are easier to acess. 
        /// </summary>
        /// <param name="input">The String from the stack</param>
        /// <returns></returns>
        public static Tuple<int,int> stringTotuple(string input)
        {
            string[] spiltstring = input.Split(' ');
            int row = int.Parse(spiltstring[0]);
            int col = int.Parse(spiltstring[1]);
            return Tuple.Create(row, col) ; 
        }
        /// <summary>
        /// Our search through every position the player can go to. Applies a stack depth-first search. 
        /// </summary>
        /// <param name="PlayersStartingPosition">The players starting position</param>
        private static void WhataeverFirstSearchAdapted(string PlayersStartingPosition)
        {
            int treasure = 0;
            
            Bag.Push(PlayersStartingPosition);
            // while the bag is not empty, get the position.      
            while(Bag.Count > 0)
            {
                string positionAtTopOfStack = Bag.Pop();
                Tuple<int,int> tuple = stringTotuple(positionAtTopOfStack);
                int row = tuple.Item1;
                int col = tuple.Item2;
                string stringIamon = inputTable[row, col];
                


                // Check to see if the position had been visied, if it hasn't mark it and add it's neighbors.
                if (markedtable[row, col] != 1)
                {
                    markedtable[row, col] = 1;
                  bool thereIsAMonster = CheckForUnwatedNeighbor(row, col, "m");
                  if(!thereIsAMonster)
                    addNeighbors(row, col);
                  Int32.TryParse(stringIamon, out treasure);
                  ending += treasure;
                }
            }   
        }
        /// <summary>
        /// Makes sure that a wall isn't being added as a neighbor. 
        /// </summary>
        /// <param name="currentRow">Row of the position we are checking the neighbors for</param>
        /// <param name="currentCol">Col of the position we are checking the neighbors for</param>
        private static void addNeighbors(int currentRow, int currentCol)
        {
                if (inputTable[currentRow + 1, currentCol].Trim() != "#")
                    Bag.Push((currentRow+1) + " " + currentCol);
                if (inputTable[currentRow - 1, currentCol].Trim() != "#")
                    Bag.Push((currentRow-1) + " " + currentCol);
                if (inputTable[currentRow, currentCol + 1].Trim() != "#")
                    Bag.Push(currentRow + " " + (currentCol+1));
                if (inputTable[currentRow, currentCol - 1].Trim() != "#")
                    Bag.Push(currentRow + " " + (currentCol-1));
        }
        /// <summary>
        /// Checks to see if any of the neighbors are a character that we don't want. 
        /// </summary>
        /// <param name="posRow">The row of the position</param>
        /// <param name="posCol">The current col of the position</param>
        /// <param name="lookFor">The character we want to make sure isn't there</param>
        /// <returns></returns>
        private static bool CheckForUnwatedNeighbor(int posRow, int posCol,string lookFor)
        {
            bool result = false;
            if (inputTable[posRow + 1, posCol].Trim() == lookFor)
                result =  true;
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
                    playerStarts = row.ToString() + " " + i.ToString();

                inputTable[row, i] = tester[i].ToString();
                markedtable[row, i] = 0;
            }
        }
    }

}
