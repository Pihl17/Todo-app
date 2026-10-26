using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.UI;

public partial class MainForm : Form
{
    private IInputHandling inputHandler;
    public ToDoListDisplay ToDoList { get { return toDoList; } }

    public MainForm(IInputHandling inputHandler)
    {
        this.inputHandler = inputHandler;
        InitializeComponent();
        toDoList.inputHandler = inputHandler;
        toDoList.parentForm = this;
        toDoList.RefreshList();

        calendarUI = new CalendarUI(inputHandler);
        calendarUI.Dock = DockStyle.Fill;
        calendarTabPage.Controls.Add(calendarUI);
    }

    public void createNewToDotoolStripButton_Click(object sender, EventArgs e)
    {
        ToDo createdToDo = inputHandler.CreateNewToDo();
        toDoList.AddToList(new ToDoDisplay(createdToDo, inputHandler));
    }

    /// <summary>
    /// on button click let the user to chose a new folder for saving their todo lists.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    private void SelectSavePath_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folder = new FolderBrowserDialog();
        if (folder.ShowDialog() == DialogResult.OK)
        {
            inputHandler.SetNewSavePath(folder.SelectedPath);
        }
    }

    /// <summary>
    /// Creates a ToDoDetailForm control and puts it into the currently selected tab page.
    /// </summary>
    /// <param name="todoId">Id of the Todo to open the detail form for</param>
    public void ShowToDoDetailView(Guid todoId)
    {
        ToDo? toDo = inputHandler.GetToDo(todoId);
        if (toDo == null)
            return;

        ToDoDetailForm toDoDetailForm = new ToDoDetailForm(toDo, inputHandler)
        {
            Dock = DockStyle.Fill
        };
        if (tabControl.SelectedTab != null)
        {
            tabControl.SelectedTab.Controls.Add(toDoDetailForm);
            toDoDetailForm.BringToFront();
        }
    }
}
