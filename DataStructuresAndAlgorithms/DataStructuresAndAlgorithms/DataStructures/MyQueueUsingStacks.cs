using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures
{
    public class MyQueueUsingStacks
    {
        private readonly Stack<int> _enqueueStack;
        private readonly Stack<int> _dequeueStack;

        public MyQueueUsingStacks()
        {
            _enqueueStack = new Stack<int>(100);
            _dequeueStack = new Stack<int>(100);
        }

        // Enqueue operation. O(1) time.
        public void Push(int x)
        {
            _enqueueStack.Push(x);
        }

        // Dequeue operation. O(n) time.
        public int Pop()
        {
            if (_dequeueStack.Count == 0)
            {
                while (_enqueueStack.Count > 0)
                {
                    _dequeueStack.Push(_enqueueStack.Pop());
                }
            }

            return _dequeueStack.Pop();
        }

        // O(n) time.
        public int Peek()
        {
            if (_dequeueStack.Count == 0)
            {
                while (_enqueueStack.Count > 0)
                {
                    _dequeueStack.Push(_enqueueStack.Pop());
                }
            }

            return _dequeueStack.Peek();
        }

        // Both stacks need to be empty for the queue to be empty.
        public bool Empty()
        {
            return _enqueueStack.Count == 0 && _dequeueStack.Count == 0;
        }
    }
}
