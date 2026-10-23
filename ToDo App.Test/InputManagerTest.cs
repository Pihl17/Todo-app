using System.Globalization;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Data;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;
using Moq;

namespace ToDo_App.Test;

public class InputManagerTest
{

    private InputManager inputManager;
    private ToDoHandler toDoHandler;
    private ToDoListSort toDoListSort;
    private CalendarManager calendar;
    private TagFilter tagFilter;
    private Mock<IDataHandler> dataMock;

    const string TEST_FOLDER_PATH = "../../../Test Folder InputManager/";
    const string LIST_PATH = TEST_FOLDER_PATH + "todoes.json";
    const string PREFERENCE_PATH = TEST_FOLDER_PATH + "UserPreferences.json";

    [SetUp]
    public void Setup()
    {
        toDoHandler = new ToDoHandler();
        toDoListSort = new ToDoListSort();
        calendar = new CalendarManager();
        tagFilter = new TagFilter();
        dataMock = new Mock<IDataHandler>();

        inputManager = new InputManager(toDoHandler, dataMock.Object, toDoListSort, tagFilter, calendar);
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
        dataMock.Setup(x => x.LoadTodoList(out initialToDoList));

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
        dataMock.Setup(x => x.LoadTodoList(out initialToDoList));

        List<ToDo> result = inputManager.GetToDoList();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CreateNewToDo_CreatesNewEmptyToDo()
    {
        ToDo actual = inputManager.CreateNewToDo();

        Assert.IsNotNull(actual);
    }

    [Test]
    public void DeleteToDo_CallsDataHandlersRemoveToDo()
    {
        dataMock.Setup(x => x.RemoveTodo(It.IsAny<Guid>())).Returns(new DataQueryResult(true));

        inputManager.DeleteToDo(new ToDo());

        dataMock.Verify(x => x.RemoveTodo(It.IsAny<Guid>()), Times.AtLeastOnce());
    }

    [Test]
    public void SetNewSavePath_SetsNewSavePath()
    {
        if (!Directory.Exists(TEST_FOLDER_PATH))
            Directory.CreateDirectory(TEST_FOLDER_PATH);
        JSONDataHandler jSONDataHandler = new JSONDataHandler()
        {
            UserPreferences = PREFERENCE_PATH,
            Path = LIST_PATH
        };
        jSONDataHandler.LoadTodoList(out List<ToDo> toDos);
        InputManager inputManagerWithJson = new InputManager(toDoHandler, jSONDataHandler, toDoListSort, tagFilter, calendar);

        string newPath = TEST_FOLDER_PATH + "Nested Test Folder/";
        if (!Directory.Exists(newPath))
            Directory.CreateDirectory(newPath);
        string expectedPath = newPath + "\\todoes.json";

        inputManagerWithJson.SetNewSavePath(newPath);

        Assert.AreEqual(expectedPath, jSONDataHandler.Path);

        File.Delete(expectedPath);
        Directory.Delete(newPath);
        File.Delete(PREFERENCE_PATH);
        Directory.Delete(TEST_FOLDER_PATH);
    }

    [Test]
    public void GetToDo_ReturnsToDo()
    {
        ToDo toDo = new ToDo();
        Guid toDoId = toDo.Id;
        dataMock.Setup(x => x.LoadTodo(toDoId, out toDo)).Returns(new DataQueryResult(true));

        ToDo? result = inputManager.GetToDo(toDoId);

        Assert.AreEqual(toDo, result);
    }

    [Test]
    public void GetToDo_ReturnsNullIfIDDoesntExists()
    {
        ToDo toDo = new ToDo();
        Guid toDoId = toDo.Id;
        ToDo emptyToDo = null;
        dataMock.Setup(x => x.LoadTodo(toDoId, out emptyToDo)).Returns(new DataQueryResult(false, new Exception()));

        ToDo? result = inputManager.GetToDo(toDoId);

        Assert.IsNull(result);
    }

    [Test]
    public void UpdateToDo_UpdatesTitle()
    {
        string expected = "Hello World";
        ToDo toDo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(toDo.Id, title: expected);

        Assert.AreEqual(expected, toDo.Title);
    }

    [Test]
    public void UpdateToDo_UpdatesDescription()
    {
        string expected = "This is a test description";
        ToDo toDo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(toDo.Id, description: expected);

        Assert.AreEqual(expected, toDo.Description);
    }

    [Test]
    public void UpdateToDo_UpdatesDeadline()
    {
        DateTime expected = new DateTime(2000, 2, 25);
        ToDo toDo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(toDo.Id, deadline: expected);

        Assert.AreEqual(expected, toDo.Deadline);
    }

    [TestCase(ToDoStatus.InProgress)]
    [TestCase(ToDoStatus.Done)]
    public void UpdateToDo_UpdatesStatus(ToDoStatus statusChange)
    {
        ToDoStatus expected = statusChange;
        ToDo toDo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

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
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

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
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(toDo.Id, checklist: expected);

        Assert.AreEqual(expected, toDo.Checklist);
    }

    [Test]
    public void UpdateToDo_UpdatesPriority()
    {
        int expected = 5;
        ToDo toDo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

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
        dataMock.Setup(x => x.LoadTodo(toDo.Id, out toDo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(toDo.Id, description: expectedDescription);

        Assert.AreEqual(expectedDescription, toDo.Description);
        Assert.AreEqual(expectedTitle, toDo.Title, "Title was changed when it shouldn't have been");
        Assert.AreEqual(expectedPriority, toDo.Priority, "Priority was changed when it shouldn't have been");
        Assert.AreEqual(expectedDeadline, toDo.Deadline, "Deadline was changed when it shouldn't have been");
    }

    [Test]
    public void UpdateToDo_ReturnsNullIfIdDoesntMatch()
    {
        ToDo nonexisitingToDo = new ToDo();
        ToDo emptyToDo = null;
        dataMock.Setup(x => x.LoadTodo(nonexisitingToDo.Id, out emptyToDo)).Returns(new DataQueryResult(false));

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
        dataMock.Setup(x => x.LoadTodoList(out toDos)).Returns(new DataQueryResult(true));
        inputManager.SetSelectedMonth(new DateTime (2026,2,1));
        int count = 0;
        foreach (var day in inputManager.GetToDosInGivenMonth())
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
        dataMock.Setup(x => x.LoadTodoList(out toDos)).Returns(new DataQueryResult(true));
        List<ToDo> result = inputManager.GetToDosOnDate(new DateTime(2026, 10, 12));

        Assert.That(result, Is.EqualTo(new List<ToDo> { onDay }));
    }

    [Test]
    public void InitializeSelectedMonth_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => inputManager.InitializeSelectedMonth());
    }

    [Test]
    public void GetSelectedMonthString_ReturnsValue()
    {
        inputManager.InitializeSelectedMonth();

        string result = inputManager.GetSelectedMonthString();

        Assert.That(result, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void NextMonth_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => inputManager.NextMonth());
    }

    [Test]
    public void PreviousMonth_DoesNotThrow()
    {
        inputManager.InitializeSelectedMonth();
        Assert.DoesNotThrow(() => inputManager.PreviousMonth());
    }

    [Test]
    public void GetAllTags_ReturnsTagsFromToDos()
    {
        ToDo todo = new ToDo();

        todo.Taglist.Add("Work");
        todo.Taglist.Add("School");

        List<ToDo> todoList = new List<ToDo>() { todo };
        dataMock.Setup(x => x.LoadTodoList(out todoList));

        List<string> result = inputManager.GetAllTags();

        Assert.That(result, Contains.Item("Work"));
        Assert.That(result, Contains.Item("School"));
    }


    [Test]
    public void GetToDosInGivenMonth_ReturnsCalendarDays()
    {
        List<ToDo> toDos = new List<ToDo>();
        dataMock.Setup(x => x.LoadTodoList(out toDos)).Returns(new DataQueryResult(true));
        var result = inputManager.GetToDosInGivenMonth();

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CreateNewToDo_WithPriority_SetsPriority()
    {
        ToDo todo = inputManager.CreateNewToDo(
            title: "Test",
            priority: 7);

        Assert.That(todo.Priority, Is.EqualTo(7));
    }

    [Test]
    public void CreateNewToDo_WithWhitespaceTitle_UsesInsertTitle()
    {
        ToDo todo = inputManager.CreateNewToDo("   ");

        Assert.That(todo.Title, Is.EqualTo("Insert title"));
    }

    [Test]
    public void PreviousMonth_CalledTwice_ChangesMonthTwice()
    {
        inputManager.InitializeSelectedMonth();

        string before = inputManager.GetSelectedMonthString();

        inputManager.PreviousMonth();
        inputManager.PreviousMonth();

        string after = inputManager.GetSelectedMonthString();

        Assert.That(after, Is.Not.EqualTo(before));
    }
    
    [Test]
    public void UpdateToDo_RemoveDeadline_TakesPriorityOverDeadline()
    {
        ToDo todo = new ToDo();
        todo.Deadline = DateTime.Today;

        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(
            todo.Id,
            deadline: DateTime.Today.AddDays(10),
            removeDeadline: true);

        Assert.That(todo.Deadline, Is.Null);
    }

    [Test]
    public void UpdateToDo_StatusNotDone_ChangesStatus()
    {
        ToDo todo = new ToDo();

        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(
            todo.Id,
            status: ToDoStatus.NotDone);

        Assert.That(todo.Status,
            Is.EqualTo(ToDoStatus.NotDone));
    }

    [Test]
    public void UpdateToDo_EmptyDescription_IsApplied()
    {
        ToDo todo = new ToDo
        {
            Description = "Old"
        };

        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(
            todo.Id,
            description: "");

        Assert.That(todo.Description,
            Is.EqualTo(""));
    }

    [Test]
    public void CreateNewToDo_NoArguments_CreatesToDoWithDefaults()
    {
        ToDo created = inputManager.CreateNewToDo();

        Assert.That(created.Title, Is.EqualTo("Insert title"));
        Assert.That(created.Checklist, Is.Empty);
        Assert.That(created.Repeat, Is.EqualTo(RepeatInterval.None));
    }

    [Test]
    public void CreateNewToDo_WithValues_CreatesToDoWithThoseValues()
    {
        DateTime deadline = new DateTime(2026, 11, 1);

        ToDo created = inputManager.CreateNewToDo("Buy milk", priority: 7, description: "2 liters", choosenDeadLine: deadline, repeat: RepeatInterval.Weekly);

        Assert.That(created.Title, Is.EqualTo("Buy milk"));
        Assert.That(created.Priority, Is.EqualTo(7));
        Assert.That(created.Description, Is.EqualTo("2 liters"));
        Assert.That(created.Deadline, Is.EqualTo(deadline));
        Assert.That(created.Repeat, Is.EqualTo(RepeatInterval.Weekly));
    }

    [Test]
    public void CreateNewToDo_WithChecklist_KeepsChecklist()
    {
        List<ChecklistItem> items = new List<ChecklistItem> { new ChecklistItem { Description = "Item 1" } };

        ToDo created = inputManager.CreateNewToDo("Test", checklist: items);

        Assert.That(created.Checklist, Has.Count.EqualTo(1));
    }

    [Test]
    public void UpdateToDo_BlankTitle_KeepOldTitle()
    {
        ToDo todo = new ToDo { Title = "Old" };
        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(todo.Id, title: "   ");

        Assert.That(todo.Title, Is.EqualTo("Old"));
    }

    [Test]
    public void UpdateToDo_RemoveDeadline_ClearsDeadline()
    {
        ToDo todo = new ToDo { Deadline = DateTime.Today };
        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(todo.Id, removeDeadline: true);

        Assert.That(todo.Deadline, Is.Null);
    }

    [Test]
    public void UpdateToDo_StatusDone_SetsCompletionDate()
    {
        ToDo todo = new ToDo();
        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(todo.Id, status: ToDoStatus.Done);

        Assert.That(todo.CompletionDate, Is.Not.Null);
    }

    [Test]
    public void UpdateToDo_StatusNotDone_SetsDoneToDoBack()
    {
        ToDo todo = new ToDo();
        todo.MarkProgress(ToDoStatus.Done);
        dataMock.Setup(x => x.LoadTodo(todo.Id, out todo)).Returns(new DataQueryResult(true));

        inputManager.UpdateToDo(todo.Id, status: ToDoStatus.NotDone);

        Assert.That(todo.Status, Is.EqualTo(ToDoStatus.NotDone));

    }
}
