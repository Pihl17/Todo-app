using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Models;

namespace ToDo_App.Handler
{
    public class StatusHandler
    {
        private void RepeatDoneRepeatable(ToDo toDo)
        {
            if (toDo.Repeat == RepeatInterval.None)
            {
                return;
            } else
            {
                toDo.MarkProgress(ToDoStatus.NotDone);
            }
            DateTime newDeadline = toDo.Deadline ?? DateTime.Now;
            switch (toDo.Repeat)
            {
                case RepeatInterval.Daily:
                    newDeadline = newDeadline.AddDays(1);
                    
                    break;
                case RepeatInterval.Weekly:
                    newDeadline = newDeadline.AddDays(7);
                    break;
                case RepeatInterval.Weekdays:
                    do
                    {
                        newDeadline = newDeadline.AddDays(1);
                    } while (newDeadline.DayOfWeek == DayOfWeek.Saturday || newDeadline.DayOfWeek == DayOfWeek.Sunday);
                    break;
                case RepeatInterval.Monthly:
                    newDeadline = newDeadline.AddMonths(1);
                    break;
                case RepeatInterval.Yearly:
                    newDeadline = newDeadline.AddYears(1);
                    break;
            }
            toDo.Deadline = newDeadline;
        }
        public void UpdateAllRepeatables(List<ToDo> toDos)
        {
            foreach (var toDo in toDos)
            {
                RepeatDoneRepeatable(toDo);
            }
        }

        public void RemoveAllDone(List<ToDo> toDos)
        {
            toDos.RemoveAll(toDo => toDo.Status == ToDoStatus.Done && toDo.Repeat == RepeatInterval.None);
        }

        public void RemoveAllDone(List<ToDo> toDos, DateTime date)
        {
            toDos.RemoveAll(toDo => toDo.Status == ToDoStatus.Done && toDo.Deadline <= date && toDo.Repeat == RepeatInterval.None);
        }
    }
}
