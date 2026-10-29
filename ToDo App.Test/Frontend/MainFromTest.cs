using Moq;
using System.Reflection;
using System.Windows.Forms;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend;

public class MainFromTest
{

    private Mock<IInputHandling> mock;

    [SetUp]
    public void Setup()
    {
        mock = new Mock<IInputHandling>();
        mock.Setup(x => x.CreateNewToDo()).Returns(new ToDo());
        mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>());
        mock.Setup(x => x.InitializeSelectedMonth());
        mock.Setup(x => x.GetSelectedMonthString()).Returns("October 2026");
        mock.Setup(x => x.GetToDosInGivenMonth())
            .Returns(() => Enumerable.Range(1, DateTime.DaysInMonth(2026, 10))
            .Select(day => new CalendarDay
        {
            Date = new DateTime(2026, 10, day),
            ToDosOfTheDay = new List<ToDo>()
        }).ToList());
    }

    [Test]
    public void createNewToDoClick_CallsCreateNewToDo()
    {
        MainForm mainForm = new MainForm(mock.Object);

        mainForm.createNewToDotoolStripButton_Click(new object(), new EventArgs());

        mock.Verify(x => x.CreateNewToDo(), Times.Once);
    }

    [Test]
    public void CreateNewToDoClick_GetsNewToDoAndAddsToListDisplay()
    {
        int expected = 1;
        MainForm mainForm = new MainForm(mock.Object);

        mainForm.createNewToDotoolStripButton_Click(new object(), new EventArgs());

        Assert.AreEqual(expected, mainForm.ToDoList.GetToDos().Count);
    }

    [Test]
    public void ShowToDoDetailView_OpensToDoDetailView()
    {
        ToDo todo = new ToDo();
        mock.Setup(x => x.GetToDo(todo.Id)).Returns(todo);
        MainForm mainForm = new MainForm(mock.Object);
        mainForm.Tabs.SelectTab(0);

        mainForm.ShowToDoDetailView(todo.Id);

        Assert.That(mainForm.Tabs.SelectedTab.Controls[0], Is.TypeOf<ToDoDetailForm>());
    }

    [Test]
    public void ShowToDoDetailView_ToDoDoesNotExsist_DoesNotOpenDetailView()
    {
        Guid todoId = new Guid();
        mock.Setup(x => x.GetToDo(It.IsAny<Guid>()));
        MainForm mainForm = new MainForm(mock.Object);
        mainForm.Tabs.SelectTab(0);

        mainForm.ShowToDoDetailView(todoId);

        Assert.That(mainForm.Tabs.SelectedTab.Controls[0], Is.TypeOf<ToDoListDisplay>());
    }

    private void ClickSave(UserControl control)
    {
        MethodInfo? method = typeof(ToDoDetailForm).GetMethod(
            "saveButton_Click", BindingFlags.Instance | BindingFlags.NonPublic);
        method!.Invoke(control, new object?[] { null, EventArgs.Empty });
    }

    [Test]
    public void ShowToDoDetailView_OnCloseRefreshesToDoList()
    {
        string expected = "expected title";
        ToDo todo = new ToDo();
        todo.Title = "initial title";
        List<ToDo> todoList = new List<ToDo>() { todo };
        mock.Setup(x => x.GetToDo(todo.Id)).Returns(todo);
        mock.Setup(x => x.GetToDoList()).Returns(todoList);
        mock.Setup(x => x.UpdateToDo(
                todo.Id,
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<List<string>?>(),
                It.IsAny<bool>()))
            .Callback(() => todo.Title = expected)
            .Returns(todo);
        MainForm mainForm = new MainForm(mock.Object);
        mainForm.Tabs.SelectTab(0);

        mainForm.ShowToDoDetailView(todo.Id);
        ClickSave((UserControl)mainForm.Tabs.SelectedTab.Controls[0]);

        Assert.That(mainForm.ToDoList.Controls[0].Controls[0], Is.TypeOf<ToDoDisplay>());
        ToDoDisplay todoDisplay = (ToDoDisplay)mainForm.ToDoList.Controls[0].Controls[0];
        Assert.That(todoDisplay.Title.Text, Is.EqualTo(expected));
    }
}
