using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    public class Task3kyu
    {
        #region Spiral
        private static LinkedList<(int, int)> directions = new LinkedList<(int, int)> (new[]
        {
            ( 0,  1), // right
            ( 1,  0), // down
            ( 0, -1), // left
            (-1,  0)  // up
        });


        public static int[,] Spiralize(int size)
        {
            int[,] spiral = new int[size, size];

            int rotates = 0;
            (int, int) current = (0, 0);
            (int, int) previous = (0, 0);
            (int, int) direction = directions.First();

            while (rotates < 2)
            {
                (int, int) next = (current.Item1 + direction.Item1, current.Item2 + direction.Item2);
                if (isOutOfSize(size, next))
                {
                    direction = NextDirection(direction);
                }
                else if ( spiral[next.Item1, next.Item2] == 0 && (
                            Neighbor((next.Item1 + 1, next.Item2), spiral) +
                            Neighbor((next.Item1 - 1, next.Item2), spiral) +
                            Neighbor((next.Item1, next.Item2 + 1), spiral) +
                            Neighbor((next.Item1, next.Item2 - 1), spiral)) <= 1)
                {
                    spiral[current.Item1, current.Item2] = 1;
                    previous = current;
                    current = next;
                    rotates = 0;
                }
                else
                {
                    rotates++;
                    current = previous;
                    direction = NextDirection(direction);
                }
            }

            return spiral;
        }

        private static (int, int) NextDirection((int, int) current)
        {
            var next = directions.Find(current).Next;
            return next == null ? directions.First.Value : next.Value;
        }

        private static bool isOutOfSize(int size, (int, int) point)
        {
            return point.Item1 < 0 || point.Item1 == size ||
                    point.Item2 < 0 || point.Item2 == size;
        }

        private static int Neighbor((int, int) point, int[,] spiral)
        {
            int size = spiral.GetLength(0);
            return isOutOfSize(size, point) ? 0 : spiral[point.Item1, point.Item2];
        }


        // Обход квадратной матрицы по часовой стрелке
        public static int[] Snail(int[][] array)
        {
            int size = array.GetLength(0);
            if (size == 0) return new int[0];

            List<int[]> snail = new();
            int borders = size / 2 + size % 2;
            for (int i = 0; i < borders; i++)
            {
                snail.Add(GetBorder(array, i));
            }

            return snail.SelectMany(x => x).ToArray();
        }

        private static int[] GetBorder(int[][] array, int border)
        {
            List<int> elements = new();
            int size = array.GetLength(0);

            for (int i = border; i < size - border; i++) // right
            {
                elements.Add(array[border][i]);
            }
            for (int i = border + 1; i < size - border; i++) // down
            {
                elements.Add(array[i][size - border - 1]);
            }
            for (int i = size - border - 2; i >= border; i--) // left
            {
                elements.Add(array[size - border - 1][i]);
            }
            for (int i = size - border - 2; i > border; i--) // up
            {
                elements.Add(array[i][border]);
            }

            return elements.ToArray();
        }

        public static int[][] Pyramid(int n)
        {

            int[][] array = new int[n][];
            for (int i = 0; i < n; i++)
            {
                array[i] = new int[i + 1];
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = 1;
                }
            }

            return array;
        }



        #endregion


        #region Primes infinity

        public static IEnumerable<int> GetPrimes()
        {
            var primes = new List<int>(new[] { 2 , 3 });
            yield return 2;
            yield return 3;

            int start = primes.Last() + 2;
            for (int n = start; ; n+=2)
            {
                int maxDivisor = (int)Math.Sqrt(n);
                if (!primes.AsParallel().Any(x => x <= maxDivisor && n % x == 0))
                {
                    primes.Add(n);
                    yield return n;
                }
            }
        }


        public static IEnumerable<int> Stream()
        {
            return Enumerable.Range(2, int.MaxValue - 1).Where(number =>
            {
                if (number == 2 || number == 3)
                    return true;
                if (number % 2 == 0 || number % 3 == 0)
                    return false;
                for (var i = 5; i * i <= number; i = i + 6)
                    if (number % i == 0 || number % (i + 2) == 0)
                        return false;
                return true;
            });
        }


        public static IEnumerable<int> GetPrimesByAtkin()
        {
            int limit = 16000000;
            var sieve = new BitArray(limit + 1);

            // Предварительное просеивание
            for (long x2 = 1L, dx2 = 3L; x2 < limit; x2 += dx2, dx2 += 2L)
                for (long y2 = 1L, dy2 = 3L, n; y2 < limit; y2 += dy2, dy2 += 2L)
                {
                    // n = 4x² + y²
                    n = (x2 << 2) + y2;
                    if (n <= limit && (n % 12L == 1L || n % 12L == 5L))
                        sieve[(int)n] ^= true;
                    // n = 3x² + y²
                    n -= x2;
                    if (n <= limit && n % 12L == 7L)
                        sieve[(int)n] ^= true;
                    // n = 3x² - y² (при x > y)
                    if (x2 > y2)
                    {
                        n -= y2 << 1;
                        if (n <= limit && n % 12L == 11L)
                            sieve[(int)n] ^= true;
                    }
                }

            // Все числа, кратные квадратам, помечаются как составные
            int r = 5;
            for (long r2 = r * r, dr2 = (r << 1) + 1L; r2 < limit; ++r, r2 += dr2, dr2 += 2L)
                if (sieve[r])
                    for (long mr2 = r2; mr2 < limit; mr2 += r2)
                        sieve[(int)mr2] = false;
            // Числа 2 и 3 — заведомо простые
            if (limit > 2)
                sieve[2] = true;
            if (limit > 3)
                sieve[3] = true;

            for (int i = 2; i < limit; i++)
            {
                if (sieve[i]) yield return i;
            }
        }

        #endregion


        #region Last digit of a large number
        public static int GetLastDigit(int n1, int n2) // n1 ^ n2
        {
            return (int)Math.Pow(10 + n1 % 10, 4 + n2 % 4) % 10;
            // return (int)BigInteger.ModPow((n1 % 10), n2, 10);

            int last = n1 % 10;

            if (n2 == 0) return 1;
            if (last == 0) return 0;
            if (last == 1) return 1;

            if (last == 2 && n2 % 4 == 0) return 6;
            if (last == 2 && n2 % 4 == 3) return 8;
            if (last == 2 && n2 % 4 == 2) return 4;
            if (last == 2 && n2 % 4 == 1) return 2;

            if (last == 3 && n2 % 4 == 0) return 1;
            if (last == 3 && n2 % 4 == 3) return 7;
            if (last == 3 && n2 % 4 == 2) return 9;
            if (last == 3 && n2 % 4 == 1) return 3;

            if (last == 4 && n2 % 2 == 0) return 6;
            if (last == 4 && n2 % 2 == 1) return 4;

            if (last == 5) return 5;
            if (last == 6) return 6;

            if (last == 7 && n2 % 4 == 0) return 1;
            if (last == 7 && n2 % 4 == 3) return 3;
            if (last == 7 && n2 % 4 == 2) return 9;
            if (last == 7 && n2 % 4 == 1) return 7;

            if (last == 8 && n2 % 4 == 0) return 6;
            if (last == 8 && n2 % 4 == 3) return 2;
            if (last == 8 && n2 % 4 == 2) return 4;
            if (last == 8 && n2 % 4 == 1) return 8;

            if (last == 9 && n2 % 2 == 0) return 1;
            if (last == 9 && n2 % 2 == 1) return 9;

            return 0;
        }


        public static int GetArrayLastDigit(int[] array) // array[0] ^ (array[1] ^ array[2])
        {
            int len = array.Length;
            if (len == 0) return 1;
            if (len == 1) return array[0] % 10;

            int pow = array[len - 1];
            for (int i = array.Length - 2; i >= 0; i--)
            {
                int number = array[i];
                if (number > 10)
                {
                    int dozens = number % 100 / 10;
                    number = (dozens % 2 == 0 ? 2 : 1) * 10 + number % 10;
                }

                if (pow > 3)
                {
                    pow = pow % 4 + 4;
                    if (number > 20 && pow > 5) pow -= 4;
                }

                pow = (int)Math.Pow(number, pow);
            }

            return pow % 10;
        }

        #endregion


        #region Drawing Lines, Circles, Ellipse, rotate by angle

        const int Width = 100, Height = 50;
        private static bool[,] Canvas = new bool[100, 50];

        public static void DrawLine(int x1, int y1, int x2, int y2)
        {
            (int, int) left = x1 < x2 ? (x1, y1) : (x2, y2);
            (int, int) right = x1 < x2 ? (x2, y2) : (x1, y1);

            double tg = Convert.ToDouble(right.Item2 - left.Item2) / (right.Item1 - left.Item1);
            double b = left.Item2 - tg * left.Item1;

            for (int x = Math.Max(left.Item1, 0); x < Math.Min(right.Item1, Width); x++)
            {
                int y = (int)(tg * x + b);
                if (y < Height && y >= 0)
                {
                    Canvas[x, y] = true;
                }
            }
        }

        public static void DrawCircle(int x, int y, int r)
        {
            for (int i = 0; i <= Math.Sqrt(r * r / 2); i++)
            {
                int d = (int)Math.Sqrt(r * r - i * i);
                if (IsInSize(x + i, y + d)) Canvas[x + i, y + d] = true;
                if (IsInSize(x + i, y - d)) Canvas[x + i, y - d] = true;
                if (IsInSize(x - i, y + d)) Canvas[x - i, y + d] = true;
                if (IsInSize(x - i, y - d)) Canvas[x - i, y - d] = true;
                if (IsInSize(x + d, y + i)) Canvas[x + d, y + i] = true;
                if (IsInSize(x + d, y - i)) Canvas[x + d, y - i] = true;
                if (IsInSize(x - d, y + i)) Canvas[x - d, y + i] = true;
                if (IsInSize(x - d, y - i)) Canvas[x - d, y - i] = true;
            }
        }

        // x^2 / a^2 + y^2 / b^2 = 1
        public static void DrawEllipse(int x, int y, int rx, int ry, int angle)
        {
            int a = Math.Max(rx, ry);
            int b = Math.Min(rx, ry);

            for (int i = 0; i <= a; i++)
            {
                int d = (int)Math.Sqrt((b - Convert.ToDouble(b * i) / a) * (b + Convert.ToDouble(b * i) / a));
                i = (int)(i * Math.Cos(angle) - d * Math.Sin(angle));
                d = (int)(i * Math.Sin(angle) + d * Math.Cos(angle));

            }
        }

        private static bool IsInSize(int x, int y)
        {
            return 0 <= x && x < Width && 0 <= y && y < Height;
        }


        #endregion


        #region Разбор математических выражений: польская запись

        private static Dictionary<char, int> operations = new ()
        {
            { '(', 0 },
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 },
            //{ '~', 4 }, // Унарный минус
            { 's', 4 },
            { 'c', 4 },
            { 't', 4 },
        };

        private static double UnaryExecute(char op, double first) => op switch
        {
            //    '~' => 0 - first,
            's' => Math.Sin(first),
            'c' => Math.Cos(first),
            't' => Math.Tan(first),
            _ => 0  // Возвращает, если не был найден подходящий оператор
        };

        private static double BinaryExecute(char op, double first, double second) => op switch
        {
            '+' => first + second,
            '-' => first - second,
            '*' => first * second,
            '/' => first / second,
            '^' => Math.Pow(first, second),
            _ => 0  // Возвращает, если не был найден подходящий оператор
        };


        public static string CreatePostfix(string exp)
        {
            Stack<char> stack = new();
            string postfixExpr = string.Empty;

            exp = exp.Replace("Math.Pow", "");
            exp = exp.Replace(",", "^");
            exp = exp.Replace("Math.Sin", "s");
            exp = exp.Replace("Math.Cos", "c");
            exp = exp.Replace("Math.Tan", "t");

            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                
                if (char.IsDigit(c) || c == '.')
                {
                    postfixExpr += c;
                    continue;
                };

                postfixExpr += postfixExpr.EndsWith(" ") ? "" : " ";
                switch (c)
                {
                    case '(':
                        stack.Push(c);
                        break;

                    case ')':
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            postfixExpr += stack.Pop() + " ";
                        }
                        stack.Pop();
                        break;

                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    //case '~':
                    case 's':
                    case 'c':
                    case 't':
                        while (stack.Count > 0 && (operations[stack.Peek()] >= operations[c]))
                        {
                            postfixExpr += stack.Pop() + " ";
                        }
                        stack.Push(c);
                        break;

                    default:
                        postfixExpr += c + " ";
                        break;
                }
            }

            postfixExpr += postfixExpr.EndsWith(" ") ? "" : " ";
            foreach (char op in stack)
            {
                postfixExpr += op + " ";
            }

            return postfixExpr.Trim();
        }

        public static double Calculate(string postfix)
        {
            Stack<double> stack = new();
            string[] parts = postfix.Split(' ');

            for (int i = 0; i < parts.Length; i++)
            {
                string c = parts[i];
                if (char.IsDigit(c[0]))
                {
                    stack.Push(Convert.ToDouble(parts[i]));
                }
                else if (operations[c[0]] == 4)
                {
                    double first = stack.Count > 0 ? stack.Pop() : 0;
                    stack.Push(UnaryExecute(c[0], first));
                }
                else
                {
                    double second = stack.Count > 0 ? stack.Pop() : 0;
                    double first  = stack.Count > 0 ? stack.Pop() : 0;
                    stack.Push(BinaryExecute(c[0], first, second));
                }
            }

            return stack.Pop();
        }

        #endregion


        #region Matrix Determinant

        public static int DeterminantCalculate(int[][] matrix)
        {
            if (matrix.GetLength(0) == 1)
            {
                return matrix[0][0];
            }

            return GetDeterminant(matrix);
        }

        private static int GetDeterminant(int[][] matrix)
        {
            int size = matrix.GetLength(0);
            if (size == 2)
            {
                return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];
            }

            int determinant = 0;
            int[][] minor = new int[size - 1][];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    minor[j] = new int[size - 1];
                    for (int l = 0; l < size - 1; l++)
                    {
                        int k = l < i ? l : l + 1;
                        minor[j][l] = matrix[j + 1][k];
                    }
                }

                determinant += matrix[0][i] * GetDeterminant(minor) * (i % 2 == 0 ? 1 : -1);
            }

            return determinant;
        }

        #endregion

        public static int ParseInt(string s)
        {
            Dictionary<string, int> numerals = new Dictionary<string, int>() {
                { "zero",  0},
                { "one",   1},
                { "two",   2},
                { "three", 3},
                { "four",  4},
                { "five",  5},
                { "six",   6},
                { "seven", 7},
                { "eight", 8},
                { "nine",  9},
                { "ten",   10},
                { "eleven",    11},
                { "twelve",    12},
                { "thirteen",  13},
                { "fourteen",  14},
                { "fifteen",   15},
                { "sixteen",   16},
                { "seventeen", 17},
                { "eighteen",  18},
                { "nineteen",  19},
                { "twenty",  20},
                { "thirty",  30},
                { "forty",   40},
                { "fifty",   50},
                { "sixty",   60},
                { "seventy", 70},
                { "eighty",  80},
                { "ninety",  90},
                { "hundred",  100},
                { "thousand", 1000},
                { "million", 1000000}
            };

            int number = 0;
            int koeff = 1;

            while (!string.IsNullOrEmpty(s))
            {
                s = s.Trim(new[] { ' ', '-' });
                s = s.EndsWith(" and") ? s.Remove(s.Length - 4) : s;

                var digit = numerals.SingleOrDefault(x => s.EndsWith(x.Key));
                if (string.IsNullOrEmpty(digit.Key))
                {
                    return -1;
                }

                s = s.Remove(s.Length - digit.Key.Length);

                if (digit.Value == 100)
                {
                    koeff = koeff < 100 ? 100 : koeff * 100;
                }
                else if (digit.Value == 1000)
                {
                    koeff = 1000;
                }
                else if (digit.Value == 1000000)
                {
                    return digit.Value;
                }
                else
                {
                    number += digit.Value * koeff;
                }
            }

            return number;
        }

        #region Conway's Life
        // ---------------------- Conway's Life
        public static int[,] ConwayLife(int[,] cells, int generation)
        {
            int[,] next = cells;
            for (int i = 0; i < generation; i++)
            {
                next = GetNextGeneration(next);
            }

            return DeleteDeadRowAndColumn(next);
        }

        public static int[,] DeleteDeadRowAndColumn(int[,] cells)
        {
            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);
            var sequence = cells.Cast<int>()
                                .Select((x, i) => new { posX = i / cols, posY = i % cols, value = x })
                                .Where(x => x.value == 1);

            if (!sequence.Any())
            {
                return new int[0, 0];
            }

            int i1 = sequence.Min(x => x.posX);
            int j1 = sequence.Min(x => x.posY);
            int i2 = sequence.Max(x => x.posX);
            int j2 = sequence.Max(x => x.posY);

            if (i1 == 0 && j1 == 0 && i2 == rows && j2 == cols)
            {
                return cells;
            }

            int[,] next = new int[i2 - i1 + 1, j2 - j1 + 1];
            for (int i = 0; i < next.GetLength(0); i++)
            {
                for (int j = 0; j < next.GetLength(1); j++)
                {
                    next[i, j] = cells[i + i1, j + j1];
                }
            }

            return next;
        }

        public static int[,] GetNextGeneration(int[,] cells)
        {
            cells = GetAroundCells(cells);

            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);
            int[,] next = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int neighbours = GetAllNeighbors(cells, i, j);
                    if (cells[i, j] == 0 && neighbours == 3)
                    {
                        next[i, j] = 1;
                    }

                    if (cells[i, j] == 1 && (neighbours == 2 || neighbours == 3))
                    {
                        next[i, j] = 1;
                    }
                }
            }

            return next;
        }

        public static int[,] GetAroundCells(int[,] cells)
        {
            const string one = "111";
            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);

            var sequence = cells.OfType<int>().Select(x => x.ToString()[0]);
            var top = string.Concat(sequence.Where((x, i) => i < cols));
            var bottom = string.Concat(sequence.Where((x, i) => i >= cols * (rows - 1)));
            var left = string.Concat(sequence.Where((x, i) => i % cols == 0));
            var right = string.Concat(sequence.Where((x, i) => i % cols == cols - 1));

            //var sequence = cells.Cast<int>()
            //                    .Select((x, i) => new { posX = i / cols, posY = i % cols, value = x.ToString()[0] });

            if (top.Contains(one) || bottom.Contains(one) || left.Contains(one) || right.Contains(one))
            {
                int[,] newCells = new int[rows + 2, cols + 2];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        newCells[i + 1, j + 1] = cells[i, j];
                    }
                }

                return newCells;
            }

            return cells;
        }

        public static int GetAllNeighbors(int[,] cells, int m, int n)
        {
            int sum = 0;
            int rows = cells.GetLength(0);
            int columns = cells.GetLength(1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    if (0 <= m + i && m + i < rows &&
                        0 <= n + j && n + j < columns)
                    {
                        sum += cells[m + i, n + j];
                    }
                }
            }

            return sum;
        }

        // -----------------------Conway's Life ----------- end
        #endregion

        #region Justify
        public static string Justify(string str, int len)
        {
            const int space = 1;

            var words = str.Split(' ');
            int firstWordIndex = 0;
            int lastWordIndex = 0;
            var justified = new StringBuilder();

            while (lastWordIndex < words.Length && firstWordIndex != words.Length - 1)
            {
                int length = 0;
                firstWordIndex = lastWordIndex;
                while (length - space <= len && lastWordIndex < words.Length)
                {
                    length += words[lastWordIndex].Length + space;
                    lastWordIndex++;
                }

                lastWordIndex = (length - space <= len && lastWordIndex == words.Length) ? lastWordIndex : lastWordIndex - 1;
                justified.Append(AddGaps(words, len, firstWordIndex, lastWordIndex));
            }

            return justified.ToString().TrimEnd();
        }

        public static StringBuilder AddGaps(string[] words, int length, int firstWordIndex, int lastWordIndex)
        {
            const char gap = ' ';
            int wordsInLine = lastWordIndex - firstWordIndex;
            if (wordsInLine <= 1)
            {
                return new StringBuilder(words[firstWordIndex]).AppendLine();
            }

            int wordsLength = words.Where((x, i) => firstWordIndex <= i && i <= lastWordIndex - 1)
                                    .Sum(x => x.Length);

            int gaps = length - wordsLength;
            int gapsBetweenWords = gaps / (wordsInLine - 1);
            int addedGapsBetweenWords = gaps % (wordsInLine - 1);

            if (lastWordIndex == words.Length)
            {
                gapsBetweenWords = 1;
                addedGapsBetweenWords = 0;
            }

            var justified = new StringBuilder();
            for (int i = firstWordIndex; i < lastWordIndex; i++)
            {
                justified.Append(words[i]);
                justified.Append(new string(gap, gapsBetweenWords));
                justified.Append(addedGapsBetweenWords-- > 0 ? gap : "");
            }

            justified.Remove(justified.Length - gapsBetweenWords, gapsBetweenWords);
            justified.AppendLine();

            return justified;
        }
        #endregion

        public static long NextBiggerNumber(long n)
        {
            var str = n.ToString().OrderByDescending(x => x);

            bool result = long.TryParse(string.Concat(str), out long max);
            if (!result)
            {
                max = long.MaxValue;
            }

            var digits = n.ToString().OrderBy(x => x);
            for (long i = n + 1; i <= max; i++)
            {
                var next = i.ToString().OrderBy(x => x);

                if (digits.SequenceEqual(next))
                {
                    return i;
                }
            }

            return -1;
        }

        public static long NextSmaller(long n)
        {
            var str = n.ToString().OrderBy(x => x);
            long min = long.Parse(string.Concat(str));

            var digits = n.ToString().OrderBy(x => x);
            for (long i = n - 1; min <= i; i--)
            {
                var next = i.ToString().OrderBy(x => x);

                if (digits.SequenceEqual(next))
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
