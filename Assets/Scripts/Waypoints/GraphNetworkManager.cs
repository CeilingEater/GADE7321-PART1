using UnityEngine;
using System.Collections.Generic;
using GraphImplementation;

public class GraphNetworkManager : MonoBehaviour
{
    public static GraphNetworkManager instance;
    
    public Graph<Waypoint> gameGraph = new Graph<Waypoint>();

    void Awake()
    {
        instance = this;
        BuildGraphFromScene();
    }

    void BuildGraphFromScene()
    {
        Waypoint[] allWaypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        
        foreach (Waypoint wp in allWaypoints)
        {
            gameGraph.AddVertex(wp);
        }
        
        foreach (Waypoint wp in allWaypoints)
        {
            foreach (Waypoint neighbor in wp.visibleNeighbors)
            {
                if (neighbor != null)
                {
                    gameGraph.AddEdge(wp, neighbor);
                }
            }
        }

        Debug.Log($"Custom Graph ADT initialized with {allWaypoints.Length} nodes!");
    }
}
