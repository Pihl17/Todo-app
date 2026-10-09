using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToDo_App.Models;
using ToDo_App.Calendar;

namespace ToDo_App_test
{
    public class CalendarManagerTests
    {
        private static readonly DateTime OCTOBER_START = new DateTime(2026, 10, 1);
        private static readonly DateTime OCTOBER_END = new DateTime(2026, 10, 31);
        private List<ToDo> toDos;
        private CalendarManager calendar;

        [SetUp]
        public void SetUp()
        {
            calendar = new CalendarManager();
        }

        private ToDo CreateToDo(DateTime? deadline, RepeatInterval repeat)
        {
            ToDo toDo = new ToDo();
            toDo.Deadline = deadline;
            toDo.Repeat = repeat;
            return toDo;
        }

        private List<DateTime> DatesOf(params DateTime[] dates)
        {
            return new List<DateTime>(dates);
        }

        // ------ GetDeadlineDates Tests ------

        [Test]
        public void GetDeadlineDates_NoRepeat_ReturnsDeadline()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_NullDeadline_ReturnsEmpty()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(null, RepeatInterval.None)
            };

            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf()));
        }

        [Test]
        public void GetDeadlineDates_EmptyList_ReturnsEmpty()
        {
            var toDos = new List<ToDo>();

            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_DeadlineAfterRange_ReturnsEmpty()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 11, 1), RepeatInterval.None)
            };

            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_DeadlineBeforeRange_ReturnsEmpty()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 9, 30), RepeatInterval.None)
            };

            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_IgnoresTimeOfDay()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10, 14, 30, 0), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_Daily_ReturnsEveryDay()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 11), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, new DateTime(2026, 10, 10), new DateTime(2026, 10, 12));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10), new DateTime(2026, 10, 11), new DateTime(2026, 10, 12))));
        }

        [Test]
        public void GetDeadlineDates_WeeklyStartingBeforeRange_SkipsDatesBeforeRange()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, new DateTime(2026, 10, 1), new DateTime(2026, 10, 14));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 12))));
        }

        [Test]
        public void GetDeadlineDates_Monthly_KeepsEndOfMonth()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 1, 31), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 2, 28), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 3, 31), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 4, 30), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, new DateTime(2026, 1, 1), new DateTime(2026, 4, 30));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 1, 31), new DateTime(2026, 2, 28), new DateTime(2026, 3, 31), new DateTime(2026, 4, 30))));
        }

        [Test]
        public void GetDeadlineDates_Yerly_ReturnsSameDateEachYear()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.Yearly)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, new DateTime(2028, 12, 31));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10), new DateTime(2027, 10, 10), new DateTime(2028, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_SameDateTwice_ReturnsOnce()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None)
            };
            toDos.Add(CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None));
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetDeadlineDates_MultipleToDos_ReturnDatesSorted()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 20), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetDeadlineDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 20))));
        }

        // ------ GetRepeatDates Tests ------

        [Test]
        public void GetRepeatDates_NoRepeat_ReturnsEmpty()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None)
            };
            List<DateTime> dates = calendar.GetRepeatDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetRepeatDates_Weekly_ExcludesOriginalDeadline()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekly)
            };
            List<DateTime> dates = calendar.GetRepeatDates(toDos, OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 12), new DateTime(2026, 10, 19), new DateTime(2026, 10, 26))));
        }

        [Test]
        public void GetRepeatDates_WeekdaysFromFriday_SkipsWeekends()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 2), RepeatInterval.Weekdays)
            };
            List<DateTime> dates = calendar.GetRepeatDates(toDos, new DateTime(2026, 10, 2), new DateTime(2026, 10, 8));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 6), new DateTime(2026, 10, 7), new DateTime(2026, 10, 8))));
        }

        [Test]
        public void GetRepeatDates_WeekdaysFromMonday_JumpsOverWeekend()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekdays)
            };
            List<DateTime> dates = calendar.GetRepeatDates(toDos, new DateTime(2026, 10, 5), new DateTime(2026, 10, 12));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 6), new DateTime(2026, 10, 7), new DateTime(2026, 10, 8), new DateTime(2026, 10, 9), new DateTime(2026, 10, 12))));
        }

        // ------ GetToDosOnDate Tests ------

        [Test]
        public void GetToDosOnDate_ReturnsOnlyToDosWithDeadlineThatDay()
        {
            ToDo onDay = CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None);
            ToDo otherDay = CreateToDo(new DateTime(2026, 10, 13), RepeatInterval.None);
            var toDos = new List<ToDo>
            {
                onDay,
                otherDay
            };
            List<ToDo> result = calendar.GetToDosOnDate(toDos, new DateTime(2026, 10, 12));

            Assert.That(result, Is.EqualTo(new List<ToDo> { onDay }));
        }

        [Test]
        public void GetToDosOnDate_RepeatingToDo_IsFoundOnRepeatDate()
        {
            ToDo weekly = CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekly);
            var toDos = new List<ToDo>
            {
                weekly
            };

            List<ToDo> result = calendar.GetToDosOnDate(toDos, new DateTime(2026, 10, 12));

            Assert.That(result, Is.EqualTo(new List<ToDo> { weekly }));
        }

        [Test]
        public void GetToDosOnDate_DayWithNoToDos_ReturnsEmpty()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None)
            };

            List<ToDo> result = calendar.GetToDosOnDate(toDos, new DateTime(2026, 10, 13));

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void GetToDosInGivenMonthTest_CheckNonRepeatingToDos()
        {
            var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 11, 1), RepeatInterval.None)
            };
            ToDo toDo1 = CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None);
            ToDo toDo2 = CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None);
            ToDo toDo3 = CreateToDo(new DateTime(2026, 11, 1), RepeatInterval.None);

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(toDos, 2026, 10);

            Assert.That(result, Has.Count.EqualTo(31));

            int actualCount = result.Sum(day => day.ToDosOfTheDay.Count);

            Assert.That(actualCount, Is.EqualTo(2));
        }
        [TestCase(RepeatInterval.Daily, 28)]
        [TestCase(RepeatInterval.Weekly, 4)]
        [TestCase(RepeatInterval.Monthly, 1)]
        [TestCase(RepeatInterval.Yearly, 1)]
        [TestCase(RepeatInterval.Weekdays, 20)]
        public void GetToDosInGivenMonthTest_CheckRepeatingToDos(RepeatInterval repeatInterval, int expectedCount)
        {
            ToDo toDo = CreateToDo(new DateTime(2025, 2, 1), repeatInterval);
            toDos = new List<ToDo> { toDo };

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(toDos, 2026, 2);

            Assert.That(result, Has.Count.EqualTo(28));

            int actualCount = result.Sum(day => day.ToDosOfTheDay.Count);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
        // This test checks that a yearly repeating ToDo does not appear in the results for a month that is not the same as its original deadline year.
        [TestCase(RepeatInterval.Yearly, 0)]
        public void GetToDosInGivenMonthTest_CheckRepeatingYearly_NoResult(RepeatInterval repeatInterval, int expectedCount)
        {
            ToDo toDo = CreateToDo(new DateTime(2025, 2, 1), repeatInterval);
            toDos = new List<ToDo> { toDo };

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(toDos, 2026, 3);

            Assert.That(result, Has.Count.EqualTo(31));

            int actualCount = result.Sum(day => day.ToDosOfTheDay.Count);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}
