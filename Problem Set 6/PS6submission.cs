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
 * Is a dictionary really okay? 
 * passing thrity out of thrity 
 */
namespace Problem_Set_6
{
    public class PS6submission
    {
        // "Map" to be used to keep track of visited nodes
        public static Dictionary<string, string> markedGraph = new Dictionary<string, string>();

        // used to store the players quests, until joint quests are in the graph. 
        public static string[] PlayerOnePath;
        public static string[] PlayerTwoPath;
        // Any resulting path is saved here. 
        public static string[] resultingPath;

        // Shows if a cycle has been found. 
        public static bool cycledeteced = false;

        static void Main(string[] args)
        {
            // The graph

            Dictionary<string, HashSet<string>> Graph = new Dictionary<string, HashSet<string>>();
            //player One Info
            string line = Console.ReadLine();
            int playerOneNumOfQ = int.Parse(line);
            PlayerOnePath = new string[playerOneNumOfQ];
            for (int i = 0; i < playerOneNumOfQ; i++)
                PlayerOnePath[i] += Console.ReadLine();

            // player two Info
            line = Console.ReadLine();
            int playerTwoNumOfQ = int.Parse(line);
            PlayerTwoPath = new string[playerTwoNumOfQ];
            for (int i = 0; i < playerTwoNumOfQ; i++)
                PlayerTwoPath[i] += Console.ReadLine();

            // Combined quests
            line = Console.ReadLine();
            int comQuests = int.Parse(line);
            for (int i = 0; i < comQuests; i++)
                Graph.Add(Console.ReadLine().Trim(), new HashSet<string>());


            Graph = StoreSavedInfomation(PlayerOnePath, PlayerTwoPath, playerOneNumOfQ, playerTwoNumOfQ, Graph);

            resultingPath = new string[Graph.Count];

            // Find the order the vertcies are completed in. 
            TopologicalSort(Graph);

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
        static public void TopologicalSort(Dictionary<string, HashSet<string>> Graph)
        {
            int counter;
            // mark each node as new.
            foreach (string key in Graph.Keys)
                if (!markedGraph.ContainsKey(key))
                    markedGraph.Add(key, "New");

            counter = markedGraph.Count - 1;

            foreach (string vertex in Graph.Keys)
            {
                if (cycledeteced)
                    return;
                if (markedGraph[vertex] == "New")
                    counter = TopSortDFS(vertex, counter, Graph);
            }

        }
        /// <summary>
        /// A helper function for topological sort, that follow a Depth first Search style organization. 
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        static public int TopSortDFS(string currentVertex, int counter, Dictionary<string, HashSet<string>> Graph)
        {
            markedGraph[currentVertex] = "Active";

            HashSet<string> neighbors;
            Graph.TryGetValue(currentVertex, out neighbors);

            if (neighbors != null)
                foreach (string neighborVertex in neighbors)
                {
                    if (markedGraph[neighborVertex] == "New")
                        counter = TopSortDFS(neighborVertex, counter, Graph);
                    else if (markedGraph[neighborVertex] == "Active") // checks to see if there are any cycles. 
                    {
                        cycledeteced = true;
                        break;
                    }
                }
            // write the node to the resultingPath and mark the node as finished 
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
        private static Dictionary<string, HashSet<string>> StoreSavedInfomation(string[] playerOne, string[] playerTwo, int one, int two, Dictionary<string, HashSet<string>> graph)
        {
            for (int i = 0; i < one; i++)
                graph = inputGraphdata(playerOne[i], "1", graph);
            for (int i = 0; i < two; i++)
                graph = inputGraphdata(playerTwo[i], "2", graph);

            return graph;
        }
        /// <summary>
        /// Puts in the rest of the graph data 
        /// </summary>
        /// <param name="quests">The quests</param>
        /// <param name="player">which player they belong to</param>
        private static Dictionary<string, HashSet<string>> inputGraphdata(string quests, string player, Dictionary<string, HashSet<string>> Graph)
        {
            string[] spiltline = quests.Split(' ');
            string questA = spiltline[0];
            string questB = spiltline[1];
            string inputQuestA = spiltline[0] + "-" + player;
            string inputQuestB = spiltline[1] + "-" + player;


            if (Graph.ContainsKey(questA) && Graph.ContainsKey(questB)) // if both A and B are joint quests
                Graph[questA].Add(questB);
            else if (Graph.ContainsKey(questA)) // If quest A is a joint quest
            {
                if (!Graph.ContainsKey(inputQuestB))
                    Graph.Add(inputQuestB, new HashSet<string>());

                Graph[questA].Add(inputQuestB);

            }
            else if (Graph.ContainsKey(questB)) // if quest B is a joint quest
            {
                if (!Graph.ContainsKey(inputQuestA))
                    Graph.Add(inputQuestA, new HashSet<string>());

                Graph[inputQuestA].Add(questB);
            }
            else // if neither are joint quests.
            {
                if (!Graph.ContainsKey(inputQuestA))
                    Graph.Add(inputQuestA, new HashSet<string>());
                if (!Graph.ContainsKey(inputQuestB))
                    Graph.Add(inputQuestB, new HashSet<string>());

                Graph[inputQuestA].Add(inputQuestB);
            }
            return Graph;
        }
    }
}