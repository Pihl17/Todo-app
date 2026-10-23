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
        public DayAllTaskDisplay(
            IInputHandling inputHandler,
            DateTime clickedDay)
        {
            InitializeComponent();

            this.inputHandler = inputHandler;
            this.clickedDay = clickedDay;

            DisplayToDosOfDay();
        }

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
                toDoDisplay.AppendText("Hello" + Environment.NewLine);
            }

            taskDisplayPanel.Controls.Add(toDoDisplay);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
