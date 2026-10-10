using ToDo_App.Input;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App_test;

public class InputManagerTest
{

    private InputManager inputManager;

    [SetUp]
    public void Setup()
    {
        ToDoHandler toDoHandler = new ToDoHandler();
        DataHandler dataHandler = new DataHandler();
        ToDoListSort toDoListSort = new ToDoListSort();
        TagFilter tagFilter = new TagFilter();

        inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter);
    }

    [Test]
    public void GetToDoList_ReturnsTheCurrentListOfToDos()
    {
        Assert.Fail();
    }

    [Test]
    public void GetToDoList_ReturnsTheListSorted()
    {
        Assert.Fail();
    }

    [Test]
    public void CreateNewToDo_CreatesNewEmptyToDoAndAddsItToTheList()
    {
        int expected = inputManager.ToDos.Count + 1;
        inputManager.CreateNewToDo();

        Assert.AreEqual(expected, inputManager.ToDos.Count);
    }

    [Test]
    public void DeleteToDo_RemovesTheTodoFromTheList()
    {
        Assert.Fail();
    }

    [Test]
    public void DeleteToDo_DoesNotRemoveToDoIfItsNotOnTheList()
    {
        Assert.Fail();
    }

    [Test]
    public void SetNewSavePath_SetsNewSavePath()
    {
        Assert.Fail();
    }
}
