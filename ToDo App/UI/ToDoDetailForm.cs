using System;
using System.Windows.Forms;
using ToDo_App.Handler;
using ToDo_App.Models;


namespace ToDo_App.UI
{
    /// <summary>
    /// A form that shows all values of one ToDo and lets the user change them.
    /// </summary>
    public partial class ToDoDetailForm : Form
    {
        private readonly ToDo toDo;
        private readonly ToDoHandler toDoHandler;

        /// <summary>
        /// Creates the form for the given ToDo.
        /// <summary>
        public ToDoDetailForm(ToDo toDo, ToDoHandler toDoHandler)
        {
            InitializeComponent();
            this.toDo = toDo;
            this.toDoHandler = toDoHandler;
            
            FormInputReader.FillEnum<ToDoStatus>(statusComboBox);
            FormInputReader.FillEnum<RepeatInterval>(repeatComboBox);
            ShowToDo();
        }

        private void ShowToDo()
        {
            idLabel.Text = toDo.Id.ToString();
            titleTextBox.Text = toDo.Title;
            descriptionTextBox.Text = toDo.Description;
            creationDateLabel.Text = toDo.CreationDate?.ToString("g") ?? "N/A";
            completionDateLabel.Text = toDo.CompletionDate?.ToString("g") ?? "N/A";
            FormInputReader.ShowDate(deadlinePicker, toDo.Deadline);
            priorityNumberBox.Value = toDo.Priority;
            statusComboBox.SelectedItem = toDo.Status;
            repeatComboBox.SelectedItem = toDo.Repeat;
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string title = FormInputReader.ReadText(titleTextBox);
            if (title.Length == 0)
            {
                MessageBox.Show("Title cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            toDoHandler.EditName(toDo, title);
            toDoHandler.EditDescription(toDo, FormInputReader.ReadText(descriptionTextBox));
            toDoHandler.EditDeadline(toDo, FormInputReader.ReadDate(deadlinePicker));
            toDoHandler.EditPriority(toDo, FormInputReader.ReadNumber(priorityNumberBox));
            toDoHandler.EditRepeat(toDo, FormInputReader.ReadEnum<RepeatInterval>(repeatComboBox));
            toDo.MarkProgress(FormInputReader.ReadEnum<ToDoStatus>(statusComboBox));

            DialogResult = DialogResult.OK;
        }
    }
}
