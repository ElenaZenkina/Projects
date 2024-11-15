using NUnit.Framework;
using System;
using System.Collections.Generic;
using Example;

namespace ExamplesTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void TestRepeatedSymbol()
        {
            Assert.AreEqual("? ? ?", Examples.RepeatedSymbol("??? ?? ??"));
        }

        [Test]
        public static void TestRepeatedSymbol1()
        {
            Assert.AreEqual(true, Examples.RepeatedSymbol2("??? ?? ??"));
            Assert.AreEqual(true, Examples.RepeatedSymbol2("12311123"));
            Assert.AreEqual(true, Examples.RepeatedSymbol2("ee eeee   efg"));
            Assert.AreEqual(false, Examples.RepeatedSymbol2("123123123"));
            Assert.AreEqual(false, Examples.RepeatedSymbol2("1231 11 23"));
            Assert.AreEqual(false, Examples.RepeatedSymbol2("ee ee ee"));
            Assert.AreEqual(false, Examples.RepeatedSymbol2("qwerty"));
        }

        [Test]
        public static void TestRepeatedSymbol2()
        {
            Assert.AreEqual(5, Examples.RepeatedSymbol3("abbcccddddccccceeeeffg"));
        }

        [Test]
        public static void TestSuffle()
        {
            var data = new List<int>(){ 1, 2, 3, 4, 5};
            var old = new List<int>() { 1, 2, 3, 4, 5};
            data.Shuffle();
            Assert.AreNotEqual(old, data);
        }


        [Test]
        public static void GetPairForSumTest()
        {
            var array = new[] { -3, 0, 2, 4, 5 };
            int k = 6;
            var actual = Examples.GetPairForSum(array, k);
            Assert.AreEqual((2, 4), actual);
        }

        [Test]
        public static void GetOrderedPairForSumTest()
        {
            var array = new[] { -3, 0, 2, 4, 5 };
            int k = 6;
            var actual = Examples.GetOrderedPairForSum(array, k);
            Assert.AreEqual((2, 4), actual);
        }

        [Test]
        public static void TemperaturesTest()
        {
            var array = new[] { 13, 12, 15, 11, 9, 12, 16 };
            var temps = new[] { 2, 1, 4, 2, 1, 1, 0 };
            var actual = Examples.Temperatures(array);
            Assert.AreEqual(temps, actual);
        }

        [TestCase(3, 2, 3)]
        [TestCase(4, 5, 35)]
        public static void PathsTest(int n, int m, int paths)
        {
            var actual = Examples.Paths(n, m);
            var actualOpt = Examples.PathsOptimize(n, m);
            Assert.AreEqual(paths, actual);
            Assert.AreEqual(paths, actualOpt);
        }



        [TestCase("()[][{}]", true)]
        [TestCase("(]", false)]
        [TestCase(")[][{}]", false)]
        [TestCase("(Hello)[][{http://t.me/my}]", true)]
        [TestCase("[{}aa]", true)]
        public static void IsBalancedTest(string text, bool result)
        {
            var actual = Examples.IsBalanced(text);
            Assert.AreEqual(result, actual);
        }


        [Test]
        public static void SortBubbleTest()
        {
            var array = new[] { 3, 2, 5, 1, 9, 6 };
            var expected = new[] { 1, 2, 3, 5, 6, 9 };
            Examples.SortBubble(array);
            Assert.AreEqual(expected, array);
        }

        [Test]
        public static void SortShellTest()
        {
            var array = new[] { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Examples.SortShell(array);
            Assert.AreEqual(expected, array);
        }

        [Test]
        public static void QuickSortBooktTest()
        {
            var array = new[] { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Examples.QuickSortBook(array, 0, array.Length - 1);
            Assert.AreEqual(expected, array);
        }

        [Test]
        public static void QuickSortTest()
        {
            var array = new[] { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Examples.QuickSort(array, 0, array.Length - 1);
            Assert.AreEqual(expected, array);
        }


        [TestCase("aaaa", true)]
        [TestCase("abca", false)]
        [TestCase("asas", false)]
        [TestCase("abcdcba", true)]
        [TestCase("aabccccbaa", true)]
        public static void IsPalindromTest(string text, bool result)
        {
            Assert.AreEqual(Examples.IsPalindrom(text, 0, text.Length - 1), result);
        }


        [TestCase(new int[] { 3, 1, 2, 2, 1}, new int[] { 10, 3, 9, 5, 6 }, 6, 25)]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 10, 15, 40 }, 6, 65)]
        [TestCase(new int[] { 10, 20, 30 }, new int[] { 60, 100, 120 }, 50, 220)]
        public static void KnapsackProblemTest(int[] weights, int[] prices, int capacity, int result)
        {
            Tuple<int, int>[] items = new Tuple<int, int>[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                items[i] = Tuple.Create(weights[i], prices[i]);
            }

            Assert.AreEqual(result, Examples.KnapsackProblem(items, capacity));
        }

    }
}
