using ToDo_App.Input;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App_test;

public class InputManagerTest
{

    private InputManager inputManager;

    const string lIST_PATH = "../../../Test List/todoes_inputManagerTest.json";
    const string PREFERENCE_PATH = "../../../Test List/UserPreferences_inputManagerTest.json";

    [SetUp]
    public void Setup()
    {
        ToDoHandler toDoHandler = new ToDoHandler();
        DataHandler dataHandler = new DataHandler()
        {
            UserPreferences = PREFERENCE_PATH
        };
        dataHandler.SelectSavePath(lIST_PATH);
        ToDoListSort toDoListSort = new ToDoListSort();
        inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort);
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        File.Delete(PREFERENCE_PATH);
        File.Delete(lIST_PATH);
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
