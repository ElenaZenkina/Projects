using System;
using System.Linq;
using System.Collections.Generic;

namespace PresidentTask
{
    public class PresidentTask
    {
        private static bool IsLiving(DateTime date)
        {
            return date.Year == 1;
        }

        public static (int, int) GetSubtractYearAndDays(DateTime begin, DateTime end)
        {
            int age = end.Year - begin.Year;
            int days = (end - begin.AddYears(age)).Days;
            if (begin.AddYears(age) > end)
            {
                age--;
                days = (end - begin.AddYears(age)).Days;
            }

            return (age, days);
        }

        public static int GetInauguralDateNumber(DateTime begin, DateTime end)
        {
            Func<int, DateTime> getInauguralDate = (x => x == 1789 ? new DateTime(x, 4, 30) :
                                                                     x <= 1933 ? new DateTime(x, 3, 4) : new DateTime(x, 1, 20));

            int number = 0;
            for (int i = 0; i <= end.Year - begin.Year; i++)
            {
                int year = begin.Year + i;
                if (year % 4 == 1 && begin <= getInauguralDate(year) && getInauguralDate(year) < end)
                {
                    number++;
                }
            }

            return number;
        }

        public static DateTime GetLastInauguralDate(DateTime begin, DateTime end)
        {
            Func<int, DateTime> getInauguralDate = (x => x == 1789 ? new DateTime(x, 4, 30) :
                                                                     x <= 1933 ? new DateTime(x, 3, 4) : new DateTime(x, 1, 20));

            DateTime date = new DateTime(1, 1, 1);
            for (int i = end.Year; i >= begin.Year; i--)
            {
                date = getInauguralDate(i);
                if (i % 4 == 1 && date < end)
                {
                    return date;
                }
            }

            return date;
        }


        private static List<President> presidents = President.GetUSAPresidents();

        #region Отложенное выполнение Deferred execution

        // Важно помнить, что хотя многие из стандартных операций запросов прототипированы на возврат IEnumerable<T>,
        // и IEnumerable<T> воспринимается как последовательность, на самом деле операции не возвращают последовательность
        // в момент их вызова.Вместо этого операции возвращают объект, который при перечислении выдает (yield)
        // очередной элемент последовательности. Во время перечисления возвращенного объекта запрос выполняется,
        // и выданный элемент помещается в выходную последовательность.Таким образом, выполнение запроса откладывается.
        public static int DeferredExecution()
        {
            var names = presidents.Select(p => p.FirstName).ToArray();
            string joseph = names[45];

            names[45] = "Sleepy Joe"; // оскорбительное прозвище Байдена, придуманное и используемое Дональдом Трампом, ставшее интернет-мемом
            return names.Count(p => p == joseph);
        }

        // Перед вами фамилии президентов США. Необходимо вывести фамилии, в которых пятый символ - гласная буква.
        // Выполнение это метода завершится:
        // - ошибкой компиляции,
        // - ошибкой RunTime,
        // - завершится без ошибок.
        // Когда перечисление достигает элемента, длина которого меньше 5 символов, возникает исключение.
        // Однако помните, что исключение не произойдет до тех пор, пока не начнется перечисление выходной последовательности.
        // Вдобавок, поскольку такого рода запросы, возвращающие IEnumerable<T>, являются отложенными,
        // код определения запроса может быть вызван однажды и затем использован его многократно,
        // с перечислением его результатов несколько раз.В случае изменения данных при каждом перечислении результатов
        // будут выдаваться разные результаты.
        public static void FifthSymbol()
        {
            IEnumerable<President> items = presidents.Where(p => "eyuioa".Contains(p.LastName[4]));
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        #endregion


        #region Фильтрация и проекция

        // Вывести те фамилии президентов, которые начинаются с буквы W.
        public static IEnumerable<string> StartsWithW()
        {
            return presidents.Where(p => p.LastName.StartsWith("W"))
                             .Select(p => p.LastName);
        }

        // Вывести фамилии президентов, чьи индексы в списке президентов меньше 5.
        // Хотя этот пример несколько надуман, стоит иметь в виду, что выполнять он будет не эффективно,
        // если входных элементов много. Дело в том, что лямбда-выражение "i < 5" будет выполнятся для
        // каждого входного элемента. Для лучшей производительности используйте Take.
        public static IEnumerable<string> IndexLess5()
        {
            return presidents.Where((p, i) => i < 5)
                             .Select(p => p.LastName);
        }

        // Вывести фамилии президентов, которых зовут James.
        // Select - тип данных элементов выходной последовательности может отличаться от типа элементов входной последовательности.
        // Операция Select вызовет метод-селектор для КАЖДОГО элемента входной последовательности, передав ему этот элемент.
        // Здесь особенно актуально использовать слово var для типа выходной последовательности.
        public static IEnumerable<string> GetNameIsJames()
        {
            return presidents.Where(p => p.FirstName == "James")
                             .Select(p => p.LastName);
        }

        #endregion


        #region Разбиение Take, TakeWhile, Skip, SkipWhile
        // Операция Take возвращает указанное количество элементов из входной последовательности, начиная с ее начала.
        // Вывести фамилии первых трех президентов.
        public static IEnumerable<string> GetThreePresidents()
        {
            return presidents.Take(3)
                             .Select(p => p.LastName);
        }

        // Операция TakeWhile возвращает элементы из входной последовательности, пока истинно некоторое условие, начиная с начала последовательности.
        // Вывести фамилии первых президентов, у которых нет MiddleName.
        public static IEnumerable<string> GetNotMiddleNamePresidents()
        {
            return presidents.TakeWhile(p => string.IsNullOrEmpty(p.MiddleName))
                             .Select(p => p.LastName);
        }
        #endregion


        #region Конкатенация
        // Операция Concat соединяет две входные последовательности и выдает одну выходную последовательность.
        // Но если нужно объединить более двух последовательностей, то можно воспользоваться следующим приемом
        public static IEnumerable<President> GetAllPresidents()
        {
            return new[]
            {
                presidents.Take(5)
                          .Skip(5)
            }
            .SelectMany(p => p);
        }
        #endregion


        #region Упорядочивание
        // Операции упорядочивания позволяют выстраивать входные последовательности в определенном порядке.
        // Важно отметить, что и OrderBy, и OrderByDescending требуют входной последовательности типа IEnumerable<T>
        // и возвращают последовательность типа IOrderedEnumerable<T> (этот интерфейс унаследован от IEnumerable<T>).
        // Если требуется большая степень упорядочивания, необходимо последовательно вызывать операции ThenBy или ThenByDescending.
        // Вызовы ThenBy и ThenByDescending могут соединяться в цепочку, т.к.они принимают в качестве входной последовательности
        // IOrderedEnumerable<T> и возвращают в качестве выходной последовательности тоже IOrderedEnumerable<T>.
        public static IEnumerable<President> OrderLastNames()
        {
            return presidents.OrderBy(s => s.LastName);
        }

        // Упорядочить президентов по возрасту их вступления в должность.
        class AgeComparer : IComparer<President>
        {
            public int Compare(President p1, President p2)
            {
                (int, int) age1 = GetSubtractYearAndDays(p1.BirthDate, p1.TermBegin);
                (int, int) age2 = GetSubtractYearAndDays(p2.BirthDate, p2.TermBegin);

                if (age1.Item1 < age2.Item1) return -1;
                if (age1.Item1 > age2.Item1) return 1;

                if (age1.Item2 == age2.Item2) return 0;
                return age1.Item2 < age2.Item2 ? -1 : 1;
            }
        }

        public static List<string> OrderTermAge()
        {
            IComparer<President> comparer = new AgeComparer();
            return presidents.OrderBy(p => p, comparer)
                             .Select(p => new { Name = p.LastName, Age = GetSubtractYearAndDays(p.BirthDate, p.TermBegin) })
                             .Select(p => $"{ p.Name}: {p.Age.Item1} years {p.Age.Item2} days")
                             .ToList();
        }

        // Сортировка, выполненная операцией OrderBy, определена как неустойчивая. Это значит, что она не сохраняет входной порядок элементов.
        // Если два входных элемента поступают в операцию OrderBy в определенном порядке, и значения ключей этих двух элементов совпадают,
        // их расположение в выходной последовательности может остаться прежним или поменяться, причем ни то, ни другое не гарантируется.

        // Сортировка, выполняемая операцией ThenBy, является устойчивой. Другими словами, она сохраняет входной порядок элементов
        // с эквивалентными ключами.Если два входных элемента поступили в операцию ThenBy в определенном порядке, и ключевое
        // значение обоих элементов одинаково, то порядок тех же выходных элементов гарантированно сохранится.
        // В отличие от OrderBy и OrderByDescending, операции ThenBy и ThenByDescending выполняют устойчивую сортировку.

        // Reverse - эта операция выводит последовательность того же типа, что и входная, но в обратном порядке.
        public static IEnumerable<President> ReversePresident()
        {
            return presidents.Reverse<President>();
        }
        #endregion


        #region Группировка
        // Операция GroupBy используется для группирования элементов входной последовательности.
        // Все прототипы операции GroupBy возвращают последовательность элементов IGrouping<K, T>.
        // Здесь IGrouping<K, T> — интерфейс, в котором определено свойство K Key { get; }
        // Порядок экземпляров IGruoping будет тем же, что и вхождения ключей в последовательности source,
        // и каждый элемент в последовательности IGruoping будет расположен в том порядке,
        // в котором элементы находились в последовательности source.

        // Простой вызов GroupBy
        // Вывести количество президентов каждой политической партии.
        public static IEnumerable<string> GroupByParty()
        {
            return presidents.GroupBy(p => p.Party)
                             .Select(g => new { Key = g.Key, Count = g.Count() })
                             .OrderBy(g => g.Count)
                             .ThenBy(g => g.Key.ToString())
                             .Select(g => $"{g.Key}: {g.Count}");
        }

        class GroupByComparer : IEqualityComparer<President>
        {
            public bool Equals(President p1, President p2)
            {
                return p1.Equals(p2);
            }

            public int GetHashCode(President p)
            {
                return p.GetHashCode();
            }
        }

        // Простой вызов GroupBy
        // Вывести список всех однофамильцев и/или родственников среди президентов.
        public static IEnumerable<string> GroupByLastName()
        {
            return presidents.GroupBy(p => p.LastName)
                             .Where(p => p.Count() > 1)
                             .OrderBy(p => p.Key)
                             .Select(p => p.Key);
        }

        // Вызов GroupBy с IEqualityComparer
        // Вывести список президентов, которые были на этой должности несколько раз с перерывом
        public static IEnumerable<string> GroupByLastNameWithComparer()
        {
            IEqualityComparer<President> comparer = new GroupByComparer();
            return presidents.GroupBy(p => p.LastName)//, comparer)
                             .Where(p => p.Count() > 1)
                             .OrderBy(p => p.Key)
                             .Select(p => p.Key);
        }

        // Группировка по нескольким полям в SQL:
        // select code1, code2, sum(field)
        // from table
        // group by code1, code2;
        // В LINQ это
        // var res = list.GroupBy(r => new { r.Code1, r.Code2})
        //               .Select(gr => new { gr.Key.Code1, gr.Key.Code2, Sum = gr.Sum(i => i.Price) });
        #endregion


        #region Множества
        // Свойства операций множеств не работают правильно с DataSet.
        // Здесь эквивалентность элементов определяется методами GetHashCode и Equals.
        // Операция Distinct удаляет дублированные элементы из входной последовательности.

        // Вывести количество уникальных имен президентов.
        public static int GetFirstNamesNumber()
        {
            return presidents.Select(p => p.FirstName)
                             .Distinct()
                             .Count();
        }

        // Concat и Distinct будет подобно действию метода Union.

        // Операция Intersect возвращает пересечение множеств из двух исходных последовательностей (без дубликатов).

        // Операция Except возвращает последовательность, содержащую все элементы первой последовательности, которых нет во второй последовательности.

        public static int DistinctWithComparer()
        {
            IEqualityComparer<President> comparer = new GroupByComparer();
            return presidents.Distinct(comparer).Count();
        }
        #endregion


        #region Преобразование
        // Операция Cast предназначена для вызова на классах, реализующих интерфейс IEnumerable, а не IEnumerable<T>.
        // В частности, речь идет об унаследованных коллекциях, разработанных до появления версии C# 2.0 и обобщений.
        // Если элемент не может быть приведен к типу T, генерируется исключение.
        // Операция OfType генерирует выходную последовательность, содержащей только те элементы, которые могут быть успешно преобразованы к указанному типу.
        // После преобразования IEnumerable в IEnumerable<T> можно вызывать стандартные операции запросов.

        // Операция AsEnumerable возвращает входную последовательность типа IEnumerable<T> как тип IEnumerable<T>.
        // LINQ to SQL использует собственный тип последовательности — IQueryable<T> — и реализует собственные операции.
        // Вызов метода Where на последовательности типа IQueryable<T> — это на самом деле вызов метода Where из LINQ to SQL,
        // а не стандартная операция запроса Where из LINQ to Objects. Попытка вызова одной из стандартных операций LINQ to Objects
        // приведет к исключению, если только не окажется одноименной операции LINQ to SQL. С помощью операции AsEnumerbale
        // можно выполнить приведение последовательности IQueryable<T> к последовательности IEnumerable<T>, что позволит вызывать
        // на ней стандартные операции запросов. Это бывает очень удобно, когда нужно контролировать, в каком API-интерфейсе вызывается операция.

        #endregion


        #region Элемент
        // Операция DefaultIfEmpty возвращает последовательность, содержащую элемент по умолчанию, если входная последовательность пуста.
        // Первый прототип операции DefaultIfEmpty возвращает объект, который при перечислении входной последовательности source
        // выдает каждый ее элемент, если только последовательность не окажется пустой — тогда возвращается последовательность из
        // одного элемента default(T). Для ссылочных и допускающих null типов значением по умолчанию является null.
        // Второй прототип позволяет указать значение по умолчанию.

        // Эта операция полезна для всех других операций, которые генерируют исключения в случае пустой входной последовательности.
        // string jones = presidents.Where(n => n.Equals("Jones")).First();
        // Если президент с именем Jones не найден, то генериться исключение.
        // string jones = presidents.Where(n => n.Equals("Jones")).DefaultIfEmpty().First();
        // Здесь операции First передается не пустая последовательность (как в прошлом примере), а последовательность, содержащая
        // один элемент null. Или такой вариант:
        // string jones = presidents.Where(n => n.Equals("Jones")).DefaultIfEmpty("Jones not found").First();
        #endregion


        #region Генерация
        // Операции генерации помогают в генерации последовательностей.
        // Range(), Repeat(), Empty()
        // Эти три метода не расширяющие, а статические методы класса System.Linq.Enumerable.

        #endregion

        // Неотложенные операции Nondeferred

        #region Преобразование
        // ToLookup() - Lookup() is immutable.
        // Difference between Lookup() and Dictionary<TKey, List<TValue>>
        // Как Dictionary<Key, List<Value>>, так и Lookup<Key, Value> логически могут хранить данные, организованные
        // аналогичным образом, и оба они имеют один и тот же порядок эффективности. Основное отличие заключается в том,
        // что Lookup() неизменяем: у него нет методов Add() и общедоступного конструктора.
        // Когда вы ищете ключ, которого нет в Lookup(), вы получаете пустую последовательность вместо KeyNotFoundException.
        // Если у вас есть последовательность данных и вы просто хотите просмотреть данные только для чтения,
        // организованные по ключу, то Lookup() очень легко построить, и вы получите моментальный снимок только для чтения.
        // 
        #endregion


        #region Эквивалентность
        // presidents.SequenceEqual()
        #endregion


        #region Элемент
        // Может возникнуть вопрос: чем отличается эта операция от вызова операции Take(1)?
        // Отличие в том, что Take возвращает последовательность элементов, даже если она состоит всего из одного элемента.
        // Операция First всегда возвращает в точности один элемент либо генерирует исключение, если возвращать нечего.

        // Single Если последовательность source пуста, либо predicate ни разу не вернул true или нашел более одного элемента,
        // для которых вернул true, генерируется исключение InvalidOperationException.

        // string name = presidents.LastOrDefault(p => p.StartsWith("B")); // -- Bush
        // string name = presidents.LastOrDefault(p => p.StartsWith("Z")); // -- null

        // Операция ElementAt возвращает элемент из исходной последовательности по указанному индексу.
        // Если последовательность реализует IList<T>, то интерфейс IList используется для извлечения индексированного элемента
        // непосредственно. Если же последовательность не реализует IList<T>, то последовательность перечисляется до тех пор,
        // пока не будет достигнут указанный индексом элемент.
        #endregion


        #region Квантификаторы
        // Операция Contains возвращает true, если любой элемент входной последовательности соответствует указанному значению.
        // Этот прототип операции Contains сначала проверяет входную последовательность на предмет реализации ею
        // интерфейса ICollection<T>, и если она его реализует, то вызывается метод Contains реализации последовательности.
        // Если последовательность не реализует интерфейс ICollection<T>, входная последовательность source перечисляется
        // с проверкой соответствия каждого элемента указанному значению.

        #endregion


        #region Агрегация

        // Операция Count() возвращает общее количество элементов во входной последовательности, проверяя сначала,
        // реализует ли она интерфейс ICollection<T>, и если да, то получает счетчик последовательности через реализацию
        // этого интерфейса. Если же входная последовательность source не реализует интерфейс ICollection<T>, тогда перечисляется
        // вся последовательность и подсчитывается количество элементов.

        // Max(), Min() возвращают только само значение, а не элемент последовательности с этим значением (для пользовательских типов).
        // Операция Min() возвращает элемент с минимальным числовым значением из входной последовательности source.
        // Если тип элемента реализует интерфейс IComparable<T>, этот интерфейс применяется для сравнения элементов.
        // Если же элемент не реализует интерфейс IComparable<T>, будет использован необобщенный интерфейс IComparable.
        // Пустая последовательность либо последовательность, состоящая только из значений null, вернет null.

        // Операция Average возвращает среднее арифметическое числовых значений элементов входной последовательности.

        // Операция Aggregate выполняет указанную пользователем функцию на каждом элементе входной последовательности,
        // передавая значение, возвращенное этой функцией для предыдущего элемента, и возвращая ее значение для последнего элемента.

        // Min и Max на нечисловых последовательностях
        public static string GetMaxLastName()
        {
            // Если для типа реализован IComparable, то можно найти Min и Max значения
            var a = presidents.Max(p => p.LastName);
            return a;
        }

        // Факториал числа
        // Применяя эту версию операции Aggregate, следует проявлять осторожность, чтобы первый элемент не был обработан дважды,
        // поскольку он передается в качестве входного для первого элемента. Здесь первый вызов лямбда-выражения func должен
        // получить 1 и 1. Поскольку два этих значения только перемножаются, и оба они равны 1, никакого вредного побочного эффекта
        // нет. Но если бы нужно было складывать два значения, то в итоговую сумму первый элемент был бы включен дважды.
        public static int GetFactorial(int n)
        {
            IEnumerable<int> intSequence = Enumerable.Range(1, n);
            // av == aggregated value, e == element
            int agg = intSequence.Aggregate((av, e) => av * e);
            return agg;
        }

        // Сумма чисел
        public static int GetSum(int n)
        {
            IEnumerable<int> intSequence = Enumerable.Range(1, n);
            // av == aggregated value, e == element
            int sum = intSequence.Aggregate(0, (s, i) => s + i);
            return sum;
        }

        #endregion


        //-------------------------------- ПРЕЗИДЕНТЫ США --------------------------


        // Самый пожилой президент на момент вступления в должность - Джо Байден, достигший к тому моменту возраста 78 лет.

        // Кто был самым пожилым президентом на момент вступления в должность?
        public static (string, int, int) GetTheOldestPresident()
        {
            var age = presidents.Max(x => GetSubtractYearAndDays(x.BirthDate, x.TermBegin));
            var president = presidents.First(p => p.BirthDate.AddYears(age.Item1).AddDays(age.Item2) == p.TermBegin);
            return ($"{president.FirstName} {president.LastName}", age.Item1, age.Item2);
        }


        // Фактически самым молодым президентом стал Теодор Рузвельт, вступивший в должность в возрасте 42 лет и 10 месяцев,
        // но он не был избран, а стал президентом после убийства в 1901 году Уильяма Мак-Кинли.
        // ( Здесь и выше вычислительная сложность O(n^2) )
        // Min и Max не возвращают элемент последовательности (в пользовательских типах), а только само значение.
        // Можно написать свой extended method для этой ситуации.

        // Кто был самым молодым президентом на момент вступления в должность?
        public static (string, int, int) GetTheYoungestPresident()
        {
            var age = presidents.Min(x => GetSubtractYearAndDays(x.BirthDate, x.TermBegin));
            var president = presidents.First(p => p.BirthDate.AddYears(age.Item1).AddDays(age.Item2) == p.TermBegin);
            return ($"{president.FirstName} {president.LastName}", age.Item1, age.Item2);
        }


        // Самым молодым избранным президентом стал Джон Кеннеди, вступивший в должность в возрасте 43 лет.
        // Фактически же самым молодым президентом стал Теодор Рузвельт, вступивший в должность в возрасте 42 лет и 10 месяцев,
        // но он не был избран, а стал президентом после убийства в 1901 году Уильяма Мак-Кинли.

        // Кто был самым молодым ИЗБРАННЫМ президентом на момент вступления в должность?
        public static (string, int, int) GetTheYoungestElectedPresident()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                                                  x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                                                  x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);
            var orderByTerm = presidents.Select(p => new { president = p,
                inaugurationAge = GetSubtractYearAndDays(p.BirthDate, p.TermBegin) })
                                        .OrderBy(p => p.inaugurationAge);
            var p = orderByTerm.First(p => isInauguralDate(p.president.TermBegin));
            return ($"{p.president.FirstName} {p.president.LastName}", p.inaugurationAge.Item1, p.inaugurationAge.Item2);
        }


        // Выведите список ныне живущих (на 2 февраля 2022 года) бывших президентов США
        // Формат вывода: имя и фамилия президента, срок полномочий в годах, возраст в годах.
        public static IEnumerable<string> GetExPresidents()
        {
            var today = new DateTime(2022, 2, 2);
            return presidents.Distinct()
                             .Where(p => p.DeathDate.Year == 1 && p.TermEnd.Year != 1)
                             .Select(p => $"{p.FirstName} {p.LastName} {p.TermBegin.Year}-{p.TermEnd.Year} {GetSubtractYearAndDays(p.BirthDate, today).Item1} old years");
        }


        // Всеобщее голосование (за выборщиков) происходит в первый вторник после первого понедельника ноября в год,
        // деление которого на четыре не образует остатка.
        // Сейчас вновь избранный президент и вице-президент вступают в должность в полдень 20 января следующего года после выборов.
        // До принятия в 1933 году Двадцатой поправки к Конституции США датой инаугурации было 4 марта.

        // Все ли президенты вступали в должность согласно этому правилу?
        public static bool IsCorrectTermBegin()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                                      x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                                      x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.All(p => isInauguralDate(p.TermBegin));
        }


        // Выведите список президентов (его номер, имя и фамилию, упорядоченные по номеру президента),
        // которые вступали в должность не будучи избранными, а после смерти действующего президента.
        public static IEnumerable<string> GetPresidentsNotInauguralDay()
        {
            return presidents.Where(p => p.DeathDate == p.TermEnd && p.TermEnd.Year != 1)
                             .Join(presidents,
                                   n => n.Number + 1,
                                   p => p.Number,
                                   (n, p) => p)
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName}");
        }


        // Смерть в должности президента:
        // четыре президента — Уильям Гаррисон, Закари Тейлор, Уоррен Гардинг и Франклин Рузвельт — умерли в должности своей смертью,
        // четыре — Авраам Линкольн, Джеймс Гарфилд, Уильям Мак-Кинли и Джон Кеннеди — были убиты.

        // Выведите список президентов (его номер, имя и фамилию, упорядоченные по номеру президента), умерших на своем посту.
        public static IEnumerable<string> GetPresidentsDeadTerm()
        {
            return presidents.Where(p => p.DeathDate == p.TermEnd && p.TermEnd.Year != 1)
                             .OrderBy(p => p.Number)
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName}");
        }


        // Гровер Кливленд (Grover Cleveland) единственный президент США, занимавший свой пост два срока с перерывом и соответственно
        // получивший двойную нумерацию в списке президентов (в 1885—1889 год как 22-й президент, и в 1893—1897 год как 24-й президент).

        // Сколько президентов занимали пост два срока с перерывом.
        public static int GetNonConsecutiveTermsNumbers()
        {
            return presidents.GroupBy(p => p)
                             .Count(p => p.Count() > 1);
        }

        // Выведите номера, имя и фамилию президентов, которые занимали пост два срока с перерывом.
        public static IEnumerable<string> GetNonConsecutiveTermsNames()
        {
            return presidents.GroupBy(p => p)
                             .Where(p => p.Count() > 1)
                             .Select(p => $"({string.Join(",", p.Select(n => n.Number).ToList())}) - {p.Key.FirstName} {p.Key.LastName}");
        }


        // Три президента были прямыми потомками предшественников:
        //   6-й президент Джон Куинси Адамс — сын 2-го президента Джона Адамса.
        //   23-й президент Бенджамин Гаррисон — внук 9-го президента Уильяма Г. Гаррисона.
        //   43-й президент Джордж Буш-младший — сын 41-го президента Джорджа Буша-старшего.
        //   26-й и 32-й президенты Рузвельты приходятся друг другу дальними родственниками.
        //   17-й и 36-й президенты Andrew Johnson и Lyndon Johnson однофамильцы.

        // Выведите список президентов с одинаковыми фамилиями.
        // Формат вывода: номер президента, его имя и фамилия.
        public static IEnumerable<string> GetNamesakes()
        {
            return presidents.Distinct()
                             .GroupBy(p => p.LastName)
                             .Where(p => p.Count() > 1)
                             .SelectMany(p => p)
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName}");
        }


        // Джон Адамс умер 4 июля 1826 года, ровно через 50 лет после принятия Декларации независимости. Его последними словами были:
        // «Томас Джефферсон ещё жив». В действительности Джефферсон, его главный политический соперник, уже был мёртв.
        // Он умер в тот же день, несколькими часами ранее.

        // 4 июля 1776 - день принятия (но не подписания) Декларации независимости США
        // Какие президенты умерли в День независимости?
        // Формат вывода: номер президента, его имя и фамилия и год смерти.
        public static IEnumerable<string> GetIndependenceDayDead()
        {
            return presidents.Distinct()
                             .Where(p => p.DeathDate.Day == 4 && p.DeathDate.Month == 7)
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName} {p.DeathDate.Year}");
        }


        // Он был первым президентом США, избранным от Демократической партии, и считается одним из её основателей.
        // Кто это? Эндрю Джексон
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetFirstDemocraticPresident()
        {
            return presidents.Where(p => p.Party == PartyAffiliation.Democratic)
                             .OrderBy(p => p.Number)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}")
                             .First();
        }


        // Партия вигов (Whig Party) — политическая партия Соединённых Штатов, существовавшая в 1832—1856 годах.
        // Партия возникла как оппозиция Эндрю Джексону и Демократической партии.
        // Название партии восходит к прозвищу американских патриотов во времена Американской революции, боровшихся против британской метрополии,
        // когда вигами (от названия британской партии вигов) называли людей, выступавших против авторитарного правления.

        // Выведите список президентов, членов партии вигов (4 президента)
        // Формат вывода: номер президента, его имя и фамилия.
        public static IEnumerable<string> GetWhigPresidents()
        {
            return presidents.Where(p => p.Party == PartyAffiliation.Whig)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}");
        }


        // Кто стал первым президентом, вступившим на должность не по избранию, а как вице-президент после кончины действующего главы государства?
        // John Tyler
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetNotElectedPresident()
        {
            var p = presidents.Where((p, i) => 0 < i && p.TermBegin == presidents[i - 1].DeathDate)
                              .OrderBy(p => p.Number)
                              .First();

            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }


        // James Polk умер через 103 дня после истечения полномочий (самое короткое пребывание в статусе экс-президента), предположительно от холеры.
        // Он прожил меньше всех президентов, не считая убитых Кеннеди и Гарфилда.

        // Кто прожил меньше всех в статусе экс-президента? 
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetDeadExPresident()
        {
            var p = presidents.Where(p => p.DeathDate.Year != 1)
                              .Select(p => new { president = p, ex = GetSubtractYearAndDays(p.TermEnd, p.DeathDate) })
                              .OrderBy(p => p.ex)
                              .First(p => p.ex.Item1 != 0 || p.ex.Item2 != 0);
            return $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}";
        }

        // У какого президента продолжительность жизни была наименьшей?
        // Формат вывода: номер президента, его имя и фамилия, возраст смерти.
        public static string GetLeastLifeSpan()
        {
            var p = presidents.Where(p => p.DeathDate.Year != 1)
                              .Select(p => new { president = p, age = GetSubtractYearAndDays(p.BirthDate, p.DeathDate) })
                              .OrderBy(p => p.age)
                              .First(p => p.age.Item1 != 0 || p.age.Item2 != 0);
            return $"{p.president.Number} - {p.president.FirstName} {p.president.LastName} ({p.age.Item1} years and {p.age.Item2} days)";
        }


        // Согласно 22-й поправке к Конституции одно и то же лицо может быть избрано президентом США
        // не более двух раз(неважно, подряд или с перерывом). Кроме того, если некоторое лицо после смерти или отставки
        // избранного президента занимало президентский пост(с поста вице-президента или иначе) на протяжении 2 лет и более,
        // то это лицо в дальнейшем может быть избрано президентом не более 1 раза.
        // Несмотря на то, что эта поправка была принята в 1951, все ли президенты соблюдали это правило?
        // Выведите список таких президентов.
        // Франклин Д.Рузвельт единственный американский президент, избиравшийся более чем на два срока. 
        // В 1940 году Франклин Д.Рузвельт был избран на третий срок, а в 1944 году — и на четвёртый (умер в 1945 году).
        public static IEnumerable<string> GetMoreTwoTermsPresidents()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, years = p.Sum(d => GetSubtractYearAndDays(d.TermBegin, d.TermEnd).Item1) })
                             .Where(p => p.years > 8)
                             .Select(p => $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}");
        }

        // Сколько президентов избиралось более чем на 2 срока?
        public static int GetMoreTwoTermNumber()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, years = p.Sum(d => GetSubtractYearAndDays(d.TermBegin, d.TermEnd).Item1) })
                             .Count(p => p.years > 8);
        }

        // Сколько раз был избран президентом (вступал в должность) 32-й президент Франлин Делано Рузвельт? 1933, 1937, 1941, 1945
        public static int GetRooseveltTermNumber()
        {
            var president = presidents.Single(p => p.Number == 32);
            return GetInauguralDateNumber(president.TermBegin, president.TermEnd);
        }

        // Сколько президентов выигрывали выборы 2 раза?
        public static int GetElectionVictoryTwoTimes()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, victories = p.Sum(d => GetInauguralDateNumber(d.TermBegin, d.TermEnd)) })
                             .Count(p => p.victories == 2);
        }

        // Выведите список президентов, дважды побеждавших на выборах.
        public static IEnumerable<string> GetElectionVictoryTwoTimesPresidents()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, victories = p.Sum(d => GetInauguralDateNumber(d.TermBegin, d.TermEnd)) })
                             .Where(p => p.victories == 2)
                             .Select(p => $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}"); ;
        }


        // Выведите количество президентов, которые никогда не побеждали на выборах президента страны,
        // то есть заняли этот пост после отставки или смерти предыдущего президента.
        public static int GetElectionVictoryZeroTime()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, victories = p.Sum(d => GetInauguralDateNumber(d.TermBegin, d.TermEnd)) })
                             .Count(p => p.victories == 0);
        }


        // Выведите список президентов, которые никогда не побеждали на выборах президента страны,
        // то есть заняли этот пост после отставки или смерти предыдущего президента.
        public static IEnumerable<string> GetElectionVictoryZeroTimePresidents()
        {
            return presidents.Where(p => p.TermEnd.Year != 1)
                             .GroupBy(p => p)
                             .Select(p => new { president = p.Key, victories = p.Sum(d => GetInauguralDateNumber(d.TermBegin, d.TermEnd)) })
                             .Where(p => p.victories == 0)
                             .Select(p => $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}"); ;
        }


        // Выведите список президентов, которые победили на выборах, а затем покинули свою должность в результате отставки или смерти,
        // упорядоченный по возрастанию количества дней нахождения в должности после последней инаугурации.
        public static IEnumerable<string> GetIncompleteTerm()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                          x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                          x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.Where(p => p.TermEnd.Year != 1 && (p.DeathDate == p.TermEnd || !isInauguralDate(p.TermEnd)))
                             .Select(p => new { president = p, last = GetSubtractYearAndDays(GetLastInauguralDate(p.TermBegin, p.TermEnd), p.TermEnd) })
                             .OrderBy(p => p.last)
                             .Select(p => $"{p.president.Number} - {p.president.FirstName} {p.president.LastName} {p.last.Item1} years and {p.last.Item2} days");
        }


        // Никсон единственный президент США, ушедший в отставку до окончания срока.

        // Были ли президенты США, ушедшие в отставку до окончания срока?
        public static bool GetIsTermEndedEarly()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                          x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                          x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.All(p => p.TermEnd == p.DeathDate || isInauguralDate(p.TermEnd));
        }

        // Сколько президентов США ушло в отставку до окончания срока?
        public static int GetTermEndedEarlyNumber()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                          x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                          x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.Count(p => p.TermEnd != p.DeathDate && !isInauguralDate(p.TermEnd));
        }

        // Кто из президентов США ушел в отставку до окончания срока?
        // Выведите список: номер президента, его имя и фамилия.
        public static IEnumerable<string> GetTermEndedEarlyPresidents()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                          x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                          x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.Where(p => p.TermEnd != p.DeathDate && !isInauguralDate(p.TermEnd))
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName}");
        }



        // Джимми Картер самый долгоживущий президент в истории США.
        // 1 октября 2018 года политик стал вторым после Джорджа Буша-старшего бывшим президентом США, достигшим возраста 94 года,
        // а спустя ещё год — первым президентом, дожившим до 95 лет.

        // Кто стал первым президентом, достигшим возраста 94 лет?
        public static string GetFirst94thPresident()
        {
            var p = presidents.Select(p => new { president = p,
                                                 age = GetSubtractYearAndDays(p.BirthDate, p.DeathDate.Year != 1 ? p.DeathDate : DateTime.Now) })
                              .Where(p => p.age.Item1 >= 94)
                              .OrderBy(p => p.president.BirthDate)
                              .First();

            return $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}";
        }

        // Кто стал вторым президентом, достигшим возраста 94 лет?
        public static string GetSecond94thPresident()
        {
            var p = presidents.Select(p => new
                              {
                                  president = p,
                                  age = GetSubtractYearAndDays(p.BirthDate, p.DeathDate.Year != 1 ? p.DeathDate : DateTime.Now)
                              })
                              .Where(p => p.age.Item1 >= 94)
                              .OrderBy(p => p.president.BirthDate)
                              .Skip(1)
                              .First();

            return $"{p.president.Number} - {p.president.FirstName} {p.president.LastName}";
        }


        // 25 ноября 2017 года Дж. Буш превзошёл Дж. Форда по продолжительности жизни (93 года 5 месяцев и 12 дней),
        // став самым долгоживущим в истории Президентом США.

        // На момент своей смерти Gerald Ford стал имел самую высокую продолжительность жизни.
        // Кто первым превзошел его и когда?
        public static string GetBushIsTheOldestDate()
        {
            var ford = presidents.Single(p => p.FirstName == "Gerald" && p.LastName == "Ford");
            var lifespan = GetSubtractYearAndDays(ford.BirthDate, ford.DeathDate);
            var p = presidents.Where(p => ford.BirthDate <= p.BirthDate)
                              .Select(p => new
                              {
                                  president = p,
                                  date = p.BirthDate.AddYears(lifespan.Item1).AddDays(lifespan.Item2 + 1)
                              })
                              .Where(p => p.date < DateTime.Now && (p.date <= p.president.DeathDate || IsLiving(p.president.DeathDate) ))
                              .OrderBy(p => p.date)
                              .First();

            return $"{p.president.Number} - {p.president.FirstName} {p.president.LastName} at {p.date.ToShortDateString()}";
        }


        // Шесть Президентов Соединённых Штатов Америки, преодолевших 90-летний рубеж (в хронологическом порядке:
        // Джон Адамс, Герберт Гувер, Рональд Рейган, Джеральд Форд, Джордж Буш, Джимми Картер).
        public static IEnumerable<string> GetOverNintyYearsPresidents()
        {
            Predicate<DateTime> isInauguralDate = (x => x.Year <= 1933 ?
                          x.Day == 4 && x.Month == 3 && x.Year % 4 == 1 :
                          x.Day == 20 && x.Month == 1 && x.Year % 4 == 1);

            return presidents.Where(p => GetSubtractYearAndDays(p.BirthDate, p.DeathDate.Year != 1 ? p.DeathDate : DateTime.Now).Item1 >= 90)
                             .OrderBy(p => p.BirthDate)
                             .Select(p => $"{p.Number} - {p.FirstName} {p.LastName}");
        }


        #region Задачи типа "при каком президенте произошло это событие"

        // Столицей США с 1800 года является Вашингтон. До этого столицами были Филадельфия, Балтимор, Нью-Йорк и др.
        // При каком президенте это произошло?
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetWashingtonIsCapital()
        {
            const int year = 1800;
            return presidents.Where(p => p.TermBegin.Year <= year && p.TermEnd.Year >= year)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}")
                             .First();
        }


        // Покупка у Франции Луизианы в 1803 удвоило территорию страны. При каком президенте это произошло? Thomas Jefferson 
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetLouisianaPurchase()
        {
            const int year = 1803;
            return presidents.Where(p => p.TermBegin.Year <= year && p.TermEnd.Year >= year)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}")
                             .First();
        }


        // Введение всеобщего десятичасового рабочего дня в США произошло в 1840. При каком президенте это случилось? Мартин Ван Бюрен
        // Формат вывода: номер президента, его имя и фамилия.
        public static string Get10WorkingHoursPresident()
        {
            const int year = 1840;
            return presidents.Where(p => p.TermBegin.Year <= year && p.TermEnd.Year >= year)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}")
                             .First();
        }


        // Покупка Аляски и Алеутских островов у России произошла в 1867. При каком президенте? Эндрю Джексон
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetAlaskaPurchase()
        {
            const int year = 1867;
            return presidents.Where(p => p.TermBegin.Year <= year && p.TermEnd.Year >= year)
                             .Select(p => $"{p.Number} {p.FirstName} {p.LastName}")
                             .First();
        }


        // Какой прездент первым родился в XIX веке? Франклин Пирс
        // Формат вывода: номер президента, его имя и фамилия, дата рождения.
        public static string GetFirstBornInXIX()
        {
            var p = presidents.Where(p => p.BirthDate.Year >= 1801)
                              .OrderBy(p => p.BirthDate.Year)
                              .First();
            return $"{p.Number} - {p.FirstName} {p.LastName} born {p.BirthDate.ToLongDateString()}";
        }


        // Какой прездент первым родился в XX веке?
        // Формат вывода: номер президента, его имя и фамилия, дата рождения.
        public static string GetFirstBornInXX()
        {
            var p = presidents.Where(p => p.BirthDate.Year >= 1901)
                              .OrderBy(p => p.BirthDate.Year)
                              .First();
            return $"{p.Number} - {p.FirstName} {p.LastName} born {p.BirthDate.ToLongDateString()}";
        }


        // Гражданская война окончилась капитуляцией Конфедеративных Штатов Америки 9 апреля 1865 года. Стране предстояло провести
        // Реконструкцию Юга и начать процесс интеграции темнокожих в американское общество. Через пять дней после окончания войны,
        // в день Страстной пятницы, 14 апреля 1865 года, на спектакле «Наш американский кузен» (в театре Форда) сторонник южан
        // актёр Джон Уилкс Бут проник в президентскую ложу и выстрелил Линкольну в голову из пистолета. Утром следующего дня,
        // не приходя в сознание, Авраам Линкольн скончался.

        // Гражданская война окончилась капитуляцией Конфедеративных Штатов Америки 9 апреля 1865 года.
        // Какой президент был тогда у власти?
        // Формат вывода: номер президента, его имя и фамилия.
        public static string GetAmericanCivilWarEnd()
        {
            var warEnd = new DateTime(1865, 4, 9);
            var p = presidents.Single(p => p.TermBegin < warEnd && warEnd < p.TermEnd);
            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }


        // Панамский канал официально открыт 12 июня 1920 года.
        // При каком президенте это произошло? Вудро Вильсон
        // Выведите номер президента, его имя и фамилию.
        public static string GetPanamaCanal()
        {
            var canal = new DateTime(1920, 6, 12);
            var p = presidents.Single(p => p.TermBegin < canal && canal < p.TermEnd);
            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }


        // США не удалось при помощи дипломатии избежать войны с Японией, которые атаковали Пёрл-Харбор пвоскресным утром 7 декабря 1941 года.
        public static string GetAttackOnPearlHarbor()
        {
            var attack = new DateTime(1941, 12, 7);
            var p = presidents.Single(p => p.TermBegin < attack && attack < p.TermEnd);
            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }


        // 6 августа 1945 года в 08:15 по местному времени американский бомбардировщик B-29 «Enola Gay» сбросил на Хиросиму первую атомную бомбу.
        // При каком президенте это произошло? Гарри Трумэн
        // Выведите номер президента, его имя и фамилию.
        public static string GetAttackNagasaki()
        {
            var attack = new DateTime(1945, 8, 6);
            var p = presidents.Single(p => p.TermBegin < attack && attack < p.TermEnd);
            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }


        // Образованы и приняты в состав США штаты Аляска и Гавайи (1959) — последнее на сегодняшний день увеличение числа штатов.
        // При каком президенте это произошло? Эзенхауэр
        // Выведите номер президента, его имя и фамилию.
        public static string GetJoiningAlaska()
        {
            int year = 1959;
            var p = presidents.Single(p => p.TermBegin.Year < year && year < p.TermEnd.Year);
            return $"{p.Number} - {p.FirstName} {p.LastName}";
        }

        #endregion



    }
}
