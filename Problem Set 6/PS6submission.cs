using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * CS 4150: Algorithms 
 * 
 * Assignment Six: Adventures with firends 
 * 
 * Author: Sephora Bateman 
 */

/*
 * 3/21 submission:
 *    21/30 - failing three test due to Invalid solution
 */
namespace Problem_Set_6
{
    public class PS6submission
    {
        // Two different "maps". One to keep track of neighbors and the other to keep track of visited nodes
        public static Dictionary<string, HashSet<string>> Graph = new Dictionary<string, HashSet<string>>();
        public static Dictionary<string,string> markedGraph = new Dictionary<string, string>();
    
        // Any resulting path is saved here. 
        public static string[] resultingPath;
    
        // confrims of a cycle has been found. 
        public static bool cycledeteced = false;

        static void Main(string[] args)
        {

            //player One Info
            string line = Console.ReadLine();
            int playerOneNumOfQ = int.Parse(line);
            string playerOneQuests = "";
            for (int i = 0; i < playerOneNumOfQ; i++)
                playerOneQuests += Console.ReadLine() + "/";

            // player two Info
            line = Console.ReadLine();
            int playerTwoNumOfQ = int.Parse(line);
            string playerTwoQuests = "";
            for (int i = 0; i < playerTwoNumOfQ; i++)
                playerTwoQuests += Console.ReadLine() + "/";

            // Combined quests
            line = Console.ReadLine();
            int comQuests = int.Parse(line);
            for (int i = 0; i < comQuests; i++)
                Graph.Add(Console.ReadLine().Trim(), new HashSet<string>());


            StoreSavedInfomation(playerOneQuests, playerTwoQuests, playerOneNumOfQ, playerTwoNumOfQ);

            resultingPath = new string[Graph.Count];

            // Find the order the vertcies are completed in. 
            TopologicalSort();

            // Print wheather a path was found or not. 
            Console.WriteLine();
            if (cycledeteced)
                Console.WriteLine("Unsolvable");
            else
                resultingPath.ToList().ForEach(x => Console.WriteLine(x));
        }
        /// <summary>
        /// Use a topological sort to find the order of visited vertexs. 
        /// </summary>
        static public void TopologicalSort()
        {
            int counter;
            // mark each node as new.
            foreach (string key in Graph.Keys)
                if(!markedGraph.ContainsKey(key))
                     markedGraph.Add(key, "New");

            counter = markedGraph.Count-1;

            foreach (string vertex in Graph.Keys)
            {
                if (cycledeteced)
                    break;
                if (markedGraph[vertex] == "New")
                    counter = TopSortDFS(vertex, counter);
            }
        }
        /// <summary>
        /// A helper function for topological sort, that follow a Depth first Search style organization. 
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        static public int TopSortDFS(string currentVertex,int counter)
        {
            markedGraph[currentVertex] = "Active";

            HashSet<string> neighbors;
            Graph.TryGetValue(currentVertex, out neighbors);

         if (neighbors != null)
            foreach (string neighborVertex in neighbors) 
                if (markedGraph[neighborVertex] == "New")
                    counter = TopSortDFS(neighborVertex, counter);
                else if (markedGraph[neighborVertex] == "Active") // checks to see if there are any cycles. 
                {
                    cycledeteced = true;
                    break;
                }

            markedGraph[currentVertex] = "Finished";
            resultingPath[counter] = currentVertex;
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
        private static void StoreSavedInfomation(string playerOne, string playerTwo, int one, int two)
        {
            string[] spiltOneQuests = playerOne.Split('/');
            string[] spiltTwoQuests = playerTwo.Split('/');
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
            string[] spiltline = quests.Split(' ');
            string questA = spiltline[0];
            string questB = spiltline[1];
            string inputQuestA = spiltline[0] + "-" + player;
            string inputQuestB = spiltline[1] + "-" + player;

            // Depending on wheather one of the quests is a combined quest, different labels are added to the list. 
            if (Graph.ContainsKey(questA) && Graph.ContainsKey(questB))
                Graph[questA].Add(questB);
            else if (Graph.ContainsKey(questA))
            {
              if (!Graph.ContainsKey(inputQuestB))
                Graph.Add(inputQuestB, new HashSet<string>());
              
                Graph[questA].Add(inputQuestB);

            }
            else if (Graph.ContainsKey(questB))
            {
              if(!Graph.ContainsKey(inputQuestA))
                Graph.Add(inputQuestA, new HashSet<string>());
            
                Graph[inputQuestA].Add(questB);
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
