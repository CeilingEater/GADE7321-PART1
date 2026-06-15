using UnityEngine;
using System.Collections.Generic;
using GraphImplementation;

public class GraphNetworkManager : MonoBehaviour
{
    public static GraphNetworkManager instance;

    // Instantiating your pure C# data structure from scratch
    public Graph<Waypoint> gameGraph = new Graph<Waypoint>();

    void Awake()
    {
        instance = this;
        BuildGraphFromScene();
    }

    void BuildGraphFromScene()
    {
        // 1. Locate every waypoint node dropped into the maze geometry
        Waypoint[] allWaypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);

        // 2. Feed them into your custom Graph ADT structure as vertices
        foreach (Waypoint wp in allWaypoints)
        {
            gameGraph.AddVertex(wp);
        }

        // 3. Construct the graph edges using the dictionary structure
        foreach (Waypoint wp in allWaypoints)
        {
            foreach (Waypoint neighbor in wp.visibleNeighbors)
            {
                if (neighbor != null)
                {
                    // Calls your custom .AddEdge method
                    gameGraph.AddEdge(wp, neighbor);
                }
            }
        }

        Debug.Log($"Custom Graph ADT initialized with {allWaypoints.Length} nodes!");
    }
}
