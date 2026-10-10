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

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? Deadline { get; set; }
        public ToDoStatus Status { private set; get; }
        public RepeatInterval Repeat { get; set; }
        public List<string> Taglist { get; set; }
        public List<ChecklistItem> Checklist { get; set; }

        /// <summary>
        /// Priority from 0 to 10. Throws if the value is outside that range.
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set
            {
                priority = Math.Clamp(value, MIN_PRIORITY, MAX_PRIORITY);
            }
        }

        /// <summary>
        /// Creates a new ToDo with the creation date set to now.
        /// </summary>
        public ToDo()
        {
            Id = Guid.NewGuid();
            Title = string.Empty;
            Description = string.Empty;
            CreationDate = DateTime.Now;
            Status = ToDoStatus.NotDone;
            Repeat = RepeatInterval.None;
            Taglist = new List<string>();
            Checklist = new List<ChecklistItem>();
        }

        /// <summary>
        /// Marks the ToDo as in progress and sets the completion date to now.
        /// </summary>
        public void MarkProgress(ToDoStatus status)
        {
            Status = status;
            if (status == ToDoStatus.Done)
                CompletionDate = DateTime.Now;
        }

        public void AddTag(string Tag) {
            if (!Taglist.Contains(Tag))
            {
                Taglist.Add(Tag);
            }
        }
    }
}
