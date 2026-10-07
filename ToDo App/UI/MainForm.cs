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
}
