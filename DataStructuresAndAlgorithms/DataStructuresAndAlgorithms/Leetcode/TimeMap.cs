using System;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.Leetcode;

/*
981. Time Based Key-Value Store

https://leetcode.com/problems/time-based-key-value-store/description/

Design a time-based key-value data structure that can store multiple values for the same key at different time stamps and retrieve the key's value at a certain timestamp.

Implement the TimeMap class:

    TimeMap() Initializes the object of the data structure.
    void set(String key, String value, int timestamp) Stores the key key with the value value at the given time timestamp.
    String get(String key, int timestamp) Returns a value such that set was called previously, with timestamp_prev <= timestamp. If there are multiple such values, it returns the value associated with the largest timestamp_prev. If there are no values, it returns "".

*/

public class TimeMap
{
    private readonly Dictionary<string, IList<(string value, int timestamp)>> _map;

    public TimeMap()
    {
        _map = new Dictionary<string, IList<(string value, int timestamp)>>();
    }

    public void Set(string key, string value, int timestamp)
    {
        if (_map.ContainsKey(key))
        {
            _map[key].Add((value, timestamp));
            return;
        }
        _map.Add(key, new List<(string value, int timestamp)> { (value, timestamp) });
    }

    public string Get(string key, int timestamp)
    {
        if (!_map.ContainsKey(key))
            return string.Empty;

        var values = _map[key];
        var (value, timestamp_prev) = (string.Empty, 0);
        void FindTimestamp(int start, int end)
        {
            if (start > end)
            {
                return;
            }

            var mid = (start + end) / 2;
            var midValue = values[mid];

            if (timestamp >= midValue.timestamp)
            {
                if (midValue.timestamp > timestamp_prev)
                {
                    value = midValue.value;
                    timestamp_prev = midValue.timestamp;
                }

                if (timestamp != midValue.timestamp)
                {
                    FindTimestamp(mid + 1, end);
                }
            }
            else // T < T1
            {
                FindTimestamp(start, mid - 1);
            }
        }

        FindTimestamp(0, values.Count - 1);

        return value;
    }
}

/**
 * Your TimeMap object will be instantiated and called as such:
 * TimeMap obj = new TimeMap();
 * obj.Set(key,value,timestamp);
 * string param_2 = obj.Get(key,timestamp);
 */
