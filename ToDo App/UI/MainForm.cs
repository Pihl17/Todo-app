using ToDo_App.Input;
using ToDo_App.Models;

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
        toDoList.RefreshList();
    }

    public void createNewToDotoolStripButton_Click(object sender, EventArgs e)
    {
        ToDo createdToDo = inputHandler.CreateNewToDo();
        toDoList.AddToList(new ToDoDisplay(createdToDo));
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
            DataHandler datahandler = new DataHandler();
            datahandler.SelectSavePath($"{folder.SelectedPath}\\todoes.json");
        }
    }

    private void calendarTabPage_Click(object sender, EventArgs e)
    {

    }
}
