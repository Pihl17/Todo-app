namespace ToDo_App.UI;

public partial class MainForm : Form
{

    CreateNewToDoDialogBox createNewDialog = new CreateNewToDoDialogBox();

    public MainForm()
    {
        InitializeComponent();
    }

    public void createToDoButton_Click(object sender, EventArgs e)
    {
        createNewDialog.ShowDialog();
    }
}
