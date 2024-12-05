using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Numerics;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Timers;
using System.Transactions;
using System.Xml.Linq;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public static partial class LeetCode
    {
        /*
        567. Permutation in String
        
        https://leetcode.com/problems/permutation-in-string/

        Given two strings s1 and s2, return true if s2 contains a
        permutation
        of s1, or false otherwise.

        In other words, return true if one of s1's permutations is the substring of s2.
        */
        public static bool CheckInclusion(string s1, string s2)
        {
            //validations here.

            var s1charCounts = new Dictionary<char, int>();
            foreach (var chr in s1)
            {
                if (s1charCounts.ContainsKey(chr))
                {
                    s1charCounts[chr] += 1;
                    continue;
                }
                s1charCounts[chr] = 1;
            }

            var left = 0;
            var right = 0;

            var s2charCounts = new Dictionary<char, int>();
            while (left <= right && right < s2.Length)
            {
                var s2char = s2[right];
                if (s2charCounts.ContainsKey(s2char))
                {
                    s2charCounts[s2char] += 1;
                }
                else
                {
                    s2charCounts.Add(s2char, 1);
                }

                int length;
                if (s1charCounts.TryGetValue(s2char, out int s1Count))
                {
                    var s2Count = s2charCounts[s2char];

                    while (s2Count > s1Count)
                    {
                        if (s2charCounts[s2[left]] > 0)
                            s2charCounts[s2[left]] -= 1;
                        left++;
                        s2Count = s2charCounts[s2char];
                    }

                    length = right - left + 1;
                    right++;
                }
                else
                {
                    s2charCounts.Remove(s2char);

                    while (
                        left < s2.Length && (left <= right || !s1charCounts.ContainsKey(s2[left]))
                    )
                    {
                        if (s2charCounts.ContainsKey(s2[left]))
                        {
                            s2charCounts[s2[left]] -= 1;
                        }
                        left++;
                    }

                    right = left;
                    length = 0;
                }

                if (length == s1.Length)
                    return true;
            }

            return false;
        }

        /*
        424. Longest Repeating Character Replacement
        
        https://leetcode.com/problems/longest-repeating-character-replacement/description/

        You are given a string s and an integer k. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most k times.

        Return the length of the longest substring containing the same letter you can get after performing the above operations.
        */
        public static int CharacterReplacement(string s, int k)
        {
            //validations here.

            var longestLength = 0;

            foreach (var repeatChar in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                var left = 0;
                var right = 0;
                var kCount = k;

                while (left <= right && right < s.Length)
                {
                    if (s[right] != repeatChar)
                    {
                        if (kCount > 0)
                        {
                            //if we can replace a character then do it and move ahead.
                            kCount -= 1;
                        }
                        else
                        {
                            while (kCount == 0)
                            {
                                while (s[left] == repeatChar)
                                {
                                    left++;
                                }
                                left++;
                                kCount += 1;
                            }

                            kCount -= 1;
                        }
                    }

                    var length = right - left + 1;

                    if (length > longestLength)
                        longestLength = length;

                    right++;
                }
            }

            return longestLength;
        }

        /*
        3. Longest Substring Without Repeating Characters
        
        https://leetcode.com/problems/longest-substring-without-repeating-characters/description/

        Given a string s, find the length of the longest substring without repeating characters.
        */
        public static int LengthOfLongestSubstring(string s)
        {
            //validations here.

            var seenChars = new HashSet<char>();
            var left = 0;
            var right = 0;
            var longestLength = 0;

            while (left <= right && right < s.Length)
            {
                if (seenChars.Contains(s[right]))
                {
                    while (s[left] != s[right])
                    {
                        seenChars.Remove(s[left]);
                        left++;
                    }
                    seenChars.Remove(s[left]);
                    left++;
                }

                seenChars.Add(s[right]);

                var length = right - left + 1;

                if (length > longestLength)
                    longestLength = length;

                right++;
            }

            return longestLength;
        }

        /*
        33. Search in Rotated Sorted Array
        
        https://leetcode.com/problems/search-in-rotated-sorted-array/description/

        There is an integer array nums sorted in ascending order (with distinct values).

        Prior to being passed to your function, nums is possibly rotated at an unknown pivot index k (1 <= k < nums.length) such that the resulting array is [nums[k], nums[k+1], ..., nums[n-1], nums[0], nums[1], ..., nums[k-1]] (0-indexed). For example, [0,1,2,4,5,6,7] might be rotated at pivot index 3 and become [4,5,6,7,0,1,2].

        Given the array nums after the possible rotation and an integer target, return the index of target if it is in nums, or -1 if it is not in nums.

        You must write an algorithm with O(log n) runtime complexity.
        */
        public static int SearchRotatedArray(int[] nums, int target)
        {
            //validations here.

            int Search(int start, int end)
            {
                if (start > end)
                    return -1;

                //rotated sorted arrays have a two portions which are sorted individually.
                //so we can use binary search on these portions to find target.

                var mid = (start + end) / 2;
                var midNum = nums[mid];

                if (target == midNum)
                    return mid;

                if (target < midNum)
                {
                    if (midNum > nums[start])
                    {
                        if (nums[start] <= target)
                        {
                            return Search(start, mid - 1);
                        }
                        else
                        {
                            return Search(mid + 1, end);
                        }
                    }
                    else
                    {
                        if (target <= nums[end])
                        {
                            return Search(mid + 1, end);
                        }
                        else
                        {
                            return Search(start, mid - 1);
                        }
                    }
                }
                else
                {
                    if (midNum > nums[start])
                    {
                        return Search(mid + 1, end);
                    }
                    else
                    {
                        if (target <= nums[end])
                        {
                            return Search(mid + 1, end);
                        }
                        else
                        {
                            return Search(start, mid - 1);
                        }
                    }
                }
            }

            return Search(0, nums.Length - 1);
        }

        /*
        153. Find Minimum in Rotated Sorted Array
        
        https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/description/

        Suppose an array of length n sorted in ascending order is rotated between 1 and n times. For example, the array nums = [0,1,2,4,5,6,7] might become:

            [4,5,6,7,0,1,2] if it was rotated 4 times.
            [0,1,2,4,5,6,7] if it was rotated 7 times.

        Notice that rotating an array [a[0], a[1], a[2], ..., a[n-1]] 1 time results in the array [a[n-1], a[0], a[1], a[2], ..., a[n-2]].

        Given the sorted rotated array nums of unique elements, return the minimum element of this array.

        You must write an algorithm that runs in O(log n) time.
        */
        public static int FindMin(int[] nums)
        {
            //validations here.

            //rotated sorted array always have two portions that are sorted.
            //so we can choose where to search by determining if the current middle point is a part of left or right sorted portion.

            var min = int.MaxValue;

            void SetMin(int start, int end)
            {
                if (start > end)
                    return;

                var mid = (start + end) / 2;

                if (nums[mid] < min)
                    min = nums[mid];

                if (nums[start] <= nums[mid])
                {
                    //mid is a part of the right sorted portion.

                    if (nums[start] < min)
                        min = nums[start];

                    SetMin(mid + 1, end);
                }
                else
                {
                    SetMin(start, mid - 1);
                }
            }

            SetMin(0, nums.Length - 1);

            return min;
        }

        /*
        875. Koko Eating Bananas
        
        https://leetcode.com/problems/koko-eating-bananas/description/

        Koko loves to eat bananas. There are n piles of bananas, the ith pile has piles[i] bananas. The guards have gone and will come back in h hours.

        Koko can decide her bananas-per-hour eating speed of k. Each hour, she chooses some pile of bananas and eats k bananas from that pile. If the pile has less than k bananas, she eats all of them instead and will not eat any more bananas during this hour.

        Koko likes to eat slowly but still wants to finish eating all the bananas before the guards return.

        Return the minimum integer k such that she can eat all the bananas within h hours.
        */
        public static int MinEatingSpeed(int[] piles, int h)
        {
            //validations here.

            //max speed for Koko can be max(piles) since she will be able to eat all the piles before h runs out since piles.length < h.

            //so Koko's speed can be in range of 1..max(piles).
            //to find minimum we can go through these speeds to find which is the minimum speed for which she can eat all piles before h runs out.
            //to do that, instead of going linearly we can use binary search since speed range is already sorted.

            long maxSpeed = 1;
            foreach (var pile in piles)
            {
                if (pile > maxSpeed)
                    maxSpeed = pile;
            }

            long minSpeed = maxSpeed;

            void FindMinSpeed(long start, long end)
            {
                if (start > end)
                    return;

                long speed = (start + end) / 2;

                long time = 0;
                foreach (var pile in piles)
                {
                    time += (int)Math.Ceiling((double)pile / speed);
                }

                if (time > h)
                {
                    FindMinSpeed(speed + 1, end);
                }
                else if (time <= h)
                {
                    if (speed < minSpeed)
                        minSpeed = speed;

                    FindMinSpeed(start, speed - 1);
                }
            }

            FindMinSpeed(1, maxSpeed);

            return (int)minSpeed;
        }

        /*
        74. Search a 2D Matrix
        
        https://leetcode.com/problems/search-a-2d-matrix/description/

        You are given an m x n integer matrix matrix with the following two properties:

            Each row is sorted in non-decreasing order.
            The first integer of each row is greater than the last integer of the previous row.

        Given an integer target, return true if target is in matrix or false otherwise.

        You must write a solution in O(log(m * n)) time complexity.
        */
        public static bool SearchMatrix(int[][] matrix, int target)
        {
            //validations here.

            //1. find row
            int[] PotentialRowFor(int start, int end)
            {
                if (start > end)
                    return null;
                var mid = (start + end) / 2;

                var row = matrix[mid];

                if (row[0] <= target && target <= row[row.Length - 1])
                    return row;

                if (start == end)
                    return null;

                if (target < row[0])
                    return PotentialRowFor(start, mid - 1);
                return PotentialRowFor(mid + 1, end);
            }

            var row = PotentialRowFor(0, matrix.Length - 1);
            if (row == null)
                return false;

            //2. search in row.
            bool BinSearch(int start, int end)
            {
                if (start > end)
                    return false;

                var mid = (start + end) / 2;

                if (target == row[mid])
                    return true;

                if (target > row[mid])
                    return BinSearch(mid + 1, end);
                return BinSearch(start, mid - 1);
            }

            return BinSearch(0, row.Length - 1);
        }

        /*
        853. Car Fleet
        
        https://leetcode.com/problems/car-fleet/description/

        There are n cars at given miles away from the starting mile 0, traveling to reach the mile target.

        You are given two integer array position and speed, both of length n, where position[i] is the starting mile of the ith car and speed[i] is the speed of the ith car in miles per hour.

        A car cannot pass another car, but it can catch up and then travel next to it at the speed of the slower car.

        A car fleet is a car or cars driving next to each other. The speed of the car fleet is the minimum speed of any car in the fleet.

        If a car catches up to a car fleet at the mile target, it will still be considered as part of the car fleet.

        Return the number of car fleets that will arrive at the destination.
        */
        public static int CarFleet(int target, int[] position, int[] speed)
        {
            //validations here.

            //cars travel on a single lane. they cannot pass each other.
            // []______[]__________[]
            //they catch upto each other. similar to next greatest temperature.
            //so using stack.

            var stack = new Stack<(int, int)>();
            var cars = new (int pos, int spd)[position.Length];

            foreach (var (pos, idx) in position.Select((pos, idx) => (pos, idx)))
            {
                cars[idx] = (pos, speed[idx]);
            }

            //sort cars by their position so we can have them in initial position.
            Array.Sort(cars, (a, b) => a.pos.CompareTo(b.pos));

            float TimeFor((int pos, int spd) car)
            {
                return (float)(target - car.pos) / car.spd;
            }

            for (var idx = cars.Length - 1; idx > -1; idx--)
            {
                var car = cars[idx];

                if (stack.Count > 0)
                {
                    var frontCar = stack.Peek();
                    if (TimeFor(frontCar) < TimeFor(car))
                    {
                        stack.Push(car);
                    }
                }
                else
                {
                    stack.Push(car);
                }
            }

            return stack.Count;
        }

        /*
        739. Daily Temperatures
        
        https://leetcode.com/problems/daily-temperatures/description/

        Given an array of integers temperatures represents the daily temperatures, return an array answer such that answer[i] is the number of days you have to wait after the ith day to get a warmer temperature. If there is no future day for which this is possible, keep answer[i] == 0 instead.
        */
        public static int[] DailyTemperatures(int[] temperatures)
        {
            //validations here.

            var answers = new int[temperatures.Length];
            Array.Fill(answers, 0);

            var stack = new Stack<(int, int)>();

            foreach (var (temp, idx) in temperatures.Select((temp, idx) => (temp, idx)))
            {
                while (stack.Count > 0 && temp > stack.Peek().Item1)
                {
                    var values = stack.Pop();

                    answers[values.Item2] = idx - values.Item2;
                }

                stack.Push((temp, idx));
            }

            return answers;
        }

        /*
        496. Next Greater Element I
        
        https://leetcode.com/problems/next-greater-element-i/description/

        The next greater element of some element x in an array is the first greater element that is to the right of x in the same array.

        You are given two distinct 0-indexed integer arrays nums1 and nums2, where nums1 is a subset of nums2.

        For each 0 <= i < nums1.length, find the index j such that nums1[i] == nums2[j] and determine the next greater element of nums2[j] in nums2. If there is no next greater element, then the answer for this query is -1.

        Return an array ans of length nums1.length such that ans[i] is the next greater element as described above.
        */
        public static int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            //validations here.

            var result = new int[nums1.Length];
            Array.Fill(result, -1);

            var idxMap = new Dictionary<int, int>();
            for (var idx = 0; idx < nums1.Length; idx++)
            {
                //able to do this because nums1 has unique elements.
                idxMap.Add(nums1[idx], idx);
            }

            var stack = new Stack<int>();
            foreach (var num in nums2)
            {
                //check if num is next greatest neighbor of stack top
                while (stack.Count > 0 && num > stack.Peek())
                {
                    var value = stack.Pop();

                    result[idxMap[value]] = num;
                }

                if (idxMap.ContainsKey(num))
                    stack.Push(num);
            }

            return result;
        }

        /*
        22. Generate Parentheses
        
        https://leetcode.com/problems/generate-parentheses/description/

        Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
        */
        public static IList<string> GenerateParenthesis(int n)
        {
            //validations here.

            //can only add open parenthesis if open count < n.
            //can only add close parenthesis if close count < open count.
            //must stop when open == close == n.

            IList<string> combinations = new List<string>();
            var stack = new Stack<string>();

            void Backtrack(int open, int close)
            {
                if (open == close && open == n)
                {
                    var combination = string.Empty;

                    combination += string.Join("", stack.Reverse());

                    combinations.Add(combination);
                    return;
                }

                if (open < n)
                {
                    stack.Push("(");
                    Backtrack(open + 1, close);
                    stack.Pop();
                }

                if (close < open)
                {
                    stack.Push(")");
                    Backtrack(open, close + 1);
                    stack.Pop();
                }
            }

            Backtrack(0, 0);

            return combinations;
        }

        /*
        150. Evaluate Reverse Polish Notation
        
        https://leetcode.com/problems/evaluate-reverse-polish-notation/description/

        You are given an array of strings tokens that represents an arithmetic expression in a Reverse Polish Notation.

        Evaluate the expression. Return an integer that represents the value of the expression.

        Note that:

            The valid operators are '+', '-', '*', and '/'.
            Each operand may be an integer or another expression.
            The division between two integers always truncates toward zero.
            There will not be any division by zero.
            The input represents a valid arithmetic expression in a reverse polish notation.
            The answer and all the intermediate calculations can be represented in a 32-bit integer.
        */
        public static int EvalRPN(string[] tokens)
        {
            //validations here.

            var stack = new Stack<int>();
            var operators = new HashSet<string>() { "+", "-", "*", "/" };
            foreach (var token in tokens)
            {
                if (operators.Contains(token))
                {
                    //perform operation and push result to stack.
                    var num2 = stack.Pop();
                    var num1 = stack.Pop();

                    if (token == "+")
                        stack.Push(num1 + num2);
                    else if (token == "-")
                        stack.Push(num1 - num2);
                    else if (token == "*")
                        stack.Push(num1 * num2);
                    else
                        stack.Push(num1 / num2);
                }
                else
                {
                    //push operand to stack.
                    stack.Push(int.Parse(token));
                }
            }

            return stack.Pop();
        }

        /*
        11. Container With Most Water

        https://leetcode.com/problems/container-with-most-water/description/

        You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the ith line are (i, 0) and (i, height[i]).

        Find two lines that together with the x-axis form a container, such that the container contains the most water.

        Return the maximum amount of water a container can store.

        Notice that you may not slant the container.
        */
        public static int MaxArea(int[] height)
        {
            //validations here.

            var l = 0;
            var r = height.Length - 1;
            var maxArea = 0;

            while (l < r)
            {
                var area = Math.Min(height[l], height[r]) * (r - l);

                if (area > maxArea)
                {
                    maxArea = area;
                }

                if (height[l] < height[r])
                {
                    //if height limited by left side then try to increase it.
                    l += 1;
                }
                else
                {
                    r -= 1;
                }
            }

            return maxArea;
        }

        /*
        15. 3Sum
        
        https://leetcode.com/problems/3sum/description/

        Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.

        Notice that the solution set must not contain duplicate triplets.
        */
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            //validations here.

            //find a + b + c = 0;

            //sort array to avoid duplicate triplets
            Array.Sort(nums); // O(nlogn)
            int? prev = null;

            IList<IList<int>> triplets = new List<IList<int>>();
            for (var idx = 0; idx < nums.Length; idx++) // O(n)
            {
                var a = nums[idx];

                if (a == prev)
                    continue;

                var l = idx + 1;
                var r = nums.Length - 1;

                while (l < r)
                { // O(n)
                    var threeSum = a + nums[l] + nums[r];

                    if (threeSum == 0)
                    {
                        //add triplet to result.
                        triplets.Add(new List<int> { a, nums[l], nums[r] });

                        l += 1;
                        //go to next non-duplicate number so that we don't add a duplicate to the list.
                        while (l < r && nums[l] == nums[l - 1])
                        {
                            l += 1;
                        }
                    }
                    else if (threeSum > 0)
                    {
                        r -= 1;
                    }
                    else
                    {
                        l += 1;
                    }
                }

                prev = a;
            }

            return triplets;
        }

        /*
        167. Two Sum II - Input Array Is Sorted
        
        https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/description/

        Given a 1-indexed array of integers numbers that is already sorted in non-decreasing order, find two numbers such that they add up to a specific target number. Let these two numbers be numbers[index1] and numbers[index2] where 1 <= index1 < index2 <= numbers.length.

        Return the indices of the two numbers, index1 and index2, added by one as an integer array [index1, index2] of length 2.

        The tests are generated such that there is exactly one solution. You may not use the same element twice.

        Your solution must use only constant extra space.
        */
        public static int[] TwoSum2(int[] numbers, int target)
        {
            //validations here.

            var l = 0;
            var r = numbers.Length - 1;

            while (l < r)
            {
                var sum = numbers[l] + numbers[r];

                if (sum == target)
                    return new int[] { l + 1, r + 1 };

                if (sum > target)
                    r -= 1;
                else
                    l += 1;
            }

            return new int[] { 0, 0 };
        }

        /*
        128. Longest Consecutive Sequence

        https://leetcode.com/problems/longest-consecutive-sequence/description/

        Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.

        You must write an algorithm that runs in O(n) time.
        */
        public static int LongestConsecutive(int[] nums)
        {
            //validations here.

            var numSet = new HashSet<int>(nums);

            var longestLength = 0;

            foreach (var num in numSet)
            {
                if (numSet.Contains(num - 1))
                    continue;

                var sequenceLength = 1;

                var nextNum = num + 1;
                while (numSet.Contains(nextNum))
                {
                    sequenceLength += 1;
                    nextNum += 1;
                }

                if (sequenceLength > longestLength)
                    longestLength = sequenceLength;
            }

            return longestLength;
        }

        /*
        36. Valid Sudoku

        https://leetcode.com/problems/valid-sudoku/description/

        Determine if a 9 x 9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:

        Each row must contain the digits 1-9 without repetition.
        Each column must contain the digits 1-9 without repetition.
        Each of the nine 3 x 3 sub-boxes of the grid must contain the digits 1-9 without repetition.
        */
        public static bool IsValidSudoku(char[][] board)
        {
            //validations here.

            //row and col validation
            for (var idx = 0; idx < 9; idx++)
            {
                var seenDigits = new HashSet<char>();

                foreach (var digit in board[idx])
                {
                    if (digit == '.')
                        continue;
                    if (seenDigits.Contains(digit))
                        return false;

                    seenDigits.Add(digit);
                }

                seenDigits.Clear();

                for (var colIdx = 0; colIdx < 9; colIdx++)
                {
                    var digit = board[colIdx][idx];
                    if (digit == '.')
                        continue;
                    if (seenDigits.Contains(digit))
                        return false;

                    seenDigits.Add(digit);
                }
            }

            //3x3 validation
            var ranges = new HashSet<(int, int)> { (0, 3), (3, 6), (6, 9) };

            foreach (var range in ranges)
            {
                var grid1Digits = new HashSet<int>();
                var grid2Digits = new HashSet<int>();
                var grid3Digits = new HashSet<int>();
                for (var rowIdx = range.Item1; rowIdx < range.Item2; rowIdx++)
                {
                    for (var colIdx = 0; colIdx < 9; colIdx++)
                    {
                        var digit = board[rowIdx][colIdx];
                        if (digit == '.')
                            continue;

                        if (colIdx < 3)
                        {
                            if (grid1Digits.Contains(digit))
                                return false;

                            grid1Digits.Add(digit);
                        }
                        else if (colIdx < 6)
                        {
                            if (grid2Digits.Contains(digit))
                                return false;

                            grid2Digits.Add(digit);
                        }
                        else
                        {
                            if (grid3Digits.Contains(digit))
                                return false;

                            grid3Digits.Add(digit);
                        }
                    }
                }
            }

            return true;
        }

        /*
        347. Top K Frequent Elements

        https://leetcode.com/problems/top-k-frequent-elements/description/
        
        Given an integer array nums and an integer k, return the k most frequent elements. You may return the answer in any order.
        */
        public static int[] TopKFrequent(int[] nums, int k)
        {
            //validations here.

            var numFreqMap = new Dictionary<int, int>();
            var maxNumFreq = 0;

            //find freq of each num and also the max frequency.
            foreach (var num in nums)
            {
                if (numFreqMap.ContainsKey(num))
                {
                    numFreqMap[num] += 1;
                }
                else
                {
                    numFreqMap.Add(num, 1);
                }

                if (numFreqMap[num] > maxNumFreq)
                {
                    maxNumFreq = numFreqMap[num];
                }
            }

            var freqNumMap = new Dictionary<int, List<int>>();

            foreach (var kv in numFreqMap)
            {
                if (freqNumMap.ContainsKey(kv.Value))
                {
                    freqNumMap[kv.Value].Add(kv.Key);
                }
                else
                {
                    freqNumMap.Add(kv.Value, new List<int> { kv.Key });
                }
            }

            var mostFrequentItems = new List<int>();

            var itemCount = 0;
            var freq = maxNumFreq;
            while (itemCount < k)
            {
                if (freqNumMap.ContainsKey(freq))
                {
                    foreach (var num in freqNumMap[freq])
                    {
                        mostFrequentItems.Add(num);
                        itemCount++;
                    }
                }
                freq--;
            }

            return mostFrequentItems.ToArray();
        }

        /*
        49. Group Anagrams

        https://leetcode.com/problems/group-anagrams/description/

        Given an array of strings strs, group the
        anagrams together. You can return the answer in any order.
        */
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            //validations here.

            static string SortedStringFor(string input)
            {
                var chars = input.ToCharArray();
                Array.Sort(chars);
                return new string(chars);
            }

            IList<IList<string>> groups = new List<IList<string>>();
            var anagramMap = new Dictionary<string, IList<string>>();

            foreach (var str in strs) // O(m) operation where m = length of input array.
            {
                var sortedString = SortedStringFor(str); //nlogn operation where n = length of string.

                if (anagramMap.ContainsKey(sortedString))
                {
                    anagramMap[sortedString].Add(str);
                }
                else
                {
                    anagramMap.Add(sortedString, new List<string> { str });
                }
            }

            foreach (var kv in anagramMap)
            {
                groups.Add(kv.Value);
            }

            return groups;
        }

        /*
        125. Valid Palindrome

        A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.

        Given a string s, return true if it is a palindrome, or false otherwise.
        */
        public static bool IsPalindrome(string s)
        {
            if (s == null || s.Length <= 1)
                return true;

            var l = 0;
            var r = s.Length - 1;

            while (l < r)
            {
                if (!char.IsLetterOrDigit(s[l]))
                {
                    l++;
                    continue;
                }

                if (!char.IsLetterOrDigit(s[r]))
                {
                    r--;
                    continue;
                }

                if (char.ToLower(s[l]) != char.ToLower(s[r]))
                    return false;

                l++;
                r--;
            }

            return true;
        }

        /*
        80. Remove Duplicates from Sorted Array II
        https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/

        Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place such that each unique element appears at most twice. The relative order of the elements should be kept the same.

        Since it is impossible to change the length of the array in some languages, you must instead have the result be placed in the first part of the array nums. More formally, if there are k elements after removing the duplicates, then the first k elements of nums should hold the final result. It does not matter what you leave beyond the first k elements.

        Return k after placing the final result in the first k slots of nums.

        Do not allocate extra space for another array. You must do this by modifying the input array in-place with O(1) extra memory.

        Custom Judge:

        The judge will test your solution with the following code:

        int[] nums = [...]; // Input array
        int[] expectedNums = [...]; // The expected answer with correct length

        int k = removeDuplicates(nums); // Calls your implementation

        assert k == expectedNums.length;
        for (int i = 0; i < k; i++) {
            assert nums[i] == expectedNums[i];
        }

        If all assertions pass, then your solution will be accepted.

        Example 1:

        Input: nums = [1,1,1,2,2,3]
        Output: 5, nums = [1,1,2,2,3,_]
        Explanation: Your function should return k = 5, with the first five elements of nums being 1, 1, 2, 2 and 3 respectively.
        It does not matter what you leave beyond the returned k (hence they are underscores).

        Example 2:

        Input: nums = [0,0,1,1,1,1,2,3,3]
        Output: 7, nums = [0,0,1,1,2,3,3,_,_]
        Explanation: Your function should return k = 7, with the first seven elements of nums being 0, 0, 1, 1, 2, 3 and 3 respectively.
        It does not matter what you leave beyond the returned k (hence they are underscores).
        
        Constraints:

            1 <= nums.length <= 3 * 104
            -104 <= nums[i] <= 104
            nums is sorted in non-decreasing order.
        */
        public static int RemoveDuplicates(int[] nums)
        {
            if (nums.Length <= 2)
                return nums.Length;

            var prev = nums[0];
            var replacePtr = 2;
            var seenCount = 1;

            var answer = 1;

            for (var idx = 1; idx < nums.Length; idx++)
            {
                var current = nums[idx];

                if (current == prev)
                {
                    seenCount++;

                    if (seenCount == 3)
                    {
                        replacePtr = idx;
                        answer += 2;
                    }
                }
                else if (seenCount <= 2)
                {
                    prev = current;
                    seenCount = 1;
                    answer += 2;
                }
                else
                {
                    nums[replacePtr] = current;
                    prev = current;
                    replacePtr++;
                    seenCount = 1;
                    answer += 1;
                }
            }
            Console.WriteLine(string.Join(',', nums));
            return replacePtr;
            // return answer;
        }

        //13. Roman to Integer
        /*
        Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.

        Symbol       Value
        I             1
        V             5
        X             10
        L             50
        C             100
        D             500
        M             1000

        For example, 2 is written as II in Roman numeral, just two ones added together. 12 is written as XII, which is simply X + II. The number 27 is written as XXVII, which is XX + V + II.

        Roman numerals are usually written largest to smallest from left to right. However, the numeral for four is not IIII. Instead, the number four is written as IV. Because the one is before the five we subtract it making four. The same principle applies to the number nine, which is written as IX. There are six instances where subtraction is used:

            I can be placed before V (5) and X (10) to make 4 and 9.
            X can be placed before L (50) and C (100) to make 40 and 90.
            C can be placed before D (500) and M (1000) to make 400 and 900.

        Given a roman numeral, convert it to an integer.
        */
        public static int RomanToInt(string s)
        {
            //do normal validations here.

            var map = new Dictionary<string, int>
            {
                { "I", 1 },
                { "V", 5 },
                { "IV", 4 },
                { "X", 10 },
                { "IX", 9 },
                { "L", 50 },
                { "XL", 40 },
                { "C", 100 },
                { "XC", 90 },
                { "D", 500 },
                { "CD", 400 },
                { "M", 1000 },
                { "CM", 900 },
            };
            var prev = char.MinValue;
            var num = 0;
            var subs = new HashSet<char> { 'I', 'X', 'C' };
            foreach (char current in s)
            {
                if (map.ContainsKey(char.ToString(prev) + char.ToString(current)))
                {
                    num =
                        num
                        - map[char.ToString(prev)]
                        + map[char.ToString(prev) + char.ToString(current)];
                }
                else
                {
                    num += map[char.ToString(current)];
                }

                prev = current;
            }

            return num;
        }

        //9. Palindrome Number
        //Given an integer x, return true if x is a palindrome, and false otherwise. Could you solve it without converting the integer to a string?
        public static bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;

            if (x < 10)
                return true;

            var digits = new List<int> { };

            var quotient = x / 10;
            var remainder = x % 10;
            digits.Add(remainder);
            while (quotient >= 10)
            {
                remainder = quotient % 10;
                digits.Add(remainder);

                quotient /= 10;
            }

            digits.Add(quotient);

            int reverseNum = 0;
            for (var idx = digits.Count - 1; idx >= 0; idx--)
            {
                reverseNum +=
                    (Convert.ToInt32(Math.Pow(10, digits.Count - (idx + 1)))) * digits[idx];
            }

            return reverseNum == x;
        }

        //43. Multiply Strings
        //Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and num2, also represented as a string.
        //Note: You must not use any built-in BigInteger library or convert the inputs to integer directly.
        public static string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0")
                return "0";

            //stores digits of the multiplication result
            var result = new int[num1.Length + num2.Length];

            for (var idx_1 = num1.Length - 1; idx_1 >= 0; idx_1--)
            {
                var digit_1 = (int)char.GetNumericValue(num1[idx_1]);

                for (var idx_2 = num2.Length - 1; idx_2 >= 0; idx_2--)
                {
                    var digit_2 = (int)char.GetNumericValue(num2[idx_2]);
                    var intermediateResult = digit_1 * digit_2;

                    result[idx_1 + idx_2 + 1] += intermediateResult;
                    result[idx_1 + idx_2] += result[idx_1 + idx_2 + 1] / 10;
                    result[idx_1 + idx_2 + 1] = result[idx_1 + idx_2 + 1] % 10;
                }
            }

            return string.Join("", result.SkipWhile(d => d == 0));
        }

        //14. Longest Common Prefix
        //Write a function to find the longest common prefix string amongst an array of strings.
        //If there is no common prefix, return an empty string "".
        public static string LongestCommonPrefix(string[] strs)
        {
            //validations
            if (strs == null || strs.Length == 0)
                return string.Empty;
            if (strs.Length == 1)
                return strs[0];

            var longestCommonPrefix = strs[0];

            for (var idx = 1; idx < strs.Length; idx++)
            {
                var word = strs[idx];

                //if (word.Length < longestCommonPrefix.Length)
                //    longestCommonPrefix = word;

                var builder = new StringBuilder();
                for (
                    var idx_char = 0;
                    idx_char < Math.Min(longestCommonPrefix.Length, word.Length);
                    idx_char++
                )
                {
                    if (longestCommonPrefix[idx_char] == word[idx_char])
                        builder.Append(longestCommonPrefix[idx_char]);
                    else
                        break;
                }
                if (builder.Length == 0)
                    return string.Empty;
                else
                    longestCommonPrefix = builder.ToString();
            }

            return longestCommonPrefix;
        }

        //54. Spiral Matrix
        //Given an m x n matrix, return all elements of the matrix in spiral order.
        public static IList<int> SpiralOrder(int[][] matrix)
        {
            //validations

            var spiralOrder = new List<int>();

            //setup high and low limit of matrix traversal
            var colLowLimit = 0;
            var colHighLimit = matrix[0].Length - 1;
            var rowLowLimit = 0;
            var rowHighLimit = matrix.Length - 1;

            static void doSpiral(
                int rowStart,
                int colStart,
                int colLowLimit,
                int colHighLimit,
                int rowLowLimit,
                int rowHighLimit,
                int[][] matrix,
                List<int> spiralOrder
            )
            {
                if (rowLowLimit > rowHighLimit || colLowLimit > colHighLimit)
                    return;

                int col;
                for (col = colStart; col <= colHighLimit; col++)
                {
                    spiralOrder.Add(matrix[rowStart][col]);
                }

                int row;
                for (row = rowStart + 1; row <= rowHighLimit; row++)
                {
                    spiralOrder.Add(matrix[row][colHighLimit]);
                }

                for (
                    col = colHighLimit - 1;
                    col >= colLowLimit && rowLowLimit != rowHighLimit;
                    col--
                )
                {
                    spiralOrder.Add(matrix[rowHighLimit][col]);
                }

                for (
                    row = rowHighLimit - 1;
                    row > rowLowLimit && colLowLimit != colHighLimit;
                    row--
                )
                {
                    spiralOrder.Add(matrix[row][colStart]);
                }

                doSpiral(
                    rowStart + 1,
                    colStart + 1,
                    colLowLimit + 1,
                    colHighLimit - 1,
                    rowLowLimit + 1,
                    rowHighLimit - 1,
                    matrix,
                    spiralOrder
                );
            }

            doSpiral(
                0,
                0,
                colLowLimit,
                colHighLimit,
                rowLowLimit,
                rowHighLimit,
                matrix,
                spiralOrder
            );

            return spiralOrder;
        }

        //202. Happy Number
        //Write an algorithm to determine if a number n is happy.
        //A happy number is a number defined by the following process:
        //  Starting with any positive integer, replace the number by the sum of the squares of its digits.
        //  Repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
        //  Those numbers for which this process ends in 1 are happy.
        //Return true if n is a happy number, and false if not.
        public static bool IsHappy(int n)
        {
            //validations

            var seenNums = new HashSet<int>();

            static int sumOfSquares(int num)
            {
                var sum = 0;
                var numString = num.ToString();
                foreach (var chr in numString)
                {
                    var digit = (int)char.GetNumericValue(chr);
                    sum += digit * digit;
                }

                return sum;
            }

            while (true)
            {
                if (seenNums.Contains(n))
                    return false;

                if (n == 1)
                    return true;

                seenNums.Add(n);

                n = sumOfSquares(n);
            }
        }

        //A palindromic number reads the same both ways.
        //The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
        //Find the largest palindrome made from the product of two 3-digit numbers.
        //580085
        public static string LargestPalindrome()
        {
            //var num1 = 999;

            string ReverseString(string input)
            {
                var builder = new StringBuilder();

                for (var idx = input.Length - 1; idx > -1; idx--)
                {
                    builder.Append(input[idx]);
                }

                return builder.ToString();
            }

            var maxPalindrome = int.MinValue;

            for (var num2 = 999; num2 >= 100; num2--)
            {
                for (var num1 = 999; num1 >= 100; num1--)
                {
                    var product = num1 * num2;
                    var productString = product.ToString();
                    var reverseProduct = ReverseString(productString);

                    if (productString == reverseProduct)
                    {
                        if (product > maxPalindrome)
                            maxPalindrome = product;
                    }
                }
            }

            return maxPalindrome.ToString();
        }

        //394. Decode String
        //Given an encoded string, return its decoded string. The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times.Note that k is guaranteed to be a positive integer.
        //You may assume that the input string is always valid; there are no extra white spaces, square brackets are well-formed, etc.Furthermore, you may assume that the original data does not contain any digits and that digits are only for those repeat numbers, k.For example, there will not be input like 3a or 2[4].
        //The test cases are generated so that the length of the output will never exceed 105.
        public static string DecodeString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            var parserStack = new Stack<string>();
            var resultBuilder = new StringBuilder();

            foreach (var chr in s)
            {
                if (chr != ']')
                    parserStack.Push(chr.ToString());
                else
                {
                    var repeatedBuilder = new StringBuilder();
                    var numString = new StringBuilder();

                    var shouldStop = parserStack.Count == 0;

                    var foundRepeatedString = false;

                    while (!shouldStop)
                    {
                        var parsedString = parserStack.Pop();

                        //already calculated repeated substring.
                        if (parsedString.Length > 1)
                            repeatedBuilder.Insert(0, parsedString);
                        else
                        {
                            var repeatedChr = parsedString[0];

                            if (char.IsLetter(repeatedChr))
                                repeatedBuilder.Insert(0, repeatedChr);
                            else if (repeatedChr == '[')
                                foundRepeatedString = true;
                            else
                                numString.Insert(0, repeatedChr);
                        }

                        shouldStop =
                            parserStack.Count == 0
                            || (
                                foundRepeatedString
                                && parserStack.TryPeek(out var top)
                                && (char.IsLetter(top[0]) || top[0] == '[')
                            );
                    }

                    var part = repeatedBuilder.ToString();
                    repeatedBuilder.Clear();
                    for (var repeat = 1; repeat <= Convert.ToInt32(numString.ToString()); repeat++)
                    {
                        repeatedBuilder.Append(part);
                    }

                    if (parserStack.Count == 0)
                        resultBuilder.Append(repeatedBuilder.ToString());
                    else
                        parserStack.Push(repeatedBuilder.ToString());
                }
            }

            var remainingStr = string.Empty;
            while (parserStack.Count != 0)
            {
                remainingStr = parserStack.Pop() + remainingStr;
            }

            resultBuilder.Append(remainingStr);

            return resultBuilder.ToString();
        }

        //299. Bulls and Cows
        //You are playing the Bulls and Cows game with your friend.
        //You write down a secret number and ask your friend to guess what the number is. When your friend makes a guess, you provide a hint with the following info:
        //  The number of "bulls", which are digits in the guess that are in the correct position.
        //  The number of "cows", which are digits in the guess that are in your secret number but are located in the wrong position.Specifically, the non-bull digits in the guess that could be rearranged such that they become bulls.
        //Given the secret number secret and your friend's guess guess, return the hint for your friend's guess.
        //The hint should be formatted as "xAyB", where x is the number of bulls and y is the number of cows. Note that both secret and guess may contain duplicate digits.
        public static string GetHint(string secret, string guess)
        {
            if (secret == null || guess == null || secret.Length != guess.Length)
                return string.Empty;

            var nonBullChars = new Dictionary<char, int>();
            var bulls = 0;
            var cows = 0;
            var potentialCowChars = new List<char>();

            for (var idx = 0; idx < secret.Length; idx++)
            {
                var sChar = secret[idx];
                var gChar = guess[idx];

                if (gChar == sChar)
                    bulls++;
                else
                {
                    potentialCowChars.Add(gChar);

                    if (nonBullChars.ContainsKey(sChar))
                        nonBullChars[sChar]++;
                    else
                        nonBullChars[sChar] = 1;
                }
            }

            foreach (var chr in potentialCowChars)
            {
                if (nonBullChars.ContainsKey(chr))
                {
                    cows++;
                    nonBullChars[chr]--;
                    if (nonBullChars[chr] == 0)
                        nonBullChars.Remove(chr);
                }
            }

            return bulls.ToString() + "A" + cows.ToString() + "B";
        }

        //play songs randomly w/o repeating already played songs. once all songs played, start playing songs from beginning with a new random order.
        public static void PlaySongs(int[] songs)
        {
            if (songs == null)
                return;

            var repeatSongPtr = songs.Length - 1;

            var randomIdx = 5;
            Console.WriteLine($"Playing song: {randomIdx}");

            //(songs[])
        }

        //[1,2,5,6,7,3,8,0,9]
        public static int MaxSum(int[] input)
        {
            if (input == null)
                return int.MinValue;

            var maxSum = int.MinValue;

            var start = 0;
            var currentSum = 0;
            for (var end = 0; end < input.Length; end++)
            {
                currentSum += input[end];

                if (end >= 3)
                {
                    currentSum -= input[start];
                    start++;
                }

                maxSum = Math.Max(maxSum, currentSum);
            }

            return maxSum;
        }

        //678. Valid Parenthesis String
        //Given a string s containing only three types of characters: '(', ')' and '*', return true if s is valid. The following rules define a valid string:
        //  Any left parenthesis '(' must have a corresponding right parenthesis ')'.
        //  Any right parenthesis ')' must have a corresponding left parenthesis '('.
        //  Left parenthesis '(' must go before the corresponding right parenthesis ')'.
        //  '*' could be treated as a single right parenthesis ')' or a single left parenthesis '(' or an empty string "".
        public static bool CheckValidString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            if (s.Length == 1 && s == "*")
                return true;

            var chars = new Stack<char>();
            var starReplacements = 0;
            foreach (var chr in s)
            {
                if (chr != ')')
                    chars.Push(chr);
                else
                {
                    if (chars.Count == 0)
                        return false;

                    var matchingChr = chars.Pop();
                    if (matchingChr == '*')
                        starReplacements++;
                }
            }

            return chars.Count == 0 || starReplacements == chars.Count;
        }

        //71. Simplify Path
        //Given a string path, which is an absolute path (starting with a slash '/') to a file or directory in a Unix-style file system, convert it to the simplified canonical path.
        //In a Unix-style file system, a period '.' refers to the current directory, a double period '..' refers to the directory up a level, and any multiple consecutive slashes(i.e. '//') are treated as a single slash '/'. For this problem, any other format of periods such as '...' are treated as file/directory names.
        //The canonical path should have the following format:
        //  The path starts with a single slash '/'.
        //  Any two directories are separated by a single slash '/'.
        //  The path does not end with a trailing '/'.
        //  The path only contains the directories on the path from the root directory to the target file or directory (i.e., no period '.' or double period '..')
        //Return the simplified canonical path.
        public static string SimplifyPath(string path)
        {
            var directories = path.Split('/');

            var items = new Stack<string>();

            foreach (var dir in directories)
            {
                if (dir == ".")
                    continue;

                if (dir == "..")
                {
                    if (items.Count > 0)
                        items.Pop();
                }
                else if (dir != string.Empty)
                    items.Push(dir);
            }

            if (items.Count > 0)
            {
                var canonicalPath = new StringBuilder();
                while (items.Count > 0)
                {
                    var itemPath = items.Pop();
                    canonicalPath.Insert(0, "/" + itemPath);
                }

                return canonicalPath.ToString();
            }

            return "/";
        }

        //844. Backspace String Compare
        //Given two strings s and t, return true if they are equal when both are typed into empty text editors. '#' means a backspace character.
        //Note that after backspacing an empty text, the text will continue empty.
        public static bool BackspaceCompare(string s, string t)
        {
            //validations here.

            var actualSString = ActualStringOf(s);
            var actualTString = ActualStringOf(t);

            return actualSString == actualTString;

            static string ActualStringOf(string s)
            {
                var builder = new StringBuilder();
                foreach (var chr in s)
                {
                    if (chr != '#')
                        builder.Append(chr);
                    else if (builder.Length > 0)
                        builder.Remove(builder.Length - 1, 1);
                }

                return builder.ToString();
            }
        }

        //424. Longest Repeating Character Replacement
        //You are given a string s and an integer k. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most k times.
        //Return the length of the longest substring containing the same letter you can get after performing the above operations.
        public static int CharacterReplacementNotWorking(string s, int k)
        {
            var maxLength = int.MinValue;

            var chars = s.ToHashSet();

            foreach (var chr in chars)
            {
                //windowSize = end - start + 1. we can make replacement for windowSize <= k.
                var start = 0;
                var currentLength = 0;
                var charCount = 0;
                for (var end = 0; end < s.Length; end++)
                {
                    var currentChar = s[end];

                    if (currentChar == chr)
                        charCount++;
                    var windowSize = end - start + 1;
                    while (windowSize - charCount > k)
                    {
                        var outChar = s[start];

                        if (outChar == chr)
                            charCount--;
                        start++;
                        windowSize = end - start + 1;
                    }

                    maxLength = Math.Max(maxLength, windowSize);
                }
            }

            return maxLength;
        }

        //643. Maximum Average Subarray I
        //You are given an integer array nums consisting of n elements, and an integer k.
        //Find a contiguous subarray whose length is equal to k that has the maximum average value and return this value.Any answer with a calculation error less than 10-5 will be accepted.
        public static double FindMaxAverage(int[] nums, int k)
        {
            var maxAverage = double.MinValue;

            var currentSum = 0;

            var start = 0;
            for (var end = 0; end < nums.Length; end++)
            {
                currentSum += nums[end];

                if (end >= k - 1)
                {
                    var avg = (double)currentSum / k;
                    maxAverage = Math.Max(maxAverage, avg);

                    currentSum -= nums[start];
                    start++;
                }
            }

            return maxAverage;
        }

        //2379. Minimum Recolors to Get K Consecutive Black Blocks
        //You are given a 0-indexed string blocks of length n, where blocks[i] is either 'W' or 'B', representing the color of the ith block. The characters 'W' and 'B' denote the colors white and black, respectively.
        //You are also given an integer k, which is the desired number of consecutive black blocks. In one operation, you can recolor a white block such that it becomes a black block.
        //Return the minimum number of operations needed such that there is at least one occurrence of k consecutive black blocks.
        public static int MinimumRecolors(string blocks, int k)
        {
            var minOperations = int.MaxValue;

            var currentOperations = 0;
            var start = 0;
            for (var end = 0; end < blocks.Length; end++)
            {
                var block = blocks[end];

                if (block == 'W')
                    currentOperations++;

                if (end >= k - 1)
                {
                    minOperations = Math.Min(minOperations, currentOperations);

                    if (blocks[start] == 'W')
                        currentOperations--;
                    start++;
                }
            }

            return minOperations;
        }

        //2269. Find the K-Beauty of a Number
        //The k-beauty of an integer num is defined as the number of substrings of num when it is read as a string that meet the following conditions:
        //  It has a length of k.
        //  It is a divisor of num.
        //Given integers num and k, return the k-beauty of num. Note:
        //  Leading zeros are allowed.
        //  0 is not a divisor of any value.
        //A substring is a contiguous sequence of characters in a string.
        public static int DivisorSubstrings(int num, int k)
        {
            var stringNum = num.ToString();

            var kBeauty = 0;
            var subStrNum = new StringBuilder();
            for (var windowEnd = 0; windowEnd < stringNum.Length; windowEnd++)
            {
                subStrNum.Append(stringNum[windowEnd]);

                if (windowEnd >= k - 1)
                {
                    var subNum = Convert.ToInt32(subStrNum.ToString());
                    if (subNum != 0 && num % subNum == 0)
                        kBeauty++;
                    subStrNum.Remove(0, 1);
                }
            }

            return kBeauty;
        }

        //1763. Longest Nice Substring
        //A string s is nice if, for every letter of the alphabet that s contains, it appears both in uppercase and lowercase. For example, "abABB" is nice because 'A' and 'a' appear, and 'B' and 'b' appear. However, "abA" is not because 'b' appears, but 'B' does not.
        //Given a string s, return the longest substring of s that is nice.If there are multiple, return the substring of the earliest occurrence.If there are none, return an empty string.
        //Haven't done with sliding window yet :|
        public static string LongestNiceSubstring(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 2)
                return string.Empty;

            var longestNiceString = string.Empty;

            return longestNiceString;
        }

        //Longest substring with at most 2 distinct characters.
        //Given a string S, find the length of the longest substring T that contains at most 2 distinct characters
        //For example, Given S = "eceba", T is "ece" which it's length is 3.
        public static int LongestSubstring(string s)
        {
            var longestLength = int.MinValue;
            var currentChars = new Dictionary<char, int>();
            var currentLength = 0;

            var windowStart = 0;
            for (var windowEnd = 0; windowEnd < s.Length; windowEnd++)
            {
                var chr = s[windowEnd];
                if (currentChars.ContainsKey(chr))
                {
                    currentChars[chr]++;
                }
                else
                    currentChars[chr] = 1;

                currentLength++;

                while (currentChars.Count > 2)
                {
                    var startChr = s[windowStart];

                    currentChars[startChr]--;

                    if (currentChars[startChr] < 1)
                        currentChars.Remove(startChr);

                    windowStart++;
                    currentLength--;
                }

                longestLength = Math.Max(longestLength, currentLength);
            }

            return longestLength;
        }

        //1876. Substrings of Size Three with Distinct Characters
        //A string is good if there are no repeated characters.
        //Given a string s​​​​​, return the number of good substrings of length three in s​​​​​​. Note that if there are multiple occurrences of the same substring, every occurrence should be counted.
        //A substring is a contiguous sequence of characters in a string.
        public static int CountGoodSubstrings(string s)
        {
            var result = 0;

            if (string.IsNullOrEmpty(s) || s.Length <= 2)
                return result;

            for (var start = 0; start < s.Length - 2; start++)
            {
                var seen = new HashSet<char>();

                for (var idx = start; idx < start + 3; idx++)
                {
                    seen.Add(s[idx]);
                }

                if (seen.Count == 3)
                    result++;
                seen.Remove(s[start]);
            }

            return result;
        }

        //438. Find All Anagrams in a String
        //Given two strings s and p, return an array of all the start indices of p's anagrams in s. You may return the answer in any order.
        //An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase, typically using all the original letters exactly once.
        public static IList<int> FindAnagrams(string s, string p)
        {
            var indices = new List<int>();
            if (s == null || s.Length == 0 || p.Length > s.Length)
                return indices;

            var chars = new Dictionary<char, int>();
            foreach (var chr in p)
            {
                if (chars.ContainsKey(chr))
                    chars[chr]++;
                else
                    chars[chr] = 1;
            }

            var visited = new Dictionary<char, int>();
            //calculate hashmap of first substring.
            for (var idx = 0; idx < p.Length; idx++)
            {
                var chr = s[idx];

                if (visited.ContainsKey(chr))
                    visited[chr]++;
                else
                    visited[chr] = 1;
            }

            for (var idx = 0; idx < s.Length && (s.Length - (idx + 1) >= p.Length); idx++)
            {
                var track = p.Length + idx - 1;
                var found = true;
                foreach (var (key, value) in chars)
                {
                    if (!visited.ContainsKey(key) || visited[key] != value)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    indices.Add(idx);
                }
                visited[s[idx]]--;

                if (visited.ContainsKey(s[track]))
                    visited[s[track]]++;
                else
                    visited[s[track]] = 1;
            }

            return indices;
        }

        //746. Min Cost Climbing Stairs
        //You are given an integer array cost where cost[i] is the cost of ith step on a staircase. Once you pay the cost, you can either climb one or two steps.
        //You can either start from the step with index 0, or the step with index 1.
        //Return the minimum cost to reach the top of the floor.
        public static int MinCostClimbingStairs(int[] cost)
        {
            if (cost == null || cost.Length == 0)
                return 0;

            var memo = new Dictionary<int, int>();
            int minCost(int step)
            {
                if (memo.TryGetValue(step, out var result))
                    return result;

                memo[step] =
                    cost[step]
                    + Math.Min(
                        step + 1 < cost.Length ? minCost(step + 1) : 0,
                        step + 2 < cost.Length ? minCost(step + 2) : 0
                    );

                return memo[step];
            }

            return Math.Min(minCost(0), minCost(1));
        }

        //70. Climbing Stairs
        //You are climbing a staircase. It takes n steps to reach the top.
        //Each time you can either climb 1 or 2 steps.In how many distinct ways can you climb to the top?
        public static int ClimbStairs(int n)
        {
            if (n <= 0)
                return 0;

            var memo = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };

            int WaysToClimb(int steps)
            {
                if (memo.TryGetValue(steps, out int ways))
                    return ways;
                else
                {
                    //if i take 1 step then ways to climb
                    memo[steps] = WaysToClimb(steps - 1) + WaysToClimb(steps - 2);
                    return memo[steps];
                }
            }

            return WaysToClimb(n);
        }

        //509. Fibonacci Number
        //The Fibonacci numbers, commonly denoted F(n) form a sequence, called the Fibonacci sequence, such that each number is the sum of the two preceding ones, starting from 0 and 1.
        //That is, F(0) = 0, F(1) = 1, F(n) = F(n - 1) + F(n - 2), for n > 1.
        //Given n, calculate F(n).
        public static int Fib(int n)
        {
            // uses DP.

            if (n < 0)
                return -1;

            var fibs = new Dictionary<int, int> { { 0, 0 }, { 1, 1 } };

            int fib(int num)
            {
                if (fibs.TryGetValue(num, out int result))
                    return result;
                else
                {
                    fibs[num] = fib(num - 1) + fib(num - 2);
                    return fibs[num];
                }
            }

            return fib(n);
        }

        //733. Flood Fill
        //An image is represented by an m x n integer grid image where image[i][j] represents the pixel value of the image.
        //You are also given three integers sr, sc, and color.You should perform a flood fill on the image starting from the pixel image[sr][sc].
        //To perform a flood fill, consider the starting pixel, plus any pixels connected 4-directionally to the starting pixel of the same color as the starting pixel, plus any pixels connected 4-directionally to those pixels (also with the same color), and so on.Replace the color of all of the aforementioned pixels with color. Return the modified image after performing the flood fill.
        public static int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            var columnLength = image[0].Length;
            var rowLength = image.Length;
            var originalColor = image[sr][sc];

            var pixels = new Queue<(int, int)>();
            pixels.Enqueue((sr, sc));

            while (pixels.Count > 0)
            {
                var (r, c) = pixels.Dequeue();

                if (image[r][c] == originalColor && image[r][c] != color)
                    image[r][c] = color;
                else
                    continue;
                if (r + 1 < rowLength)
                    pixels.Enqueue((r + 1, c));

                if (r - 1 >= 0)
                    pixels.Enqueue((r - 1, c));

                if (c + 1 < columnLength)
                    pixels.Enqueue((r, c + 1));

                if (c - 1 >= 0)
                    pixels.Enqueue((r, c - 1));
            }

            return image;
        }

        //235. Lowest Common Ancestor of a Binary Search Tree
        //Given a binary search tree (BST), find the lowest common ancestor (LCA) node of two given nodes in the BST.
        //According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two nodes p and q as the lowest node in T that has both p and q as descendants(where we allow a node to be a descendant of itself).”
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode lca(TreeNode node)
            {
                if (node == null || p == null || q == null)
                    return null;

                // p will always be less than q.
                if (p.val > q.val)
                {
                    (q, p) = (p, q);
                }

                if (p.val <= node.val && node.val <= q.val)
                    return node;

                if (p.val > node.val)
                    return lca(node.right);

                return lca(node.left);
            }

            return lca(root);
        }

        //98. Validate Binary Search Tree
        //Given the root of a binary tree, determine if it is a valid binary search tree (BST).
        //A valid BST is defined as follows:
        //The left subtree of a node contains only nodes with keys less than the node's key.
        //The right subtree of a node contains only nodes with keys greater than the node's key.
        //Both the left and right subtrees must also be binary search trees.
        public static bool IsValidBST(TreeNode root)
        {
            //tuple format -> (tells whether tree is valid, min value in tree, max value in tree)
            static (bool, int, int) minMaxAndValidation(TreeNode node)
            {
                //base case.
                if (node.right == null && node.left == null)
                    return (true, node.val, node.val);

                //recursive case.
                //find the max and min for left and right subtree
                //the current node must be between max of left and min of right.
                (bool, int, int)? left;
                (bool, int, int)? right;

                left = node.left == null ? null : minMaxAndValidation(node.left);
                right = node.right == null ? null : minMaxAndValidation(node.right);

                var isValidSubtree =
                    (left.HasValue ? left.Value.Item1 && left.Value.Item3 < node.val : true)
                    && (right.HasValue ? node.val < right.Value.Item2 && right.Value.Item1 : true);

                var minMax = new List<int> { node.val };
                if (left.HasValue)
                {
                    minMax.Add(left.Value.Item2);
                    minMax.Add(left.Value.Item3);
                }

                if (right.HasValue)
                {
                    minMax.Add(right.Value.Item2);
                    minMax.Add(right.Value.Item3);
                }

                return (isValidSubtree, minMax.Min(), minMax.Max());
            }
            return minMaxAndValidation(root).Item1;
        }

        //102. Binary Tree Level Order Traversal
        //Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e., from left to right, level by level).
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            var levelOrder = new List<IList<int>>();

            if (root == null)
                return levelOrder;

            var levels = new Queue<IList<TreeNode>>();
            levels.Enqueue(new List<TreeNode> { root });

            while (levels.Count > 0)
            {
                var level = levels.Dequeue();
                var levelList = new List<int>();

                var newLevel = new List<TreeNode>();

                foreach (var node in level)
                {
                    if (node == null)
                        continue;

                    levelList.Add(node.val);

                    newLevel.Add(node.left);
                    newLevel.Add(node.right);
                }

                if (newLevel.Count > 0)
                    levels.Enqueue(newLevel);

                if (levelList.Count > 0)
                    levelOrder.Add(levelList);
            }

            return levelOrder;
        }

        //589. N-ary Tree Preorder Traversal
        //Given the root of an n-ary tree, return the preorder traversal of its nodes' values.
        //Nary-Tree input serialization is represented in their level order traversal.Each group of children is separated by the null value. See example:
        //Input: root = [1,null,3,2,4,null,5,6]
        //Output: [1,3,5,6,2,4]
        public static IList<int> PreorderIterative(Node root)
        {
            var preOrderList = new List<int>();

            if (root == null)
                return preOrderList;

            var nodes = new Stack<Node>();
            nodes.Push(root);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Pop();
                preOrderList.Add(currentNode.val);

                for (var idx = currentNode.children.Count - 1; idx >= 0; idx--)
                {
                    var child = currentNode.children[idx];
                    nodes.Push(child);
                }
            }

            return preOrderList;
        }

        //589. N-ary Tree Preorder Traversal
        //Given the root of an n-ary tree, return the preorder traversal of its nodes' values.
        //Nary-Tree input serialization is represented in their level order traversal.Each group of children is separated by the null value. See example:
        //Input: root = [1,null,3,2,4,null,5,6]
        //Output: [1,3,5,6,2,4]
        public static IList<int> PreorderRecursive(Node root)
        {
            var preOrderList = new List<int>();

            if (root == null)
                return preOrderList;

            void recursivePreOrder(Node node, IList<int> nodes)
            {
                if (node == null)
                    return;

                nodes.Add(node.val);

                foreach (var child in node.children)
                {
                    recursivePreOrder(child, nodes);
                }
            }

            recursivePreOrder(root, preOrderList);

            return preOrderList;
        }

        //409. Longest Palindrome
        //Given a string s which consists of lowercase or uppercase letters, return the length of the longest palindrome that can be built with those letters.
        //Letters are case sensitive, for example, "Aa" is not considered a palindrome here.
        public static int LongestPalindrome(string s)
        {
            if (s == null || s.Length == 0)
                return 0;
            if (s.Length == 1)
                return 1;

            var charCounts = new Dictionary<char, int>();

            foreach (var chr in s)
            {
                if (charCounts.ContainsKey(chr))
                    charCounts[chr]++;
                else
                    charCounts[chr] = 1;
            }

            var oddUsed = false;
            var palindromeLength = 0;
            foreach (var kv in charCounts)
            {
                if (kv.Value % 2 == 0)
                    palindromeLength += kv.Value;
                else
                {
                    if (oddUsed)
                    {
                        palindromeLength += kv.Value - 1;
                    }
                    else
                    {
                        oddUsed = true;
                        palindromeLength += kv.Value;
                    }
                }
            }

            return palindromeLength;
        }

        //121. Best Time to Buy and Sell Stock
        //You are given an array prices where prices[i] is the price of a given stock on the ith day.
        //You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock. Return the maximum profit you can achieve from this transaction.If you cannot achieve any profit, return 0.
        public static int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length < 2)
                return 0;

            var maxProfit = 0;
            var minPrice = prices[0];

            foreach (var price in prices)
            {
                minPrice = Math.Min(minPrice, price);
                maxProfit = Math.Max(maxProfit, price - minPrice);
            }

            return maxProfit;
        }

        //142. Linked List Cycle II
        //Given the head of a linked list, return the node where the cycle begins. If there is no cycle, return null.
        //There is private a cycle in private a linked list if there is private some node in private the list private that can private be reached private again by private continuously following private the next pointer.Internally, pos is private used to private denote the private index of private the node private that tail's next pointer is connected to (0-indexed). It is -1 if there is no cycle. Note that pos is not passed as a parameter.
        //Do not modify the linked list.
        //Time = O(n) | Space = O(1)
        //Distance from head of list to cycle start = distance from meeting point of slow and fast pointers to the cycle start.
        public static ListNode DetectCycleConstantMemory(ListNode head)
        {
            if (head == null)
                return null;

            var slow = head;
            var fast = head;
            var cycleLength = 0;

            //Step 1: Find the cycle exists in list.
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                {
                    do
                    {
                        slow = slow.next;
                        cycleLength++;
                    } while (slow != fast);
                    break;
                }
            }

            //return if no cycle.
            if (fast == null || fast.next == null)
                return null;

            //Step 2: find cycle start.
            //Distance from head of list to cycle start = distance from meeting point of slow and fast pointers to the cycle start.
            var cyclePtr = head;
            while (cycleLength != 0)
            {
                cycleLength--;
                cyclePtr = cyclePtr.next;
            }

            while (cyclePtr != head)
            {
                cyclePtr = cyclePtr.next;
                head = head.next;
            }

            return head;
        }

        //142. Linked List Cycle II
        //Given the head of a linked list, return the node where the cycle begins. If there is no cycle, return null.
        //There is private a cycle in private a linked list if there is private some node in private the list private that can private be reached private again by private continuously following private the next pointer.Internally, pos is private used to private denote the private index of private the node private that tail's next pointer is connected to (0-indexed). It is -1 if there is no cycle. Note that pos is not passed as a parameter.
        //Do not modify the linked list.
        //Time = O(n) | Space = O(n)
        public static ListNode DetectCycle(ListNode head)
        {
            if (head == null)
                return null;

            var nodes = new HashSet<ListNode>();
            while (head != null)
            {
                if (nodes.Contains(head))
                    return head;
                nodes.Add(head);
                head = head.next;
            }

            return null;
        }

        //876. Middle of the Linked List
        //Given the head of a singly linked list, return the middle node of the linked list.
        //If there are two middle nodes, return the second middle node.
        public static ListNode MiddleNode(ListNode head)
        {
            if (head == null)
                return null;

            var mid = head;
            for (var length = 1; head != null; length++)
            {
                if (length % 2 == 0)
                    mid = mid.next;

                head = head.next;
            }

            return mid;
        }

        //206. Reverse Linked List
        //Given the head of a singly linked list, reverse the list, and return the reversed list.
        // Time = O(n) | Space = O(1)
        public static ListNode ReverseListIterative(ListNode head)
        {
            if (head == null)
                return null;

            ListNode prev = null;
            ListNode next = null;

            while (head.next != null)
            {
                next = head.next;
                head.next = prev;
                prev = head;
                head = next;
            }

            head.next = prev;

            return head;
        }

        //206. Reverse Linked List
        //Given the head of a singly linked list, reverse the list, and return the reversed list.
        //This approach is using Stack. Time = O(2n) | Space = O(n)
        //Out of Memory error.
        public static ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return null;

            var nodes = new Stack<ListNode>();
            while (head != null)
            {
                nodes.Push(head);
                head = head.next;
            }

            var newHead = nodes.Pop();
            var prev = newHead;

            while (nodes.Count > 0)
            {
                var node = nodes.Pop();
                prev.next = node;
                prev = node;
            }

            return newHead;
        }

        //21. Merge Two Sorted Lists
        //You are given the heads of two sorted linked lists list1 and list2.
        //Merge the two lists in a one sorted list.The list should be made by splicing together the nodes of the first two lists. Return the head of the merged linked list.
        public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            //Use more strict validations.
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            ListNode head = null;
            ListNode prev = null;
            ListNode smaller = null;

            while (list1 != null && list2 != null)
            {
                if (list1.val > list2.val)
                {
                    smaller = list2;
                    list2 = list2.next;
                }
                else
                {
                    smaller = list1;
                    list1 = list1.next;
                }

                if (head == null)
                {
                    head = smaller;
                }
                else
                {
                    prev.next = smaller;
                }
                prev = smaller;
            }

            if (list1 == null)
            {
                prev.next = list2;
            }
            else
                prev.next = list1;

            return head;
        }

        //392. Is Subsequence
        //Given two strings s and t, return true if s is a subsequence of t, or false otherwise.
        //A subsequence of a string is a new string that is formed from the original string by deleting some(can be none) of the characters without disturbing the relative positions of the remaining characters. (i.e., "ace" is a subsequence of "abcde" while "aec" is not).
        public static bool IsSubsequence(string s, string t)
        {
            if (s == null || t == null || s.Length > t.Length)
                return false;
            if (s.Equals(string.Empty))
                return true;

            var s_idx = 0;

            foreach (var chr in t)
            {
                if (chr == s[s_idx])
                    s_idx++;

                if (s_idx == s.Length)
                    return true;
            }

            return false;
        }

        //205. Isomorphic Strings
        //Given two strings s and t, determine if they are isomorphic.
        //Two strings s and t are isomorphic if the characters in s can be replaced to get t. All occurrences of a character must be replaced with another character while preserving the order of characters.No two characters may map to the same character, but a character may map to itself.
        public static bool IsIsomorphic(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            var s2tMap = new Dictionary<char, char>();
            var t2sMap = new Dictionary<char, char>();

            for (var idx = 0; idx < s.Length; idx++)
            {
                var char_S = s[idx];
                var char_T = t[idx];

                //check char_S has a map in S->T.
                if (s2tMap.ContainsKey(char_S))
                {
                    var char_Replaced = s2tMap[char_S];

                    if (char_Replaced != char_T)
                        return false;
                }

                //check char_T has previously been mapped to a char in S.
                if (t2sMap.ContainsKey(char_T))
                {
                    var mapped_SChar = t2sMap[char_T];

                    if (mapped_SChar != char_S)
                        return false;
                }

                s2tMap[char_S] = char_T;
                t2sMap[char_T] = char_S;
            }

            return true;
        }

        //724. Find Pivot Index
        //Given an array of integers nums, calculate the pivot index of this array.
        //The pivot index is the index where the sum of all the numbers strictly to the left of the index is equal to the sum of all the numbers strictly to the index's right. If the index is on the left edge of the array, then the left sum is 0 because there are no elements to the left.This also applies to the right edge of the array. Return the leftmost pivot index.If no such index exists, return -1.
        public static int PivotIndex(int[] nums)
        {
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return 0;

            var leftSum = 0;
            var rightSum = nums.Sum();

            for (var idx = 0; idx < nums.Length; idx++)
            {
                if (idx != 0)
                {
                    leftSum += nums[idx - 1];
                }

                rightSum -= nums[idx];

                if (leftSum == rightSum)
                    return idx;
            }

            return -1;
        }

        //1480. Running Sum of 1d Array
        //Given an array nums. We define a running sum of an array as runningSum[i] = sum(nums[0]…nums[i]). Return the running sum of nums.
        public static int[] RunningSum(int[] nums)
        {
            //validations.
            if (nums == null || nums.Length == 0)
                return Array.Empty<int>();

            var runningSums = new int[nums.Length];

            var prev = 0;

            for (var idx = 0; idx < nums.Length; idx++)
            {
                var sum = prev + nums[idx];
                runningSums[idx] = sum;
                prev = sum;
            }

            return runningSums;
        }

        //Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target. You may assume that each input would have exactly one solution, and you may not use the same element twice. You can return the answer in any order.
        public static int[] TwoSum(int[] nums, int target)
        {
            var complements = new Hashtable();

            for (var i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (complements.ContainsKey(num))
                {
                    return new int[] { (int)complements[num], i };
                }
                else if (!complements.ContainsKey(target - num))
                {
                    complements.Add(target - num, i);
                }
            }

            return null;
        }

        //Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum. A subarray is a contiguous part of an array.
        public static int MaxSubArray(int[] nums)
        {
            return 0;
        }

        //Given an integer array nums, move all 0's to the end of it while maintaining the relative order of the non-zero elements. Note that you must do this in-place without making a copy of the array.
        public static int[] MoveZeroes(int[] nums)
        {
            if (nums == null || nums.Length == 1)
                return nums;

            var ptr1 = 0;
            var ptr2 = 1;

            while (ptr2 < nums.Length)
            {
                var srcNum = nums[ptr1];
                var destNum = nums[ptr2];

                if (srcNum == 0 && destNum != 0)
                {
                    nums[ptr1] = destNum;
                    nums[ptr2] = srcNum;
                    ptr1++;
                }

                if (srcNum != 0)
                    ptr1++;

                ptr2++;
            }

            return nums;
        }

        //Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.
        public static bool ContainsDuplicate(int[] nums)
        {
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return false;

            var previouslySeenNums = new HashSet<int>();

            foreach (var num in nums)
            {
                if (previouslySeenNums.Contains(num))
                    return true;

                previouslySeenNums.Add(num);
            }

            return false;
        }

        //Given an integer array nums and an integer k, return true if there are two distinct indices i and j in the array such that nums[i] == nums[j] and abs(i - j) <= k.
        public static bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return false;

            var previouslySeenNums = new Dictionary<int, int>();

            for (var idx = 0; idx < nums.Length; idx++)
            {
                var num = nums[idx];

                if (previouslySeenNums.ContainsKey(num))
                {
                    if (Math.Abs(previouslySeenNums[num] - idx) <= k) // Check if duplicate was within k positions of current num.
                        return true;
                    else
                        previouslySeenNums[num] = idx; // Since we're looking for nearby duplicate at-most k positions before so, update the last seen index of num in case it might match another duplicate at-most k positions ahead.
                }
                else
                    previouslySeenNums.Add(num, idx); // Haven't seen this num before in array.
            }

            return false;
        }

        // Given an integer array nums and two integers k and t, return true if there are two distinct indices i and j in the array such that abs(nums[i] - nums[j]) <= t and abs(i - j) <= k.
        public static bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return false;

            var previouslySeenNums = new Dictionary<int, int>();

            for (var idx = 0; idx < nums.Length; idx++)
            {
                var num = nums[idx];

                if (previouslySeenNums.ContainsKey(num))
                {
                    if (Math.Abs(previouslySeenNums[num] - idx) <= k) // Check if duplicate was within k positions of current num.
                        return true;
                    else
                        previouslySeenNums[num] = idx; // Since we're looking for nearby duplicate at-most k positions before so, update the last seen index of num in case it might match another duplicate at-most k positions ahead.
                }
                else
                    previouslySeenNums.Add(num, idx); // Haven't seen this num before in array.
            }

            return false;
        }

        //Given an array, rotate the array to the right by k steps, where k is non-negative.
        // Time - O(n) Space - O(n)
        // Time - O(n) Space - O(1) ... how?
        public static int[] Rotate(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return nums;

            if (k > nums.Length)
                k %= nums.Length;

            var shiftedArray = new int[nums.Length];

            // Add un-rotated elems to new array.
            for (var i = k; i < nums.Length; i++)
            {
                shiftedArray[i] = nums[i - k];
            }

            var offset = nums.Length - k;

            // Add rotated elems to new array.
            for (var i = 0; i < k; i++)
            {
                shiftedArray[i] = nums[offset + i];
            }

            return shiftedArray;
        }

        //Given a string s, find the first non-repeating character in it and return its index. If it does not exist, return -1.
        public static int FirstUniqChar(string s)
        {
            if (string.IsNullOrEmpty(s))
                return -1;

            if (s.Length == 1)
                return 0;

            var dict = new Dictionary<char, int>(); //Dict to hold unique elems. If value == -1 then elem repeats in string.
            var resultIdx = -1;

            for (var idx = 0; idx < s.Length; idx++)
            {
                var letter = s[idx];
                if (dict.ContainsKey(letter))
                    dict[letter] = -1;
                else
                    dict.Add(letter, idx);
            }

            foreach (var kv in dict)
            {
                if (kv.Value == -1)
                    continue;
                else if (kv.Value < resultIdx || resultIdx == -1)
                    resultIdx = kv.Value;
            }

            return resultIdx;
        }

        //Give a binary string s, return the number of non-empty substrings that have the same number of 0's and 1's, and all the 0's and all the 1's in these substrings are grouped consecutively.
        //Substrings that occur multiple times are counted the number of times they occur.
        public static int CountBinarySubstrings(string s)
        {
            var subStringCount = 0;
            var countZero = 0;
            var countOne = 0;

            var prevPtr = 0;
            var currentPtr = 0;

            //Iterate over string.
            while (currentPtr < s.Length)
            {
                var prevChar = s[prevPtr];
                var currentChar = s[currentPtr];

                prevPtr = currentPtr;
                currentPtr++;

                //current char same as previous char.
                if (currentChar == prevChar)
                {
                    if (currentChar == '0')
                    {
                        countZero++;
                    }
                    else
                    {
                        countOne++;
                    }
                }
                //change from 0 -> 1 or 1 -> 0.
                else
                {
                    //wait until all consecutive 1's and 0's have been counted.
                    if (countZero != 0 && countOne != 0)
                        subStringCount += Math.Min(countZero, countOne);

                    //Re-initialize 1/0's count to 1 because it has been seen for the 1st time in the substring.
                    if (currentChar == '1')
                    {
                        countOne = 1;
                    }
                    else
                    {
                        countZero = 1;
                    }
                }
            }

            //Add the count of sub-substring's from the last substring.
            subStringCount += Math.Min(countZero, countOne);

            return subStringCount;
        }

        //Tic-tac-toe is played by two players A and B on a 3 x 3 grid. The rules of Tic-Tac-Toe are:

        //Players take turns placing characters into empty squares ' '.
        //The first player A always places 'X' characters, while the second player B always places 'O' characters.
        //'X' and 'O' characters are always placed into empty squares, never on filled ones.
        //The game ends when there are three of the same (non-empty) character filling any row, column, or diagonal.
        //The game also ends if all squares are non-empty.
        //No more moves can be played if the game is over.

        //Given a 2D integer array moves where moves[i] = [rowi, coli] indicates that the ith move will be played on grid[rowi][coli]. return the winner of the game if it exists(A or B). In case the game ends in a draw return "Draw". If there are still movements to play return "Pending".

        //You can assume that moves is valid(i.e., it follows the rules of Tic-Tac-Toe), the grid is initially empty, and A will play first.
        public static string Tictactoe(int[][] moves)
        {
            var movesRemaining = 9; //3x3 tic tac toe.

            //A's connect dicts. 1 for rows, 1 for cols, and 1 for diagonals.
            var ARowConnects = new int[3];
            var AColConnects = new int[3];
            var ADiagConnects = new int[2];

            //B's connect dicts.
            var BRowConnects = new int[3];
            var BColConnects = new int[3];
            var BDiagConnects = new int[2];

            for (var i = 0; i < moves.Length; i++)
            {
                var move = moves[i];
                var rowIdx = move[0];
                var colIdx = move[1];

                movesRemaining--;

                //A's turn. A always goes first.
                if (i % 2 == 0)
                {
                    //Upsert relevant connect.

                    //1st diagonal.
                    if (rowIdx == colIdx)
                    {
                        ADiagConnects[0]++;

                        if (ADiagConnects[0] == 3)
                            return "A";
                    }

                    //2nd diagnoal
                    if (rowIdx + colIdx == 2)
                    {
                        ADiagConnects[1]++;

                        if (ADiagConnects[1] == 3)
                            return "A";
                    }

                    ARowConnects[rowIdx]++;
                    if (ARowConnects[rowIdx] == 3)
                        return "A";

                    AColConnects[colIdx]++;
                    if (AColConnects[colIdx] == 3)
                        return "A";
                }
                //B's turn.
                else
                {
                    //1st diagonal.
                    if (rowIdx == colIdx)
                    {
                        BDiagConnects[0]++;

                        if (BDiagConnects[0] == 3)
                            return "B";
                    }

                    //2nd diagnoal
                    if (rowIdx + colIdx == 2)
                    {
                        BDiagConnects[1]++;

                        if (BDiagConnects[1] == 3)
                            return "B";
                    }

                    BRowConnects[rowIdx]++;
                    if (BRowConnects[rowIdx] == 3)
                        return "B";

                    BColConnects[colIdx]++;
                    if (BColConnects[colIdx] == 3)
                        return "B";
                }
            }

            if (movesRemaining > 0)
                return "Pending";
            else
                return "Draw";
        }

        public static int MaximumUnits(int[][] boxTypes, int truckSize)
        {
            if (truckSize == 0 || boxTypes == null || boxTypes.Length == 0)
                return 0;

            var maxUnits = 0;

            var comparator = Comparer<int>.Default;

            Array.Sort(boxTypes, (b1, b2) => comparator.Compare(b2[1], b1[1]));

            var currentBoxType = 0;

            while (truckSize > 0 && currentBoxType < boxTypes.Length)
            {
                var boxesAvailable = boxTypes[currentBoxType][0];
                var unitsPerBox = boxTypes[currentBoxType][1];

                if (truckSize >= boxesAvailable)
                {
                    maxUnits += boxesAvailable * unitsPerBox;
                    truckSize -= boxesAvailable;
                    currentBoxType++;
                }
                else
                {
                    maxUnits += truckSize * unitsPerBox;
                    truckSize = 0;
                }
            }

            return maxUnits;
        }

        public static bool IsAnagram(string s, string t)
        {
            if (s == null || t == null)
                return false;

            if (s.Length != t.Length)
                return false;

            var sLetters = GetLettersDict(s);
            var tLetters = GetLettersDict(t);

            if (sLetters.Count != tLetters.Count)
                return false;

            foreach (var kv in sLetters)
            {
                if (!tLetters.ContainsKey(kv.Key) || kv.Value != tLetters[kv.Key])
                    return false;
            }

            return true;
        }

        private static Dictionary<char, int> GetLettersDict(string input)
        {
            var letters = new Dictionary<char, int>();

            foreach (var chr in input)
            {
                if (letters.ContainsKey(chr))
                    letters[chr]++;
                else
                    letters.Add(chr, 1);
            }

            return letters;
        }

        //Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals, and return an array of the non-overlapping intervals that cover all the intervals in the input.
        public static int[][] Merge(int[][] intervals)
        {
            if (intervals == null || intervals.Length == 1)
                return intervals;

            //O(n log n) sort.
            var comparator = Comparer<int>.Default;

            Array.Sort(intervals, (a, b) => comparator.Compare(a[0], b[0]));

            var mergedIntervals = new List<int[]>();

            var mergedInterval = intervals[0];
            var arrayPointer = 1;

            while (arrayPointer < intervals.Length)
            {
                var currentInterval = intervals[arrayPointer];
                if (CanMerge(mergedInterval, currentInterval))
                {
                    mergedInterval = DoMerge(mergedInterval, currentInterval);
                }
                else
                {
                    mergedIntervals.Add(mergedInterval);
                    mergedInterval = currentInterval;
                }

                arrayPointer++;
            }

            mergedIntervals.Add(mergedInterval);

            return mergedIntervals.ToArray();
        }

        private static bool CanMerge(int[] firstArray, int[] secondArray)
        {
            return secondArray[0] <= firstArray[1];
        }

        private static int[] DoMerge(int[] firstArray, int[] secondArray)
        {
            return new int[2] { firstArray[0], Math.Max(firstArray[1], secondArray[1]) };
        }

        //Given an m x n grid of characters board and a string word, return true if word exists in the grid.
        //The word can be constructed from letters of sequentially adjacent cells, where adjacent cells are horizontally or vertically neighboring.The same letter cell may not be used more than once.
        // This solution is TLE :|
        public static bool Exist(char[][] board, string word)
        {
            //validations.
            if (
                board == null
                || board.Length == 0
                || word == null
                || word.Length == 0
                || word.Length > board.Length * board[0].Length
            )
                return false;

            var lastRowIdx = board.Length - 1;
            var lastColIdx = board[0].Length - 1;

            var charsInBoard = new HashSet<char>();

            for (var i = 0; i <= lastRowIdx; i++)
            {
                for (var j = 0; j <= lastColIdx; j++)
                {
                    charsInBoard.Add(board[i][j]);
                }
            }

            foreach (var chr in word)
            {
                if (!charsInBoard.Contains(chr))
                    return false;
            }

            for (var i = 0; i <= lastRowIdx; i++)
            {
                for (var j = 0; j <= lastColIdx; j++)
                {
                    if (board[i][j] == word[0])
                    {
                        var currentSubStr = new Stack<int[]>();
                        var visitedPositions = new Dictionary<int[], HashSet<string>>();
                        var usedCharPostions = new HashSet<string>(word.Length);

                        currentSubStr.Push(new int[] { i, j });
                        usedCharPostions.Add($"{i}:{j}");

                        var ptr = 1;

                        while (currentSubStr.Count != 0 && ptr < word.Length)
                        {
                            var nextChar = word[ptr];
                            var position = currentSubStr.Peek();
                            var row = position[0];
                            var col = position[1];

                            var belowRow = row + 1;
                            var aboveRow = row - 1;
                            var previousCol = col - 1;
                            var nextCol = col + 1;

                            if (!visitedPositions.ContainsKey(position))
                                visitedPositions.Add(position, new HashSet<string>(4));

                            if (
                                belowRow <= lastRowIdx
                                && !usedCharPostions.Contains($"{belowRow}:{col}")
                                && !visitedPositions[position].Contains($"{belowRow}:{col}")
                                && board[belowRow][col] == nextChar
                            )
                            {
                                currentSubStr.Push(new int[] { belowRow, col });
                                usedCharPostions.Add($"{belowRow}:{col}");
                                visitedPositions[position].Add($"{belowRow}:{col}");
                                ptr++;
                            }
                            else if (
                                aboveRow >= 0
                                && !usedCharPostions.Contains($"{aboveRow}:{col}")
                                && !visitedPositions[position].Contains($"{aboveRow}:{col}")
                                && board[aboveRow][col] == nextChar
                            )
                            {
                                currentSubStr.Push(new int[] { aboveRow, col });
                                usedCharPostions.Add($"{aboveRow}:{col}");
                                visitedPositions[position].Add($"{aboveRow}:{col}");
                                ptr++;
                            }
                            else if (
                                previousCol >= 0
                                && !usedCharPostions.Contains($"{row}:{previousCol}")
                                && !visitedPositions[position].Contains($"{row}:{previousCol}")
                                && board[row][previousCol] == nextChar
                            )
                            {
                                currentSubStr.Push(new int[] { row, previousCol });
                                usedCharPostions.Add($"{row}:{previousCol}");
                                visitedPositions[position].Add($"{row}:{previousCol}");
                                ptr++;
                            }
                            else if (
                                nextCol <= lastColIdx
                                && !usedCharPostions.Contains($"{row}:{nextCol}")
                                && !visitedPositions[position].Contains($"{row}:{nextCol}")
                                && board[row][nextCol] == nextChar
                            )
                            {
                                currentSubStr.Push(new int[] { row, nextCol });
                                usedCharPostions.Add($"{row}:{nextCol}");
                                visitedPositions[position].Add($"{row}:{nextCol}");
                                ptr++;
                            }
                            //no connection found
                            else
                            {
                                currentSubStr.Pop();
                                usedCharPostions.Remove($"{row}:{col}");
                                ptr--;
                            }
                        }

                        if (ptr == word.Length)
                            return true;
                    }
                }
            }

            return false;
        }

        //Given an m x n grid of characters board and a string word, return true if word exists in the grid.
        //The word can be constructed from letters of sequentially adjacent cells, where adjacent cells are horizontally or vertically neighboring.The same letter cell may not be used more than once.
        public static bool ExistRecursive(char[][] board, string word)
        {
            //do validations.

            var usedPositions = new HashSet<string>();
            var lastRowIdx = board.Length - 1;
            var lastColIdx = board[0].Length - 1;

            bool FindWord(int row, int col, int wordPtr)
            {
                //base case.
                if (wordPtr == word.Length)
                    return true;

                var currentPosition = $"{row}:{col}";

                //invalid indices
                if (
                    row > lastRowIdx
                    || col > lastColIdx
                    || row < 0
                    || col < 0
                    || board[row][col] != word[wordPtr]
                    || usedPositions.Contains(currentPosition)
                )
                    return false;

                usedPositions.Add(currentPosition);

                if (
                    FindWord(row - 1, col, wordPtr + 1)
                    || FindWord(row + 1, col, wordPtr + 1)
                    || FindWord(row, col - 1, wordPtr + 1)
                    || FindWord(row, col + 1, wordPtr + 1)
                )
                    return true;

                usedPositions.Remove(currentPosition);
                return false;
            }

            for (var row = 0; row <= lastRowIdx; row++)
            {
                for (var col = 0; col <= lastColIdx; col++)
                {
                    if (FindWord(row, col, 0))
                        return true;
                }
            }

            return false;
        }

        //Leetcode - 200
        //Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
        //An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.You may assume all four edges of the grid are all surrounded by water.
        //BFS - Naive Approach. Leads to TLE.
        public static int NumIslands(char[][] grid)
        {
            //do validations.
            if (grid == null || grid.Length == 0)
                return 0;

            var lastRow = grid.Length - 1;
            var lastCol = grid[0].Length - 1;

            var partOfExistingIslands = new HashSet<string>();
            var numOfIslands = 0;

            bool ShouldProcessNum(int row, int col)
            {
                var num = grid[row][col];

                return num == '1' && !partOfExistingIslands.Contains($"{row}:{col}");
            }

            void AddNeighborToQueue(int row, int col, Queue<int[]> queue)
            {
                if (row < 0 || row > lastRow || col < 0 || col > lastCol)
                    return;

                if (ShouldProcessNum(row, col))
                    queue.Enqueue(new int[] { row, col });
            }

            for (var row = 0; row <= lastRow; row++)
            {
                for (var col = 0; col <= lastCol; col++)
                {
                    if (ShouldProcessNum(row, col))
                    {
                        var numsToProcess = new Queue<int[]>();
                        numsToProcess.Enqueue(new int[] { row, col });

                        numOfIslands++;

                        while (numsToProcess.Count > 0)
                        {
                            var position = numsToProcess.Dequeue();

                            partOfExistingIslands.Add($"{position[0]}:{position[1]}");

                            AddNeighborToQueue(position[0] + 1, position[1], numsToProcess);
                            AddNeighborToQueue(position[0] - 1, position[1], numsToProcess);
                            AddNeighborToQueue(position[0], position[1] + 1, numsToProcess);
                            AddNeighborToQueue(position[0], position[1] - 1, numsToProcess);
                        }
                    }
                }
            }

            return numOfIslands;
        }

        public static int NumIslands2(char[][] grid)
        {
            //validations
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
                return 0;

            var lastRow = grid.Length - 1;
            var lastCol = grid[0].Length - 1;

            var numOfIslands = 0;

            var visited = new HashSet<(int, int)>();

            //utility function.
            void AddNeighborToQueue(int r, int c, Queue<(int, int)> queue)
            {
                if (
                    r >= 0
                    && r <= lastRow
                    && c >= 0
                    && c <= lastCol
                    && grid[r][c] == '1'
                    && !visited.Contains((r, c))
                )
                {
                    visited.Add((r, c));
                    queue.Enqueue((r, c));
                }
            }

            //iterate over grid to find the islands.
            for (var row = 0; row <= lastRow; row++)
            {
                for (var col = 0; col <= lastCol; col++)
                {
                    //found an island.
                    if (grid[row][col] == '1' && !visited.Contains((row, col)))
                    {
                        ++numOfIslands;

                        //add all neighbors to part of island.
                        var queue = new Queue<(int, int)>();

                        queue.Enqueue((row, col));

                        while (queue.Count > 0)
                        {
                            var node = queue.Dequeue();

                            (var r, var c) = (node.Item1, node.Item2);

                            //grid[r][c] = '0';
                            visited.Add((r, c));

                            AddNeighborToQueue(r + 1, c, queue);
                            AddNeighborToQueue(r - 1, c, queue);
                            AddNeighborToQueue(r, c + 1, queue);
                            AddNeighborToQueue(r, c - 1, queue);
                        }
                    }
                }
            }

            return numOfIslands;
        }

        public static int NumIslands3(char[][] grid)
        {
            //validations
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
                return 0;

            var numOfIslands = 0;

            var rowLength = grid.Length;
            var columnLength = grid[0].Length;

            var visitedLandAreas = new HashSet<(int, int)>();

            void VisitAdjacentLand(int row, int col, Queue<(int, int)> queue)
            {
                if (
                    row < 0
                    || row >= rowLength
                    || col < 0
                    || col >= columnLength
                    || visitedLandAreas.Contains((row, col))
                    || grid[row][col] != '1'
                )
                    return;

                visitedLandAreas.Add((row, col));
                queue.Enqueue((row, col));
            }

            for (var row = 0; row < rowLength; row++)
            {
                for (var col = 0; col < columnLength; col++)
                {
                    var area = grid[row][col];

                    //found land. span all the connected land.
                    if (area == '1' && !visitedLandAreas.Contains((row, col)))
                    {
                        numOfIslands++;

                        var landAreas = new Queue<(int, int)>();
                        landAreas.Enqueue((row, col));

                        while (landAreas.Count > 0)
                        {
                            var (r, c) = landAreas.Dequeue();

                            //grid[r][c] = 'X';

                            VisitAdjacentLand(r + 1, c, landAreas);
                            VisitAdjacentLand(r - 1, c, landAreas);
                            VisitAdjacentLand(r, c + 1, landAreas);
                            VisitAdjacentLand(r, c - 1, landAreas);
                        }
                    }
                    else
                        continue;
                }
            }

            return numOfIslands;
        }

        public static string[] ReorderLogFiles(string[] logs)
        {
            //validations.
            if (logs == null || logs.Length == 0 || logs.Length == 1)
                return logs;

            //final results
            var orderedLogs = new string[logs.Length];

            //will hold sorted letter logs.
            var letterLogs = new List<string>();

            //hold digit logs.
            var digitLogs = new List<string>();

            var digitSet = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //letter log comparer.
            static int CompareLogs(string log1, string log2)
            {
                var idx1 = log1.IndexOf(" ");
                var idx2 = log2.IndexOf(" ");

                var content1 = log1[(idx1 + 1)..];
                var content2 = log2[(idx2 + 1)..];

                var comp = content1.CompareTo(content2);

                //equal strings.
                if (comp == 0)
                {
                    var id1 = log1.Substring(0, idx1);
                    var id2 = log2.Substring(0, idx2);

                    return id1.CompareTo(id2);
                }

                return comp;
            }

            foreach (var log in logs)
            {
                var words = log.Split(' ');
                var isDigitLog = false;

                // check if it is a digit log.
                for (var idx = 1; idx < words.Length; idx++)
                {
                    //found it is a digit log.
                    if (digitSet.Contains(words[idx][0]))
                    {
                        digitLogs.Add(log);
                        isDigitLog = true;
                        break;
                    }
                }

                if (!isDigitLog)
                    letterLogs.Add(log);
            }

            letterLogs.Sort(CompareLogs);

            letterLogs.CopyTo(orderedLogs, 0);
            digitLogs.CopyTo(orderedLogs, letterLogs.Count);

            return orderedLogs;
        }

        //228. Summary Ranges
        //You are given a sorted unique integer array nums.
        //Return the smallest sorted list of ranges that cover all the numbers in the array exactly.That is, each element of nums is covered by exactly one of the ranges, and there is no integer x such that x is in one of the ranges but not in nums.
        //Each range[a, b] in the list should be output as:
        //"a->b" if a != b
        //"a" if a == b
        public static IList<string> SummaryRanges(int[] nums)
        {
            //validation
            if (nums == null || nums.Length == 0)
                return new List<string>();

            IList<string> ranges = new List<string>();

            int prev = nums[0];
            var range = new StringBuilder();
            range.Append(Convert.ToString(nums[0]));

            for (var idx = 1; idx < nums.Length; idx++)
            {
                var current = nums[idx];

                //range started.
                if (current == prev + 1)
                {
                    if (!range[^1].Equals('>'))
                    {
                        range.Append("->");
                    }
                }
                //range has ended.
                else
                {
                    if (range[^1].Equals('>'))
                    {
                        range.Append(Convert.ToString(prev));
                    }

                    ranges.Add(range.ToString());

                    range.Clear();

                    range.Append(Convert.ToString(current));
                }

                prev = current;
            }

            if (range[^1].Equals('>'))
                range.Append(nums[^1]);

            ranges.Add(range.ToString());

            return ranges;
        }

        //704. Binary Search
        //Given an array of integers nums which is sorted in ascending order, and an integer target, write a function to search target in nums. If target exists, then return its index. Otherwise, return -1.
        //You must write an algorithm with O(log n) runtime complexity.
        public static int Search(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            if (nums.Length == 1)
            {
                if (nums[0] == target)
                    return 0;
                else
                    return -1;
            }

            var mid = nums.Length / 2;
            var midNum = nums[mid];
            if (midNum == target)
                return mid;

            //search in right side.
            if (target > midNum)
            {
                var rightLength = nums.Length - (mid + 1);
                var rightArray = new int[rightLength];

                Array.Copy(nums, mid + 1, rightArray, 0, rightLength);

                var idx = Search(rightArray, target);
                if (idx == -1)
                    return idx;
                else
                    return mid + 1 + idx;
            }
            //search in left side.
            else
            {
                var leftLength = mid;
                var leftArray = new int[leftLength];

                Array.Copy(nums, 0, leftArray, 0, mid);
                return Search(leftArray, target);
            }
        }

        //198. House Robber
        //You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security systems connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
        //Given an integer array nums representing the amount of money of each house, return the maximum amount of money you can rob tonight without alerting the police.
        //public static int Rob(int[] nums)
        //{
        //}

        //799. Champagne Tower
        //top-down DP
        public static double ChampagneTower(int poured, int query_row, int query_glass)
        {
            //validations

            var glasses = new Dictionary<string, double>();

            void fillGlasses(double poured, int r, int g)
            {
                double current = 0;

                if (glasses.ContainsKey($"{r}:{g}"))
                    current = glasses[$"{r}:{g}"];

                //base.
                if (poured == 0)
                    return;

                if (current + poured > 1)
                {
                    //recursive
                    double remainingChampagne = poured + current - 1;
                    current = 1;
                    glasses[$"{r}:{g}"] = current;

                    if (r < query_row)
                    {
                        fillGlasses(remainingChampagne / 2, r + 1, g);
                        fillGlasses(remainingChampagne / 2, r + 1, g + 1);
                    }
                }
                else
                {
                    current += poured;
                    glasses[$"{r}:{g}"] = current;
                }
            }

            fillGlasses(poured, 0, 0);

            return glasses.ContainsKey($"{query_row}:{query_glass}")
                ? glasses[$"{query_row}:{query_glass}"]
                : 0;
        }

        //799. Champagne Tower
        //bottom-up DP
        public static double ChampagneTowerDP(int poured, int query_row, int query_glass)
        {
            //validations

            var memo = new Dictionary<string, double>();

            double fillGlass(int r, int g)
            {
                var key = $"{r}:{g}";
                if (memo.ContainsKey(key))
                    return memo[key];

                //base
                if (r == 0)
                {
                    return poured;
                }

                //recursive
                double left = 0;
                if (r - 1 >= 0 && g - 1 >= 0)
                {
                    var leftParent = fillGlass(r - 1, g - 1);

                    if (leftParent > 1)
                    {
                        left = (leftParent - 1) / 2;
                    }
                }

                double right = 0;
                if (r - 1 >= 0 && g <= r - 1)
                {
                    var rightParent = fillGlass(r - 1, g);

                    if (rightParent > 1)
                    {
                        right = (rightParent - 1) / 2;
                    }
                }
                memo.Add(key, left + right);
                return memo[key];
            }

            var receivedChampagne = fillGlass(query_row, query_glass);

            return receivedChampagne > 1 ? 1 : receivedChampagne;
        }
    }
}
