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
        ToDoNoteUserControl toDoNote = new ToDoNoteUserControl();
        toDoListLayoutPanel.Controls.Add(toDoNote);
    }

    public void GetToDoList()
    {
        throw new NotImplementedException();
    }
}
