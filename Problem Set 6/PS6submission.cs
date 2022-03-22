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
            for (int i = 0; i < playerOneNumOfQ; i++)
                inputGraphdata("1");

            // player two Info
            line = Console.ReadLine();
            int playerTwoNumOfQ = int.Parse(line);
            for (int i = 0; i < playerTwoNumOfQ; i++)
                inputGraphdata("2");

            // Combined quests
            line = Console.ReadLine();
            int comQuests = int.Parse(line);
            for (int i = 0; i < comQuests; i++)
                CombinedQuests();

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
        /// 
        /// </summary>
        private static void CombinedQuests()
        {
            string line = Console.ReadLine();
            Graph.Add(line, new HashSet<string>());
            HashSet<string> value;
            HashSet<string> newList = new HashSet<string>();
            foreach (string key in Graph.Keys)
            {
                bool keyWasRemoved = false;
                Graph.TryGetValue(key, out value);
                foreach (string neighbor in value)
                {
                    newList.Add(neighbor);
                    if (neighbor.Substring(0, neighbor.Length - 2).Equals(line))
                    {
                        newList.Remove(neighbor);
                        newList.Add(line);
                    }
                    if (key.Substring(0, key.Length - 2).Equals(line))
                    {
                        Graph[line].Add(neighbor);
                        Graph.Remove(key);
                        keyWasRemoved = true;
                    }
                
                }
                if (!keyWasRemoved)
                {
                    Graph[key] = newList;
                }
                newList = new HashSet<string>();
            }

               
        }
        /// <summary>
        /// Puts in the rest of the graph data 
        /// </summary>
        /// <param name="quests">The quests</param>
        /// <param name="player">which player they belong to</param>
        private static void inputGraphdata(string player)
        {
            string quests = Console.ReadLine();
            string[] spiltline = quests.Split(' ');
            string inputQuestA = spiltline[0] + "-" + player;
            string inputQuestB = spiltline[1] + "-" + player;

                if (!Graph.ContainsKey(inputQuestA))
                    Graph.Add(inputQuestA, new HashSet<string>());
                if (!Graph.ContainsKey(inputQuestB))
                    Graph.Add(inputQuestB, new HashSet<string>());

                Graph[inputQuestA].Add(inputQuestB);
            
        }
    }
}
