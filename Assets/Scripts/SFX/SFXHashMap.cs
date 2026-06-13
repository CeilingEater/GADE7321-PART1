using UnityEngine;

public class SFXHashMap
{
    private class MapNode
    {
        public string Key { get; }
        public AudioClip Value { get; set; }
        public MapNode Next { get; set; }

        public MapNode(string key, AudioClip value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private readonly int bucketSize;
    private readonly MapNode[] buckets;

    //constructor 
    public SFXHashMap(int size = 32)
    {
        bucketSize = size;
        buckets = new MapNode[bucketSize];
    }
    
    private int GetBucketIndex(string key)
    {
        if (key == null) return 0;
        
        int hashCode = key.GetHashCode();
        int index = hashCode % bucketSize;
        
        return Mathf.Abs(index);
    }
    
    public void Put(string key, AudioClip value)
    {
        if (string.IsNullOrEmpty(key)) return;

        int index = GetBucketIndex(key);
        MapNode head = buckets[index];

        //checking if key already exists 
        while (head != null)
        {
            if (head.Key == key)
            {
                head.Value = value; 
                return;
            }
            head = head.Next;
        }

        //else we insert
        MapNode newNode = new MapNode(key, value);
        newNode.Next = buckets[index];
        buckets[index] = newNode;
    }
    
    public AudioClip Get(string key)
    {
        if (string.IsNullOrEmpty(key)) return null;

        int index = GetBucketIndex(key);
        MapNode head = buckets[index];

        while (head != null)
        {
            if (head.Key == key)
            {
                return head.Value;
            }
            head = head.Next;
        }

        //sound not found
        return null;
    }
}