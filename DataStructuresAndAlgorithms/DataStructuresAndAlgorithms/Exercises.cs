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
    }
}
