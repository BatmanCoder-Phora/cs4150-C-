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
 *  represents the player requried quests in two sperate graphs 
 *  use topological sort tow find the order the player can either complete the quest 
 *  print weather both players were able to complete the quest. 
 */
namespace Problem_Set_6
{
    public class PS6submission
    {
       
       public static Dictionary<string,HashSet<string>> Graph = new Dictionary<string, HashSet<string>>();
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
                Graph.Add(Console.ReadLine().Trim(), new HashSet<string>());

            StoreInfomation(playerOne, playerTwo,playerOneNumOfQ,playerTwoNumOfQ);
        }

        private static void StoreInfomation(string playerOne, string playerTwo, int v,int z)
        {
            string[] spiltOneQuests = playerOne.Split('/');
            string[] spiltTwoQuests = playerTwo.Split('/');
            int total = v+z;
            for (int i = 0; i < total; i++)
                if (i < v)
                    inputGraphdata(spiltOneQuests[i], "1");
                else
                    inputGraphdata(spiltOneQuests[i], "2");
        }

        private static void inputGraphdata(string quests,string player)
        {
            string[] spiltline = quests.Split(" ");
            string questA = spiltline[0];
            string questB = spiltline[1];

            if (Graph.ContainsKey(questA) && Graph.ContainsKey(questB))
                Graph[questA].Add(questB);
            else if (Graph.ContainsKey(questA))
                Graph[questA].Add(questB + "-" + player);
            else if (Graph.ContainsKey(questB))
                Graph[questA + "-" + player].Add(questB);
            else
            {
                Graph.Add(questA + "-" + player, new HashSet<string>());
                Graph.Add(questB + "-" + player, new HashSet<string>());
                Graph[questA + "-" + player].Add(questB + "-" + player);
            }
        }

        static public void TopologicalSort()
        {
            foreach (string s in Graph.Keys)
            {
                // mark them
            }
                  // run topSort DFS
            
        }
        static public void TopSOrtDFS()
        {
     
        }
        
    }
}
