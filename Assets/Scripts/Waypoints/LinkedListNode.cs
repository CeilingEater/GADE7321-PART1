using System;
using UnityEngine;

public class LinkedListNode<T>
{
    private Node<T> _head;
    private Node<T> _tail;
    private int _size;
    
    //_listOfNames[0] = "Ceili";

    public T this[int index]
    {
        get =>  GetValueAt(index);
        set => SetValueAt(value, index);
    }
    
    public int Size => _size;

    public LinkedListNode()
    {
        Clear();
    }

    public void Clear()
    {
        _head = new Node<T>(default(T), null, _tail);
        _tail = new Node<T>(default, _head, null);
        _size = 0;
    }

    public void SetValueAt(T value, int index)
    {
        Node<T> targetNode = FindNodeAt(index);
        targetNode.value = value;
    }

    public void Insert(T value)
    {
        InsertBefore(value, _tail);
    }

    public void InsertAt(T value, int index)
    {
        Node<T> targetNode = FindNodeAt(index);
        InsertBefore(value, targetNode);
    }
    
    public int FindIndex(T value)
    {
        int index = FindNode(value, out Node<T> targetNode);
        return index;
    }

    
    public T GetValueAt(int index)
    {
        return FindNodeAt(index).value;
    }

    // remove the first occurence of "value" from the list
    public bool Remove(T value)
    {
        Node<T> nodeToRemove;
        int index = FindNode(value, out nodeToRemove);

        if (index == -1)
            return false;

        nodeToRemove.previous.next = nodeToRemove.next;
        nodeToRemove.next.previous = nodeToRemove.previous;
        _size--;

        return true;
    }

    private void InsertBefore(T value, Node<T> node)
    {
        Node<T> newNode = new Node<T>(value, node.previous , node);
        node.previous.next = newNode;
        node.previous = newNode;
        _size++;
    }

    private int FindNode(T value, out Node<T> node)
    {
        node = _head;
        for (int i = 0; i < _size; i++)
        {
            node = node.next;
            if (node.value.Equals(value)) 
            { 
                return i;
            }
        }
        
        node = null;
        return -1;
    }

    private Node<T> FindNodeAt(int index)
    {
        if (index < 0 || index > _size)
        {
            throw new IndexOutOfRangeException("Invalid index " + index + "for List of size ");
        }
        bool StartAtHead = index < _size / 2;
        Node<T> foundNode;

        if (StartAtHead)
        {
            foundNode = _head;
            for (int i = 0; i <= index; i++)
            {
                foundNode = foundNode.next;
            }
        }
        else
        {
            foundNode = _tail;
            for (int i = _size - 1; i >= index; i--)
            {
                foundNode = foundNode.previous;
            }
        }
        
        return foundNode;
    }
    
    private class Node<TN>
    {
        public Node<TN> next;
        public Node<TN> previous;
        public TN value;

        public Node(TN value, Node<TN> previous, Node<TN> next)
        {
            this.value = value;
            this.next = next;
            this.previous = previous;
        }
    }
}
