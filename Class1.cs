using System.Collections;

namespace MyLinkedList
{   
    /// <summary>
    /// Class which contains the data of type short and reference to next node.
    /// </summary>
    public class Node
    {        
        public short Data;
        public Node? Next;
        public Node(short data)
        {
            Data = data;
            Next = null;
        }
    }
    /// <summary>
    /// Class which contains the attributes and methods for LinkedList.
    /// </summary>
    public class TheLinkedList : IEnumerable<short>
    {        
        private int _numOfElements;
        private Node? _head;
        private Node? _tail;   
        /// <summary>
        /// Constructor for creating a new empty list.
        /// </summary>
        public TheLinkedList()
        {
            _numOfElements = 0;
            _head = null;
            _tail = null;
        }
        /// <summary>
        /// Adds one number at the end of list.
        /// </summary>
        /// <param name="value">The number which will be added to list.</param>
        public void Add(short value)
        {
            Node node = new Node(value);
            if (this.IsEmpty())
                _head = _tail = node;
            else
            {
                _tail.Next = node;
                _tail = node;
            }
            _numOfElements++;
        }
        /// <summary>
        /// Adds few numbers at the end of list.
        /// </summary>
        /// <param name="value">Array of numbers which will be added to list.</param>
        public void AddSomeElements(params short[] value)
        {
            foreach (short element in value)
                Add(element);
        }  
        /// <summary>
        /// The number of elements in list.
        /// </summary>
        public int Size => _numOfElements;
        /// <summary>
        /// Checks whether list is empty or not.
        /// </summary>
        /// <returns>True if list is empty, otherwise false.</returns>
        public bool IsEmpty() => _head == null;
        /// <summary>
        /// Searches for node at specified index in list.
        /// </summary>
        /// <param name="index">Index of node which is wanted to be found.</param>
        /// <returns>Node at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Index is less than zero or greater than max possible one.</exception>
        /// <exception cref="NullReferenceException">List is empty.</exception>
        private Node GetNodeAtIndex(int index)
        {
            if (index < 0 || index > _numOfElements - 1)
                throw new IndexOutOfRangeException("Oops, index is out of range");
            if (this.IsEmpty())
                throw new NullReferenceException("Sorry, but list doesn't contain any element");
            Node current = _head;
            for (int i = 0; i < index; i++)
                current = current.Next;
            return current;
        }
        /// <summary>
        /// Indexator to reach element in list at specified index.
        /// </summary>
        /// <param name="index">Index of element in list.</param>
        /// <returns>The element at specifiead index.</returns>
        public short this[int index]
        {            
            get => GetNodeAtIndex(index).Data;
            set => GetNodeAtIndex(index).Data = value;
        }
        /// <summary>
        /// Sets the size of list to 0. Sets both "head" and "tail" fields to null.
        /// </summary>
        public void Clear()
        {
            _numOfElements = 0;
            _head = _tail = null;
        }
        /// <summary>
        /// Removes element from list at specified index.
        /// </summary>
        /// <param name="index">Index of element to be removed.</param>
        /// <exception cref="IndexOutOfRangeException">Index is less than zero or greater than max possible one.</exception>
        /// <exception cref="NullReferenceException">List is empty, so there are no elements to delete.</exception>
        public void DeleteTheElementAtIndex(int index)
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Sorry, but list doesn't contain any element to delete");
            if (index < 0 || index > _numOfElements - 1)
                throw new IndexOutOfRangeException("Oops, index is out of range");            
            if (index == 0)
            {
                _head = _head.Next;
                if (_head == null)
                    _tail = null;
            }
            else
            {
                Node temp = GetNodeAtIndex(index - 1);             
                temp.Next = temp.Next.Next;
                if (index == _numOfElements - 1)
                    _tail = temp;
            }
            _numOfElements--;
        }
        /// <summary>
        /// Finds the number which is multiple to element.
        /// </summary>
        /// <param name="element">The dividor.</param>
        /// <param name="multiple">The multiple to element.</param>
        /// <returns>True if multiple is found, otherwise false.</returns>
        public bool FindTheMultipleNumber(short element, ref short multiple)
        {
            if (element == 0)
                return false;
            Node temp = _head;
            bool isFound = false;
            while (temp != null && !isFound)
            {
                if (temp.Data % element == 0)
                {
                    multiple = temp.Data;
                    isFound = true;
                }
                temp = temp.Next;
            }
            return isFound;
        }
        /// <summary>
        /// Changes elements with even indexes to 0.
        /// </summary>
        public void ChangeElementsWithEvenIndex()
        {
            if (this.IsEmpty()) 
                return;
            Node temp = _head;
            int index = 0;
            while (temp != null)
            {
                if (index % 2 == 0)
                    temp.Data = 0;
                temp = temp.Next;
                index++;
            }
        }
        /// <summary>
        /// Creates a new list of elements which greater than specified value.
        /// </summary>
        /// <param name="number">Specified value which is less than every element in new list.</param>
        /// <returns>New list, which also may be empty if no elements greater than specified value.</returns>
        public TheLinkedList CreateListWithGreaterNumbers(short number)
        {
            TheLinkedList newList = new TheLinkedList();
            Node temp = _head;
            while (temp != null)
            {
                if (temp.Data > number)
                    newList.Add(temp.Data);
                temp = temp.Next;
            }
            return newList;
        }
        /// <summary>
        /// Removes elements with odd indexes from the list.
        /// </summary>
        public void DeleteElementsWithOddIndex() 
        {
            if (_head == null || _head.Next == null)
                return;
            Node temp = _head;
            while (temp != null && temp.Next != null)
            {
                temp.Next = temp.Next.Next;
                _numOfElements--;
                temp = temp.Next;
            }
            _tail = temp;
        }
        /// <summary>
        /// Returns an iterator that iterates through all the elements of the list in the order they were added.
        /// </summary>
        /// <returns>Iterator for traversing list elements of type <see cref="short">.</returns>
        public IEnumerator<short> GetEnumerator()
        {
            Node? current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        /// <summary>
        /// Returns a non-generic iterator for iterating over the elements of a list.
        /// </summary>
        /// <returns>Iterator <see cref="System.Collections.IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
