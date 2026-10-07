namespace ToDo_App.UI;

public partial class MainForm : Form
{

    public MainForm()
    {
        InitializeComponent();
        //toDoList.RefreshList(); Uncomment when the Inputmanager is ready
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
            datahandler.SelectSavePath(folder.SelectedPath);
        }

    }
}
