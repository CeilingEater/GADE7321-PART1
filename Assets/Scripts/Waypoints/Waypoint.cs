using UnityEngine;
using System;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour, IComparable
{
    [Header("Connections")]
    // Used to visually configure paths in the Unity editor
    public List<Waypoint> visibleNeighbors = new List<Waypoint>();

    // Satisfies your 'where T : IComparable' graph constraint
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        
        Waypoint other = obj as Waypoint;
        if (other != null)
        {
            // Compare unique Unity engine reference keys
            return this.GetInstanceID().CompareTo(other.GetInstanceID());
        }
        
        throw new ArgumentException("Object is not a Waypoint");
    }

    // Draws cyan lines in the scene view to see paths clearly
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (Waypoint neighbor in visibleNeighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}
