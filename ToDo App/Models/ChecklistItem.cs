using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo_App.Models
{
    public class ChecklistItem
    {
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
