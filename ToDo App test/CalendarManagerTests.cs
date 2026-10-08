using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToDo_App.Models;
using ToDo_App.Calender;

namespace ToDo_App_test
{
    public class CalendarManagerTests
    {
        private static readonly DateTime OCTOBER_START = new DateTime(2026, 10, 1);
        private static readonly DateTime OCTOBER_END = new DateTime(2026, 10, 31);

        private ToDo CreateToDo(DateTime? deadline, RepeatInterval repeat)
        {
            ToDo toDo = new ToDo();
            toDo.Deadline = deadline;
            toDo.Repeat = repeat;
            return toDo;
        }

        private CalendarManager CreateCalendar(params ToDo[] toDos)
        {
            return new CalendarManager(new List<ToDo>(toDos));
        }

        private List<DateTime> DatesOf(params DateTime[] dates)
        {
            return new List<DateTime>(dates);
        }

        // ------ GetDeadlineDates Tests ------

        [Test]
        public void GetDeadlineDates_NoRepeat_ReturnsDeadline()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_NullDeadline_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(null, RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf()));
        }

        [Test]
        public void GetDeadlineDates_EmptyList_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar();

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_DeadlineAfterRange_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 11, 10), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_DeadlineBeforeRange_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 9, 10), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetDeadlineDates_IgnoresTimeOfDay()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 10, 15, 30, 0), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_Daily_ReturnsEveryDay()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.Daily));

            List<DateTime> dates = calendar.GetDeadlineDates(new DateTime(2026, 10, 10), new DateTime(2026, 10, 12));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10), new DateTime(2026, 10, 11), new DateTime(2026, 10, 12))));
        }

        [Test]
        public void GetDeadlineDates_WeeklyStartingBeforeRange_SkipsDatesBeforeRange()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 9, 28), RepeatInterval.Weekly));

            List<DateTime> dates = calendar.GetDeadlineDates(new DateTime(2026, 10, 1), new DateTime(2026, 10, 14));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 12))));
        }

        [Test]
        public void GetDeadlineDates_Monthly_KeepsEndOfMonth()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 1, 31), RepeatInterval.Monthly));

            List<DateTime> dates = calendar.GetDeadlineDates(new DateTime(2026, 1, 1), new DateTime(2026, 4, 30));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 1, 31), new DateTime(2026, 2, 28), new DateTime(2026, 3, 31), new DateTime(2026, 4, 30))));
        }

        [Test]
        public void GetDeadlineDates_Yerly_ReturnsSameDateEachYear()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.Yearly));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, new DateTime(2028, 12, 31));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 10), new DateTime(2027, 10, 10), new DateTime(2028, 10, 10))));
        }

        [Test]
        public void GetDeadlineDates_SameDateTwice_ReturnsOnce()
        {
            CalendarManager calendar = CreateCalendar(
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetDeadlineDates_MultipleToDos_ReturnDatesSorted()
        {
            CalendarManager calendar = CreateCalendar(
                CreateToDo(new DateTime(2026, 10, 20), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None));

            List<DateTime> dates = calendar.GetDeadlineDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 20))));
        }

        // ------ GetRepeatDates Tests ------

        [Test]
        public void GetRepeatDates_NoRepeat_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None));

            List<DateTime> dates = calendar.GetRepeatDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.Empty);
        }

        [Test]
        public void GetRepeatDates_Weekly_ExcludesOriginalDeadline()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekly));

            List<DateTime> dates = calendar.GetRepeatDates(OCTOBER_START, OCTOBER_END);

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 12), new DateTime(2026, 10, 19), new DateTime(2026, 10, 26))));
        }

        [Test]
        public void GetRepeatDates_WeekdaysFromFriday_SkipsWeekends()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 2), RepeatInterval.Weekdays));

            List<DateTime> dates = calendar.GetRepeatDates(new DateTime(2026, 10, 2), new DateTime(2026, 10, 8));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 5), new DateTime(2026, 10, 6), new DateTime(2026, 10, 7), new DateTime(2026, 10, 8))));
        }

        [Test]
        public void GetRepeatDates_WeekdaysFromMonday_JumpsOverWeekend()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekdays));

            List<DateTime> dates = calendar.GetRepeatDates(new DateTime(2026, 10, 5), new DateTime(2026, 10, 12));

            Assert.That(dates, Is.EqualTo(DatesOf(new DateTime(2026, 10, 6), new DateTime(2026, 10, 7), new DateTime(2026, 10, 8), new DateTime(2026, 10, 9), new DateTime(2026, 10, 12))));
        }

        // ------ GetToDosOnDate Tests ------

        [Test]
        public void GetToDosOnDate_ReturnsOnlyToDosWithDeadlineThatDay()
        {
            ToDo onDay = CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None);
            ToDo otherDay = CreateToDo(new DateTime(2026, 10, 13), RepeatInterval.None);
            CalendarManager calendar = CreateCalendar(onDay, otherDay);

            List<ToDo> result = calendar.GetToDosOnDate(new DateTime(2026, 10, 12));

            Assert.That(result, Is.EqualTo(new List<ToDo> { onDay }));
        }

        [Test]
        public void GetToDosOnDate_RepeatingToDo_IsFoundOnRepeatDate()
        {
            ToDo weekly = CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekly);
            CalendarManager calendar = CreateCalendar(weekly);

            List<ToDo> result = calendar.GetToDosOnDate(new DateTime(2026, 10, 12));

            Assert.That(result, Is.EqualTo(new List<ToDo> { weekly }));
        }

        [Test]
        public void GetToDosOnDate_DayWithNoToDos_ReturnsEmpty()
        {
            CalendarManager calendar = CreateCalendar(CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.Weekly));

            List<ToDo> result = calendar.GetToDosOnDate(new DateTime(2026, 10, 13));

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void GetToDosInGivenMonthTest_CheckNonRepeatingToDos()
        {
            ToDo toDo1 = CreateToDo(new DateTime(2026, 10, 5), RepeatInterval.None);
            ToDo toDo2 = CreateToDo(new DateTime(2026, 10, 10), RepeatInterval.None);
            ToDo toDo3 = CreateToDo(new DateTime(2026, 11, 1), RepeatInterval.None);

            CalendarManager calendar = CreateCalendar(toDo1, toDo2, toDo3);

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(2026, 10);

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

            CalendarManager calendar = CreateCalendar(toDo);

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(2026, 2);

            Assert.That(result, Has.Count.EqualTo(28));

            int actualCount = result.Sum(day => day.ToDosOfTheDay.Count);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
        // This test checks that a yearly repeating ToDo does not appear in the results for a month that is not the same as its original deadline year.
        [TestCase(RepeatInterval.Yearly, 0)]
        public void GetToDosInGivenMonthTest_CheckRepeatingYearly_NoResult(RepeatInterval repeatInterval, int expectedCount)
        {
            ToDo toDo = CreateToDo(new DateTime(2025, 2, 1), repeatInterval);

            CalendarManager calendar = CreateCalendar(toDo);

            List<CalendarDay> result = calendar.GetToDosInGivenMonth(2026, 3);

            Assert.That(result, Has.Count.EqualTo(31));

            int actualCount = result.Sum(day => day.ToDosOfTheDay.Count);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}
