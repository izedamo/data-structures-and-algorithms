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

        //Uses dynamic programming to calculate Nth fib number.
        public static BigInteger GetFibonacciDP(int num)
        {
            //input validation.
            if (num < 0)
                return -1;

            var memoizedFibs = new Dictionary<int, BigInteger>
            {
                { 0, 1 },
                { 1, 1 }
            };

            //function to calculate nth fib number. uses memo.
            BigInteger FibDP(int n)
            {
                if (memoizedFibs.ContainsKey(n))
                    return memoizedFibs[n];

                else
                {
                    //recursive case. getting closer to base case.
                    var fib = FibDP(n - 1) + FibDP(n - 2);
                    memoizedFibs.Add(n, fib);

                    return fib;
                }
            }

            return FibDP(num);
        }

        public static string ReverseStringRecursive(string input)
        {
            //base case.
            if (input.Length <= 1)
                return input;

            //recursive case.
            return $"{input[^1]}{ReverseStringRecursive(input[0..^1])}";
        }

        //sorts in place. T: O(n^2), S: O(1).
        public static void BubbleSort(int[] arrayToBeSorted)
        {
            if (arrayToBeSorted == null || arrayToBeSorted.Length == 1 || arrayToBeSorted.Length == 0)
                return;

            //For bubble sort we need outer passes to be the length of the array.
            var passesRemaining = arrayToBeSorted.Length;

            //For the last pass, we don't need to do anything since only 1 elem remains to be sorted and since all other elems are already sorted this elem would be at its correct place already.
            while (passesRemaining > 1)
            {
                var prevIdx = 0;

                //For the 1st pass, we need to go till the end of array(N-1) to bubble out the largest elem. For the 2nd pass, we need to go till 2nd last of array i.e., N-2th idx only since after that all elems are already sorted. Incidentally, the idx till which we need to go = passesRemaining - 1.
                for (var idx = 1; idx < passesRemaining; idx++)
                {
                    if (arrayToBeSorted[prevIdx] > arrayToBeSorted[idx])
                    {
                        var tmp = arrayToBeSorted[idx];
                        arrayToBeSorted[idx] = arrayToBeSorted[prevIdx];
                        arrayToBeSorted[prevIdx] = tmp;
                    }

                    prevIdx = idx;
                }

                passesRemaining--;
            }
        }

        //sorts in place. T: O(n^2), S: O(1).
        public static void SelectionSort(int[] arrayToBeSorted)
        {
            if (arrayToBeSorted == null || arrayToBeSorted.Length == 1 || arrayToBeSorted.Length == 0)
                return;

            for (var idx = 0; idx < arrayToBeSorted.Length; idx++)
            {
                var red = idx;

                for (var blue = idx + 1; blue < arrayToBeSorted.Length; blue++)
                {
                    if (arrayToBeSorted[red] > arrayToBeSorted[blue])
                        red = blue;
                }

                if (red != idx)
                {
                    var tmp = arrayToBeSorted[idx];
                    arrayToBeSorted[idx] = arrayToBeSorted[red];
                    arrayToBeSorted[red] = tmp;
                }
            }
        }

        //works by inserting each element in its correct position. best T: O(n), worst T: O(n^2). S: O(1)
        public static void InsertionSort(List<int> nums)
        {
            if (nums == null || nums.Count == 0 || nums.Count == 1)
                return;

            //loop over nums to find correct elem for each idx.
            for (var idx = 1; idx < nums.Count; idx++)
            {
                //we only need to do something if the current element is less than the largest elem in the already sorted sublist.
                if (nums[idx] < nums[idx - 1])
                {
                    var replaceIdx = idx;

                    //we will start pushing the current element to its correct position in the sorted sublist. we keep in mind that when the sorted list has size 1 then we must avoid out of range scenario.
                    while (replaceIdx >= 1 && nums[replaceIdx - 1] > nums[replaceIdx])
                    {
                        var tmp = nums[replaceIdx - 1];
                        nums[replaceIdx - 1] = nums[replaceIdx];
                        nums[replaceIdx] = tmp;

                        replaceIdx--;
                    }
                }
            }
        }

        public static int[] MergeSort(int[] nums)
        {
            //validations.
            if (nums == null || nums.Length == 1)
                return nums;

            //base case.
            if (nums.Length == 2)
            {
                if (nums[0] > nums[1])
                    return new int[] { nums[1], nums[0] };
                else
                    return nums;
            }

            //recursive case.
            var mid = nums.Length / 2;

            //sort left half of array.
            int[] leftHalf = new int[mid];
            Array.Copy(nums, 0, leftHalf, 0, mid);
            leftHalf = MergeSort(leftHalf);

            //sort right half of array.
            int[] rightHalf = new int[nums.Length - mid];
            Array.Copy(nums, mid, rightHalf, 0, nums.Length - mid);
            rightHalf = MergeSort(rightHalf);

            //merge sorted halves.
            var sortedNums = new int[nums.Length];
            var ptrSortedNums = 0;
            var ptrLeftHalf = 0;
            var ptrRightHalf = 0;

            void AddRemainingNumsToArray(bool isRightHalfRemaining = false)
            {
                var remainingNums = leftHalf;
                var remainingPtr = ptrLeftHalf;
                if (isRightHalfRemaining)
                {
                    remainingNums = rightHalf;
                    remainingPtr = ptrRightHalf;
                }

                while (remainingPtr < remainingNums.Length)
                {
                    sortedNums[ptrSortedNums] = remainingNums[remainingPtr];
                    remainingPtr++;
                    ptrSortedNums++;
                }
            }

            while (ptrLeftHalf < leftHalf.Length && ptrRightHalf < rightHalf.Length)
            {
                if (leftHalf[ptrLeftHalf] <= rightHalf[ptrRightHalf])
                {
                    sortedNums[ptrSortedNums] = leftHalf[ptrLeftHalf];
                    ptrLeftHalf++;
                }
                else
                {
                    sortedNums[ptrSortedNums] = rightHalf[ptrRightHalf];
                    ptrRightHalf++;
                }
                ptrSortedNums++;
            }

            if (ptrLeftHalf == leftHalf.Length)
            {
                AddRemainingNumsToArray(true);
            }
            else
            {
                AddRemainingNumsToArray();
            }

            return sortedNums;
        }

        //given an MxN grid, find the number of ways a person a can travel from top-left to bottom-right.
        public static BigInteger GridTraveller(int m, int n)
        {
            //store previous travel results.
            var memo = new Dictionary<(int, int), BigInteger>
            {
                { (1, 1), 1 }
            };

            //memoized fn.
            BigInteger Travel(int r, int c)
            {
                if (r == 0 || c == 0)
                    return 0;

                if (memo.ContainsKey((r, c)))
                    return memo[(r, c)];

                if (memo.ContainsKey((c, r)))
                    return memo[(c, r)];

                var ways = Travel(r - 1, c) + Travel(r, c - 1);

                memo.Add((r, c), ways);

                return memo[(r, c)];
            }

            return Travel(m, n);
        }

        //given a target sum and a list of non-negative integers, determine(true/false) whether the sum can be achieved by using numbers from the list.
        //use dynamic programming.
        public static bool CanSum(int targetSum, int[] nums)
        {
            //validations
            if (nums == null || nums.Length == 0)
                return false;

            var memo = new Dictionary<int, bool>();

            bool CanSum(int target)
            {
                if (memo.ContainsKey(target))
                    return memo[target];

                //base case.
                if (target < 0)
                    return false;
                if (target == 0)
                    return true;

                //recursive case.
                //we check if the remaining sum can be obtained by adding nums from the list by subtracting a num from the target.
                foreach (var num in nums)
                {
                    //target can be reached by adding num multiple times.
                    //if (target % num == 0)
                    //{
                    //    memo.Add(target, true);
                    //    return memo[target];
                    //}

                    var newTarget = target - num;

                    if (CanSum(newTarget))
                    {
                        memo.Add(newTarget, true);
                        return memo[newTarget];
                    }
                }

                memo.Add(target, false);
                return memo[target];
            }

            return CanSum(targetSum);
        }

        //given a target sum and a list of nums, return a list of nums that add upto the target sum. if target cannot be achieved then return null.
        public static List<int> HowSum(int targetSum, int[] nums)
        {
            //validations
            if (nums == null || nums.Length == 0)
                return null;

            var memo = new Dictionary<int, bool>();
            var answer = new List<int>();

            //recursive fn.
            bool HowSum(int target)
            {
                if (memo.ContainsKey(target))
                    return memo[target];

                if (target == 0)
                    return true;
                if (target < 0)
                {
                    if (answer.Count > 0)
                        answer.RemoveAt(answer.Count - 1);
                    return false;
                }

                foreach (var num in nums)
                {
                    if (HowSum(target - num))
                    {
                        memo.Add(target - num, true);
                        answer.Add(num);
                        return memo[target - num];
                    }
                }

                if (answer.Count > 0)
                    answer.RemoveAt(answer.Count - 1);
                memo.Add(target, false);
                return memo[target];
            }

            HowSum(targetSum);

            return answer.Count > 0 ? answer : null;
        }

        //given a target sum and a list of nums, return the SHORTEST combination of nums that add upto the target sum. if target cannot be achieved then return null.
        public static List<int> BestSum(int targetSum, int[] nums)
        {
            //do validations here.

            List<int> bestSum(int target, Dictionary<int, List<int>> memo)
            {
                //initialize memo if not passed.
                if (memo == null)
                    memo = new Dictionary<int, List<int>>();

                if (memo.ContainsKey(target))
                    return memo[target];

                //base case.
                if (target == 0)
                    return new List<int>();
                if (target < 0)
                    return null;

                List<int> shortestCombination = null;
                foreach (var num in nums)
                {
                    var remainder = target - num;
                    var remainderList = bestSum(remainder, memo);

                    if (remainderList != null)
                    {
                        var combination = new List<int>(remainderList)
                        {
                            num
                        };

                        if (shortestCombination == null || combination.Count < shortestCombination.Count)
                        {
                            shortestCombination = combination;
                        }
                    }
                }

                memo.Add(target, shortestCombination);
                return shortestCombination;
            }

            return bestSum(targetSum, null);
        }
    }
}
