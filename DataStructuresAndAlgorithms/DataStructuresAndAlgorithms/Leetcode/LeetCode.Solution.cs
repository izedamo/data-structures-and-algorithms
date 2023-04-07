namespace DataStructuresAndAlgorithms.Leetcode
{
    public static partial class LeetCode
    {
        //278. First Bad Version
        //You are a product manager and currently leading a team to develop a new product. Unfortunately, the latest version of your product fails the quality check. Since each version is developed based on the previous version, all the versions after a bad version are also bad.
        //Suppose you have n versions[1, 2, ..., n] and you want to find out the first bad one, which causes all the following ones to be bad. You are given an API bool isBadVersion(version) which returns whether version is bad. Implement a function to find the first bad version.You should minimize the number of calls to the API.

        /* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */

        public class VersionControl
        {
            public static bool IsBadVersion(int version)
            {
                //dummy method.
                return false;
            }
        }

        public class Solution : VersionControl
        {
            public int FirstBadVersion(int n)
            {
                if (n <= 0)
                    return 0;

                if (n == 1)
                    return IsBadVersion(n) ? n : 0;

                var good = n;
                var bad = n;

                while (IsBadVersion(good))
                {
                    good = good / 2;
                }

                //now good has a good value. bad has initial value of n since bad <= n so n is definitely bad.
                var mid = bad + ((good - bad) / 2);

                while (true)
                {
                    var isBad = IsBadVersion((int)mid);
                    if (isBad)
                        if (IsBadVersion((int)mid - 1))
                        {
                            bad = mid;
                        }
                        else
                        {
                            return mid;
                        }
                    else
                    {
                        bool found = IsBadVersion((int)mid + 1);

                        if (found)
                        {
                            return mid + 1;
                        }
                        else
                        {
                            good = mid;
                        }
                    }

                    mid = bad + ((good - bad) / 2);
                }
            }
        }
    }
}