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

        /// <summary>
        /// creating a list of tags 
        /// </summary>
        /// <param name="todoes">a list of todoes used for creating TagLibrary</param>
        public void GenerateLibrary (List<ToDo> todoes) {
            foreach (ToDo todo in todoes) {
                foreach (string Tag in todo.Taglist) {
                    if (!TagLibrary.Contains(Tag)) {
                        TagLibrary.Add(Tag);
                    }
                }
            }
            TagLibrary.Sort();
        }

        /// <summary>
        /// recive a tag and checks if its in the list, if its not then add and sort
        /// </summary>
        /// <param name="Tag">tag as a string</param>
        public void AddTagToLibrary(string Tag) {
            if (!TagLibrary.Contains(Tag)) {
                TagLibrary.Add(Tag);
                TagLibrary.Sort();
            }
        }
    }
}
