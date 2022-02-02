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
    }
}
