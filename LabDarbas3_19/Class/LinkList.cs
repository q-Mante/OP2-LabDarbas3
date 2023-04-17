using System;
using System.Collections;
using System.Collections.Generic;

namespace LabDarbas3_19.Class
{
    public sealed class LinkList<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T>, IEquatable<T>
    {
        public sealed class Node<T> where T : IComparable<T>, IEquatable<T>
        {
            public T Data { get; set; }
            public Node<T> Link { get; set; }
            public Node(T value, Node<T> link)
            {
                Data = value;
                Link = link;
            }

            public void Invalidate()
            {
                Data = default;
                Link = null;
            }
        }

        public int Count { get; private set; }
        public bool IsReadOnly
        {
            get { return false; }
        }

        private Node<T> Head;
        private Node<T> Tail;

        public LinkList()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        public void Add(T value)
        {
            var newNode = new Node<T>(value, null);

            if (Head != null)
            {
                Tail.Link = newNode;
                Tail = newNode;
                Count++;
            }
            else
            {
                Head = newNode;
                Tail = newNode;
                Count++;
            }
        }

        public void Clear()
        {
            Node<T> next = Head;
            while (next != null)
            {
                Node<T> prev = next;
                next = next.Link;
                prev.Invalidate();
            }
            Head = null;
            Count = 0;
        }

        public bool Contains(T value)
        {
            return this.Find(value) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("Array");

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new IndexOutOfRangeException("Index");

            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Insufficient space");

            Node<T> next = Head;
            while (next != null)
            {
                array[arrayIndex++] = next.Data;
                next = next.Link;
            }
        }

        public bool Remove(T value)
        {
            if (Head != null)
            {
                if (Head.Data.Equals(value))
                {
                    Head = Head.Link;
                    return true;
                }

                for (Node<T> iterator = Head; iterator != null; iterator = iterator.Link)
                {
                    if (iterator.Link != null && iterator.Link.Data.Equals(value))
                    {
                        iterator.Link = iterator.Link.Link;
                        return true;
                    }
                }
            }

            return false;
        }

        public Node<T> Find(T value)
        {
            Node<T> next = Head;

            if (next != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (next.Data.Equals(value)) return next;
                        next = next.Link;
                    }
                    while (next != null);
                }
                else
                {
                    do
                    {
                        if (next.Data == null) return next;
                        next = next.Link;
                    }
                    while (next != null);
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> iterator = Head; iterator != null; iterator = iterator.Link)
            {
                yield return iterator.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            for (Node<T> iterator1 = Head; iterator1 != null; iterator1 = iterator1.Link)
            {
                var min = iterator1;
                for (Node<T> iterator2 = iterator1.Link; iterator2 != null; iterator2 = iterator2.Link)
                {
                    if (iterator2.Data.CompareTo(min.Data) < 0)
                        min = iterator2;
                }
                (iterator1.Data, min.Data) = (min.Data, iterator1.Data);
            }
        }
    }
}