using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo_App.Models
{
    /// <summary>
    /// Represents a single ToDo note with a title, description, date,
    /// status, priority, repeat settings and a checklist.
    /// </summary>
    public class ToDo
    {
        private const int MIN_PRIORITY = 0;
        private const int MAX_PRIORITY = 10;

        private int priority;

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? Deadline { get; set; }
        public ToDoStatus Status { get; set; }
        public RepeatInterval Repeat { get; set; }
        public List<ChecklistItem> Checklist { get; set; }

        /// <summary>
        /// Priority from 0 to 10. Throws if the value is outside that range.
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set
            {
                if (value < MIN_PRIORITY || value > MAX_PRIORITY)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "priority must be between 0 and 10.");
                }
                priority = value;

            }
        }

        /// <summary>
        /// Creates a new ToDo with the creation date set to now.
        /// </summary>
        public ToDo()
        {
            Title = string.Empty;
            Description = string.Empty;
            CreationDate = DateTime.Now;
            Status = ToDoStatus.NotDone;
            Repeat = RepeatInterval.None;
            Checklist = new List<ChecklistItem>();
        }

        /// <summary>
        /// Marks the ToDo as done and sets the completion date to now.
        /// </summary>
        public void MarkAsDone()
        {
            Status = ToDoStatus.Done;
            CompletionDate = DateTime.Now;
        }
    }
}
