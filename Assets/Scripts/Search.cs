using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //Get node at top of stack
            var node = nodes.Peek();

            bool forward = false;

            //mark as visited
            node.visited = true;

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

            //check if going forward, if not pop off from stack
            if(!forward)
            {
                nodes.Pop();
            }
        }

        path = new List<GraphNode>(nodes);

        //reverse stack so its first in last out
        path.Reverse();

        return found;
    }

        //will use parent in graphnodes
    public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
        bool found = false;

        var nodes = new Queue<GraphNode>();

        //set source node visited to true
        source.visited = true;

        //enqueue source node
        nodes.Enqueue(source);

        int steps = 0;
        while (!found && nodes.Count > 0 && steps++ < maxSteps)
        {
            //dequeue node
            var node = nodes.Dequeue();

            foreach(var edge in node.edges)
            {
                if (!edge.nodeB.visited)
                {
                    edge.nodeB.visited = true;

                    edge.nodeB.parent = node;

                    nodes.Enqueue(edge.nodeB);
                }

                if (edge.nodeB == destination)
                {
                    found = true;
                    break;
                }
            }
        }

        path = new List<GraphNode>();

        if(found)
        {
            var node = destination;

            while(node != null)
            {
                path.Add(node);

                node = node.parent;
            }

            path.Reverse();
        }
        else
        {
            path = nodes.ToList();
        }

        return found;
    }
}