using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo_App.Models;

namespace ToDo_App.Calendar
{

    /// <summary>
    /// Finds the dates where ToDos have a deadline or repeat, for use in a calendar view.
    /// </summary>
    public class CalendarManager
    {
        public DateTime selectedMonth;
        public string selectedMonthString;


        /// <summary>
        /// Returns all dates in a range with a deadline, including repeated deadlines.
        /// </summary>
        public List<DateTime> GetDeadlineDates(List<ToDo> toDos, DateTime from, DateTime to)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (ToDo toDo in toDos)
            {
                dates.AddRange(GetOccurrences(toDo, from, to, 0));
            }
            return dates.Distinct().OrderBy(date => date).ToList();
        }

        /// <summary>
        /// Returns all dates in the range where a repeating ToDo comes back.
        /// The original deadline is not included.
        /// </summary>
        public List<DateTime> GetRepeatDates(List<ToDo> toDos, DateTime from, DateTime to)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (ToDo toDo in toDos)
            {
                dates.AddRange(GetOccurrences(toDo, from, to, 1));
            }
            return dates.Distinct().OrderBy(date => date).ToList();
        }



        /// <summary>
        /// Returns the ToDos that have a deadline or repeat on the given date.
        /// </summary>
        public List<ToDo> GetToDosOnDate(List<ToDo> toDos, DateTime date)
        {
            List<ToDo> result = new List<ToDo>();
            foreach (ToDo toDo in toDos)
            {
                if (GetOccurrences(toDo, date, date, 0).Count > 0)
                {
                    result.Add(toDo);
                }
            }
            return result;
        }
        // first 0 includes the original deadline, 1 starts at the first repeat.
        private List<DateTime> GetOccurrences(ToDo toDo, DateTime from, DateTime to, int firstStep)
        {
            List<DateTime> dates = new List<DateTime>();

            if (toDo.Deadline == null)
            {
                return dates;
            }
            if (toDo.Repeat == RepeatInterval.None && firstStep > 0)
            {
                return dates;
            }

            DateTime start = toDo.Deadline.Value.Date;
            int step = firstStep;
            DateTime occurrence = GetOccurrences(start, toDo.Repeat, step);
            while (occurrence <= to.Date)
            {
                if (occurrence >= from.Date)
                {
                    dates.Add(occurrence);
                }
                if (toDo.Repeat == RepeatInterval.None)
                {
                    break;
                }
                step++;
                occurrence = GetOccurrences(start, toDo.Repeat, step);
            }
            return dates;

        }

        // Always counts from the original deadline, so Jan 31 monthly gives 
        // Feb 28, Mar 31 and not Feb 28, Mar 28.
        private DateTime GetOccurrences(DateTime start, RepeatInterval repeat, int step)
        {
            switch (repeat)
            {
                case RepeatInterval.Daily:
                    return start.AddDays(step);
                case RepeatInterval.Weekly:
                    return start.AddDays(7 * step);
                case RepeatInterval.Weekdays:
                    return AddWeekdays(start, step);
                case RepeatInterval.Monthly:
                    return start.AddMonths(step);
                case RepeatInterval.Yearly:
                    return start.AddYears(step);
                default:
                    return start;
            }
        }

        /// <summary>
        /// Adds a given number of weekdays to a start date, skipping weekends.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private DateTime AddWeekdays(DateTime start, int count)
        {
            DateTime date = start;
            int added = 0;
            while (added < count)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    added++;
                }
            }
            return date;
        }


        /// <summary>
        /// Returns a list of CalendarDay objects for the given month and year, each containing the date and the ToDos that have a deadline or repeat on that date.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<CalendarDay> GetToDosInGivenMonth(List<ToDo> toDos, int year, int month)
        {
            var days = new List<CalendarDay>();
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                var date = new DateTime(year, month, day);
                var toDosOfTheDay = GetToDosOnDate(toDos, date);
                {
                    days.Add(new CalendarDay { Date = date, ToDosOfTheDay = toDosOfTheDay });

                }
            }

            return days;
        }
        public void NextMonth()
        {
            selectedMonth = selectedMonth.AddMonths(1);
            UpdateSelectedMonthString();
        }

        public void PreviousMonth()
        {
            selectedMonth = selectedMonth.AddMonths(-1);
            UpdateSelectedMonthString();
        }

        private void UpdateSelectedMonthString()
        {
            selectedMonthString = selectedMonth.ToString("MMMM yyyy");
        }

        public void InitializeSelectedMonth()
        {
            selectedMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            UpdateSelectedMonthString();

        }



    }

    /// <summary>
    /// Represents a day in the calendar with its associated ToDos.
    /// </summary>
    public class CalendarDay
    {
        public List<ToDo> ToDosOfTheDay { get; set; }
        public DateTime Date { get; set; }
    }
}