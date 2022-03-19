using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * CS 4150: Algorithms 
 * 
 * Assignment Six: Adventures with firends '
 * 
 * Author: Sephora Bateman 
 */

/*
 * Steps:
 *  Have a graph repersentes by a dictionary, now moving on to the topological order. 
 *  How to do the topological order 
 *  How to do the clock 
 *  How to repsesnt the status
 */
namespace Problem_Set_6
{
    public class PS6submission
    {

        public static Dictionary<string, HashSet<string>> Graph = new Dictionary<string, HashSet<string>>();

        static void Main(string[] args)
        {

            //player One
            string line = Console.ReadLine();
            int playerOneNumOfQ = int.Parse(line);
            string playerOne = "";
            for (int i = 0; i < playerOneNumOfQ; i++)
                playerOne += Console.ReadLine() + "/";

            // player two
            line = Console.ReadLine();
            int playerTwoNumOfQ = int.Parse(line);
            string playerTwo = "";
            for (int i = 0; i < playerTwoNumOfQ; i++)
                playerTwo += Console.ReadLine() + "/";

            // combined quests
            line = Console.ReadLine();
            int comQuests = int.Parse(line);
            for (int i = 0; i < comQuests; i++)
            {
                Graph.Add(Console.ReadLine().Trim(), new HashSet<string>());
            }

            StoreInfomation(playerOne, playerTwo, playerOneNumOfQ, playerTwoNumOfQ);

            TopologicalSort();
        }

        static public void TopologicalSort()
        {

            foreach (string s in Graph.Keys)
            {

            }
            foreach (string s in Graph.Keys)
            {

            }

        }
        static public void TopSOrtDFS(string s)
        {

        }



        /* Storing the graph helper functions */
        private static void StoreInfomation(string playerOne, string playerTwo, int one, int two)
        {
            string[] spiltOneQuests = playerOne.Split('/');
            string[] spiltTwoQuests = playerTwo.Split('/');
            int total = one + two;
            for (int i = 0; i < one; i++)
                inputGraphdata(spiltOneQuests[i], "1");
            for (int i = 0; i < two; i++)
                inputGraphdata(spiltTwoQuests[i], "2");
        }

        private static void inputGraphdata(string quests, string player)
        {
            string[] spiltline = quests.Split(" ");
            string questA = spiltline[0];
            string questB = spiltline[1];
            string inputQuestA = spiltline[0] + "-" + player;
            string inputQuestB = spiltline[1] + "-" + player;

            if (Graph.ContainsKey(questA) && Graph.ContainsKey(questB))
                Graph[questA].Add(questB);
            else if (Graph.ContainsKey(questA))
                Graph[questA].Add(inputQuestB);
            else if (Graph.ContainsKey(questB))
                Graph[inputQuestA].Add(questB);
            else
            {
                if (!Graph.ContainsKey(inputQuestA))
                    Graph.Add(inputQuestA, new HashSet<string>());
                if (!Graph.ContainsKey(inputQuestB))
                    Graph.Add(inputQuestB, new HashSet<string>());

                Graph[inputQuestA].Add(inputQuestB);
            }
        }



    }
    

}
