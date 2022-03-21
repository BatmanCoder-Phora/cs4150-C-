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
 * Have yet to test it
 */
namespace Problem_Set_6
{
    public class PS6submission
    {

        public static Dictionary<string, HashSet<string>> Graph = new Dictionary<string, HashSet<string>>();
        public static Dictionary<string,string> markedGraph = new Dictionary<string, string>();
     
        public static string[] resultingPath;
        
        public static bool cycledeteced = false;
        public static int questcounter = 0;


        static void Main(string[] args)
        {

            //player One Info
            string line = Console.ReadLine();
            int playerOneNumOfQ = int.Parse(line);
            string playerOne = "";
            for (int i = 0; i < playerOneNumOfQ; i++)
                playerOne += Console.ReadLine() + "/";

            // player two Info
            line = Console.ReadLine();
            int playerTwoNumOfQ = int.Parse(line);
            string playerTwo = "";
            for (int i = 0; i < playerTwoNumOfQ; i++)
                playerTwo += Console.ReadLine() + "/";

            // Combined quests
            line = Console.ReadLine();
            int comQuests = int.Parse(line);
            for (int i = 0; i < comQuests; i++)
            {
                Graph.Add(Console.ReadLine().Trim(), new HashSet<string>());
                questcounter++;
            }

            StoreInfomation(playerOne, playerTwo, playerOneNumOfQ, playerTwoNumOfQ);

            resultingPath = new string[Graph.Count];

            // Find an order of the vertices. 
            TopologicalSort();
            
            Console.WriteLine();

            // Print Weather a path was found or not. 
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
            
            foreach (string key in Graph.Keys)
                if(!markedGraph.ContainsKey(key))
                     markedGraph.Add(key, "New");

            counter = markedGraph.Count-1;

            foreach (string vertex in Graph.Keys)
            {
                if (cycledeteced)
                    break;
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
                else if (markedGraph[neighborvertex] == "Active") // checks to see if there are any cycles. 
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
        private static void StoreInfomation(string playerOne, string playerTwo, int one, int two)
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
            string[] spiltline = quests.Split(" ");
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
