using ToDo_App.Input;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App_test;

public class InputManagerTest
{

    private InputManager inputManager;

    const string TEST_FOLDER_PATH = "../../../Test Folder InputManager/";
    const string lIST_PATH = TEST_FOLDER_PATH + "todoes_inputManagerTest.json";
    const string PREFERENCE_PATH = TEST_FOLDER_PATH + "UserPreferences_inputManagerTest.json";

    [SetUp]
    public void Setup()
    {
        Directory.CreateDirectory(TEST_FOLDER_PATH);
        ToDoHandler toDoHandler = new ToDoHandler();
        DataHandler dataHandler = new DataHandler()
        {
            UserPreferences = PREFERENCE_PATH,
            Path = lIST_PATH
        };
        ToDoListSort toDoListSort = new ToDoListSort();
        inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort);
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        File.Delete(PREFERENCE_PATH);
        File.Delete(lIST_PATH);
        Directory.Delete(TEST_FOLDER_PATH);
    }

    [Test]
    public void GetToDoList_ReturnsTheCurrentListOfToDos()
    {
        int expectedCount = 2;
        List<ToDo> initialToDoList = new List<ToDo>()
            {
                new ToDo(),
                new ToDo()
            };
        inputManager.ToDos = initialToDoList;

        List<ToDo> result = inputManager.GetToDoList();
        
        Assert.AreEqual(expectedCount, result.Count);
        Assert.That(result, Is.EquivalentTo(initialToDoList));
    }

    [Test]
    public void GetToDoList_ReturnsTheListSorted()
    {
        ToDo toDo1 = new ToDo() { Priority = 8, Deadline = new DateTime(2000, 1, 10) };
        ToDo toDo2 = new ToDo() { Priority = 5, Deadline = new DateTime(2000, 1, 10) };
        ToDo toDo3 = new ToDo() { Priority = 10 };
        ToDo toDo4 = new ToDo() { Priority = 2 };
        List<ToDo> expected = new List<ToDo>()
        {
            toDo1, toDo2, toDo3, toDo4
        };
        List<ToDo> initialToDoList = new List<ToDo>()
            {
                toDo3, toDo4, toDo2, toDo1
            };
        inputManager.ToDos = initialToDoList;

        List<ToDo> result = inputManager.GetToDoList();

        Assert.That(result, Is.EqualTo(expected));
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
        ToDo toDo1 = new ToDo();
        ToDo toDo2 = new ToDo();
        List<ToDo> expected = new List<ToDo>()
        {
            toDo1
        };
        List<ToDo> initialToDoList = new List<ToDo>()
            {
                toDo2, toDo1
            };
        inputManager.ToDos = initialToDoList;

        inputManager.DeleteToDo(toDo2);

        Assert.AreEqual(expected, inputManager.ToDos);
    }

    [Test]
    public void DeleteToDo_DoesNotRemoveToDoIfItsNotOnTheList()
    {
        ToDo toDo1 = new ToDo();
        ToDo toDo2 = new ToDo();
        ToDo toDo3 = new ToDo();
        List<ToDo> expected = new List<ToDo>()
        {
            toDo2, toDo1
        };
        List<ToDo> initialToDoList = new List<ToDo>()
            {
                toDo2, toDo1
            };
        inputManager.ToDos = initialToDoList;

        inputManager.DeleteToDo(toDo3);

        Assert.AreEqual(expected, inputManager.ToDos);
    }

    [Test]
    public void SetNewSavePath_SetsNewSavePath()
    {
        Assert.Fail();
    }
}
