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
            var arr1 = new int[] { 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1 };
            var arr2 = new int[] { -1, 0, 0, 0, 0, 1000 };

            //var result = Exercises.MergeSortedArrays(arr1, arr2);

            var idxs = LeetCode.TwoSum(arr1, 11);

            //var result = LeetCode.MoveZeroes(new int[] { 1, 0, 1, 0, 3, 12 });

            LeetCode.Rotate(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3);
            //Console.WriteLine(string.Join(", ", result));

            //var myStack = new MyStack<int>(5);

            //myStack.Push(1)
            //       .Push(2)
            //       .Push(3);

            //Console.WriteLine(myStack.Peek());

            //Console.WriteLine(myStack.Pop());

            //Console.WriteLine(myStack.Peek());

            var myBST = new BinarySearchTree();
            myBST.Insert(29);
            myBST.Insert(10);
            myBST.InsertSlightlyFaster(35);
            myBST.InsertSlightlyFaster(30);
            myBST.Insert(50);
            myBST.Insert(46);
            myBST.InsertSlightlyFaster(49);
            myBST.Insert(47);
            myBST.Insert(45);
            myBST.Insert(40);
            myBST.Insert(44);

            Console.WriteLine(myBST.Contains(40));
            Console.WriteLine(myBST.Contains(15));
            Console.WriteLine(myBST.Contains(5));
            Console.WriteLine(myBST.Contains(35));

            myBST.Remove(29);

            Console.WriteLine(myBST.Contains(29));
        }
    }
}