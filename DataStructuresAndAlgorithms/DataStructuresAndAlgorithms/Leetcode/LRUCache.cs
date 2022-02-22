using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public class LRUCache
    {
        private readonly Dictionary<int, int[]> entries;
        private readonly Queue<int> evictionQueue;
        private readonly int _capacity;

        public LRUCache(int capacity)
        {
            entries = new Dictionary<int, int[]>(capacity);
            evictionQueue = new Queue<int>(capacity);
            _capacity = capacity;
        }

        //this is O(1).
        public int Get(int key)
        {
            if (entries.ContainsKey(key))
            {
                entries[key][1]++;
                evictionQueue.Enqueue(key);
                return entries[key][0];
            }
            return -1;
        }

        //Supposed to be O(1) in average case.
        public void Put(int key, int value)
        {
            if (entries.ContainsKey(key))
            {
                entries[key][0] = value;
            }
            else
            {
                if(entries.Count < _capacity)
                {
                    entries.Add(key, new int[2] { value, 0 });
                }
                else
                {
                    var evictedEntry = evictionQueue.Dequeue();
                    while(entries[evictedEntry][1] > 1)
                    {
                        entries[evictedEntry][1]--;
                        evictedEntry = evictionQueue.Dequeue();
                    }
                    entries.Remove(evictedEntry);

                    entries.Add(key, new int[2] { value, 0 });
                }
            }

            evictionQueue.Enqueue(key);
            entries[key][1]++;
        }
    }
}
