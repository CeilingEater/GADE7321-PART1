using System;
using UnityEngine;

    public class StackNode<T>
    {
        private Node<T> _top;

        public StackNode()
        {
            _top = null;
        }

        public void Push(T value)
        {
            Node<T> newNode = new Node<T>(value, _top);
            _top = newNode;
        }
    
        public T Pop()
        {
            if (_top is null) 
                throw new InvalidOperationException("Stack is empty");

            T value = _top.Value;
            _top = _top.Next;

            return value;
        }

        public bool IsEmpty()
        {
            return _top == null;
        }
    
        public T Peek()
        {
            if (_top is null) 
                throw new InvalidOperationException("Stack is empty");
        
            return _top.Value;
        }

        private class Node<TX>
        {
            public TX Value { get; set; }
        
            public Node<TX> Next { get; set; }

            public Node(TX value, Node<TX> next)
            {
                Value = value;
                Next = next;
            }
        }
        
    }

