using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures
{
    // Stacks using dynamic arrays - List
    public class MyStack<T>
    {
        private readonly List<T> _stackList;
        public readonly int Capacity;

        public MyStack(int capacity)
        {
            _stackList = new List<T>(capacity);
            Capacity = capacity;
        }

        public T Peek()
        {
            if (_stackList.Count > 0)
                return _stackList[^1];

            return default;
        }

        public MyStack<T> Push(T value)
        {
            if (_stackList.Count == Capacity)
                throw new OutOfMemoryException("Stack capacity full!!");
            
            _stackList.Add(value);

            return this;
        }

        public T Pop()
        {
            if (_stackList.Count == 0)
                throw new InvalidOperationException("Stack is empty!!");

            var deletedEntry = _stackList[^1];
            _stackList.RemoveAt(_stackList.Count - 1);

            return deletedEntry;
        }

        public bool IsEmpty()
        {
            return _stackList.Count == 0;
        }
    }
}
