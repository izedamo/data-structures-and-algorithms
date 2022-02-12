using DataStructuresAndAlgorithms.Leetcode;
using System;

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
        }
    }
}