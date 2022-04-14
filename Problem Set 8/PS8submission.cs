using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Assignment: PRoblem Set 8
/// Class: Cs 4150 - Algorithm 
/// 
/// Author: Sephora Bateman 
///
/// Prompt Given By: Professor Jensen- Univeristy of Utah - Spring 2022
/// </summary>

/// DELETE THIS:
/* WHAT I NEED TO DO:
 *    - Find How to Generate the weights. 
 *    - Figure out how the search give sus our answer. 
 *            Then compare mutilpe answers to give most wanted one. 
*/
namespace Problem_Set_8
{
    public class PS8submission
    {
        public static int roadSegNumber;
        public static List<string> AnswerSet = new List<string>();
        static void Main(string[] args)
        {
            // get the road number and intersections 
            string line = Console.ReadLine();
            string[] parts = line.Split(' ');
            roadSegNumber = int.Parse(parts[0]);
            int intersectionNum = int.Parse(parts[1]);
            string[,] roadMap = new string[roadSegNumber, roadSegNumber];
            // get the driversStarting poistion and ending position
            line = Console.ReadLine();
            parts = line.Split(' ');
            int driversStart = int.Parse(parts[0]);
            int driversEnd = int.Parse(parts[1]);
            //input the rest of the data. 
            int x;
            int y;
            for (int i = 0; i < intersectionNum; i++)
            {
                line = Console.ReadLine();
                parts = line.Split(' ');
                x = int.Parse(parts[0]);
                y = int.Parse(parts[2]);
                roadMap[x, y] = parts[1];           
            }
            //Find the shorest path 
            FindShortestPathAlgorithm(driversStart, driversEnd, roadMap);

            //Print the shorest path
            Console.WriteLine();
            Console.Write(AnswerSet.Count+ " ");
            AnswerSet.Reverse();
            Console.Write(String.Join(" ", AnswerSet));
        }

        /// <summary>
        ///  This is the algorithm that finds the shortest path from any start point to any end point. 
        /// </summary>
        /// <param name="driversStart"></param>
        /// <param name="driversEnd"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void FindShortestPathAlgorithm(int driversStart, int driversEnd,string[,] roadMap)
        {
            int[] dist = new int[roadSegNumber];
            int?[] pred = new int?[roadSegNumber];

            InitSSSP(driversStart, dist, pred,roadMap);
            
            PriorityQueue<int,int> pathBag = new PriorityQueue<int, int>();
            pathBag.Enqueue(driversStart,dist[driversStart]);
            while (pathBag.Count > 0)
            {
                int popVertex = pathBag.Dequeue();
                for (int i = 0; i < roadSegNumber; i++)
                    if (roadMap[popVertex, i] != null)
                        if (dist[i] > dist[popVertex] + GenerateWeight(roadMap[popVertex,i])) // tense 1 = w(u->v)
                        {
                            dist[i] = dist[popVertex] + GenerateWeight(roadMap[popVertex, i]); // relax 1 = w(u->v)
                            pred[i] = popVertex;
                            pathBag.Enqueue(i,dist[i]);
                        }
            }

            BackStep(pred,driversEnd,driversStart,roadMap);
        }

        /// <summary>
        /// Steps back through the transverals to find the path. 
        /// </summary>
        /// <param name="pred">The list of preds for each vertex</param>
        /// <param name="end">The ending vertes</param>
        /// <param name="start">The starting vertex</param>
        /// <param name="roadMap">The map to add the directions</param>
        private static void BackStep(int?[] pred, int end, int start, string[,] roadMap)
        {
            Queue<int> bag = new Queue<int>();
            bag.Enqueue(end);
            while (bag.Count > 0)
            {
                int vertex = bag.Dequeue();
                int predVertex = pred[vertex].Value;
                if (pred[vertex].Equals(start))
                {
                    AnswerSet.Add(roadMap[predVertex, vertex]);
                    break;
                }
                else
                {
                    AnswerSet.Add(roadMap[predVertex, vertex]);
                    bag.Enqueue(predVertex);
                }
            }
        }
        /// <summary>
        /// Based on which way the turn goes it calculates a route. 
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static int GenerateWeight(string weight)
        {
            if (weight.Equals("right"))
                return 1;
            else if (weight.Equals("left"))
                return 20000000;
            else if (weight.Equals("straight"))
                return 1000;
            else
                return 0;
        }
        /// <summary>
        /// Sets up the distance and perdessor tables
        /// </summary>
        /// <param name="driversStart">The first vertex</param>
        /// <param name="dist"> The distance array </param>
        /// <param name="pred">the parent vertex</param>
        /// <param name="roadMap">The road map </param>
        private static void InitSSSP(int driversStart, int[] dist, int?[] pred, string[,] roadMap)
        {
            dist[driversStart] = 0;
            pred[driversStart] = null;
            for (int i = 0; i < roadSegNumber; i++)
            {
                if (i != driversStart)
                {
                    dist[i] = Int32.MaxValue;
                    pred[i] = null;
                }
            }
                
        }
    }
}
