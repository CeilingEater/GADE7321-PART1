using System;
using System.Collections.Generic;

namespace GraphImplementation
{
    public class Graph<T> where T : IComparable
    {
        Dictionary<T, List<T>> vertices = new Dictionary<T, List<T>>();

        public void AddVertex(T data)
        {
            if (!vertices.ContainsKey(data))
            {
                vertices.Add(data, new List<T>());
            }
        }

        public bool AddEdge(T from, T to)
        {
            if (vertices.ContainsKey(from) && vertices.ContainsKey(to))
            {
                if (!vertices[from].Contains(to))
                {
                    vertices[from].Add(to);
                    return true;
                }
            }
            return false;
        }

        public List<T> GetConnectedVertices(T data)
        {
            if (vertices.ContainsKey(data))
            {
                return vertices[data];
            }
            return null;
        }

        public bool RemoveVertex(T data)
        {
            if (vertices.ContainsKey(data))
            {
                vertices.Remove(data);
                return true;
            }
            return false;
        }

        public bool ContainsVertex(T data)
        {
            return vertices.ContainsKey(data);
        }
    }
} 
