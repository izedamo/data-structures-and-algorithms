using DataStructuresAndAlgorithms.DataStructures;
using DataStructuresAndAlgorithms.DataStructures.Trees;
using DataStructuresAndAlgorithms.Leetcode;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, -8, 2, 5, 10, 3 };
            Console.WriteLine(string.Join(", ", Exercises.MergeSort(arr)));

            //var board = new char[][]
            //{
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'},
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'},
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'},
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'},
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'},
            //    new char[]{'a', 'a', 'a', 'a', 'a', 'a'}
            //};

            //var board2 = new char[][]
            //{
            //    new char[]{'A'}
            //};

            //Console.WriteLine(LeetCode.ExistRecursive(board, "aaaaaaaxaaaaaaa"));

            //Console.WriteLine(LeetCode.IsAnagram("tea", "eat"));

            //var boxes = new int[][] { new int[] { 1, 3 }, new int[] { 2, 2 }, new int[] { 3, 1 } };

            //Console.WriteLine(LeetCode.MaximumUnits(boxes, 4));

            //var arr = new int[] { 1, -8, 2, 5, 10, 3 };
            //var list = new List<int> { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            //Exercises.InsertionSort(list);
            //Console.WriteLine(string.Join(", ", list));

            //Console.WriteLine(Exercises.ReverseStringRecursive("yoyo mastery"));

            //Console.WriteLine(Exercises.GetFibonacciIterative(100));

            //var mygraph = new MyGraph();

            //mygraph.AddNode(0);
            //mygraph.AddNode(1);
            //mygraph.AddNode(2);
            //mygraph.AddNode(3);
            //mygraph.AddNode(4);
            //mygraph.AddNode(5);
            //mygraph.AddNode(6);

            //mygraph.AddEdge(3, 1);
            //mygraph.AddEdge(3, 4);
            //mygraph.AddEdge(4, 2);
            //mygraph.AddEdge(4, 5);
            //mygraph.AddEdge(1, 2);
            //mygraph.AddEdge(1, 0);
            //mygraph.AddEdge(0, 2);
            //mygraph.AddEdge(6, 5);

            //Console.WriteLine(mygraph.GetConnections());

            //mygraph.RemoveNode(0);

            //Console.WriteLine(mygraph.GetConnections());

            //Console.WriteLine(LeetCode.CountBinarySubstrings("00110011"));
            //Console.WriteLine(LeetCode.CountBinarySubstrings("10101"));

            //var arr1 = new int[] { 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1 };
            //var arr2 = new int[] { -1, 0, 0, 0, 0, 1000 };

            //var result = Exercises.MergeSortedArrays(arr1, arr2);

            //var idxs = LeetCode.TwoSum(arr1, 11);

            //var result = LeetCode.MoveZeroes(new int[] { 1, 0, 1, 0, 3, 12 });

            //LeetCode.Rotate(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3);
            //Console.WriteLine(string.Join(", ", result));

            //var myStack = new MyStack<int>(5);

            //myStack.Push(1)
            //       .Push(2)
            //       .Push(3);

            //Console.WriteLine(myStack.Peek());

            //Console.WriteLine(myStack.Pop());

            //Console.WriteLine(myStack.Peek());

            //var myBST = new BinarySearchTree();
            //myBST.Insert(29);
            //myBST.Insert(10);
            //myBST.InsertSlightlyFaster(35);
            //myBST.InsertSlightlyFaster(30);
            //myBST.Insert(50);
            //myBST.Insert(46);
            //myBST.InsertSlightlyFaster(49);
            //myBST.Insert(47);
            //myBST.Insert(45);
            //myBST.Insert(40);
            //myBST.Insert(44);

            //Console.WriteLine(myBST.Contains(40));
            //Console.WriteLine(myBST.Contains(15));
            //Console.WriteLine(myBST.Contains(5));
            //Console.WriteLine(myBST.Contains(35));

            //myBST.Remove(29);

            //Console.WriteLine(myBST.Contains(29));

            //Console.WriteLine(LeetCode.FirstUniqChar("loeleetcod"));
        }
    }
}