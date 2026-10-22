using System.Globalization;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test;

public class InputManagerTest
{

    private InputManager inputManager;

    const string TEST_FOLDER_PATH = "../../../Test Folder InputManager/";
    const string LIST_PATH = TEST_FOLDER_PATH + "todoes_inputManagerTest.json";
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
            Path = LIST_PATH
        };
        ToDoListSort toDoListSort = new ToDoListSort();
        CalendarManager calendar = new CalendarManager();
        TagFilter tagFilter = new TagFilter();

        inputManager = new InputManager(toDoHandler, dataHandler, toDoListSort, tagFilter, calendar);
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        File.Delete(PREFERENCE_PATH);
        File.Delete(LIST_PATH);
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

        Assert.IsNull(result);
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

    [Test]
    public void UpdateToDo_ReturnsNullIfIdDoesntMatch()
    {
        ToDo existingToDo = new ToDo();
        ToDo nonexisitingToDo = new ToDo();
        inputManager.ToDos = new List<ToDo>() { existingToDo };

        ToDo? result = inputManager.UpdateToDo(nonexisitingToDo.Id);

        Assert.IsNull(result);
    }
    [Test]
    public void InitializeSelectedMonth_Test()
    {
        inputManager.InitializeSelectedMonth();
        DateTime currentMonth = DateTime.Now;
        Assert.That(inputManager.GetSelectedMonth().Month, Is.EqualTo(currentMonth.Month));
        Assert.That(inputManager.GetSelectedMonth().Year, Is.EqualTo(currentMonth.Year));
    }
    [Test]
    public void GetSelectedMonthString_Test()
    {
        inputManager.InitializeSelectedMonth();
        DateTime currentMonth = DateTime.Now;
        string currentMonthString = currentMonth.ToString("MMMM yyyy");
        Assert.That(inputManager.GetSelectedMonthString(), Is.EqualTo(currentMonthString));
    }
    [Test]
    public void SetSelectedMonth_Test()
    {
        inputManager.InitializeSelectedMonth();
        inputManager.SetSelectedMonth(new DateTime(2026, 10, 1));
        Assert.AreEqual(inputManager.GetSelectedMonthString(), new DateTime(2026, 10, 1).ToString("MMMM yyyy"));
    }
    [Test]
    public void NextMonth_Test()
    {
        inputManager.InitializeSelectedMonth();
        inputManager.SetSelectedMonth(new DateTime(2026, 10, 1));
        inputManager.NextMonth();
        Assert.AreEqual(inputManager.GetSelectedMonthString(), new DateTime(2026, 11, 1).ToString("MMMM yyyy"));
    }
    [Test]
    public void PreviousMonth_Test()
    {
        inputManager.InitializeSelectedMonth();
        inputManager.SetSelectedMonth(new DateTime(2026, 10, 1));
        inputManager.PreviousMonth();
        Assert.AreEqual(inputManager.GetSelectedMonthString(), new DateTime(2026, 9, 1).ToString("MMMM yyyy"));
    }
    [Test]
    public void NextYear_Test()
    {
        inputManager.InitializeSelectedMonth();
        inputManager.SetSelectedMonth(new DateTime(2026, 10, 1));
        inputManager.NextYear();
        Assert.AreEqual(inputManager.GetSelectedMonthString(), new DateTime(2027, 10, 1).ToString("MMMM yyyy"));
    }
    [Test]
    public void PreviousYear_Test()
    {
        inputManager.InitializeSelectedMonth();
        inputManager.SetSelectedMonth(new DateTime(2026, 10, 1));
        inputManager.PreviousYear();
        Assert.AreEqual(inputManager.GetSelectedMonthString(), new DateTime(2025, 10, 1).ToString("MMMM yyyy"));
    }
    private ToDo CreateToDo(DateTime? deadline, RepeatInterval repeat)
    {
        ToDo toDo = new ToDo();
        toDo.Deadline = deadline;
        toDo.Repeat = repeat;
        return toDo;
    }
    [Test]
    public void GetToDosInGivenMonthTest()
    {
        var toDos = new List<ToDo>
            {
                CreateToDo(new DateTime(2026, 2, 5), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 2, 10), RepeatInterval.None),
                CreateToDo(new DateTime(2026, 11, 1), RepeatInterval.None)
            };

        inputManager.SetSelectedMonth(new DateTime (2026,2,1));
        int count = 0;
        foreach (var day in inputManager.GetToDosInGivenMonth(toDos))
        {
            count += day.ToDosOfTheDay.Count;
        }

        Assert.That(count, Is.EqualTo(2));
    }
    [Test]
    public void ChangeToMonthTest()
    {
        inputManager.ChangeToMonth(12);
        Assert.That(inputManager.GetSelectedMonth().Month, Is.EqualTo(12));
    }
    [Test]
    public void GetToDosOnDate_ReturnsOnlyToDosWithDeadlineThatDay()
    {
        ToDo onDay = CreateToDo(new DateTime(2026, 10, 12), RepeatInterval.None);
        ToDo otherDay = CreateToDo(new DateTime(2026, 10, 13), RepeatInterval.None);
        var toDos = new List<ToDo>
            {
                onDay,
                otherDay
            };
        List<ToDo> result = inputManager.GetToDosOnDate(toDos, new DateTime(2026, 10, 12));

        Assert.That(result, Is.EqualTo(new List<ToDo> { onDay }));
    }

}
