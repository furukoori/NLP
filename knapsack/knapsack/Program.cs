using System;

namespace knapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            n = 4;
            W = new int[] { 2, 1, 3, 2 };
            V = new int[] { 3, 2, 4, 2 };
            w = 5;
            Console.WriteLine(BitMask());
            Console.WriteLine("---------------------------");
            Console.WriteLine(Dp());
        }
        static public int n, w;
        static public int[] W, V;
        static public long BitMask()
        {
            long v_res = 0;
            for (int i = 0; i < (1<<n); i++)
            {
                var d = new int[n];
                for (int j = 0; j < n; j++)
                {
                    if (((i >> j) & 1) == 1) { d[j] = 1; }
                }
                printArray(d);
                long w_sum = 0;
                long v_sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (d[j] == 1)
                    {
                        w_sum += W[j];
                        v_sum += V[j];
                    }
                }
                if (w_sum <= w) v_res = (long)Math.Max(v_res, v_sum);
            }
            return v_res;
        }
        static public void printArray(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                Console.Write(A[i] + " ");
            }
            Console.WriteLine();
        }
        static public int Dp()
        {
            var dp = new int[n + 1, w + 1];
            for (int i = n-1; i >= 0; i--)
            {
                for (int j = 0; j <= w; j++)
                {
                    if (j < W[i]) dp[i, j] = dp[i + 1, j];
                    else dp[i, j] = (int)Math.Max(dp[i + 1, j], dp[i + 1, j - W[i]] + V[i]);
                    printArray2(dp);
                }
            }
            return (dp[0,w]);
        }
        static public void printArray2(int[,] A)
        {
            for (int i = 0; i < n+1; i++)
            {
                for (int j = 0; j < w+1; j++)
                {
                    Console.Write(" " + A[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
