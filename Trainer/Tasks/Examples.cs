using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Example
{
    public class Examples
    {
        #region Разложение числа на простые множители
        public static void Divide(int n)
        {
            var divides = new List<int>();
            var div = 2;
            while (n % div == 0)
            {
                divides.Add(div);
                n /= div;
            }

            div = 3;

            while (Math.Pow(div, 2) <= n)
            {
                if (n % div == 0)
                {
                    divides.Add(div);
                    n /= div;
                }
                else
                {
                    div += 2;
                }
            }

            if (n > 1)
            {
                divides.Add(n);
            }
        }

        #endregion

        #region Все возможные комбинации букв в слове
        public static void AllPermute()
        {
            string str = "Bahas";
            HashSet<string> permutations = new HashSet<string>();
            Permute(str, 0, str.Length - 1, permutations);
        }

        static string Swap(string str, int i, int j)
        {
            var charArray = str.ToCharArray();
            char temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            return new string(charArray);
        }

        static void Permute(string str, int l, int r, HashSet<string> permutations)
        {
            if (l == r)
                permutations.Add(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = Swap(str, l, i);
                    Permute(str, l + 1, r, permutations);
                    str = Swap(str, l, i);
                }
            }
        }
        #endregion

        #region Задача о рюкзаке
        public static int KnapsackProblem(Tuple<int, int>[] items, int capacity)
        {
            int[,] dp = new int[items.Length + 1, capacity + 1];
            for (int i = 1; i <= items.Length; i++) // items[weight, price]
            {
                int weight = items[i - 1].Item1;
                int price = items[i - 1].Item2;

                for (int j = 1; j <= capacity; j++)
                {
                    if (weight <= j)
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], price + dp[i - 1, j - weight]);
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j];
                    }

                }
            }

            return dp[items.Length, capacity];
        }

        #endregion

        #region В массиве целых чисел все нули переместить в конец, остальные числа оставить без изменений
        public static int[] MoveZeros(int[] array)
        {
            var a = array.OrderBy(x => x == 0).ToArray();
            return array;
        }
        #endregion

        #region RegEx с повторяющимися символами
        public static string RepeatedSymbol(string str)
        {
            // Если необходимо убрать повторяющиеся вопросительные знаки
            // "??? ?? ??"      =>      "? ? ?"
            string result = Regex.Replace(str, @"(\?){2,}", @"$1");
            return result;
        }
        public static string RepeatedSymbol1(string str)
        {
            // Если необходимо вырезать из строки вопросительные знаки, когда их количество больше одного
            // "example ? example1 ??? example2 ?? example3 ??"      =>      "example ? example1  example2  example3"
            string result = Regex.Replace(str, @"\?{2,}", "");
            return result;
        }

        public static bool RepeatedSymbol2(string str)
        {
            // Поиск 3 подряд одинаковых символов
            // Объяснение: в регулярке можно использовать уже захваченные группы,
            //(.) — захватывает любой символ, \1{2} — еще 2 повторения первой группы, т. е. того, что захватило выражение (.)
            var pattern = @"(.)\1{2}";
            var result = Regex.IsMatch(str, pattern);
            return result;
        }

        public static int RepeatedSymbol3(string str)
        {
            // Найти самую длинную последовательность, состоящую из одинаковых символов

            var a = Regex.Matches(str, @"(\w)\1*");

            string s = "AACCCCJDKSJKACCCCCK";
            int max = 0;
            for (int i = 0; i < s.Length; i++)
                max = Math.Max(s[i..].Count(), max);
            //max = Math.Max(s[i..].TakeWhile(x => x == 'C').Count(), max);

            var a1 = s[1..];

            return Regex.Matches(str, @"(\w)\1*").Select(m => m.Value.Length).Max();
        }

        #endregion

        #region Работа со строками

        // string.Join(separator, array) - объединяет все строки массива в одну с использованием сепаратора
        // string.Concat(array) - объединяет все строки массива в одну

        // Concatenation + operator - сложно читать и т.к. string is immutable, то каждый раз создается новая строка.
        // Interpolation - $"Hello, {person.Name}"
        // Formatting - String.Format("Hello, {0}", person.Name)

        // StringBuilder - для частого изменения строк
        //      string[] many = { "ab", "bc", "cd", "de" };
        //      StringBuilder sb = new StringBuilder();
        //      many.ForEach(x => sb.Append(x));

        // Двумерный массив в плоский
        // int cols =   array.GetLength(1);
        // array.Cast<int>()
        //      .Select((x, i) => new { posX = i / cols, posY = i % cols, value = x })

        #endregion

        #region Enum

        // Enum - value type, замаскированные числа
        // public enum MovieGenre
        // {
        //     Action = 23,
        //     Comedy,
        //     Drama
        // }
        // По умолчанию Action = 0, можно установить принудительно, все остальные имеют значение +1
        // var action = MovieGenre.Action; // remember, its value is 23

        // action.ToString();    // "Action"
        // action.ToString("g"); // "Action"
        // action.ToString("f"); // "Action"
        // action.ToString("x"); // "00000017"
        // action.ToString("d"); // "23"
        // var n = (int)MovieGenre.Action;
        // Enum.TryParse<MovieGenre>("Action", out MovieGenre e);

        // Flags attribute [Flag] установите каждый элемент как степень 2
        // [Flags]
        // public enum MovieGenre
        // {
        //     Action = 1,
        //     Comedy = 2,
        //     Drama = 4
        // }
        // Genre = MovieGenre.Action | MovieGenre.Comedy;
        // if (mg.HasFlag(MovieGenre.Comedy)) {}

        // [Flags]
        // enum Beverage
        // {
        //     Water = 1,
        //     Beer = 2,
        //     Tea = 4,
        //     RedWine = 8,
        //     WhiteWine = 16,
        //     Wine = RedWine | WhiteWine
        // }

        // var beverage = Beverage.RedWine | Beverage.WhiteWine;
        // if(beverage.HasFlag(Beverage.Wine))
        // {
        //     Console.WriteLine("This is wine");
        // }

        #endregion

        #region GUID

        // GIUD is struct, 128 бит или 16 байт, дефисы - лишь часть строкового представления

        void CreateGuid()
        {
            var initialGuid = Guid.NewGuid();
            Console.WriteLine("Before: " + initialGuid); // Before: d7241bf7-2778-42a9-a2e2-99228ada8c54

            updateGuid(initialGuid);
            Console.WriteLine("After: " + initialGuid); // After: d7241bf7-2778-42a9-a2e2-99228ada8c54

            updateGuidRef(ref initialGuid);
            Console.WriteLine("After ref: " + initialGuid); // AfterRef: b4274547-089b-42c9-a2d1-5d4d3a62f37a

            initialGuid.ToString("D"); // default d7241bf7-2778-42a9-a2e2-99228ada8c54
            initialGuid.ToString("N"); // without hyphens d7241bf7277842a9a2e299228ada8c54
            initialGuid.ToString("B"); // with braces {d7241bf7-2778-42a9-a2e2-99228ada8c54}
            initialGuid.ToString("D"); // with parentheses (d7241bf7-2778-42a9-a2e2-99228ada8c54)
            initialGuid.ToString("X"); // hexadecimal {0xe10deb88,0x171b,0x4c34,{0x81,0xf7,0x05,0xfc,0x17,0xd1,0x63,0x16}}

        }

        void updateGuid(Guid tmpGuid)
        {
            tmpGuid = Guid.NewGuid();
        }
        void updateGuidRef(ref Guid tmpGuid)
        {
            tmpGuid = Guid.NewGuid();
        }
        #endregion

        #region Tuple in C# 7.0
        public static void TupleWithSwap()
        {
            int[] nums = { 54, 7, -41, 2, 4, 2, 89, 33, -5, 12 };

            // сортировка пузырьком
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        (nums[i], nums[j]) = (nums[j], nums[i]);
                    }
                }
            }
        }
        #endregion

        #region Индексы(Indices) в C# 8
        // В C# 8 версии добавили новую индексацию справа налево (начинается с конца массива), начинающаяся с 1.
        //      var numbers = new int[] { 5, 1, 4, 2, 3, 7 };
        //      numbers[^1]); => последний элемент 7
        #endregion


        #region LINQ

        // Фичи с https://www.code4it.dev/
        // First vs FirstOrDefault - для массивов типа int возможна такая двусмысленность
        public static (int, int) FirstVSFirstOrDefault()
        {
            var numbers1 = new[] { -5, 0, 7, 11 };
            var first1 = numbers1.First(x => x % 2 == 0);

            var numbers2 = new[] { -5, 1, 7, 11 };
            var first2 = numbers2.FirstOrDefault(x => x % 2 == 0);

            return (first1, first2);
        }


        // SingleOrDefault - если значений больше 1, то исключение
        public static void FirstVSSingle()
        {
            int[] numbers = new int[] { -2, 1, 6, 12 };

            numbers.Single(n => n % 4 == 0);          // 12
            numbers.SingleOrDefault(n => n % 4 == 0); // 12
            numbers.SingleOrDefault(n => n % 7 == 0); // 0, because no items are %7
            numbers.SingleOrDefault(n => n % 3 == 0); // throws exception because more 2 results
        }


        // Any vs Count - для проверки пустой ли массив лучше использовать Any(), чем выражение Count() == 0
        public static void AnyVSCount()
        {
            int[] numbers = new int[] { -2, 1, 6, 12 };

            numbers.Any(n => n % 3 == 0); // true
            numbers.Count(n => n % 3 == 0); // 2
        }


        // Sort vs Order
        // The main difference is that Sort sorts the items in place, modifying the original collection.
        // OrderBy create a new collection of items with the same items of the original sequence but sorted.
        // Also, notice that Sort is valid only on List<T>, and not Arrays or generic Enumerables.
        public static void SortVSOrder()
        {
            List<int> originalNumbers = new List<int> { -7, 1, 5, -6 };
            originalNumbers.Sort(); // originalNumbers now is [-7, -6, 1, 5]

            originalNumbers = new List<int> { -7, 1, 5, -6 };
            var sortedNumbers = originalNumbers.OrderBy(n => n);
            // sortedNumbers is [-7, -6, 1, 5];
            // originalNumbers is [-7, 1, 5, -6];
        }


        // list.Skip(5).Take(2) = list.Take(6..8)

        // CROSS JOIN with LINQ
        public static void CrossJoin()
        {
            string digits = "12345";
            string letters = "ABCDE";
            var crossJoin1 = digits.SelectMany(x => letters.Select(y => Tuple.Create(y.ToString() + x)));
            var crossJoin2 = digits.SelectMany(x => letters, (y, z) => { return z.ToString() + y; });
            var crossJoin3 = digits.SelectMany(x => letters, (y, z) => new { a = z.ToString() + y } );
        }

        #endregion

        #region Задачи на массивы (Саша Лукин)

        // Дан массив чисел и число k. Найти 2 числа из массива, в сумме дающие k.
        // Сложность O(n), дополнительная память O(n)
        public static (int, int) GetPairForSum(int[] array, int k)
        {
            var set = new HashSet<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (!set.Contains(k - array[i]))
                {
                    set.Add(array[i]);
                }
                else
                {
                    return (k - array[i], array[i]);
                }
            }

            return (0, 0);
        }

        // Дан отсортированный массив чисел и число k. Найти 2 числа из массива, в сумме дающие k.
        // Так как массив отсортирован, то воспользуемся методом двух указателей.
        // Сложность O(n), дополнительная память O(1).
        // Если нам надо найти пару чисел, чья сумма максимально приближена к k, то мы запомним sum и будем сравнивать его с другими sum.
        public static (int, int) GetOrderedPairForSum(int[] array, int k)
        {
            int start = 0;
            int end = array.Length - 1;
            while (start < end)
            {
                int sum = array[start] + array[end];
                if (sum == k)
                {
                    return (array[start], array[end]);
                }
                else if (sum < k)
                {
                    start++;
                }
                else
                {
                    end--;
                }
            }

            return (0, 0);
        }


        // Дан двумерный массив n x m и число k, все значения в строках и столбцах этого массива упорядочены по возрастанию.
        // Вернуть true, если число k есть в массиве, false в противном случае.
        // Решение сложностью O(m + n).
        // Начинаем с правого верхнего элемента массива и идем влево, пока текущий элемент больше k, и
        // вниз, пока текущий элемент меньше искомого.
        public static bool SearchMatrix(int[][] matrix, int k)
        {
            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;

            int i = 0; 
            int m = matrix.Length;
            int n = matrix[0].Length;
            int j = n - 1;

            while (i < m && j >= 0)
            {
                if (matrix[i][j] == k) return true;

                if (matrix[i][j] > k)
                {
                    j--;
                }
                else
                {
                    i++;
                }
            }

            return false;
        }



        // Дан массив чисел, представляющий собой значения температур за определенный день.
        // Найти для каждого дня сколько суток должно пройти до наступления более теплого дня.
        // При решении будем двигаться с конца массива и заносить в стек значения (температуру и номер дня)
        // о предыдущих днях. Если температура в текущих день меньше дня в вершине стека, то добавляем новый
        // день в стек. В противном случае удаляем из стека все дни, пока температура меньше текущей.
        public static int[] Temperatures(int[] array)
        {
            var stack = new Stack<(int, int)>(); // value, index
            var result = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                // Удалить из стека все дни, температура которых ниже текущего дня
                while (stack.Any() && stack.Peek().Item1 <= array[i])
                {
                    stack.Pop();
                }

                if (stack.Any())
                {
                    result[i] = stack.Peek().Item2 - i;
                }

                stack.Push((array[i], i));
            }

            return result;
        }


        // Задача на динамическое программирование: количество уникальных путей.
        // Дано двумерное поле n x m и робот, который изначально находится в ячейке (1, 1). За один ход он может двигаться
        // только вверх или вправо. Дано дверь в координатах (n, m). Найти количество способов добраться до выхода.
        // paths(n, m) = paths(n - 1, m) + paths(n, m - 1);
        // Здесь O(2^(n + m))
        public static int Paths(int n, int m)
        {
            if (n < 1 || m < 1) return 0;
            if (n == 1 && m == 1) return 1;

            return Paths(n - 1, m) + Paths(n, m - 1);
        }

        // Чтобы значение paths не вычислялось для одной и той же клетки несколько раз, сделаем оптимизацию.
        // Применим динамическое программирование, т.е. посчитав значение paths для клетки, запомним его.
        // Здесь O(n * m)
        public static int PathsOptimize(int n, int m)
        {
            return Helper(n, m, new int[n + 1, m + 1]);
        }

        private static int Helper(int n, int m, int[,] array)
        {
            if (n < 1 || m < 1) return 0;
            if (n == 1 && m == 1) return 1;

            if (array[n, m] != 0) return array[n, m];

            array[n, m] = Helper(n - 1, m, array) + Helper(n, m - 1, array);
            return array[n, m];
        }

        // Задача поиска знаменитости. Метод двух указателей.
        // Дано k человек. Знаменитостью называется такой человек, которого знают все, но он никого не знает.
        // За минимальное число шагов найти знаменитость (она может быть только одна) или доказать, что ее нет.
        // Если задавать вопрос каждой паре человек, то O(k^2)
        // Здесь цикл while() задает k вопросов, а цикл for задает 2*k вопросов. Итого сложность O(k).
        // public static Person FindCelebrity(Person[] persons)
        // {
        //     int l = 0;
        //     int r = persons.Length - 1;
        //     while (l != r)
        //     {
        //         if (persons[l].knows(presons[r]))
        //         {
        //             l++;
        //         }
        //         else
        //         {
        //             r--;
        //         }
        //     }

        //     for (int i = 0; i < persons.Length; i++)
        //     {
        //         if (i != l && (!persons[i].knows(persons[l]) || persons[l].knows(persons[i])))
        //         {
        //             return null;
        //         }
        //     }

        //     return repsons[l];
        // }

        #endregion

        #region Задача на баланс скобок и палиндром
        public static bool IsBalanced(string text)
        {
            var pairs = new Dictionary<char, char>()
            {
                { '(', ')'},
                { '[', ']'},
                { '{', '}'}
            };
            var stack = new Stack<char>();

            foreach (var symbol in text)
            {
                if (pairs.ContainsKey(symbol))
                {
                    stack.Push(symbol);
                }
                else if (pairs.ContainsValue(symbol))
                {
                    if (stack.Count() == 0 || pairs[stack.Pop()] != symbol)
                    {
                        return false;
                    }
                }
            }

            return stack.Count() == 0;
        }

        public static bool IsPalindrom(string text, int first, int last)
        {
            if (last - first < 1)
            {
                return true;
            }

            return text[first] == text[last] && IsPalindrom(text, ++first, --last);
        }

        #endregion

        #region Records are the new data type introduced in 2021 with C# 9 and .NET Core 5.

        // record - третий тип данных после class и struct
        // 1 - Records are immutable - после создания экземпляря вы не можете менять его поля.
        // 2 - Records implement equality - сравнение по значению реализовано out-of-the-box "из коробки"
        // 3 - Records can be cloned or updated using 'with'
        // var me = new Person("Davide", 1); var anotherMe = me with { Id = 2 };
        record Office(string Place, string[] PhoneNumbers);

        public static void SelectMany()
        {
            List<Office> myCompanyOffices = new List<Office>
            {
                new Office("Turin", new string[]{"011-1234567", "011-2345678", "011-34567890"}),
                new Office("Rome", new string[]{"031-4567", "031-5678", "031-890"}),
                new Office("Dublin", new string[]{"555-031-4567", "555-031-5678", "555-031-890"}),
            };

            List<string> allPhoneNumbers = myCompanyOffices.SelectMany(b => b.PhoneNumbers).ToList();
            allPhoneNumbers = myCompanyOffices.SelectMany(b => b.PhoneNumbers ?? Enumerable.Empty<string>()).ToList();
        }

        #endregion

        #region Ханойские башни

        private static List<(int, int, int)> steps = new List<(int, int, int)>(); // form, to, disk number

        // n-количество дисков, i-номер стержня-источника, p-номер стержня-приёмника, v-вспомогательный стержень
        private static void Hanoy(int n, byte i, byte p, byte v)
        {
            if (n > 0)
            {
                // Перекладываем сначала стопку из n-1 элементов на вспомогательный стержень.
                // А стержень приёмник играет пока роль вспомогательного.
                Hanoy(n - 1, i, v, p);
                // Перекладываем самый большой диск из стержня-источника на стержень-приёмник.
                //Console.WriteLine(i + "->" + p);
                steps.Add((i, p, n));
                // Перекладываем стопку из n-1 элементов уже на стержень-приёмник.
                // А стержень-источник играет пока роль вспомогательного.
                Hanoy(n - 1, v, p, i);
            }
        }

        #endregion


        #region Задачи из LeetCode

        // Given a sorted array of distinct integers and a target value, return the index if the target is found.
        // If not, return the index where it would be if it were inserted in order.
        public int SearchInsert(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (target <= nums[i])
                    return i;
            }

            return nums.Length;
        }

        // You are given the heads of two sorted linked lists list1 and list2.
        // Merge the two lists in a one sorted list.The list should be made by splicing together the nodes of the first two lists.
        // Return the head of the merged linked list.
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
        #endregion

        #region Сортировка
        // Сортировка в танцах https://habr.com/ru/sandbox/74546/

        // СОРТИРОВКА ПУЗЫРЬКОМ - O(N * N)
        public static void SortBubble(int[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }


        // СОРТИРОВКА ШЕЛЛА - O(N)
        // Этот метод является усовершенствованием метода вставок.Суть его заключается в разбиении
        // сортируемого массива на ряд цепочек из равноотстоящих друг от друга элементов.
        // В этой сортировке важен правильный выбор величины шагов.
        // stepk = stepk-1*3/5, начиная с step1 = N/2 и заканчивая шагом, равным единице.
        public static void SortShell(int[] a)
        {
            int N = a.Length;
            int step = N / 2; // первый шаг
            while (step >= 1)
            {
                int k = step;
                for (int i = k; i < N; i++)
                {
                    int tmp = a[i];
                    int j = i - k;
                    while ((j >= 0) && (tmp < a[j]))
                    {
                        a[j + k] = a[j];
                        j = j - k;
                    }
                    a[j + k] = tmp;
                }

                step = 3 * step / 5; // определение следующего шага
            }
        }


        // БЫСТРАЯ СОРТИРОВКА
        // Один из самых быстрых известных универсальных алгоритмов сортировки массивов: в среднем O(n * log n)
        // Если опорный элемент выбран неудачно, то O(n * n)
        // Это как в книге C# Алгоритмы и структуры данных [2018] Тюкачев
        public static void QuickSortBook(int[] numbers, int first, int last)
        {
            int i = first;
            int j = last;
            int x = numbers[(first + last) / 2];
            while (i <= j)
            {
                while (numbers[i] < x) i++;
                while (numbers[j] > x) j--;
                if (i <= j)
                {
                    (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
                    i++;
                    j--;
                }
            }

            if (first < j)
            {
                QuickSortBook(numbers, first, j);
            }

            if (i < last)
            {
                QuickSortBook(numbers, i, last);
            }

        }


        private static int Partition(int[] array, int start, int end)
        {
            int marker = start; // divides left and right subarrays
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end]) // array[end] is pivot
                {
                    (array[marker], array[i]) = (array[i], array[marker]);
                    marker++;
                }
            }
            // put pivot(array[end]) between left and right subarrays
            (array[marker], array[end]) = (array[end], array[marker]);
            return marker;
        }

        public static void QuickSort(int[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int pivot = Partition(array, start, end);
            QuickSort(array, start, pivot - 1);
            QuickSort(array, pivot + 1, end);
        }



        #endregion

        // Какова алгоритмическая сложность для операций чтения и записи для коллекции Dictionary?
        // Чтение очень быстрое, потому что используются хэш-таблицы и сложность в этом случае стремится к O(1).
        // Запись проходит тоже очень быстро(O(1)), в том случае если.Count меньше емкости, если же больше, то скорость стремится к O(n).
    }

    public static class Utils
    {
        #region Как перемешать (случайно переставить) элементы в массиве?
        // Shuffle any(I)List with an extension method based on the Fisher-Yates shuffle:

        private static Random rng = new Random(); // new Random((int) DateTime.Now.Ticks & 0x0000FFFF);

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            // Или немного по-другому
            //for (int i = data.Length - 1; i >= 1; i--)
            //{
            //    int j = random.Next(i + 1);
            //    // обменять значения data[j] и data[i]
            //    var temp = data[j];
            //    data[j] = data[i];
            //    data[i] = temp;
            //}
        }

        // var shuffledcards = cards.OrderBy(a => rng.Next()).ToList();
        // items.Select(i=> new {I=i, sort= rng.Next()}).OrderBy (i =>i.sort).Select(i=>i.I);
        #endregion

    }
}