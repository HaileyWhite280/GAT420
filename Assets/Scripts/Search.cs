using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public static class Search
{
    public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);

    static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
    {
        if (source == null || destination == null) return false;

        // reset graph nodes
        GraphNode.ResetNodes();

        // search for path from source to destination nodes        
        bool found = searchAlgorithm(source, destination, ref path, steps);

        return found;
    }

    public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Stack<GraphNode>();

        //push node to stack
        nodes.Push(source);

        int steps = 0;
        while(!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            var node = nodes.Peek();

            //mark as visited
            node.visited = true;

            bool forward = false;

            foreach(var edge in node.edges)
            {
                //check if node has been visited
                if(!edge.nodeB.visited)
                {
                    nodes.Push(edge.nodeB);
                    forward = true;

                    //check if destination
                    if(edge.nodeB == destination)
                    {
                        found = true;
                    }

                    break;
                }
            }

            //check if going forward, pop off from stack
            if(!forward)
            {
                nodes.Pop();
            }
        }

        //reverse stack so its first in last out
        path = new List<GraphNode>(nodes);
        path.Reverse();

        return found;
    }

    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        //will use parent in graphnodes

        return found;
    }
}