using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.Leetcode;

/*
155. Min Stack

https://leetcode.com/problems/min-stack/description/

Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.

Implement the MinStack class:

    MinStack() initializes the stack object.
    void push(int val) pushes the element val onto the stack.
    void pop() removes the element on the top of the stack.
    int top() gets the top element of the stack.
    int getMin() retrieves the minimum element in the stack.

You must implement a solution with O(1) time complexity for each function.
*/
public class MinStack
{
    private readonly Stack<(int, int)> _stack;

    public MinStack()
    {
        _stack = new Stack<(int, int)>();
    }

    public void Push(int val)
    {
        if (_stack.Count == 0)
        {
            _stack.Push((val, val));
            return;
        }

        var min = Math.Min(val, _stack.Peek().Item2);
        _stack.Push((val, min));
    }

    public void Pop()
    {
        _stack.Pop();
    }

    public int Top()
    {
        return _stack.Peek().Item1;
    }

    public int GetMin()
    {
        return _stack.Peek().Item2;
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(val);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */
