using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Xml.Linq;

namespace DataStructuresAndAlgorithms.Leetcode
{
    public static class LeetCode
    {
        //102. Binary Tree Level Order Traversal
        //Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e., from left to right, level by level).
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            var levelOrder = new List<IList<int>>();

            if (root == null)
                return levelOrder;

            var levels = new Queue<IList<TreeNode>>();
            levels.Enqueue(new List<TreeNode>
            {
                root
            });

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
                    }
                    while (slow != fast);
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
            if (head == null) return null;

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
            if (head == null) return null;

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
            if (s.Equals(string.Empty)) return true;

            var s_idx = 0;

            foreach (var chr in t)
            {
                if (chr == s[s_idx])
                    s_idx++;

                if (s_idx == s.Length) return true;
            }

            return false;
        }

        //205. Isomorphic Strings
        //Given two strings s and t, determine if they are isomorphic.
        //Two strings s and t are isomorphic if the characters in s can be replaced to get t. All occurrences of a character must be replaced with another character while preserving the order of characters.No two characters may map to the same character, but a character may map to itself.
        public static bool IsIsomorphic(string s, string t)
        {
            if (s.Length != t.Length) return false;

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

                    if (char_Replaced != char_T) return false;
                }

                //check char_T has previously been mapped to a char in S.
                if (t2sMap.ContainsKey(char_T))
                {
                    var mapped_SChar = t2sMap[char_T];

                    if (mapped_SChar != char_S) return false;
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
            if (nums == null || nums.Length == 0 || nums.Length == 1) return 0;

            var leftSum = 0;
            var rightSum = nums.Sum();

            for (var idx = 0; idx < nums.Length; idx++)
            {
                if (idx != 0)
                {
                    leftSum += nums[idx - 1];
                }

                rightSum -= nums[idx];

                if (leftSum == rightSum) return idx;
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
            else return "Draw";
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

            if (sLetters.Count != tLetters.Count) return false;

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
            if (board == null || board.Length == 0 || word == null || word.Length == 0 || word.Length > board.Length * board[0].Length)
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
                            var row = position[0]; var col = position[1];

                            var belowRow = row + 1;
                            var aboveRow = row - 1;
                            var previousCol = col - 1;
                            var nextCol = col + 1;

                            if (!visitedPositions.ContainsKey(position))
                                visitedPositions.Add(position, new HashSet<string>(4));

                            if (belowRow <= lastRowIdx && !usedCharPostions.Contains($"{belowRow}:{col}") && !visitedPositions[position].Contains($"{belowRow}:{col}") && board[belowRow][col] == nextChar)
                            {
                                currentSubStr.Push(new int[] { belowRow, col });
                                usedCharPostions.Add($"{belowRow}:{col}");
                                visitedPositions[position].Add($"{belowRow}:{col}");
                                ptr++;
                            }
                            else if (aboveRow >= 0 && !usedCharPostions.Contains($"{aboveRow}:{col}") && !visitedPositions[position].Contains($"{aboveRow}:{col}") && board[aboveRow][col] == nextChar)
                            {
                                currentSubStr.Push(new int[] { aboveRow, col });
                                usedCharPostions.Add($"{aboveRow}:{col}");
                                visitedPositions[position].Add($"{aboveRow}:{col}");
                                ptr++;
                            }
                            else if (previousCol >= 0 && !usedCharPostions.Contains($"{row}:{previousCol}") && !visitedPositions[position].Contains($"{row}:{previousCol}") && board[row][previousCol] == nextChar)
                            {
                                currentSubStr.Push(new int[] { row, previousCol });
                                usedCharPostions.Add($"{row}:{previousCol}");
                                visitedPositions[position].Add($"{row}:{previousCol}");
                                ptr++;
                            }
                            else if (nextCol <= lastColIdx && !usedCharPostions.Contains($"{row}:{nextCol}") && !visitedPositions[position].Contains($"{row}:{nextCol}") && board[row][nextCol] == nextChar)
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
                if (row > lastRowIdx || col > lastColIdx || row < 0 || col < 0 || board[row][col] != word[wordPtr] || usedPositions.Contains(currentPosition))
                    return false;

                usedPositions.Add(currentPosition);

                if (FindWord(row - 1, col, wordPtr + 1) || FindWord(row + 1, col, wordPtr + 1) || FindWord(row, col - 1, wordPtr + 1) || FindWord(row, col + 1, wordPtr + 1))
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
                if (r >= 0 && r <= lastRow && c >= 0 && c <= lastCol && grid[r][c] == '1' && !visited.Contains((r, c)))
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

            var digitSet = new HashSet<char> {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9'
        };

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
                    return mid + 1 + Search(rightArray, target);
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

            return glasses.ContainsKey($"{query_row}:{query_glass}") ? glasses[$"{query_row}:{query_glass}"] : 0;
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