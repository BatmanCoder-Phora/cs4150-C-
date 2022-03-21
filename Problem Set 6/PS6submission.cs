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
        public static Dictionary<string,string> markedGraph = new Dictionary<string, string>();
        public static string[] output;
        public static bool cycledeteced = false;
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
            output = new string[playerOneNumOfQ+playerTwoNumOfQ+comQuests];

            TopologicalSort();

            Console.WriteLine(output);
        }
        /// <summary>
        /// Use a topological sort to find the order of visited vertexs. 
        /// </summary>
        static public void TopologicalSort()
        {
            int counter;
            foreach (string key in Graph.Keys)
                if(!markedGraph.ContainsKey(key))
                markedGraph.Add(key, "New");

            counter = markedGraph.Count-1;

            foreach (string vertex in Graph.Keys)
            {
                if (cycledeteced == true)
                {
                    break;
                    Console.WriteLine("Unsolvable");
                }
                if (markedGraph[vertex] == "New")
                    counter = TopSOrtDFS(vertex, counter);
            }
        }
        /// <summary>
        /// A helper function for topological sort, that follow a Depth first Search style organization. 
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        static public int TopSOrtDFS(string currentVertex,int counter)
        {
            markedGraph[currentVertex] = "Active";
            HashSet<string> neigh = new HashSet<string>();
            Graph.TryGetValue(currentVertex, out neigh);

          if(neigh != null)
            foreach (string neighborvertex in neigh) 
                if (markedGraph[neighborvertex] == "New")
                    counter = TopSOrtDFS(neighborvertex, counter);
                else if (markedGraph[neighborvertex] == "Active")
                {
                    cycledeteced = true;
                    break;
                }

            markedGraph[currentVertex] = "Finished";
            output[counter] = currentVertex;
            counter = counter - 1;
            return counter;

        }



                                                            /* Storing the graph helper functions */
        /// <summary>
        /// ReStores the infomation saved about player ones and player two's quests.
        /// </summary>
        /// <param name="playerOne">player one quests</param>
        /// <param name="playerTwo">player two quests</param>
        /// <param name="one">number of quest for one</param>
        /// <param name="two">number of quest for two</param>
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
        /// <summary>
        /// Puts in the rest of the graph data 
        /// </summary>
        /// <param name="quests">The quests</param>
        /// <param name="player">which player they belong to</param>
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
            {
                Graph[questA].Add(inputQuestB);
                markedGraph.Add(inputQuestB, "New");
            }
            else if (Graph.ContainsKey(questB))
            {
                Graph[inputQuestA].Add(questB);
                markedGraph.Add(inputQuestA, "New");
            }
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
