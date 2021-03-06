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

            for(var i = 0; i <= lastRowIdx; i++)
            {
                for(var j = 0; j <= lastColIdx; j++)
                {
                    charsInBoard.Add(board[i][j]);
                }
            }

            foreach(var chr in word)
            {
                if (!charsInBoard.Contains(chr))
                    return false;
            }

            for(var i = 0; i <= lastRowIdx; i++)
            {
                for(var j = 0; j <= lastColIdx; j++)
                {
                    if(board[i][j] == word[0])
                    {
                        var currentSubStr = new Stack<int[]>();
                        var visitedPositions = new Dictionary<int[], HashSet<string>>();
                        var usedCharPostions = new HashSet<string>(word.Length);

                        currentSubStr.Push(new int[] { i, j });
                        usedCharPostions.Add($"{i}:{j}");

                        var ptr = 1;

                        while(currentSubStr.Count != 0 && ptr < word.Length)
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

                            else if (aboveRow >= 0 && !usedCharPostions.Contains($"{aboveRow}:{col}") && !visitedPositions[position].Contains($"{aboveRow}:{col}") &&  board[aboveRow][col] == nextChar)
                            {
                                currentSubStr.Push(new int[] { aboveRow, col });
                                usedCharPostions.Add($"{aboveRow}:{col}");
                                visitedPositions[position].Add($"{aboveRow}:{col}");
                                ptr++;
                            }

                            else if (previousCol >= 0 && !usedCharPostions.Contains($"{row}:{previousCol}") && !visitedPositions[position].Contains($"{row}:{previousCol}")  && board[row][previousCol] == nextChar)
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
                for(var col = 0; col <= lastColIdx; col++)
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

            for(var row = 0; row <= lastRow; row++)
            {
                for(var col = 0; col <= lastCol; col++)
                {
                    if(ShouldProcessNum(row, col))
                    {
                        var numsToProcess = new Queue<int[]>();
                        numsToProcess.Enqueue(new int[] { row, col });

                        numOfIslands++;

                        while(numsToProcess.Count > 0)
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

                    if (leftParent > 1) {
                        left = (leftParent - 1) / 2;
                    }
                }

                double right = 0;
                if (r - 1 >= 0 && g <= r - 1)
                {
                    var rightParent = fillGlass(r - 1, g);

                    if(rightParent > 1)
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
