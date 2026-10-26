using System;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;


namespace ToDo_App.UI
{
    /// <summary>
    /// A form that shows all values of one ToDo and lets the user change them.
    /// </summary>
    public partial class ToDoDetailForm : UserControl
    {
        private readonly ToDo toDo;
        private readonly IInputHandling inputManager;
        public DialogResult DialogResult;

        /// <summary>
        /// Creates the form for the given ToDo.
        /// </summary>
        /// <param name="toDo">The ToDo to show end edit </param>
        /// <param name="inputManager">The input manager that saves the changes </param>
        public ToDoDetailForm(ToDo toDo, IInputHandling inputManager)
        {
            InitializeComponent();
            this.toDo = toDo;
            this.inputManager = inputManager;

            FormInputReader.FillEnum<ToDoStatus>(statusComboBox);
            FormInputReader.FillEnum<RepeatInterval>(repeatComboBox);
            ShowToDo();
        }

        /// <summary>
        /// Fills all controls with the values of th ToDo.
        /// </summary>
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

        /// <summary>
        /// Reads the controls and asks the input manager to save the changes.
        /// The form closes with OK if it worked, otherwise a message is shown
        /// and the formstays open.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            string title = FormInputReader.ReadText(titleTextBox);
            if (title.Length == 0)
            {
                ShowMessage("The title cannot be empty");
                return;
            }

            ToDoStatus selectedStatus = FormInputReader.ReadEnum<ToDoStatus>(statusComboBox);
            DateTime? selectedDeadline = FormInputReader.ReadDate(deadlinePicker);

            ToDo? updated = inputManager.UpdateToDo(
                toDo.Id,
                title: title,
                description: FormInputReader.ReadText(descriptionTextBox),
                deadline: selectedDeadline,
                removeDeadline: selectedDeadline == null && toDo.Deadline != null,
                status: selectedStatus != toDo.Status ? selectedStatus : null,
                repeat: FormInputReader.ReadEnum<RepeatInterval>(repeatComboBox),
                priority: FormInputReader.ReadNumber(priorityNumberBox));

            if (updated == null)
            {
                ShowMessage("The ToDo no longer exists");
                return;
            }
            DialogResult = DialogResult.OK;
            Dispose();
        }

        /// <summary>
        /// Closes down the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        /// <summary>
        /// Tells the input handler to delete the todo and closes down the view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            inputManager.DeleteToDo(toDo);
            DialogResult = DialogResult.OK;
            Dispose();
        }

        /// <summary>
        /// Shows a message to the user. Can be overridden so tests don't open a dialog.
        /// </summary>
        /// <param name="message">The text to show</param>
        [ExcludeFromCodeCoverage]
        protected virtual void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

    }
}
