using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Viterbi
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "東京都に住む";
            Dp = new item[text.Length,root];
            //全てのセルを大きな値で初期化
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < root; j++)
                {
                    Dp[i, j].sum = INF;
                }
            }
            SetUp();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                var Queue = new Queue<mecab_word>();
                foreach (var w in Words)
                {
                    if (Regex.IsMatch(w.w, "^"+ c + ".*")) Queue.Enqueue(w);
                }
                while (Queue.Count > 0)
                {
                    var w = Queue.Dequeue();
                    int idx = w.w.Length + i - 1;
                    int cc = GetConnectCost(0, w.l);
                    if (i == 0) Dp[idx, GetUsedLow(idx)] = new item(w.w, w.l, w.r, w.c, w.d, w.c+cc, w.w);
                    else
                    {
                        int minlow = GetMinLow(idx,w.w);
                        if (minlow != -1)
                        {
                            int cnt = GetUsedLow(idx);
                            cc = GetConnectCost(Dp[i - 1, minlow].r, w.l);
                            if (Dp[idx, cnt].sum > w.c + Dp[i - 1, minlow].sum + cc)
                            {
                                Dp[idx, cnt] = new item
                                    (w.w, w.l, w.r, w.c, w.d, 
                                    w.c + Dp[i - 1, minlow].sum + cc, 
                                    Dp[i - 1, minlow].root+" "+ w.w
                                    );
                            }
                        }
                    }
                }
            }
            //結果出力
            var last = new item();
            long min = INF;
            for (int i = 0; i < root; i++)
            {
                if (min > Dp[text.Length - 1, i].sum)
                {
                    min = Dp[text.Length - 1, i].sum;
                    last = Dp[text.Length - 1, i];
                }
            }
            Console.WriteLine(last.root);
        }
        static public int root = 3;//最大分岐の数
        static public int INF = 1001001001;
        static public item[,] Dp;
        static public int GetMinLow(int n, string w)
        {
            long min = int.MaxValue;
            int l = w.Length;
            int ret = -1;
            for (int i = 0; i < root; i++)
            {
                if (min > Dp[n-l, i].sum)
                {
                    min = Dp[n-l, i].sum;
                    ret = i;
                }
            }
            return ret;
        }
        static public int GetUsedLow(int n)
        {
            for (int i = 0; i < root; i++)
            {
                if (Dp[n, i].sum == INF) return i;
            }
            return -1;
        }
        static public int GetConnectCost(long pre, long post)
        {
            foreach(var x in Matrix)
            {
                if (x.pre == pre && x.post == post) return x.c;
            }
            return 0;
        }
        static public mecab_word[] Words;
        static public matrix[] Matrix;
        static public void SetUp()
        {
            Words = new mecab_word[]
            {
                new mecab_word("に",1,1,4304,"助詞"),
                new mecab_word("に",2,2,11880,"動詞"),
                new mecab_word("京",3,3,10791,"名詞"),
                new mecab_word("京都",3,3,2135,"名詞"),
                new mecab_word("住む",4,4,7048,"動詞"),
                new mecab_word("東",5,5,6245,"名詞"),
                new mecab_word("東京",3,3,3003,"名詞"),
                new mecab_word("都",5,5,7595,"名詞、一般"),
                new mecab_word("都",6,6,9428,"名詞、接尾"),
            };
            Matrix = new matrix[]{
                new matrix(0,3,-310),//前件文脈IDが0だと文頭
                new matrix(0,5,-283),
                new matrix(1,4,-3547),
                new matrix(2,4,-12165),
                new matrix(3,1,-3838),
                new matrix(3,2,-1220),
                new matrix(3,5,-1303),
                new matrix(3,6,-9617),
                new matrix(4,0,-409),//後件文脈IDが0だと文末
                new matrix(5,1,-4811),
                new matrix(5,2,-811),
                new matrix(5,3,-368),
                new matrix(5,3,-368),
                new matrix(6,1,-3573),
                new matrix(6,2,1387),
            };

        }
        public struct mecab_word
        {
            //表層形、備考、生起コスト、左文脈ID、右文脈ID
            public string w,d;
            public int c, l, r;
            public mecab_word(string w, int l, int r, int c, string d)
            {
                this.w = w;
                this.c = c;
                this.l = l;
                this.r = r;
                this.d = d;
            }
        }
        public struct matrix
        {
            public int pre, post, c;
            public matrix(int pre, int post,int c)
            {
                this.pre = pre;
                this.post = post;
                this.c = c;
            }
        }
        public struct item
        {
            public string w,d,root;
            public long c,sum,l,r;
            public item(string w, long l, long r, long c, string d, long sum, string root)
            {
                this.w = w;
                this.c = c;
                this.d = d;
                this.sum = sum;
                this.root = root;
                this.l = l;
                this.r = r;
            }
        }

    }
}
