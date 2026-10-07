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
        inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort);
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
        Assert.Fail();
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
