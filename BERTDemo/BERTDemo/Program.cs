using System;

namespace BERTDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OrderN(10));
            Console.WriteLine(OrderN2(10));

            Console.WriteLine(SearchIndexN(2));
            Console.WriteLine(SearchIndexLogN(2));
            Console.WriteLine(SearchIndexN(10));
            Console.WriteLine(SearchIndexLogN(10));
            Console.WriteLine(SearchIndexN(98));
            Console.WriteLine(SearchIndexLogN(98));
        }

        static public int OrderN(int n)
        {
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                cnt++;
            }
            return cnt;
        }
        static public int OrderN2(int n)
        {
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cnt++;
                    //Console.Write(", " + (10 * i + j));
                }
            }
            return cnt;
        }
        static public int[] Array = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100};
        static public int SearchIndexN(int key)
        {
            int cnt =0;
            for (int i = 0; i < Array.Length; i++)
            {
                cnt++;
                if (key == i) return cnt;
            }
            return -1;
        }
        static public int SearchIndexLogN(int key)
        {
            int cnt = 0;
            //配列Arrayの左端と右端
            int left = 0, right = Array.Length - 1;
            while (right >= left)
            {
                cnt++;
                int mid = left + (right - left) / 2;
                if (Array[mid] == key) return cnt;
                else if (Array[mid] > key) right = mid - 1;
                else if (Array[mid] < key) left = mid + 1; 
            }
            return -1;
        }
    }
}
