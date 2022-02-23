using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures
{
    public class MyMinHeap
    {
        private readonly List<int> heap;

        public int Count { get; private set; }

        public MyMinHeap()
        {
            heap = new List<int>();
            Count = 0;
        }

        public MyMinHeap(int initialCapacity)
        {
            heap = new List<int>(initialCapacity);
            Count = 0;
        }

        //push an integer on the heap.
        public void Push(int num)
        {
            heap.Add(num);
            Count++;

            Heapify();
        }

        //peek the smallest value in the heap.
        public int Peek()
        {
            return heap[0];
        }

        //get the heap elements
        public List<int> GetHeapElements()
        {
            return heap;
        }

        //delete the smallest value from heap and return it.
        public void Pop()
        {
            //replace the top element with the last element.
            heap[0] = heap[Count - 1];

            //remove the last element from bottom.
            heap.RemoveAt(Count - 1);

            //decrease heap count by 1.
            Count--;

            //bubble the top element to its rightful position in heap.
            var parent = 0;
            var leftChild = HeapLeftChild(parent);

            var parentVal = heap[parent];

            //bubble down the top value until all its children are larger than it.
            while (leftChild < Count)
            {
                var rightChild = HeapRightFromLeft(leftChild);

                //find the minimum of the 2 childs.
                int minChild;
                if (rightChild < Count) //rightChild it does not exist if rightChild >= Count.
                    minChild = heap[leftChild] < heap[rightChild] ? leftChild : rightChild;
                else
                    minChild = leftChild;

                //if parent value is greater than minimum of 2 children then swap their places.
                if (parentVal > heap[minChild])
                {
                    heap[parent] = heap[minChild];
                    heap[minChild] = parentVal;

                    parent = minChild;
                    leftChild = HeapLeftChild(parent);
                }

                else
                {
                    return;
                }
            }
        }

        //move the appended item to its rightful position in heap.
        private void Heapify()
        {
            var numIdx = Count - 1;
            var parentIdx = (numIdx - 1) / 2;

            while(parentIdx >= 0 && heap[numIdx] < heap[parentIdx])
            {
                var tmp = heap[numIdx];
                heap[numIdx] = heap[parentIdx];
                heap[parentIdx] = tmp;
                numIdx = parentIdx;
                parentIdx = (numIdx - 1) / 2;
            }
        }

        //get leftChild index given parent index.
        private static int HeapLeftChild(int i) => (i * 2) + 1;

        //get right child index given leftchild index.
        private static int HeapRightFromLeft(int i) => i + 1;
    }
}
