using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach(var elem in firstArray) //O(n)
            {
                hashset.Add(elem);
            }

            foreach(var elem in secondArray) //O(m)
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
            for(var i = 0; i < array.Length; i++)
            {
                var elem1 = array[i];
                for(var j = i+1; j < array.Length; j++)
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

            foreach(var elem in array)
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
    }
}
