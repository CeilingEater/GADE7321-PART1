using System;
using UnityEngine;

public class QueueNode<T>
{
    private Node<T> _front;
    private Node<T> _back;
    private int _count;
    
    public bool IsEmpty => _front == null && _back == null; // if both back and front are null then queue is empty

    public QueueNode()
    {
        _front = null;
        _back = null;
        _count = 0;
    }

    public void Enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item, _back, null);

        if (IsEmpty)
        {
            _front = newNode;
        }
        else
        {
            _back.next = newNode;
        }
        
        _back = newNode;
        _count++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Can't dequeue on an empty queue");
        
        T value = _front.value;
        
        _front = _front.next;
        //_front.prev = null;
        
        if (_front == null)
        {
            _back = null;
        }
        _count--;
        
        return value;
    }
    
    private class Node<TX>
    {
        public Node<TX> next;
        public Node<TX> prev;
        public TX value;

        public Node(TX value, Node<TX> prev, Node<TX> next)
        {
            this.value = value;
            this.prev = prev;
            this.next = next;
        }
    }
}
