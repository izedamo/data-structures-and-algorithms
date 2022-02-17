using DataStructuresAndAlgorithms.DataStructures.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms
{
    class Exercises
    {
        //O(m+n) = Linear time.
        public static bool HaveCommonItems(string[] firstArray, string[] secondArray)
        {
            var hashset = new HashSet<string>(firstArray.Length); //O(1) lookup

            foreach (var elem in firstArray) //O(n)
            {
                hashset.Add(elem);
            }

            foreach (var elem in secondArray) //O(m)
            {
                if (hashset.Contains(elem))
                    return true;
            }
            return false;
        }

        //Determine whether the array has a pair of integers whose sum is equal to the sum parameter. -ve ints are allowed.
        // O(n^2) time complexity.
        public static bool HasPairWithSumNaive(int[] array, int sum)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var elem1 = array[i];
                for (var j = i + 1; j < array.Length; j++)
                {
                    var elem2 = array[j];

                    if (elem1 + elem2 == sum)
                        return true;
                }
            }

            return false;
        }

        //Better version of HasPairWithSumNaive.
        // O(n) time complexity.
        public static bool HasPairWithSumBetter(int[] array, int sum)
        {
            var comp = new HashSet<int>(); // set of complements.

            foreach (var elem in array)
            {
                if (comp.Contains(elem))
                    return true;
                else
                    comp.Add(sum - elem);
            }

            return false;
        }

        public static string ReverseTheString(string stringToReverse)
        {
            var reversedString = new StringBuilder();

            for (int idx = stringToReverse.Length - 1; idx >= 0; idx--)
            {
                reversedString.Append(stringToReverse[idx]);
            }

            return reversedString.ToString();
        }

        // O(max(m,n)) - Linear time merge sorted arrays.
        public static int[] MergeSortedArrays(int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr2 == null)
                return null;

            if (arr1.Length == 0)
                return arr2;

            if (arr2.Length == 0)
                return arr1;

            var resultArray = new int[arr1.Length + arr2.Length];

            var pointer1 = 0;
            var pointer2 = 0;
            var pointer3 = 0;

            while (pointer1 < arr1.Length && pointer2 < arr2.Length)
            {
                if (arr1[pointer1] > arr2[pointer2])
                {
                    resultArray[pointer3] = arr2[pointer2];
                    pointer2++;
                }
                else
                {
                    resultArray[pointer3] = arr1[pointer1];
                    pointer1++;
                }
                pointer3++;
            }

            AddRemainingItemsToResultArray(arr1, arr2, resultArray, ref pointer1, ref pointer2, ref pointer3);

            return resultArray;
        }

        private static void AddRemainingItemsToResultArray(int[] arr1, int[] arr2, int[] resultArray, ref int pointer1, ref int pointer2, ref int pointer3)
        {
            if (pointer1 < arr1.Length)
            {
                for (; pointer1 < arr1.Length; pointer1++)
                {
                    resultArray[pointer3] = arr1[pointer1];
                    pointer3++;
                }
            }
            else
            {
                for (; pointer2 < arr2.Length; pointer2++)
                {
                    resultArray[pointer3] = arr2[pointer2];
                    pointer3++;
                }
            }
        }

        //Given the root node of a complete binary tree, return its array representation.
        public static List<int> GetArrayRepresentation(BTNode rootNode)
        {
            if (rootNode == null)
                return null;

            if (rootNode.Left == null && rootNode.Right == null)
                return new List<int> { rootNode.Data };

            var resultArray = new List<int>(); //Using dynamic array since nodes in tree is unknown.

            var currentNode = rootNode;

            //TODO: Complete this section.
            //Q: Can we do it without recursion and queues?
            while (currentNode != null)
            {
                resultArray.Add(currentNode.Data);

                if (currentNode.Left != null)
                    currentNode = currentNode.Left;
            }

            return null;
        }

        public static long GetFactorialRecursive(int num)
        {
            //base case. assumes factorial of negative ints as 1.
            if (num <= 1)
                return 1;

            //recursive case. getting closer to base case by decreasing the num value each time.
            return num * GetFactorialRecursive(num - 1);
        }

        public static long GetFactorialIterative(int num)
        {
            if (num <= 1)
                return 1;

            var fact = 1;
            for (int multiplier = 2; multiplier <= num; multiplier++)
            {
                fact *= multiplier;
            }

            return fact;
        }

        //BADDDDD. T: O(2^n).
        public static long GetFibonacciRecursive(int num)
        {
            //input validation.
            if (num < 0)
                return -1;

            //base case.
            if (num == 0 || num == 1)
                return num;

            //recursive case. getting closer to base case.
            return GetFibonacciRecursive(num - 1) + GetFibonacciRecursive(num - 2);
        }

        //T: O(n).
        //Uses BigInt since it can calculate large fib values.
        public static BigInteger GetFibonacciIterative(int num)
        {
            //input validation.
            if (num < 0)
                return -1;

            if (num == 0 || num == 1)
                return num;

            //Nth fib = (N-1)th fib + (N-2)th fib.

            //we know that we have to find fib value for idx >= 2. so, we choose a starting point at idx = 3 for the loop. at which (N-1)th fib = 1 and (N-2)th fib also = 1.
            BigInteger currentFib = 1;
            BigInteger prevFib = 1;

            for (var idx = 3; idx <= num; idx++)
            {
                //we calculate the Nth fib value by adding the (N-1)th and (N-2)th values and store it in currentFib. in the next iteration, currentFib becomes the (N-1)the value and prevFib becomes (N-2)th value as we exchange their values in the previous iteration.
                var tmp = currentFib;
                currentFib += prevFib;
                prevFib = tmp;
            }

            return currentFib;
        }

        public static string ReverseStringRecursive(string input)
        {
            //base case.
            if (input.Length <= 1)
                return input;

            //recursive case.
            return $"{input[^1]}{ReverseStringRecursive(input[0..^1])}";
        }
    }
}
