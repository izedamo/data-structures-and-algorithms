using System;

namespace DataStructuresAndAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[4] { 1, 2, 4, 4 };

            var result = Exercises.HasPairWithSumBetter(arr, 8);
            Console.WriteLine(result);
        }
    }
}