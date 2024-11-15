using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using PresidentTask;

namespace TasksTest
{
    class PresidentTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [TestCase("07/01/1978", "23/01/2023", 45, 16)]
        [TestCase("20/03/1978", "23/01/2023", 44, 309)]
        public void GetTest(string startDate, string endDate, int years, int days)
        {
            DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            (int, int) expected = PresidentTask.PresidentTask.GetSubtractYearAndDays(start, end);
            Assert.AreEqual(expected, (years, days));
        }

        #region Тренировка
        [Test]
        public void DeferredExecutionTest()
        {
            Assert.AreEqual(0, PresidentTask.PresidentTask.DeferredExecution());
        }

        [Test]
        public void StartsWithWTest()
        {
            var presidentsW = new List<string>() { "Washington", "Wilson" };
            Assert.AreEqual(presidentsW, PresidentTask.PresidentTask.StartsWithW());
        }

        [Test]
        public void GetNameIsJamesTest()
        {
            var expected = new List<string>() { "Madison", "Monroe", "Polk", "Buchanan", "Garfield", "Carter" };
            var actual = PresidentTask.PresidentTask.GetNameIsJames();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetThreePresidentsTest()
        {
            var expected = new List<string>() { "Washington", "Adams", "Jefferson" };
            var actual = PresidentTask.PresidentTask.GetThreePresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNotMiddleNamePresidentsTest()
        {
            var expected = new List<string>() { "Washington", "Adams", "Jefferson", "Madison", "Monroe" };
            var actual = PresidentTask.PresidentTask.GetNotMiddleNamePresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderTermAgeTest()
        {
            var actual = PresidentTask.PresidentTask.OrderTermAge().Count;
            Assert.AreEqual(46, actual);
        }

        [Test]
        public void GroupByPartyTest()
        {
            var expected = new List<string>() { "Federalist: 1", "Unaffiliated: 1", "Democratic_Republican: 4",
                                                "Whig: 4", "Democratic: 16", "Republican: 20" };
            var actual = PresidentTask.PresidentTask.GroupByParty();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GroupByLastNameTest()
        {
            var expected = new List<string>() { "Adams", "Bush", "Cleveland", "Harrison", "Johnson", "Roosevelt" };
            var actual = PresidentTask.PresidentTask.GroupByLastName();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFirstNamesNumberTest()
        {
            var expected = 30;
            var actual = PresidentTask.PresidentTask.GetFirstNamesNumber();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DistinctWithComparerTest()
        {
            var expected = 45;
            var actual = PresidentTask.PresidentTask.DistinctWithComparer();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMaxLastNameTest()
        {
            var expected = "Wilson";
            var actual = PresidentTask.PresidentTask.GetMaxLastName();
            Assert.AreEqual(expected, actual);
        }

        #endregion

        [Test]
        public void GetTheOldestPresidentTest()
        {
            var expected = ("Joseph Biden", 78, 61);
            var actual = PresidentTask.PresidentTask.GetTheOldestPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTheYoungestPresidentTest()
        {
            var expected = ("Theodore Roosevelt", 42, 322);
            var actual = PresidentTask.PresidentTask.GetTheYoungestPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTheYoungestElectedPresidentTest()
        {
            var expected = ("John Kennedy", 43, 236);
            var actual = PresidentTask.PresidentTask.GetTheYoungestElectedPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsCorrectTermBeginTest()
        {
            Assert.AreEqual(false, PresidentTask.PresidentTask.IsCorrectTermBegin());
        }

        [Test]
        public void GetPresidentsNotInauguralDayTest()
        {
            var expected = new string[] {
                "10 - John Tyler",
                "13 - Millard Fillmore",
                "17 - Andrew Johnson",
                "21 - Chester Arthur",
                "26 - Theodore Roosevelt",
                "30 - Calvin Coolidge",
                "33 - Harry Truman",
                "36 - Lyndon Johnson"
            };
            var actual = PresidentTask.PresidentTask.GetPresidentsNotInauguralDay();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetPresidentsDeadTermTest()
        {
            var expected = new string[] {
                "9 - William Harrison",
                "12 - Zachary Taylor",
                "16 - Abraham Lincoln",
                "20 - James Garfield",
                "25 - William McKinley",
                "29 - Warren Harding",
                "32 - Franklin Roosevelt",
                "35 - John Kennedy"
            };
            var actual = PresidentTask.PresidentTask.GetPresidentsDeadTerm();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMoreTwoTermsPresidentsTest()
        {
            var expected = new string[] { "32 - Franklin Roosevelt" };
            var actual = PresidentTask.PresidentTask.GetMoreTwoTermsPresidents();
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void GetNonConsecutiveTermsNumbersTest()
        {
            Assert.AreEqual(1, PresidentTask.PresidentTask.GetNonConsecutiveTermsNumbers());
        }

        [Test]
        public void GetNonConsecutiveTermsNamesTest()
        {
            var expected = new string[] { "(22,24) - Grover Cleveland" };
            var actual = PresidentTask.PresidentTask.GetNonConsecutiveTermsNames();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNamesakesTest()
        {
            var expected = new string[] {
                "2 - John Adams",
                "6 - John Adams",
                "9 - William Harrison",
                "23 - Benjamin Harrison",
                "17 - Andrew Johnson",
                "36 - Lyndon Johnson",
                "26 - Theodore Roosevelt",
                "32 - Franklin Roosevelt",
                "41 - George Bush",
                "43 - George Bush"
            };
            var actual = PresidentTask.PresidentTask.GetNamesakes();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIndependenceDayDeadTest()
        {
            var expected = new string[] {
                "2 - John Adams 1826",
                "3 - Thomas Jefferson 1826",
                "5 - James Monroe 1831"
            };
            var actual = PresidentTask.PresidentTask.GetIndependenceDayDead();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetExPresidentsTest()
        {
            var expected = new string[] {
                "James Carter 1977-1981 97 old years",
                "William Clinton 1993-2001 75 old years",
                "George Bush 2001-2009 75 old years",
                "Barack Obama 2009-2017 60 old years",
                "Donald Trump 2017-2021 75 old years"
            };
            var actual = PresidentTask.PresidentTask.GetExPresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLouisianaPurchaseTest()
        {
            var expected = "3 Thomas Jefferson";
            var actual = PresidentTask.PresidentTask.GetLouisianaPurchase();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetWashingtonIsCapitalTest()
        {
            var expected = "2 John Adams";
            var actual = PresidentTask.PresidentTask.GetWashingtonIsCapital();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFirstDemocraticPresidentTest()
        {
            var expected = "7 Andrew Jackson";
            var actual = PresidentTask.PresidentTask.GetFirstDemocraticPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetWhigPresidentsTest()
        {
            var expected = new string[] {
                "9 William Harrison",
                "10 John Tyler",
                "12 Zachary Taylor",
                "13 Millard Fillmore"
            };
            var actual = PresidentTask.PresidentTask.GetWhigPresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get10WorkingHoursPresidentTest()
        {
            var expected = "8 Martin Van Buren";
            var actual = PresidentTask.PresidentTask.Get10WorkingHoursPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAlaskaPurchaseTest()
        {
            var expected = "17 Andrew Johnson";
            var actual = PresidentTask.PresidentTask.GetAlaskaPurchase();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNotElectedPresidentTest()
        {
            var expected = "10 - John Tyler";
            var actual = PresidentTask.PresidentTask.GetNotElectedPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetDeadExPresidentTest()
        {
            var expected = "11 - James Polk";
            var actual = PresidentTask.PresidentTask.GetDeadExPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeastLifeSpanTest()
        {
            var expected = "35 - John Kennedy (46 years and 177 days)";
            var actual = PresidentTask.PresidentTask.GetLeastLifeSpan();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFirstBornInXIXTest()
        {
            var expected = "14 - Franklin Pierce born Friday, November 23, 1804";
            var actual = PresidentTask.PresidentTask.GetFirstBornInXIX();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFirstBornInXXTest()
        {
            var expected = "36 - Lyndon Johnson born Thursday, August 27, 1908";
            var actual = PresidentTask.PresidentTask.GetFirstBornInXX();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAmericanCivilWarEndTest()
        {
            var expected = "16 - Abraham Lincoln";
            var actual = PresidentTask.PresidentTask.GetAmericanCivilWarEnd();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMoreTwoTermNumberTest()
        {
            var expected = 1;
            var actual = PresidentTask.PresidentTask.GetMoreTwoTermNumber();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Get32TermNumberTest()
        {
            var expected = 4;
            var actual = PresidentTask.PresidentTask.GetRooseveltTermNumber();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIsTermEndedEarlyTest()
        {
            Assert.IsFalse(PresidentTask.PresidentTask.GetIsTermEndedEarly());
        }

        [Test]
        public void GetTermEndedEarlyNumberTest()
        {
            var expected = 1;
            var actual = PresidentTask.PresidentTask.GetTermEndedEarlyNumber();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTermEndedEarlyPresidentsTest()
        {
            var expected = new[] { "37 - Richard Nixon" };
            var actual = PresidentTask.PresidentTask.GetTermEndedEarlyPresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetOverNintyYearsPresidentsTest()
        {
            var expected = new[] {
                "2 - John Adams",
                "31 - Herbert Hoover",
                "40 - Ronald Reagan",
                "38 - Gerald Ford",
                "41 - George Bush",
                "39 - James Carter"
            };
            var actual = PresidentTask.PresidentTask.GetOverNintyYearsPresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetElectionVictoryTwoTimesTest()
        {
            var actual = PresidentTask.PresidentTask.GetElectionVictoryTwoTimes();
            Assert.AreEqual(16, actual);
        }


        [Test]
        public void GetElectionVictoryTwoTimesPresidentsTest()
        {
            var expected = new[] {
                "1 - George Washington",
                "3 - Thomas Jefferson",
                "4 - James Madison",
                "5 - James Monroe",
                "7 - Andrew Jackson",
                "16 - Abraham Lincoln",
                "18 - Ulysses Grant",
                "22 - Grover Cleveland",
                "25 - William McKinley",
                "28 - Woodrow Wilson",
                "34 - Dwight Eisenhower",
                "37 - Richard Nixon",
                "40 - Ronald Reagan",
                "42 - William Clinton",
                "43 - George Bush",
                "44 - Barack Obama"
            };
            var actual = PresidentTask.PresidentTask.GetElectionVictoryTwoTimesPresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetElectionVictoryZeroTimeTest()
        {
            var actual = PresidentTask.PresidentTask.GetElectionVictoryZeroTime();
            Assert.AreEqual(5, actual);
        }


        [Test]
        public void GetElectionVictoryZeroTimePresidentsTest()
        {
            var expected = new[] {
                "10 - John Tyler",
                "13 - Millard Fillmore",
                "17 - Andrew Johnson",
                "21 - Chester Arthur",
                "38 - Gerald Ford"
            };
            var actual = PresidentTask.PresidentTask.GetElectionVictoryZeroTimePresidents();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetIncompleteTermTest()
        {
            var expected = new[] {
                "9 - William Harrison 0 years and 31 days",
                "16 - Abraham Lincoln 0 years and 42 days",
                "32 - Franklin Roosevelt 0 years and 82 days",
                "25 - William McKinley 0 years and 194 days",
                "20 - James Garfield 0 years and 199 days",
                "12 - Zachary Taylor 1 years and 127 days",
                "37 - Richard Nixon 1 years and 201 days",
                "29 - Warren Harding 2 years and 151 days",
                "35 - John Kennedy 2 years and 306 days"
            };
            var actual = PresidentTask.PresidentTask.GetIncompleteTerm();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFirst94thPresidentTest()
        {
            var expected = "41 - George Bush";
            var actual = PresidentTask.PresidentTask.GetFirst94thPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetSecond94thPresidentTest()
        {
            var expected = "39 - James Carter";
            var actual = PresidentTask.PresidentTask.GetSecond94thPresident();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBushIsTheOldestDateTest()
        {
            var expected = "41 - George Bush at 25-Nov-17";
            var actual = PresidentTask.PresidentTask.GetBushIsTheOldestDate();
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void GetPanamaCanalTest()
        {
            var expected = "28 - Woodrow Wilson";
            var actual = PresidentTask.PresidentTask.GetPanamaCanal();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAttackOnPearlHarborTest()
        {
            var expected = "32 - Franklin Roosevelt";
            var actual = PresidentTask.PresidentTask.GetAttackOnPearlHarbor();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAttackNagasakiTest()
        {
            var expected = "33 - Harry Truman";
            var actual = PresidentTask.PresidentTask.GetAttackNagasaki();
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void GetJoiningAlaskaTest()
        {
            var expected = "34 - Dwight Eisenhower";
            var actual = PresidentTask.PresidentTask.GetJoiningAlaska();
            Assert.AreEqual(expected, actual);
        }


    }
}
