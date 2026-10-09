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

    [OneTimeSetUp]
    public void StartSetup()
    {
        Directory.CreateDirectory(TEST_FOLDER_PATH);
    }

    [SetUp]
    public void Setup()
    {
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

    [Test]
    public void GetToDo_ReturnsToDo()
    {
        ToDo toDo1 = new ToDo();
        ToDo toDo2 = new ToDo();
        Guid toDoId = toDo2.Id;
        inputManager.ToDos = new List<ToDo>() { toDo1, toDo2 };

        ToDo? result = inputManager.GetToDo(toDoId);

        Assert.AreEqual(toDo2, result);
    }

    [Test]
    public void GetToDo_ReturnsNullIfIDDoesntExists()
    {
        ToDo toDo1 = new ToDo();
        ToDo toDo2 = new ToDo();
        ToDo toDo3 = new ToDo();
        Guid toDoId = toDo3.Id;
        inputManager.ToDos = new List<ToDo>() { toDo1, toDo2 };

        ToDo? result = inputManager.GetToDo(toDoId);

        Assert.AreEqual(null, result);
    }

    [Test]
    public void UpdateToDo_UpdatesTitle()
    {
        string expected = "Hello World";
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, title: expected);

        Assert.AreEqual(expected, toDo.Title);
    }

    [Test]
    public void UpdateToDo_UpdatesDescription()
    {
        string expected = "This is a test description";
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, description: expected);

        Assert.AreEqual(expected, toDo.Description);
    }

    [Test]
    public void UpdateToDo_UpdatesDeadline()
    {
        DateTime expected = new DateTime(2000, 2, 25);
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, deadline: expected);

        Assert.AreEqual(expected, toDo.Deadline);
    }

    [TestCase(ToDoStatus.InProgress)]
    [TestCase(ToDoStatus.Done)]
    public void UpdateToDo_UpdatesStatus(ToDoStatus statusChange)
    {
        ToDoStatus expected = statusChange;
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, status: statusChange);

        Assert.AreEqual(expected, toDo.Status);
    }

    [TestCase(RepeatInterval.Daily)]
    [TestCase(RepeatInterval.Weekly)]
    [TestCase(RepeatInterval.Weekdays)]
    [TestCase(RepeatInterval.Monthly)]
    [TestCase(RepeatInterval.Yearly)]
    public void UpdateToDo_UpdatesRepeat(RepeatInterval repeatChange)
    {
        RepeatInterval expected = repeatChange;
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, repeat: repeatChange);

        Assert.AreEqual(expected, toDo.Repeat);
    }

    [Test]
    public void UpdateToDo_UpdatesChecklist()
    {
        List<ChecklistItem> expected = new List<ChecklistItem>()
        {
            new ChecklistItem()
        };
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, checklist: expected);

        Assert.AreEqual(expected, toDo.Checklist);
    }

    [Test]
    public void UpdateToDo_UpdatesPriority()
    {
        int expected = 5;
        ToDo toDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, priority: expected);

        Assert.AreEqual(expected, toDo.Priority);
    }

    [Test]
    public void UpdateToDo_UpdatesOnlyNonNullArguments()
    {
        string expectedTitle = "Test title";
        string expectedDescription = "This is a new test description, hello!";
        int expectedPriority = 3;
        DateTime expectedDeadline = new DateTime(2000, 2, 2);
        ToDo toDo = new ToDo()
        {
            Title = "Test title",
            Description = "test description",
            Priority = 3,
            Deadline = new DateTime(2000, 2, 2)
        };
        inputManager.ToDos = new List<ToDo>() { toDo };

        inputManager.UpdateToDo(toDo.Id, description: expectedDescription);

        Assert.AreEqual(expectedDescription, toDo.Description);
        Assert.AreEqual(expectedTitle, toDo.Title, "Title was changed when it shouldn't have been");
        Assert.AreEqual(expectedPriority, toDo.Priority, "Priority was changed when it shouldn't have been");
        Assert.AreEqual(expectedDeadline, toDo.Deadline, "Deadline was changed when it shouldn't have been");
    }

}
