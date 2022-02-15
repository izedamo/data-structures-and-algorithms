using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures
{
    public class MyQueue<T>
    {
        private readonly LinkedList<T> _queue;

        public MyQueue()
        {
            _queue = new LinkedList<T>();
        }

        public T Peek()
        {
            if (_queue.Count > 0)
                return _queue.First.Value;

            else return default;
        }

        public MyQueue<T> Enqueue(T value)
        {
            _queue.AddLast(value);

            return this;
        }

        public T Dequeue()
        {
            if (_queue.Count > 0)
            {
                var removedValue = _queue.First.Value;
                _queue.RemoveFirst();
                return removedValue;
            }
            else
                return default;
        }
    }
}
