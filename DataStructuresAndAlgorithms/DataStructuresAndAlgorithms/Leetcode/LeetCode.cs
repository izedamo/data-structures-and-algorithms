using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public class LeetCode
    {
        //Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target. You may assume that each input would have exactly one solution, and you may not use the same element twice. You can return the answer in any order.
        public static int[] TwoSum(int[] nums, int target)
        {
            var complements = new Hashtable();

            for(var i = 0; i < nums.Length; i++)
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

            while(ptr2 < nums.Length)
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

            foreach(var num in nums)
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

            for(var idx = 0; idx < nums.Length; idx++)
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
    }
}
