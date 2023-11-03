using System;

namespace Editor.Command
{
    public class Dequeue<T>
    {
        private LinkedNode<T> first;
        private LinkedNode<T> last;
        private int count;
        public int Count => count;
        
        public Dequeue(int initialCount)
        {
            if (initialCount < 0)
                throw new ArgumentException("Initial count cannot be negative.");

            count = initialCount;

            for (int i = 0; i < initialCount; i++)
            {
                AddLast(default(T)); 
            }
        }


     
        public void AddFirst(T value)
        {
            LinkedNode<T> node = new LinkedNode<T>(value);
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                first.Previous = node;
                node.Next = first;
                first = node;
            }
            count++;
        }
 
        public void AddLast(T value)
        {
            LinkedNode<T> node = new LinkedNode<T>(value);
            if (last == null)
            {
                first = node;
                last = node;
            }
            else
            {
                last.Next = node;
                node.Previous = last;
                last = node;
            }
            count++;
        }
 
        public T RemoveFirst()
        {
            if (count == 0)
                new Exception("Dequeue is empty!");
            T value = first.Value;
            first = first.Next;
            if (first != null)
                first.Previous = null;
            count--;
            return value;
        }
 
        public T RemoveLast()
        {
            if (count == 0)
                new Exception("Dequeue is empty!");
            T value = last.Value;
            last = last.Previous;
            if (last != null)
                last.Next = null;
            count--;
            return value;
        }
    }
 
    public class LinkedNode<T>
    {
        public T Value { get; set; }
        public LinkedNode<T> Previous { get; set; }
        public LinkedNode<T> Next { get; set; }
 
        public LinkedNode(T value)
        {
            Value = value;
        }
    }
}