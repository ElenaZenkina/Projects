using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tasks
{
    public class LeetCode
    {
        // Даны две строки s и t, верните true, если s - подпоследовательность строки t, false - в ином случае.
        public static bool isSubSequence(string sequence, string subSequence)
        {
            int i = 0;
            int j = 0;
            while (i < sequence.Length && j < subSequence.Length)
            {
                if (sequence[i] == subSequence[j])
                {
                    j++;
                }

                if (j == subSequence.Length) return true;
                i++;
            }

            return j == subSequence.Length;
        }


        // Дана строка в формате: k[encoded_string], где k - число повторений зашифрованной строки.
        // Необходимо вывести результирующую строку, которая соответствует расшифровке исходной строки.
        // Ввод: s = "3[a]2[bc]"   Вывод:  "aaabcbc"
        // Ввод: s = "3[a2[c]]"    Вывод: "accaccacc"
        public static string Decode(string s) // "3[z]2[2[y]pq4[2[jk]e1[f]]]ef", "zzzyypqjkjkefjkjkefjkjkefjkjkefyypqjkjkefjkjkefjkjkefjkjkefef"
        {
            Stack<int> numbers = new();
            Stack<string> letters = new();

            int i = 0;
            while (i < s.Length)
            {
                if (char.IsDigit(s[i]))
                {
                    string number = string.Empty;
                    while (char.IsDigit(s[i]))
                    {
                        number += s[i];
                        i++;
                    }

                    numbers.Push(Convert.ToInt32(number));
                }
                else if (char.IsLetter(s[i]))
                {
                    string str = string.Empty;
                    while (i < s.Length && char.IsLetter(s[i]))
                    {
                        str += s[i];
                        i++;
                    }

                    letters.Push(str);
                }
                else if (s[i] == ']')
                {
                    string str = string.Empty;
                    while (letters.Count > 0 && letters.Peek() != "[")
                    {
                        str = str.Insert(0, letters.Pop());
                    }

                    str = string.Join("", Enumerable.Repeat(str, numbers.Pop()));
                    letters.Pop(); // remove '['
                    letters.Push(str);
                    i++;
                }
                else
                {
                    letters.Push("[");
                    i++;
                }
            }

            string result = string.Empty;
            while (letters.Count > 0)
            {
                result = result.Insert(0, letters.Pop());
            }

            return result;
        }


        // Дано: односвязный список цифр образует число. Необходимо найти сумму двух таких чисел.
        // Ввод: number1 = [2,1,3], number2 = [5,4,6]
        // Вывод: sum = [7,5,9]
        public static LinkedList<int> GetSum(LinkedList<int> n1, LinkedList<int> n2)
        {
            LinkedList<int> result = new();
            result.AddLast(0);
            var point = result.Last;
            var point1 = n1.Last;
            var point2 = n2.Last;

            bool isDozen = false;
            while (point1 != null || point2 != null)
            {
                int digit = (point1?.Value ?? 0) + (point2?.Value ?? 0) + (isDozen ? 1 : 0);
                isDozen = digit >= 10;
                result.AddBefore(point, digit % 10);
                point = result.First;
                point1 = point1?.Previous;
                point2 = point2?.Previous;
            }

            if (isDozen)
            {
                result.AddBefore(point, 1);
            }

            result.RemoveLast();
            return result;
        }


        public static int LengthOfLongestSubstring(string s)
        {
            if (s.Length < 2) return s.Length;

            int start = 0;
            int end = 1;
            int len = 1;
            string sub = string.Empty;

            while (end < s.Length)
            {
                char last = s[end];
                sub = s.Substring(start, end - start);
                if (sub.Contains(last))
                {
                    len = sub.Length > len ? sub.Length : len;
                    start++;
                    end = start + 1;
                }
                else
                {
                    end++;
                }
            }

            sub = s.Substring(start, end - start);
            return sub.Length > len ? sub.Length : len;
        }


        // Решение задачи поиска наибольшей общей подстроки для двух строк s1 и s2, длины которых m и n соответственно,
        // заключается в заполнении таблицы A{ij} размером (m + 1) x (n + 1) по следующему правилу,
        // принимая, что символы в строке нумеруются от единицы: https://en.wikipedia.org/wiki/Longest_common_substring
        // Максимальное число A{uv} в таблице это и есть длина наибольшей общей подстроки.

        public static string GetLongestSubstring(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] A = new int[m + 1, n + 1];
            (int, int) max = (0, 0);

            // Все элементы таблицы равны 0, кроме A[i,j] = A[i-1, j-1] + 1, если s1[i] = s2[j] и i != 0 и j != 0
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        A[i, j] = A[i - 1, j - 1] + 1;
                        if (max.Item1 < A[i, j])
                        {
                            max = (A[i, j], i);
                        }
                    }
                }
            }

            return s1.Substring(max.Item2 - max.Item1, max.Item1);
        }


        //  Manacher's algorithm
        public static string LongestPalindrome(string s)
        {
            if (s.Length < 2) return s;

            char bogus = '|';
            string sBogus = bogus + string.Join(bogus, s.ToCharArray()) + bogus;

            int center = 0;
            int radius = 0;
            int[] radii = new int[sBogus.Length];
            while (center < sBogus.Length)
            {
                while (0 <= center - (radius + 1) && center + (radius + 1) < sBogus.Length &&
                      sBogus[center - (radius + 1)] == sBogus[center + (radius + 1)])
                {
                    radius++;
                }

                radii[center] = radius;

                int oldCenter = center;
                int oldRadius = radius;
                center++;
                radius = 0;
                while (center <= oldCenter + oldRadius)
                {
                    int mirroredCenter = oldCenter - (center - oldCenter);
                    int maxMirroredRadius = oldCenter + oldRadius - center;

                    if (radii[mirroredCenter] < maxMirroredRadius)
                    {
                        radii[center] = radii[mirroredCenter];
                        center++;
                    }
                    else if (radii[mirroredCenter] > maxMirroredRadius)
                    {
                        radii[center] = radii[maxMirroredRadius];
                        center++;
                    }
                    else
                    {
                        radius = maxMirroredRadius;
                        break;
                    }
                }
            }
            int max = radii.Max();
            int index = Array.FindIndex(radii, x => x == max);
            center = (index - 1) / 2;

            return s.Substring(center - (max / 2 - (max % 2 == 0 ? 1 : 0)), max);
        }


        public static string ZigzagConvert(string s, int numRows)
        {
            if (numRows == 1) return s;

            int shift = 0;
            bool isDown = true;
            string[] zigzag = new string[numRows];
            for (int i = 0; i < s.Length; i++)
            {
                zigzag[shift] += s[i];
                if (isDown && shift < numRows - 1)
                {
                    shift++;
                }
                else if (!isDown && shift > 0)
                {
                    shift--;
                }
                else if (isDown && shift == numRows - 1)
                {
                    shift--;
                    isDown = false;
                }
                else if (!isDown && shift == 0)
                {
                    shift++;
                    isDown = true;
                }
            }

            return string.Join("", zigzag);
        }

        public static IList<string> LetterCombinations(string digits)
        {
            int n = 4;
            int[,] board = new int[n, n];
            int[][] board1 = new int[n][];

            int a = board.GetLength(0);
            board[1, 1] = 1;
            board[2, 2] = 1;

            var aa = board.Cast<int>()
                          .Select(x => x == 0 ? '.' : 'Q')
                          .Select(i => new { Row = i / 4, Col = i % 4 });

            Dictionary<char, string> mapping = new()
            {
                ['2'] = "abc", // {'2', "abc"},
                ['3'] = "def",
                ['4'] = "ghi",
                ['5'] = "jkl",
                ['6'] = "mno",
                ['7'] = "pqrs",
                ['8'] = "tuv",
                ['9'] = "wxyz"
            };

            if (string.IsNullOrEmpty(digits)) return new List<string>();

            var strs = digits.Select(x => mapping[x]);
            var result = strs.First().Select(x => x.ToString());

            for (int i = 1; i < digits.Length; i++)
            {
                var next = strs.Skip(i)
                                .First()
                                .Select(x => x.ToString());
                result = result.SelectMany(x => next.Select(y => string.Concat(x, y)));
            }

            return result.ToList();
        }

        // Given an integer array nums of unique elements, return all possible subsets.
        // Using Bit Manipulation: 1 << N, это 2 в степени N.
        // Все двоичные представления этого числа: 0 - элемент не включен, 1 - элемент включен в subset.
        // https://www.geeksforgeeks.org/backtracking-to-find-all-subsets/
        public static IList<IList<int>> Subsets(int[] nums)
        {
            var results = new List<IList<int>>();
            int n = nums.Length;
            int number = 1 << n;
            for (int i = 0; i < number; i++)
            {
                var result = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        result.Add(nums[j]);
                    }

                }

                results.Add(result);
            }

            return results;
        }


        // Дано: два односвязных отсортированных списков типа integer. Merge the two lists in a one sorted list.
        // Ввод: list1 = [1,2,4], list2 = [2,3,6]
        // Вывод: merge = [1,2,2,3,4,6]
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null)
            {
                return null;
            }
            if (list1 == null)
            {
                return list2;
            }
            if (list2 == null)
            {
                return list1;
            }
            if (list1.val <= list2.val)
            {
                list1.next = MergeTwoLists(list1.next, list2);
                return list1;
            }
            else
            {
                list2.next = MergeTwoLists(list1, list2.next);
                return list2;
            }
        }

        public static ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null) return null;

            ListNode total = new();

            for (int i = 0; i < lists.Length; i++)
            {

            }

            return total;
        }


        public static int LongestValidParentheses(string s)
        {
            int longest = 0;
            Stack<int> stack = new();

            const int p1 = -1; // (
            const int p2 = -2; // )
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ')')
                {
                    if (stack.Count > 0 && stack.Peek() == p1)
                    {
                        stack.Pop();
                        stack.Push(2);
                        TotalSum(stack);
                        longest = Math.Max(longest, stack.Peek());
                    }
                    else if (stack.Count > 0 && stack.Peek() > 0)
                    {
                        TotalSum(stack);
                        int sum = stack.Pop();
                        if (stack.Count > 0 && stack.Peek() == p1)
                        {
                            stack.Pop();
                            stack.Push(sum + 2);
                            TotalSum(stack);
                            longest = Math.Max(longest, stack.Peek());
                        }
                        else
                        {
                            stack.Push(sum);
                            stack.Push(p2);
                        }
                    }
                    else
                    {
                        stack.Push(p2);
                    }
                }
                else
                {
                    stack.Push(p1);
                }

            }

            TotalSum(stack);
            return Math.Max(longest, stack.Count > 0 ? stack.Peek() : 0);
        }

        private static void TotalSum(Stack<int> stack)
        {
            int sum = 0;
            while (stack.Count > 0 && stack.Peek() > 0)
            {
                sum += stack.Pop();
            }

            stack.Push(sum);
        }


        public static int SetZeroes(int[] nums)
        {
            bool[] temp = new bool[nums.Length + 1];
            for (int i = 0; i < nums.Length; i++)
            {
                if (0 < nums[i] && nums[i] <= nums.Length)
                {
                    temp[nums[i]] = true;
                }
            }

            var positive = temp.Select((x, i) => new { x, i })
                               .FirstOrDefault(x => 0 < x.i && !x.x);
            return positive == null ? temp.Length : positive.i;
        }


        public static int SetZeroes1()
        {
            int[][] matrix = new int[3][];
            matrix[0] = new int[4];
            matrix[1] = new int[5];
            matrix[2] = new int[6];

            Tuple<int, int> t = new Tuple<int, int>(1, 1);

            int m = matrix.Length; // Размерность массива = 3
            int m1 = matrix.Rank;  // Количество измерений = 1, в массиве new int[,] Rank = 2
            int n = matrix.GetLength(0);
            int n2 = matrix.GetLowerBound(0); // Индекс первого элемента в размерности = 0
            int n4 = matrix.GetUpperBound(0); // Индекс последнего элемента в размерности = 2

            return 0;
        }

        public static double FindMedian(int[] nums)
        {
            int index = 0;
            int count = 0;
            int itemsBeforeMedian = 0;
            SortedList<int, int> list = new();

            foreach (var num in nums)
            {
                //int key = list.GetKeyAtIndex(index);
                int key = count == 0 ? num : list.Keys[index]; // 6, 10, 2, 6, 5, 0, 6, 3, 1, 0, 0
                if (!list.ContainsKey(num))
                {
                    list.Add(num, 0);
                }

                list[num]++;
                count++;

                if (num < key) itemsBeforeMedian++;
                if (list[num] == 1 && num < key) index++;

                int median = count / 2;
                if (itemsBeforeMedian > median)
                {
                    index--;
                    itemsBeforeMedian -= list.Values[index];
                }
                else if (itemsBeforeMedian + list[key] == median)
                {
                    itemsBeforeMedian += list[key];
                    index++;
                }
            }


             if (count % 2 == 1)
            {
                return list.Keys[index];
            }

            int middle1 = list.Keys[index];
            int middle2 = list.Keys[index];
            if (itemsBeforeMedian == count / 2)
            {
                middle2 = list.Keys[index - 1];
            }

            return (middle1 + middle2) / 2.0;
        }

        private static int GetTargetIndex(int[] nums, int target, Func<int, int> increment, int start)
        {
            int i = start;
            while (0 <= i && i < nums.Length && nums[i] == target)
            {
                i = increment(i);
            }

            return i;
        }

        public static bool WordSearch(char[][] board, string word)
        {
            int m = board.Length;
            int n = board[0].Length;

            List<Tuple<int, int>> letters = new();
            List<Tuple<int, int>> directions = new List<Tuple<int, int>>()
            {
                Tuple.Create(1, 0), // down
                Tuple.Create(-1, 0),// up
                Tuple.Create(0, 1), // right
                Tuple.Create(0, -1),// left
            };

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Solve(i, j, 0))
                    {
                        return true;
                    }
                }
            }

            bool Solve(int i, int j, int k)
            {
                if (k == word.Length - 1) return board[i][j] == word[k];
                if (board[i][j] != word[k]) return false;

                char c = board[i][j];
                board[i][j] = ' ';
                foreach (var direction in directions)
                {
                    int x = i + direction.Item1;
                    int y = j + direction.Item2;
                    if (0 <= x && 0 <= y && x < m && y < n && board[x][y] != ' ' && Solve(x, y, k + 1))
                    {
                        return true;
                    }
                }

                board[i][j] = c;
                return false;
            }

            return false;
        }


        public static IList<int> FindAnagrams(string s, string p)
        {
            IList<int> answer = new List<int>();
            int start = 0;
            int letters = 0;
            bool prevAnagram = false;
            Dictionary<char, int> dictS = new();
            Dictionary<char, int> dictP = GetDictionary(p);
            foreach (var item in dictP)
            {
                dictS.Add(item.Key, 0);
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (prevAnagram && s[i] == s[i - p.Length])
                {
                    answer.Add(i - p.Length + 1);
                    continue;
                }

                prevAnagram = false;
                if (!dictP.ContainsKey(s[i]))
                {
                    foreach (var item in dictS) dictS[item.Key] = 0;
                    start = i + 1;
                    letters = 0;
                    continue;
                }

                //if (i >= p.Length && dictS.ContainsKey(s[i - p.Length]) && dictS[s[i - p.Length]] > 0)
                if (i - start >= p.Length && dictS.ContainsKey(s[i - p.Length]) && dictS[s[i - p.Length]] > 0)
                {
                    letters--;
                    dictS[s[i - p.Length]]--;
                }

                letters++;
                dictS[s[i]]++;

                if (letters == p.Length && dictP.SequenceEqual(dictS))
                {
                    answer.Add(i - p.Length + 1);
                    prevAnagram = true;
                }
            }

            return answer;
        }

        private static Dictionary<char, int> GetDictionary(string p)
        {
            Dictionary<char, int> dict = new();
            while (p.Length > 0)
            {
                char letter = p[0];
                string str = p.Replace(letter.ToString(), "");
                dict.Add(letter, p.Length - str.Length);
                p = str;
            }

            return dict;
        }

        public static int CoinChange(int[] coins, int amount)
        {
            //Build DP array
            int[] dp = new int[amount + 1];
            //Fill array with Amount + 1
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = amount + 1;
            }
            //Base case
            dp[0] = 0;

            //Outer loop is for coins
            foreach (int coin in coins)
            {
                //Iterate over DP array to find how many coins needed to form that total of amount
                //Start from the denomination of coin as it's not possible to make total less than the denomination with this coin
                for (int i = coin; i <= amount; i++)
                {
                    //Update the DP array with minimum number of coins needed to make the total
                    dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
            //Catch condition where if its not possible to form total with given coins return -1
            return dp[amount] <= amount ? dp[amount] : -1;
        }

        public static int LongestCommonSubsequence(string text1, string text2)
        {
            int m = text1.Length;
            int n = text2.Length;

            return LCS(text1, text2, m, n);
        }

        // Returns length of LCS for X[0..m-1], Y[0..n-1]
        private static int LCS(string text1, string text2, int m, int n)
        {
            int[,] L = new int[m + 1, n + 1];

            // Following steps build L[m+1][n+1] in bottom up fashion.
            // Note that L[i][j] contains length of LCS of X[0..i-1] and Y[0..j-1]
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        L[i, j] = 0;
                    else if (text1[i - 1] == text2[j - 1])
                        L[i, j] = L[i - 1, j - 1] + 1;
                    else
                        L[i, j] = Max(L[i - 1, j], L[i, j - 1]);
                }
            }

            return L[m, n];
        }

        private static int Max(int a, int b) => (a > b) ? a : b;


        // *************************************************** TreeNode *****************************************
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public static IList<int> InorderTraversal(TreeNode root)
        {
            IList<int> inorder = new List<int>();
            InOrder(root);

            void InOrder(TreeNode root)
            {
                if (root == null) return;
                if (root.left != null) InOrder(root.left);
                inorder.Add(root.val);
                if (root.right != null) InOrder(root.right);
            }

            return inorder;
        }


        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<int> level = new List<int>();
            IList<IList<int>> levels = new List<IList<int>>();
            if (root == null) return levels;

            Queue<TreeNode> queue = new();
            queue.Enqueue(root);
            queue.Enqueue(null);

            while (queue.Count != 0)
            {
                TreeNode node = queue.Dequeue();
                if (node == null)
                {
                    if (level.Count == 0) break;
                    levels.Add(level);
                    level = new List<int>();
                    queue.Enqueue(null);
                }
                else
                {
                    level.Add(node.val);
                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
            }

            return levels;
        }

        public static int SolveTheCode()
        {
            const string digits = "123456789";
            int result = -1;
            for (int i = 123456789; i <= 987654321; i++)
            {
                string s = i.ToString();
                if (s.Contains('0') || digits.Any(c => !s.Contains(c))) continue;

                int temp = i;
                for (int j = digits.Length; j > 1; j--)
                {
                    if (temp % j != 0) break;
                    temp /= 10;

                    if (temp < 10) return i;
                }
            }

            return result;
        }

        public static int MaxDifferent(string str)
        {
            int max = 0;
            int i = 0;
            Dictionary<char, int> dict = new();
            while (i < str.Length)
            {
                char symbol = str[i];
                if (!dict.ContainsKey(symbol))
                {
                    dict.Add(symbol, i);
                    i++;
                }
                else
                {
                    max = Math.Max(max, dict.Count);
                    i = dict[symbol] + 1;
                    dict.Clear();
                }
            }

            return Math.Max(max, dict.Count);
        }

        public static void GroupByTheSame()
        {
            string text = "ababbc";
            var sequences = Regex.Matches(text, @"([a-z])\1*")
                                 .Select(m => new { c = m.Value[0], n = m.Value.Length });

            var aa = text.GroupBy(x => x)
                         .ToDictionary(x => x.Key, value => value.Count());
                         //.Select(x => new { c = x.Key, n = x.Count() })
                         //.FirstOrDefault(x => x.n == 1);
        }
    }
}
