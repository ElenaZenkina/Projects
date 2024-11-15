using NUnit.Framework;
using System.Collections.Generic;
using Tasks;
using static Tasks.LeetCode;

namespace LeetCodeTest
{
    public class Test
    {
        [TestCase("abc", "abc", true)]
        [TestCase("ab", "abc", false)]
        [TestCase("awebeeeceee", "abc", true)]
        [TestCase("aaaawebeeeceee", "abc", true)]
        [TestCase("aaaawecbeeeeee", "abc", false)]
        public void isSubSequenceTest(string sequence, string subSequence, bool isTue)
        {
            Assert.AreEqual(LeetCode.isSubSequence(sequence, subSequence), isTue);
        }


        [TestCase("3[a]2[bc]", "aaabcbc")]
        [TestCase("1[a]2[bc]3[dx]", "abcbcdxdxdx")]
        [TestCase("2[abc]3[cd]ef", "abcabccdcdcdef")]
        [TestCase("3[a2[c]]", "accaccacc")]
        [TestCase("2[a2[ac4[x]]]", "aacxxxxacxxxxaacxxxxacxxxx")]
        [TestCase("3[z]2[2[y]pq4[2[jk]e1[f]]]ef", "zzzyypqjkjkefjkjkefjkjkefjkjkefyypqjkjkefjkjkefjkjkefjkjkefef")]
        public void DecodeTest(string input, string output)
        {
            Assert.AreEqual(LeetCode.Decode(input), output);
        }

        [TestCase(new[] { 1, 2, 3 }, new[] { 5, 6, 4 }, new[] { 6, 8, 7 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 9, 6, 4 }, new[] { 1, 0, 8, 7 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 6, 4 }, new[] { 1, 2, 4, 8, 7 })]
        [TestCase(new[] { 9, 9, 9, 9, 9, 9, 9 }, new[] { 9, 9, 9, 9 }, new[] { 1, 0, 0, 0, 9, 9, 9, 8 })]
        public void GetSumTest(int[] a, int[] b, int[] c)
        {
            LinkedList<int> n1 = new(a);
            LinkedList<int> n2 = new(b);
            LinkedList<int> sum = new(c);

            Assert.AreEqual(LeetCode.GetSum(n1, n2), sum);
        }


        [TestCase("dvdf", 3)]
        [TestCase("au", 2)]
        [TestCase("pwwkew", 3)]
        [TestCase("abcabcbb", 3)]
        [TestCase("bbbbbb", 1)]
        public void LengthOfLongestSubstringTest(string s, int len)
        {
            Assert.AreEqual(LeetCode.LengthOfLongestSubstring(s), len);
        }

        [TestCase("SUBSEQUENCE ", "SUBEUENCS", "UENC")]
        public void GetLongestSubstringTest(string s1, string s2, string s3)
        {
            Assert.AreEqual(LeetCode.GetLongestSubstring(s1, s2), s3);
        }


        [TestCase("abacdfgdcaba ", "aba")]
        [TestCase("cbabad ", "bab")]
        [TestCase("bb ", "bb")]
        [TestCase("cabbad ", "abba")]
        [TestCase("abacaba ", "abacaba")]
        public void LongestPalindromeTest(string s1, string s2)
        {
            Assert.AreEqual(LeetCode.LongestPalindrome(s1), s2);
        }


        [TestCase("PAYPALISHIRING", 1, "PAYPALISHIRING")]
        [TestCase("PAYPALISHIRING", 2, "PYAIHRNAPLSIIG")]
        [TestCase("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")]
        [TestCase("PAYPALISHIRING", 4, "PINALSIGYAHRPI")]
        public void ZigzagConvertTest(string s1, int n, string s2)
        {
            Assert.AreEqual(LeetCode.ZigzagConvert(s1, n), s2);
        }


        [TestCase("23", new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" })]
        public void LetterCombinationsTest(string digits, string[] answers)
        {
            Assert.AreEqual(LeetCode.LetterCombinations(digits), answers);
        }



        [Test]
        public void MergeTwoListsTest()
        {
            LeetCode.ListNode list1 = new(1);
            list1.next = new(2);
            list1.next.next = new(4);
            LeetCode.ListNode list2 = new(1);
            list2.next = new(3);
            list2.next.next = new(4);

            LeetCode.ListNode a = LeetCode.MergeTwoLists(list1, list2);
            Assert.AreEqual(5, 5);
        }


        [TestCase("(()", 2)]
        [TestCase("())()", 2)]
        [TestCase("()(()", 2)]
        [TestCase(")()())", 4)]
        [TestCase("()(())", 6)]
        [TestCase(")()(((())))(", 10)]
        [TestCase(")(((((()())()()))()(()))(", 22)]
        public void LongestValidParenthesesTest(string s, int n)
        {
            int p = LeetCode.LongestValidParentheses(s);
            Assert.AreEqual(n, p);
        }


        [TestCase(2, new[] { 1 })]
        [TestCase(3, new[] { 1, 2, 0 })]
        [TestCase(2, new[] { 3, 4, 0, -1, 1 })]
        [TestCase(1, new[] { 3, 4, 7, 8, 9, 10, 11, 12 })]
        public void SetZeroesTest(int min, int[] nums)
        {
            var m = LeetCode.SetZeroes(nums);
            Assert.AreEqual(m, min);
        }


        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 1, 2 }, 1.5)]
        [TestCase(new[] { 1, 2, 5 }, 2)]
        [TestCase(new[] { 1, 2, 5, 7 }, 3.5)]
        [TestCase(new[] { -1, -2, -3, -4, -5 }, -3)]
        [TestCase(new[] { 6, 10, 2, 6, 5, 0, 6, 3, 1, 0, 0 }, 3)]
        public void FindMedianTest(int[] nums, double expected)
        {
            var actual = LeetCode.FindMedian(nums);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(new[] { 1, 2, 5 }, 11, 3)]
        [TestCase(new[] { 3, 4, 5 }, 11, 3)]
        public void CoinChangeTest(int[] nums, int amount, int expected)
        {
            var actual = LeetCode.CoinChange(nums, amount);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new string[] { "ABCE", "SFCS", "ADEE" }, "ABCCED", true)]
        [TestCase(new string[] { "ABCE", "SFCS", "ADEE" }, "SEE", true)]
        [TestCase(new string[] { "ABCE", "SFCS", "ADEE" }, "ABCB", false)]
        public void WordSearchTest(string[] cross, string word, bool expected)
        {
            char[][] board = new char[cross.Length][];
            for (int i = 0; i < cross.Length; i++)
            {
                board[i] = cross[i].ToCharArray();
            }

            var actual = LeetCode.WordSearch(board, word);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("cbaebabacd", "abc", new[] { 0, 6 })]
        [TestCase("abab", "ab", new[] { 0, 1, 2 })]
        public void FindAnagramsTest(string s, string p, int[] expected)
        {
            var actual = LeetCode.FindAnagrams(s, p);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("ezupkr", "ubmrapg", 2)]
        [TestCase("mhunuzqrkzsnidwbun", "szulspmhwpazoxijwbq", 6)]
        public void LongestCommonSubsequenceTest(string text1, string text2, int expected)
        {
            var actual = LeetCode.LongestCommonSubsequence(text1, text2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InorderTraversalTest()
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.left.left = new TreeNode(4);
            root.left.right = new TreeNode(5);
            root.right.right = new TreeNode(6);

            Assert.AreEqual(new[] { 4, 2, 5, 1, 3, 6 }, InorderTraversal(root));
        }


        [Test]
        public void LevelOrderTest()
        {
            TreeNode root = new TreeNode(3);
            root.left = new TreeNode(9);
            root.right = new TreeNode(20);
            root.right.left = new TreeNode(15);
            root.right.right = new TreeNode(7);

            Assert.AreEqual(new[] { 4, 2, 5, 1, 3, 6 }, LevelOrder(root));
        }


        [Test]
        public void SolveTheCodeTest()
        {
            Assert.AreEqual(381654729, SolveTheCode());
        }


        [TestCase("lena", 4)]
        [TestCase("slava", 4)]
        [TestCase("abcdecabdefg", 7)]
        public void MaxDifferentTest(string str, int expected)
        {
            Assert.AreEqual(expected, LeetCode.MaxDifferent(str));
        }

    }

}
