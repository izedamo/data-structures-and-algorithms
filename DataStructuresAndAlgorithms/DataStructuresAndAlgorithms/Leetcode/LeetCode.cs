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
            
            else return "Draw";
        }
    }
}
