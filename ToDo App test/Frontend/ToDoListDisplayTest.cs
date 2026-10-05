using ToDo_App.Models;
using ToDo_App.UI;
using Moq;
using ToDo_App.Input;
using System.Windows.Forms;

namespace ToDo_App_test.Frontend;

public class ToDoListDisplayTest
{

    ToDo[] testToDos = new ToDo[5];

    [SetUp]
    public void Setup()
    {
        for (int i = 0; i < testToDos.Length; i++)
        {
            testToDos[i] = new ToDo();
            testToDos[i].Title = "testTitle" + i;
        }
    }


    [Test]
    public void RefreshList_GetsToDoListAndAddsToList()
    {
        Control.ControlCollection expectedControls = new Control.ControlCollection(new Control());
        expectedControls.AddRange(new ToDoDisplay(testToDos[0]), new ToDoDisplay(testToDos[1]), new ToDoDisplay(testToDos[2]));
        List<ToDo> returnedToDos = new List<ToDo>() { testToDos[0], testToDos[1], testToDos[2] };
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.GetToDoList()).Returns(returnedToDos);
        ToDoListDisplay list = new ToDoListDisplay(mock.Object);

        list.RefreshList();

        mock.Verify(x => x.GetToDoList(), Times.AtLeastOnce());
        Assert.That(list.GetToDos(), Is.EqualTo(expectedControls));
    }

    [Test]
    public void RefreshList_ListAlreadyExists_AddsMissingToDosToList()
    {
        Control.ControlCollection expectedControls = new Control.ControlCollection(new Control());
        expectedControls.AddRange(new ToDoDisplay(testToDos[0]), new ToDoDisplay(testToDos[1]), new ToDoDisplay(testToDos[2]), new ToDoDisplay(testToDos[3]), new ToDoDisplay(testToDos[4]));
        List<ToDo> returnedToDos = new List<ToDo>() { testToDos[0], testToDos[1], testToDos[2], testToDos[3], testToDos[4] };
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.GetToDoList()).Returns(returnedToDos);
        ToDoListDisplay list = new ToDoListDisplay(mock.Object);
        list.AddToList(new ToDoDisplay(testToDos[0]));
        list.AddToList(new ToDoDisplay(testToDos[1]));
        list.AddToList(new ToDoDisplay(testToDos[3]));

        list.RefreshList();

        Assert.That(list.GetToDos().Count, Is.EqualTo(5));
        Assert.That(list.GetToDos(), Is.EquivalentTo(expectedControls));
    }

    [Test]
    public void RefreshList_ListAlreadyExists_RemovesToDosFromList()
    {
        Control.ControlCollection expectedControls = new Control.ControlCollection(new Control());
        expectedControls.AddRange(new ToDoDisplay(testToDos[0]), new ToDoDisplay(testToDos[2]), new ToDoDisplay(testToDos[4]));
        List<ToDo> returnedToDos = new List<ToDo>() { testToDos[0], testToDos[2], testToDos[4] };
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.GetToDoList()).Returns(returnedToDos);
        ToDoListDisplay list = new ToDoListDisplay(mock.Object);
        for (int i = 0; i < testToDos.Length; i++)
        {
            list.AddToList(new ToDoDisplay(testToDos[i]));
        }

        list.RefreshList();

        Assert.That(list.GetToDos().Count, Is.EqualTo(3));
        Assert.That(list.GetToDos(), Is.EquivalentTo(expectedControls));
    }

    [Test]
    public void AddToList_ToDoDisplayBecomesChildControl()
    {
        ToDoListDisplay list = new ToDoListDisplay();
        ToDoDisplay toDo = new ToDoDisplay();

        list.AddToList(toDo);

        Assert.That(list.GetToDos().Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveFromList_RemovesOnlyGivenOneToDo()
    {
        ToDoDisplay todo = new ToDoDisplay(testToDos[0]);
        ToDoListDisplay list = new ToDoListDisplay(new ToDoDisplay(), todo, new ToDoDisplay());

        list.RemoveFromList(todo);

        Assert.That(list.GetToDos().Count, Is.EqualTo(2));
        Assert.That(list.GetToDos(), Is.Not.Contains(todo));
    }

    [Test]
    public void RemoveFromList_ToDoNotInList_ListRemainsUnchanged()
    {
        ToDoDisplay todo1 = new ToDoDisplay(testToDos[0]);
        ToDoDisplay todo2 = new ToDoDisplay(testToDos[1]);
        ToDoDisplay nonexistingToDo = new ToDoDisplay(testToDos[2]);
        ToDoListDisplay list = new ToDoListDisplay(todo1, todo2);
        var initialList = list.GetToDos();

        list.RemoveFromList(nonexistingToDo);

        Assert.That(list.GetToDos(), Is.EqualTo(initialList));
    }

}
