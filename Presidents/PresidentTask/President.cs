using System;
using System.Collections.Generic;

namespace PresidentTask
{
    public enum PartyAffiliation
    {
        Unaffiliated,
        Federalist,
        Whig,
        Democratic,
        Republican,
        Democratic_Republican
    }

    public class President
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public DateTime TermBegin { get; set; }
        public DateTime TermEnd { get; set; }
        public PartyAffiliation Party { get; set; }

        #region Equals
        public override int GetHashCode()
        {
            return BirthDate.GetHashCode() ^ DeathDate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as President);
        }

        public bool Equals(President p)
        {
            if (object.ReferenceEquals(p, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, p))
            {
                return true;
            }

            if (this.GetType() != p.GetType())
            {
                return false;
            }

            return (LastName == p.LastName) && (BirthDate == p.BirthDate) && (DeathDate == p.DeathDate);
        }

        public static bool operator ==(President p1, President p2)
        {
            if (object.ReferenceEquals(p1, null))
            {
                if (object.ReferenceEquals(p2, null))
                {
                    return true;
                }

                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(President p1, President p2)
        {
            return !(p1 == p2);
        }
        #endregion

        public override string ToString()
        {
            return LastName;
        }

        public static List<President> GetUSAPresidents()
        {
            return new List<President>()
            {
                new President()
                {
                    Number = 1,
                    FirstName = "George", LastName = "Washington",
                    BirthDate = new DateTime(1732, 2, 22), DeathDate = new DateTime(1799, 12, 14),
                    TermBegin = new DateTime(1789, 4, 30), TermEnd = new DateTime(1797, 3, 4),
                    Party = PartyAffiliation.Unaffiliated
                },
                new President()
                {
                    Number = 2,
                    FirstName = "John", LastName = "Adams",
                    BirthDate = new DateTime(1735, 10, 30), DeathDate = new DateTime(1826, 7, 4),
                    TermBegin = new DateTime(1797, 3, 4), TermEnd = new DateTime(1801, 3, 4),
                    Party = PartyAffiliation.Federalist
                },
                new President()
                {
                    Number = 3,
                    FirstName = "Thomas", LastName = "Jefferson",
                    BirthDate = new DateTime(1743, 4, 13), DeathDate = new DateTime(1826, 7, 4),
                    TermBegin = new DateTime(1801, 3, 4), TermEnd = new DateTime(1809, 3, 4),
                    Party = PartyAffiliation.Democratic_Republican
                },
                new President()
                {
                    Number = 4,
                    FirstName = "James", LastName = "Madison",
                    BirthDate = new DateTime(1751, 3, 16), DeathDate = new DateTime(1836, 6, 28),
                    TermBegin = new DateTime(1809, 3, 4), TermEnd = new DateTime(1817, 3, 4),
                    Party = PartyAffiliation.Democratic_Republican
                },
                new President()
                {
                    Number = 5,
                    FirstName = "James", LastName = "Monroe",
                    BirthDate = new DateTime(1758, 4, 28), DeathDate = new DateTime(1831, 7, 4),
                    TermBegin = new DateTime(1817, 3, 4), TermEnd = new DateTime(1825, 3, 4),
                    Party = PartyAffiliation.Democratic_Republican
                },
                new President()
                {
                    Number = 6,
                    FirstName = "John", MiddleName = "Quincy", LastName = "Adams",
                    BirthDate = new DateTime(1767, 7, 11), DeathDate = new DateTime(1848, 2, 28),
                    TermBegin = new DateTime(1825, 3, 4), TermEnd = new DateTime(1829, 3, 4),
                    Party = PartyAffiliation.Democratic_Republican
                },
                new President()
                {
                    Number = 7,
                    FirstName = "Andrew", LastName = "Jackson",
                    BirthDate = new DateTime(1767, 3, 15), DeathDate = new DateTime(1845, 6, 8),
                    TermBegin = new DateTime(1829, 3, 4), TermEnd = new DateTime(1837, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 8,
                    FirstName = "Martin", LastName = "Van Buren",
                    BirthDate = new DateTime(1782, 12, 5), DeathDate = new DateTime(1862, 7, 24),
                    TermBegin = new DateTime(1837, 3, 4), TermEnd = new DateTime(1841, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 9,
                    FirstName = "William", MiddleName = "Henry", LastName = "Harrison",
                    BirthDate = new DateTime(1773, 2, 9), DeathDate = new DateTime(1841, 4, 4),
                    TermBegin = new DateTime(1841, 3, 4), TermEnd = new DateTime(1841, 4, 4),
                    Party = PartyAffiliation.Whig
                },
                new President()
                {
                    Number = 10,
                    FirstName = "John", LastName = "Tyler",
                    BirthDate = new DateTime(1790, 3, 29), DeathDate = new DateTime(1862, 1, 18),
                    TermBegin = new DateTime(1841, 4, 4), TermEnd = new DateTime(1845, 3, 4),
                    Party = PartyAffiliation.Whig
                },
                new President()
                {
                    Number = 11,
                    FirstName = "James", MiddleName = "Knox", LastName = "Polk",
                    BirthDate = new DateTime(1795, 11, 2), DeathDate = new DateTime(1849, 6, 15),
                    TermBegin = new DateTime(1845, 3, 4), TermEnd = new DateTime(1849, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 12,
                    FirstName = "Zachary", LastName = "Taylor",
                    BirthDate = new DateTime(1784, 11, 24), DeathDate = new DateTime(1850, 7, 9),
                    TermBegin = new DateTime(1849, 3, 4), TermEnd = new DateTime(1850, 7, 9),
                    Party = PartyAffiliation.Whig
                },
                new President()
                {
                    Number = 13,
                    FirstName = "Millard", LastName = "Fillmore",
                    BirthDate = new DateTime(1800, 1, 7), DeathDate = new DateTime(1874, 3, 8),
                    TermBegin = new DateTime(1850, 7, 9), TermEnd = new DateTime(1853, 3, 4),
                    Party = PartyAffiliation.Whig
                },
                new President()
                {
                    Number = 14,
                    FirstName = "Franklin", LastName = "Pierce",
                    BirthDate = new DateTime(1804, 11, 23), DeathDate = new DateTime(1869, 10, 8),
                    TermBegin = new DateTime(1853, 3, 4), TermEnd = new DateTime(1857, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 15,
                    FirstName = "James", LastName = "Buchanan",
                    BirthDate = new DateTime(1791, 4, 21), DeathDate = new DateTime(1868, 6, 1),
                    TermBegin = new DateTime(1857, 3, 4), TermEnd = new DateTime(1861, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 16,
                    FirstName = "Abraham", LastName = "Lincoln",
                    BirthDate = new DateTime(1809, 2, 12), DeathDate = new DateTime(1865, 4, 15),
                    TermBegin = new DateTime(1861, 3, 4), TermEnd = new DateTime(1865, 4, 15),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 17,
                    FirstName = "Andrew", LastName = "Johnson",
                    BirthDate = new DateTime(1808, 12, 29), DeathDate = new DateTime(1875, 7, 31),
                    TermBegin = new DateTime(1865, 4, 15), TermEnd = new DateTime(1869, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 18,
                    FirstName = "Ulysses", LastName = "Grant",
                    BirthDate = new DateTime(1822, 4, 27), DeathDate = new DateTime(1885, 7, 23),
                    TermBegin = new DateTime(1869, 3, 4), TermEnd = new DateTime(1877, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 19,
                    FirstName = "Rutherford", MiddleName = "Birchard", LastName = "Hayes",
                    BirthDate = new DateTime(1822, 10, 4), DeathDate = new DateTime(1893, 1, 17),
                    TermBegin = new DateTime(1877, 3, 4), TermEnd = new DateTime(1881, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 20,
                    FirstName = "James", MiddleName = "Abram", LastName = "Garfield",
                    BirthDate = new DateTime(1831, 11, 19), DeathDate = new DateTime(1881, 9,19),
                    TermBegin = new DateTime(1881, 3, 4), TermEnd = new DateTime(1881, 9, 19),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 21,
                    FirstName = "Chester", MiddleName = "Alan", LastName = "Arthur",
                    BirthDate = new DateTime(1829, 10, 5), DeathDate = new DateTime(1886, 11, 18),
                    TermBegin = new DateTime(1881, 9, 19), TermEnd = new DateTime(1885, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 22,
                    FirstName = "Grover", LastName = "Cleveland",
                    BirthDate = new DateTime(1837, 3, 18), DeathDate = new DateTime(1908, 6, 24),
                    TermBegin = new DateTime(1885, 3, 4), TermEnd = new DateTime(1889, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 23,
                    FirstName = "Benjamin", LastName = "Harrison",
                    BirthDate = new DateTime(1833, 8, 20), DeathDate = new DateTime(1901, 3, 13),
                    TermBegin = new DateTime(1889, 3, 4), TermEnd = new DateTime(1893, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 24,
                    FirstName = "Grover", LastName = "Cleveland",
                    BirthDate = new DateTime(1837, 3, 18), DeathDate = new DateTime(1908, 6, 24),
                    TermBegin = new DateTime(1893, 3, 4), TermEnd = new DateTime(1897, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 25,
                    FirstName = "William", LastName = "McKinley",
                    BirthDate = new DateTime(1843, 1, 29), DeathDate = new DateTime(1901, 9, 14),
                    TermBegin = new DateTime(1897, 3, 4), TermEnd = new DateTime(1901, 9, 14),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 26,
                    FirstName = "Theodore", LastName = "Roosevelt",
                    BirthDate = new DateTime(1858, 10, 27), DeathDate = new DateTime(1919, 1, 6),
                    TermBegin = new DateTime(1901, 9, 14), TermEnd = new DateTime(1909, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 27,
                    FirstName = "William",MiddleName = "Howard", LastName = "Taft",
                    BirthDate = new DateTime(1857, 9, 15), DeathDate = new DateTime(1930, 3, 8),
                    TermBegin = new DateTime(1909, 3, 4), TermEnd = new DateTime(1913, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 28,
                    FirstName = "Woodrow", LastName = "Wilson",
                    BirthDate = new DateTime(1856, 12, 28), DeathDate = new DateTime(1924, 2, 3),
                    TermBegin = new DateTime(1913, 3, 4), TermEnd = new DateTime(1921, 3, 4),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 29,
                    FirstName = "Warren", MiddleName = "Gamaliel", LastName = "Harding",
                    BirthDate = new DateTime(1865, 11, 2), DeathDate = new DateTime(1923, 8, 2),
                    TermBegin = new DateTime(1921, 3, 4), TermEnd = new DateTime(1923, 8, 2),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 30,
                    FirstName = "Calvin", LastName = "Coolidge",
                    BirthDate = new DateTime(1872, 7, 4), DeathDate = new DateTime(1933, 1, 5),
                    TermBegin = new DateTime(1923, 8, 2), TermEnd = new DateTime(1929, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 31,
                    FirstName = "Herbert", MiddleName = "Clark", LastName = "Hoover",
                    BirthDate = new DateTime(1874, 8, 10), DeathDate = new DateTime(1964, 10, 20),
                    TermBegin = new DateTime(1929, 3, 4), TermEnd = new DateTime(1933, 3, 4),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 32,
                    FirstName = "Franklin", MiddleName = "Delano", LastName = "Roosevelt",
                    BirthDate = new DateTime(1882, 1, 30), DeathDate = new DateTime(1945, 4, 12),
                    TermBegin = new DateTime(1933, 3, 4), TermEnd = new DateTime(1945, 4, 12),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 33,
                    FirstName = "Harry", LastName = "Truman",
                    BirthDate = new DateTime(1884, 5, 8), DeathDate = new DateTime(1972, 12, 26),
                    TermBegin = new DateTime(1945, 4, 12), TermEnd = new DateTime(1953, 1, 20),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 34,
                    FirstName = "Dwight", MiddleName = "David", LastName = "Eisenhower",
                    BirthDate = new DateTime(1890, 10, 14), DeathDate = new DateTime(1969, 3, 28),
                    TermBegin = new DateTime(1953, 1, 20), TermEnd = new DateTime(1961, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 35,
                    FirstName = "John", MiddleName = "Fitzgerald", LastName = "Kennedy",
                    BirthDate = new DateTime(1917, 5, 29), DeathDate = new DateTime(1963, 11, 22),
                    TermBegin = new DateTime(1961, 1, 20), TermEnd = new DateTime(1963, 11, 22),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 36,
                    FirstName = "Lyndon", MiddleName ="Baines", LastName = "Johnson",
                    BirthDate = new DateTime(1908, 8, 27), DeathDate = new DateTime(1973, 1, 22),
                    TermBegin = new DateTime(1963, 11, 22), TermEnd = new DateTime(1969, 1, 20),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 37,
                    FirstName = "Richard", MiddleName = "Milhous", LastName = "Nixon",
                    BirthDate = new DateTime(1913, 1, 9), DeathDate = new DateTime(1994, 4, 22),
                    TermBegin = new DateTime(1969, 1, 20), TermEnd = new DateTime(1974, 8, 9),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 38,
                    FirstName = "Gerald", MiddleName = "Rudolph", LastName = "Ford",
                    BirthDate = new DateTime(1913, 7, 14), DeathDate = new DateTime(2006, 12, 26),
                    TermBegin = new DateTime(1974, 8, 9), TermEnd = new DateTime(1977, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 39,
                    FirstName = "James", MiddleName = "Earl", LastName = "Carter",
                    BirthDate = new DateTime(1924, 10, 1),
                    TermBegin = new DateTime(1977, 1, 20), TermEnd = new DateTime(1981, 1, 20),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 40,
                    FirstName = "Ronald", MiddleName = "Wilson", LastName = "Reagan",
                    BirthDate = new DateTime(1911, 2, 6), DeathDate = new DateTime(2004, 6, 5),
                    TermBegin = new DateTime(1981, 1, 20), TermEnd = new DateTime(1989, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 41,
                    FirstName = "George", MiddleName = "Herbert", LastName = "Bush",
                    BirthDate = new DateTime(1924, 6, 12), DeathDate = new DateTime(2018, 11, 30),
                    TermBegin = new DateTime(1989, 1, 20), TermEnd = new DateTime(1993, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 42,
                    FirstName = "William", MiddleName = "Jefferson", LastName = "Clinton",
                    BirthDate = new DateTime(1946, 8, 19),
                    TermBegin = new DateTime(1993, 1, 20), TermEnd = new DateTime(2001, 1, 20),
                    Party = PartyAffiliation.Democratic 
                },
                new President()
                {
                    Number = 43,
                    FirstName = "George", MiddleName = "Walker", LastName = "Bush",
                    BirthDate = new DateTime(1946, 7, 6),
                    TermBegin = new DateTime(2001, 1, 20), TermEnd = new DateTime(2009, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                { Number = 44,
                    FirstName = "Barack", MiddleName = "Hussein", LastName = "Obama",
                    BirthDate = new DateTime(1961, 8, 4),
                    TermBegin = new DateTime(2009, 1, 20), TermEnd = new DateTime(2017, 1, 20),
                    Party = PartyAffiliation.Democratic
                },
                new President()
                {
                    Number = 45,
                    FirstName = "Donald", MiddleName = "John", LastName = "Trump",
                    BirthDate = new DateTime(1946, 6, 14),
                    TermBegin = new DateTime(2017, 1, 20), TermEnd = new DateTime(2021, 1, 20),
                    Party = PartyAffiliation.Republican
                },
                new President()
                {
                    Number = 46,
                    FirstName = "Joseph", MiddleName = "Robinette", LastName = "Biden",
                    BirthDate = new DateTime(1942, 11, 20),
                    TermBegin = new DateTime(2021, 1, 20),
                    Party = PartyAffiliation.Democratic
                }
            };
        }
    }
}
