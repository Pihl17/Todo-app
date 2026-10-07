using ToDo_App.Input;

namespace ToDo_App.UI;

public partial class MainForm : Form
{

    private IInputHandling inputHandler;

    public MainForm(IInputHandling inputHandler)
    {
        this.inputHandler = inputHandler;
        InitializeComponent();
        toDoList.inputHandler = inputHandler;
        //toDoList.RefreshList(); Uncomment when the Inputmanager is ready
    }

}
