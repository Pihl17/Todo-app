using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ToDo_App.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ToDo_App.Handler
{
    public class TagFilter
    {
        public List<string> TagLibrary { get; set; } = [];

        public void GenerateLibrary (List<ToDo> todoes) {
            foreach (ToDo todo in todoes) {
                foreach (string Tag in todo.Taglist) {
                    if (!TagLibrary.Contains(Tag)) {
                        TagLibrary.Add(Tag);
                    }
                }
            }
        }

        public void AddTagToLibrary(string Tag) {
            if (!TagLibrary.Contains(Tag)) {
                TagLibrary.Add(Tag);
            }
        }
    }
}
