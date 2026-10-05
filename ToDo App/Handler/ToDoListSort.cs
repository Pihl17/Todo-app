using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Models;

namespace ToDo_App.Handler
{
    public class ToDoListSort
    {
        /// <summary>
        /// Sorts a list of ToDos by their importance based on both the priority and deadline.
        /// Higher priority ToDos will be placed before lower priority ones, and among ToDos with the same priority, those with earlier deadlines will come first.
        /// </summary>
        /// <param name="toDos">The list of ToDos to sort.</param>
        public List<ToDo> SortList(List<ToDo> toDos)
        {
            toDos = toDos.OrderBy(toDo => toDo.Deadline ?? DateTime.MaxValue)
                  .ThenByDescending(toDo => toDo.Priority)
                  .ToList();

            return toDos;
        }
    }
}

