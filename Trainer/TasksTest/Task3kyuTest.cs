 using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tasks;

namespace Task3kyuTest
{
    class Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Spiralize5Test()
        {
            int[,] expected = new int[,]
            {
                {1, 1, 1, 1, 1},
                {0, 0, 0, 0, 1},
                {1, 1, 1, 0, 1},
                {1, 0, 0, 0, 1},
                {1, 1, 1, 1, 1}
            };

            var actual = Task3kyu.Spiralize(5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Spiralize8Test()
        {
            int[,] expected = new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 1},
                {1, 0, 1, 0, 0, 1, 0, 1},
                {1, 0, 1, 1, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
            };

            var actual = Task3kyu.Spiralize(8);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SnailTest()
        {
            var a0 = new int[0][] { };
            var a1 = new int[][] { new[] { 1 } };
            var a2 = new int[][] { new[] { 1, 2 }, new[] { 3, 4 } };
            var a3 = new int[][] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, new[] { 7, 8, 9 } };
            //var a4 = new int[][] { new[] { 1, 2, 3, 4}, new[] { 5, 6, 7, 8 }, new[] { 9, 10, 11, 12 }, new[] { 13, 14, 15, 16 } };
            var a4 = new int[][] { new[] { 1, 2, 3, 4}, new[] { 12, 13, 14, 5 }, new[] { 11, 16, 15, 6 }, new[] { 10, 9, 8, 7 } };
            //var a5 = new int[][] { new[] { 1, 2, 3, 4, 5 }, new[] { 6, 7, 8, 9, 10 }, new[] { 11, 12, 13, 14, 15 }, new[] { 16, 17, 18, 19, 20 }, new[] { 21, 22, 23, 24, 25 } };
            var a5 = new int[][] { new[] { 1, 2, 3, 4, 5 }, new[] { 16, 17, 18, 19, 6 }, new[] { 15, 24, 25, 20, 7 }, new[] { 14, 23, 22, 21, 8 }, new[] { 13, 12, 11, 10, 9 } };

            Assert.AreEqual(Task3kyu.Snail(a0), new int[] { });
            Assert.AreEqual(Task3kyu.Snail(a1), new int[] { 1 });
            Assert.AreEqual(Task3kyu.Snail(a2), new int[] { 1, 2, 4, 3 });
            Assert.AreEqual(Task3kyu.Snail(a3), new int[] { 1, 2, 3, 6, 9, 8, 7, 4, 5 });
            Assert.AreEqual(Task3kyu.Snail(a4), Enumerable.Range(1, 16).ToArray());
            Assert.AreEqual(Task3kyu.Snail(a5), Enumerable.Range(1, 25));
        }

        [Test]
        public void PyramidTest()
        {
            Assert.AreEqual(new int[][] { }, Task3kyu.Pyramid(0));
            Assert.AreEqual(new int[][] { new int[] { 1 } }, Task3kyu.Pyramid(1));
            Assert.AreEqual(new int[][] { new int[] { 1 }, new int[] { 1, 1 } }, Task3kyu.Pyramid(2));
            Assert.AreEqual(new int[][] { new int[] { 1 }, new int[] { 1, 1 }, new int[] { 1, 1, 1 } }, Task3kyu.Pyramid(3));
        }



        [TestCase(0, 1, new[] { 2 })]
        [TestCase(0, 2, new[] { 2, 3 })]
        [TestCase(0, 3, new[] { 2, 3, 5 })]
        [TestCase(0, 10, new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })]
        [TestCase(10, 10, new[] { 31, 37, 41, 43, 47, 53, 59, 61, 67, 71 })]
        [TestCase(100, 10, new[] { 547, 557, 563, 569, 571, 577, 587, 593, 599, 601 })]
        [TestCase(1000, 10, new[] { 7927, 7933, 7937, 7949, 7951, 7963, 7993, 8009, 8011, 8017 })]
        public void GetPrimesTest(int skip, int n, int[] actual)
        {
            var expected = Task3kyu.GetPrimes().Skip(skip).Take(n);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(0, 1, new[] { 2 })]
        [TestCase(0, 2, new[] { 2, 3 })]
        [TestCase(0, 3, new[] { 2, 3, 5 })]
        [TestCase(0, 10, new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })]
        [TestCase(10, 10, new[] { 31, 37, 41, 43, 47, 53, 59, 61, 67, 71 })]
        [TestCase(100, 10, new[] { 547, 557, 563, 569, 571, 577, 587, 593, 599, 601 })]
        [TestCase(1000, 10, new[] { 7927, 7933, 7937, 7949, 7951, 7963, 7993, 8009, 8011, 8017 })]
        public void GetPrimesByAtkinTest(int skip, int n, int[] actual)
        {
            var expected = Task3kyu.GetPrimesByAtkin().Skip(skip).Take(n);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(0, 0)]
        [TestCase(9, 0)]
        [TestCase(10, 2)]
        [TestCase(11, 3)]
        [TestCase(12, 6)] // 1 - 2; 2 - 4; 3 - 8; 4 - 6
        [TestCase(12, 7)]
        [TestCase(12, 8)]
        [TestCase(12, 9)]
        [TestCase(13, 6)] // 1 - 3, 2 - 9, 3 - 7, 4 - 1
        [TestCase(13, 7)]
        [TestCase(13, 8)]
        [TestCase(13, 9)]
        [TestCase(14, 6)] // 1 - 4, 2 - 6
        [TestCase(14, 7)]
        [TestCase(15, 4)]
        [TestCase(16, 7)]
        [TestCase(17, 6)] // 1 - 7; 2 - 9; 3 - 3; 4 - 1;
        [TestCase(17, 7)]
        [TestCase(17, 8)]
        [TestCase(17, 9)]
        [TestCase(18, 6)] // 1 - 8; 2 - 4; 3 - 2; 4 - 6;
        [TestCase(18, 7)]
        [TestCase(18, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 8)] // 1 - 9, 2 - 1
        [TestCase(19, 9)]

        public void GetLastDigitTest(int n1, int n2) // n1^n2
        {
            long pow = (long)Math.Pow(n1, n2);
            int actual = (int)(pow % 10);
            var expected = Task3kyu.GetLastDigit(n1, n2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 1)]
        [TestCase(new int[] {0, 0}, 1)]
        [TestCase(new int[] {0, 0, 0}, 0)]
        [TestCase(new int[] { 1, 2 }, 1)]
        [TestCase(new int[] { 3, 4, 5 }, 1)]
        [TestCase(new int[] { 4, 3, 6 }, 4)]
        [TestCase(new int[] { 7, 6, 21 }, 1)]
        [TestCase(new int[] { 2, 2, 2, 0 }, 4)]
        [TestCase(new int[] { 12, 30, 21 }, 6)]
        [TestCase(new int[] { 2, 2, 101, 2 }, 6)]
        [TestCase(new int[] { 82242, 254719, 736371 }, 8)]
        [TestCase(new int[] { 937640, 767456, 981242 }, 0)]
        [TestCase(new int[] { 123232, 694022, 140249 }, 6)]
        [TestCase(new int[] { 499942, 898102, 846073 }, 6)]
        public void GetArrayLastDigitTest(int[] array, int expected) // n0^n1^n2...
        {
            var actual = Task3kyu.GetArrayLastDigit(array);
            Assert.AreEqual(expected, actual);
        }


        [TestCase(0, 0, 50, 30)]
        [TestCase(70, 40, 10, 20)]
        [TestCase(-100, -50, 200, 80)]
        public void DrawLineTest(int x1, int y1, int x2, int y2)
        {
            //Task3kyu.DrawLine(x1, y1, x2, y2);
        }


        [TestCase(30, 20, 15)]
        public void DrawCircleTest(int x, int y, int r)
        {
            //Task3kyu.DrawCircle(x, y, r);
        }

        [TestCase(50, 25, 30, 20, 45)]
        public void DrawEllipseTest(int x, int y, int rx, int ry, int angle)
        {
            //Task3kyu.DrawEllipse(x, y, rx, ry, angle);
        }


        [TestCase("5+2", "5 2 +")]
        [TestCase("3*(5+2)", "3 5 2 + *")]
        [TestCase("7-2*3", "7 2 3 * -")]
        [TestCase("27-Math.Pow(2,3)", "27 2 3 ^ -")]
        [TestCase("Math.Pow(2,3)+Math.Pow(2,4)/8", "2 3 ^ 2 4 ^ 8 / +")]
        [TestCase("7-Math.Sin(2)", "7 2 s -")]
        [TestCase("Math.Cos(4)-Math.Pow(2,3)", "4 c 2 3 ^ -")]

        [TestCase("x*x*0.05-15", "x x * 0.05 * 15 -")]
        [TestCase("Math.Sin(x*0.2)*23", "x 0.2 * s 23 *")]
        [TestCase("Math.Pow(x,2)*0.05-15", "x 2 ^ 0.05 * 15 -")]
        [TestCase("Math.Pow(x,3)*0.001", "x 3 ^ 0.001 *")]
        [TestCase("Math.Tan(x*0.05)*2", "x 0.05 * t 2 *")]
        [TestCase("0.3*Math.Pow(x,2)-Math.Pow(x,3)*0.03+x*0.2+2", "0.3 x 2 ^ * x 3 ^ 0.03 * - x 0.2 * + 2 +")]
        public void CreatePostfixTest(string exp, string postfix)
        {
            string expected = Task3kyu.CreatePostfix(exp);
            Assert.AreEqual(expected, postfix);
        }

#region Delete
        [TestCase("5 2 +", 7.0)]
        [TestCase("3 5 2 + *", 21.0)]
        [TestCase("7 2 3 * -", 1.0)]
        [TestCase("27 2 3 ^ -", 19)]
        [TestCase("2 3 ^ 2 4 ^ 8 / +", 10)]
        [TestCase("7 2 s -", 6.09070257317)]
        [TestCase("4 c 2 3 ^ -", -8.6536436)]
        public void CalculatePostfixTest(string postfix, double result)
        {
            double expected = Task3kyu.Calculate(postfix);
            Assert.AreEqual(expected, result);
        }
#endregion

        [TestCase("5+2", 7)]
        [TestCase("3*(5+2)", 21)]
        [TestCase("7/(3*(5-2)-21/3)", 3.5)]
        [TestCase("3.1*(5.5-3.4)-21/3", -0.49)]
        [TestCase("3.1-Math.Pow(2.5,1.4)", -0.5067)]
        [TestCase("Math.Sin(2*4)+6/2", 3.98935824)]
        [TestCase("Math.Sin(2*4)+Math.Pow(2*2,4/2)", 16.98935824)]

        // x = 10
        [TestCase("10*10*0.05-15", -10)]
        [TestCase("Math.Sin(10*0.2)*23", 20.913840)]
        [TestCase("Math.Pow(10,2)*0.05-15", -10)]
        [TestCase("Math.Pow(10,3)*0.001", 1)]
        [TestCase("Math.Tan(10*0.05)*2", 1.0926)]
        [TestCase("0.3*Math.Pow(10,2)-Math.Pow(10,3)*0.03+10*0.2+2", 4)]

        [TestCase("15/(7-(1+1))*3-(2+(1+1))*15/(7-(200+1))*3-(2+(1+1))*(15/(7-(1+1))*3-(2+(1+1))+15/(7-(1+1))*3-(2+(1+1)))", -30.0721649485)]
        public void CalculateTest(string expression, double result)
        {
            string postfix = Task3kyu.CreatePostfix(expression);
            double expected = Task3kyu.Calculate(postfix);
            Assert.LessOrEqual(Math.Abs(Math.Abs(expected) - Math.Abs(result)), 0.0001);
        }


        [Test]
        public void DeterminantTest()
        {
            int[][][] matrix =
            {
                new int[][] { new [] { 1 } },
                new int[][] { new [] { 1, 3 }, new [] { 2, 5 } },
                new int[][] { new [] { 2, 5, 3 }, new [] { 1, -2, -1 }, new [] { 1, 3, 4 } },
                new int[][] { new [] { 3, -3, -5, 8 }, new [] { -3, 2, 4, -6 }, new [] { 2, -5, -7, 5 }, new[] { -4, 3, 5, -6 } }
            };

            int[] expected = { 1, -1, -20, 18 };

            for (int n = 0; n < expected.Length; n++)
            {
                Assert.AreEqual(expected[n], Task3kyu.DeterminantCalculate(matrix[n]));
            }
        }

        [Test]
        public void TestParseInt()
        {
            Assert.AreEqual(-1, Task3kyu.ParseInt("ff-"));
            Assert.AreEqual(0, Task3kyu.ParseInt("zero"));
            Assert.AreEqual(7, Task3kyu.ParseInt("seven"));
            Assert.AreEqual(12, Task3kyu.ParseInt("twelve"));
            Assert.AreEqual(19, Task3kyu.ParseInt("nineteen"));
            Assert.AreEqual(22, Task3kyu.ParseInt("twenty-two"));
            Assert.AreEqual(50, Task3kyu.ParseInt("fifty"));
            Assert.AreEqual(64, Task3kyu.ParseInt("sixty-four"));
            Assert.AreEqual(91, Task3kyu.ParseInt("ninety-one"));
            Assert.AreEqual(131, Task3kyu.ParseInt("one hundred and thirty-one"));
            Assert.AreEqual(246, Task3kyu.ParseInt("two hundred forty-six"));
            Assert.AreEqual(300, Task3kyu.ParseInt("three hundred"));
            Assert.AreEqual(302, Task3kyu.ParseInt("three hundred two"));
            Assert.AreEqual(311, Task3kyu.ParseInt("three hundred eleven"));
            Assert.AreEqual(320, Task3kyu.ParseInt("three hundred twenty"));
            Assert.AreEqual(586, Task3kyu.ParseInt("five hundred eighty-six"));
            Assert.AreEqual(5000, Task3kyu.ParseInt("five thousand"));
            Assert.AreEqual(5200, Task3kyu.ParseInt("five thousand two hundred"));
            Assert.AreEqual(5245, Task3kyu.ParseInt("five thousand two hundred forty-five"));
            Assert.AreEqual(17017, Task3kyu.ParseInt("seventeen thousand seventeen"));
            Assert.AreEqual(25389, Task3kyu.ParseInt("twenty-five thousand three hundred eighty-nine"));
            Assert.AreEqual(52601, Task3kyu.ParseInt("fifty-two thousand six hundred one"));
            Assert.AreEqual(100000, Task3kyu.ParseInt("one hundred thousand"));
            Assert.AreEqual(100100, Task3kyu.ParseInt("one hundred thousand one hundred"));
            Assert.AreEqual(205008, Task3kyu.ParseInt("two hundred five thousand eight"));
            Assert.AreEqual(783919, Task3kyu.ParseInt("seven hundred eighty-three thousand nine hundred nineteen"));
            Assert.AreEqual(1000000, Task3kyu.ParseInt("one million"));
        }

        [Test]
        public void TestGetAllNeighbors()
        {
            int[,] cells1 = new int[,]
            {
                { 1, 0, 0 },
                { 0, 1, 1 },
                { 1, 1, 0 }
            };

            int[,] cells2 = new int[,]
            {
                { 1, 0, 0, 1 },
                { 0, 1, 1, 0 },
                { 1, 1, 0, 0 }
            };

            Assert.AreEqual(4, Task3kyu.GetAllNeighbors(cells1, 1, 1));
            Assert.AreEqual(1, Task3kyu.GetAllNeighbors(cells1, 0, 0));
            Assert.AreEqual(3, Task3kyu.GetAllNeighbors(cells1, 2, 1));
            Assert.AreEqual(3, Task3kyu.GetAllNeighbors(cells2, 2, 2));
            Assert.AreEqual(3, Task3kyu.GetAllNeighbors(cells2, 1, 2));
            Assert.AreEqual(1, Task3kyu.GetAllNeighbors(cells2, 2, 3));
        }

        [Test]
        public void TestGetGetAroundCells()
        {
            int[,] cells1 = new int[,]
            {
                { 0, 1, 1 },
                { 0, 0, 1 },
                { 1, 0, 1 }
            };

            int[,] next1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };

            int[,] cells2 = new int[,]
{
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
};

            int[,] next2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };


            Assert.AreEqual(next1, Task3kyu.GetAroundCells(cells1));
            Assert.AreEqual(next2, Task3kyu.GetAroundCells(cells2));
        }

        [Test]
        public void TestGetNextGeneration()
        {
            int[,] cells1 = new int[,]
            {
                { 1, 0, 0, 1 },
                { 0, 1, 1, 0 },
                { 1, 1, 0, 0 }
            };

            int[,] next1 = new int[,]
            {
                { 0, 1, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 }
            };

            int[,] cells2 = new int[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 1, 1, 1 }
            };

            int[,] next2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 1, 0, 0 }
            };

            Assert.AreEqual(next1, Task3kyu.GetNextGeneration(cells1));
            Assert.AreEqual(next2, Task3kyu.GetNextGeneration(cells2));
        }

        [Test]
        public void TestConwayLife()
        {
            int[,] cells1 = new int[,]
            {
                { 1, 0, 0 },
                { 0, 1, 1 },
                { 1, 1, 0 }
            };

            int[,] next1 = new int[,]
            {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            };

            int[,] newNext1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };

            int[,] newNext2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 1, 0, 0 }
            };

            int[,] next2 = new int[,]
            {
                { 1, 0, 1 },
                { 0, 1, 1 },
                { 0, 1, 0 }
            };


            Assert.AreEqual(next2, Task3kyu.ConwayLife(cells1, 2));
        }

        [Test]
        public void TestDeleteDeadRowAndColumn()
        {
            int[,] cells0 = new int[,]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };

            int[,] next0 = new int[0, 0];

            int[,] cells1 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };

            int[,] next1 = new int[,]
            {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            };

            int[,] cells2 = new int[,]
            {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 1, 0, 0 }
            };

            int[,] next2 = new int[,]
            {
                { 1, 0, 1 },
                { 0, 1, 1 },
                { 0, 1, 0 }
            };

            Assert.AreEqual(next0, Task3kyu.DeleteDeadRowAndColumn(cells0));
            Assert.AreEqual(next1, Task3kyu.DeleteDeadRowAndColumn(next1));
            Assert.AreEqual(next1, Task3kyu.DeleteDeadRowAndColumn(cells1));
            Assert.AreEqual(next2, Task3kyu.DeleteDeadRowAndColumn(cells2));
        }

        [Test]
        public void TestJustify()
        {
            string input = "Lorem ipsum dolor sit amet. Mauris, at element ligula tempor eget.";
            string output13 =
@"Lorem   ipsum
dolor     sit
amet. Mauris,
at    element
ligula tempor
eget.";
            string output22 =
@"Lorem  ipsum dolor sit
amet.    Mauris,    at
element  ligula tempor
eget.";

            string longStr = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sagittis dolor mauris, at elementum ligula tempor eget.";
            string outputLong15 =
@"Lorem     ipsum
dolor sit amet,
consectetur
adipiscing
elit.
Vestibulum
sagittis  dolor
mauris,      at
elementum
ligula   tempor
eget.";

            Assert.AreEqual(output13, Task3kyu.Justify(input, 13));
            Assert.AreEqual(output22, Task3kyu.Justify(input, 22));
            Assert.AreEqual(outputLong15, Task3kyu.Justify(longStr, 15));
        }

        [Test]
        public void TestNextBiggerNumber()
        {
            Assert.AreEqual(1234567908, Task3kyu.NextBiggerNumber(1234567890));
        }

        [Test]
        public void TestNextSmaller()
        {
            Assert.AreEqual(123456789, Task3kyu.NextSmaller(123456798));
        }
    }
}
