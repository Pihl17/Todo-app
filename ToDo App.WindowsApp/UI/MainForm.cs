using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WindowsApp.UI;

namespace ToDo_App.UI;

public partial class MainForm : Form
{
    private IInputHandling inputHandler;
    public ToDoListDisplay ToDoList { get { return toDoList; } }
    public TabControl Tabs { get { return tabControl; } }

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
    /// <summary>
    /// Displays the CreateToDo formula.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void createNewToDotoolStripButton_Click(object sender, EventArgs e)
    {
        ToDo createdToDo = inputHandler.CreateNewToDo();
        UserControl createToDoDisplay = new CreateToDo(inputHandler);

        this.Controls.Add(createToDoDisplay);
        createToDoDisplay.Dock = DockStyle.Fill;
        createToDoDisplay.BringToFront();
        createToDoDisplay.Disposed += HandleCreateNewToDo_Disposal;
    }
    /// <summary>
    /// Refreshes the to do list when the creation of a todo completes (or when creation is cancelled).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void HandleCreateNewToDo_Disposal(object sender, EventArgs e)
    {
        toDoList.RefreshList();
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
            toDoDetailForm.Disposed += toDoList.RefreshList;
        }
    }
}
