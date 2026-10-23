using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Input;
using ToDo_App.UI;
using System.Linq;

namespace ToDo_App.UI
{
    public partial class DayAllTaskDisplay : UserControl
    {
        public Button CloseButton => closeBtn;
        public RichTextBox ToDoDisplay => taskDisplayPanel.Controls.OfType<RichTextBox>().Last();
        public DateTime clickedDay;
        private IInputHandling inputHandler;
        public DayAllTaskDisplay() { }
        /// <summary>
        /// Constructor that initializes the display panel
        /// </summary>
        /// <param name="inputHandler"></param>
        /// <param name="clickedDay"></param>
        public DayAllTaskDisplay(IInputHandling inputHandler, DateTime clickedDay)
        {
            InitializeComponent();

            this.inputHandler = inputHandler;
            this.clickedDay = clickedDay;

            DisplayToDosOfDay();
        }
        /// <summary>
        /// Displays all the to dos of a given day, showing the selected day first.
        /// </summary>
        public void DisplayToDosOfDay()
        {
            var relevantToDos = inputHandler.GetToDosOnDate(
                inputHandler.GetToDoList(),
                clickedDay
            );

            var toDoDisplay = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Text = $"To dos for  {clickedDay.ToString("dd / MM / yyyy")}\n"
            };

            foreach (var toDo in relevantToDos)
            {
                toDoDisplay.AppendText((toDo.Title == null ? "Untitled ToDo" : toDo.Title) + Environment.NewLine);
            }

            taskDisplayPanel.Controls.Add(toDoDisplay);
        }
        /// <summary>
        /// Close button that closes the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
