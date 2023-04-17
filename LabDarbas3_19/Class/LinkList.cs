using System;
using System.Collections;
using System.Collections.Generic;

namespace LabDarbas3_19.Class
{
    /// <summary>
    /// A linked list implementation that stores objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to be stored in the linked list.</typeparam>
    public sealed class LinkList<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Represents a node in the linked list.
        /// </summary>
        /// <typeparam name="T">The type of objects stored in the linked list.</typeparam>
        public sealed class Node<T> where T : IComparable<T>, IEquatable<T>
        {
            /// <summary>
            /// Gets or sets the value stored in the node.
            /// </summary>
            public T Data { get; set; }

            /// <summary>
            /// Gets or sets the next node in the linked list.
            /// </summary>
            public Node<T> Link { get; set; }

            /// <summary>
            /// Initializes a new instance of the Node class with the specified value and next node.
            /// </summary>
            /// <param name="value">The value to be stored in the node.</param>
            /// <param name="link">The next node in the linked list.</param>
            public Node(T value, Node<T> link)
            {
                Data = value;
                Link = link;
            }

            /// <summary>
            /// Invalidates the node by setting its value to the default value of T and its next node to null.
            /// </summary>
            public void Invalidate()
            {
                Data = default;
                Link = null;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the linked list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the linked list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        // Private instance fields.
        private Node<T> Head;
        private Node<T> Tail;

        /// <summary>
        /// Initializes a new instance of the LinkList class.
        /// </summary>
        public LinkList()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// Adds an object to the end of the linked list.
        /// </summary>
        /// <param name="value">The object to be added to the linked list.</param>
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

        /// <summary>
        /// Removes all elements from the linked list.
        /// </summary>
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

        /// <summary>
        /// Determines whether the linked list contains a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the linked list.</param>
        /// <returns>true if the value is found in the linked list; otherwise, false.</returns>
        public bool Contains(T value)
        {
            return this.Find(value) != null;
        }

        /// <summary>
        /// Copies the elements of the linked list to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the linked list.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown when the array is null.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when the array index is less than zero or greater than the length of the array.</exception>
        /// <exception cref="ArgumentException">Thrown when the space available in the array is less than the number of elements in the linked list.</exception>
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

        /// <summary>
        /// Removes the first occurrence of a specific element from the linked list.
        /// </summary>
        /// <param name="value">The element to remove from the linked list.</param>
        /// <returns>true if the element is successfully removed; otherwise, false.</returns>
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

        /// <summary>
        /// Finds the first occurrence of a specific value in the linked list.
        /// </summary>
        /// <param name="value">The value to locate in the linked list.</param>
        /// <returns>The first node containing the specified value, if found; otherwise, null.</returns>
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

        /// <summary>
        /// Returns an enumerator that iterates through the linked list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the linked list.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> iterator = Head; iterator != null; iterator = iterator.Link)
            {
                yield return iterator.Data;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the linked list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the linked list.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sorts the linked list in ascending order using the default comparer for the type of elements in the list.
        /// </summary>
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