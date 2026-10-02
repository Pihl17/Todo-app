namespace ToDo_App.UI;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void createToDoButton_Click(object sender, EventArgs e)
    {
        AddToDoToListControl();
    }

    public void AddToDoToListControl()
    {
        ToDoDisplay toDoNote = new ToDoDisplay();
        toDoList.AddToList(toDoNote);
    }

    public void GetToDoList()
    {
        throw new NotImplementedException();
    }
}
