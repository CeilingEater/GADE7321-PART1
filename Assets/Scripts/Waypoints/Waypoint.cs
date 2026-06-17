using UnityEngine;
using System;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour, IComparable
{
    [Header("Connections")]
    public List<Waypoint> visibleNeighbors = new List<Waypoint>();
    
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        
        Waypoint other = obj as Waypoint;
        if (other != null)
        {
            return this.GetInstanceID().CompareTo(other.GetInstanceID());
        }
        
        throw new ArgumentException("Object is not a Waypoint");
    }
    
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
