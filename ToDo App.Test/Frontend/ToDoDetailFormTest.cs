using System.Reflection;
using System.Windows.Forms;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend;

 public class ToDoDetailFormTest
{
    // A stand-in for InputManager that records what the form sends to it.
    private class FakeInputHandling : IInputHandling
    {
        public bool ToDoExists = true;
        public int UpdateCalls;
        public Guid? UpdatedId;
        public string? UpdatedTitle;
        public ToDoStatus? UpdatedStatus;
        public bool? RemoveDeadlineValue;

        public List<ToDo> GetToDoList() { return new List<ToDo>(); }
        public ToDo CreateNewToDo() { return new ToDo(); }
        public void DeleteToDo(ToDo todo) { }
        public void SetNewSavePath(string path) { }
        public ToDo? GetToDo(Guid toDoId) { return null; }

        public ToDo? UpdateToDo(Guid toDoId, string? title = null, string? description = null,
           DateTime? deadline = null, ToDoStatus? status = null, RepeatInterval? repeat = null,
           List<ChecklistItem>? checklist = null, int? priority = null, bool removeDeadline = false)
        {
            UpdateCalls++;
            UpdatedId = toDoId;
            UpdatedTitle = title;
            UpdatedStatus = status;
            RemoveDeadlineValue = removeDeadline;
            return ToDoExists ? new ToDo() : null;
        }

        public void InitializeSelectedMonth()
        {
            throw new NotImplementedException();
        }

        public string GetSelectedMonthString()
        {
            throw new NotImplementedException();
        }

        public void NextMonth()
        {
            throw new NotImplementedException();
        }

        public void PreviousMonth()
        {
            throw new NotImplementedException();
        }
        public void NextYear()
        {
            throw new NotImplementedException();
        }
        public void PreviousYear()
        {
            throw new NotImplementedException();
        }
        public List<CalendarDay> GetToDosInGivenMonth(List<ToDo> toDos)
        {
            return new List<CalendarDay>();
        }
        public List<ToDo> GetToDosOnDate(List<ToDo> toDos, DateTime dt)
        {
            return new List<ToDo>();
        }
        public void ChangeToMonth(int i)
        {
            throw new NotImplementedException();
        }
    }
    // The form with ShowMessage replaced, so no dialog opens during tests.
    private class TestableForm : ToDoDetailForm
    {
        public string? LastMessage;

        public TestableForm(ToDo toDo, IInputHandling inputHandling) : base(toDo, inputHandling) { }

        protected override void ShowMessage(string message)
        {
            LastMessage = message;
        }
    }

    private T FindControl<T>(Form form, string name) where T : Control
    {
        return (T)form.Controls.Find(name, true)[0];
    }

    private void ClickSave(Form form)
    {
        MethodInfo? method = typeof(ToDoDetailForm).GetMethod(
            "saveButton_Click", BindingFlags.Instance | BindingFlags.NonPublic);
        method!.Invoke(form, new object?[] { null, EventArgs.Empty });
    }

    // ---------- Showing the ToDo ----------

    [Test]
    public void Constructor_ShowsToDoValues()
    {
        ToDo toDo = new ToDo { Title = "Buy milk", Description = "2 liters" };
        toDo.Priority = 7;

        using TestableForm form = new TestableForm(toDo, new FakeInputHandling());

        Assert.That(FindControl<Label>(form, "idLabel").Text, Is.EqualTo(toDo.Id.ToString()));
        Assert.That(FindControl<TextBox>(form, "titleTextBox").Text, Is.EqualTo("Buy milk"));
        Assert.That(FindControl<TextBox>(form, "descriptionTextBox").Text, Is.EqualTo("2 liters"));
        Assert.That(FindControl<NumericUpDown>(form, "priorityNumberBox").Value, Is.EqualTo(7));
    }

    [Test]
    public void Constructor_NotCompletedToDo_ShowsNotAvailableAsCompletionDate()
    {
        ToDo toDo = new ToDo { Title = "Test" };

        using TestableForm form = new TestableForm(toDo, new FakeInputHandling());

        Assert.That(FindControl<Label>(form, "completionDateLabel").Text, Is.EqualTo("N/A"));
    }

    [Test]
    public void Constructor_CompletedToDo_ShowsCompletionDate()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        toDo.CompletionDate = new DateTime(2026, 10, 10, 12, 0, 0);

        using TestableForm form = new TestableForm(toDo, new FakeInputHandling());

        Assert.That(FindControl<Label>(form, "completionDateLabel").Text,
            Is.EqualTo(toDo.CompletionDate.Value.ToString("g")));
    }

    [Test]
    public void Constructor_SelectsCurrentStatusAndRepeat()
    {
        ToDo toDo = new ToDo { Title = "Test", Repeat = RepeatInterval.Weekly };
        toDo.MarkProgress(ToDoStatus.InProgress);

        using TestableForm form = new TestableForm(toDo, new FakeInputHandling());

        Assert.That(FindControl<ComboBox>(form, "statusComboBox").SelectedItem, Is.EqualTo(ToDoStatus.InProgress));
        Assert.That(FindControl<ComboBox>(form, "repeatComboBox").SelectedItem, Is.EqualTo(RepeatInterval.Weekly));
    }

    // ---------- Saving ----------

    [Test]
    public void Save_EmptyTitle_ShowsMessageAndDoesNotUpdate()
    {
        ToDo toDo = new ToDo { Title = "Old" };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);
        FindControl<TextBox>(form, "titleTextBox").Text = "   ";

        ClickSave(form);

        Assert.That(form.LastMessage, Does.Contain("title"));
        Assert.That(fake.UpdateCalls, Is.EqualTo(0));
        Assert.That(form.DialogResult, Is.Not.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_ChangedTitle_SendsTitleAndAccepts()
    {
        ToDo toDo = new ToDo { Title = "Old" };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);
        FindControl<TextBox>(form, "titleTextBox").Text = "New";

        ClickSave(form);

        Assert.That(fake.UpdatedId, Is.EqualTo(toDo.Id));
        Assert.That(fake.UpdatedTitle, Is.EqualTo("New"));
        Assert.That(form.DialogResult, Is.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_ToDoNoLongerExists_ShowsMessageAndDoesNotAccept()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        FakeInputHandling fake = new FakeInputHandling { ToDoExists = false };
        using TestableForm form = new TestableForm(toDo, fake);

        ClickSave(form);

        Assert.That(form.LastMessage, Does.Contain("no longer exists"));
        Assert.That(form.DialogResult, Is.Not.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_StatusChanged_SendsStatus()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);
        FindControl<ComboBox>(form, "statusComboBox").SelectedItem = ToDoStatus.InProgress;

        ClickSave(form);

        Assert.That(fake.UpdatedStatus, Is.EqualTo(ToDoStatus.InProgress));
    }

    [Test]
    public void Save_StatusUnchanged_SendsNoStatus()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);

        ClickSave(form);

        Assert.That(fake.UpdatedStatus, Is.Null);
    }

    // ---------- Deadline ----------

    [Test]
    public void Save_DeadlineUnticked_AskaToRemoveDeadline()
    {
        ToDo toDo = new ToDo { Title = "Test", Deadline = new DateTime(2026, 10, 20) };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);
        FindControl<DateTimePicker>(form, "deadlinePicker").Checked = false;

        ClickSave(form);

        Assert.That(fake.RemoveDeadlineValue, Is.True);
    }

    [Test]
    public void Save_DeadlineKept_DoesNotRemoveDeadline()
    {
        ToDo toDo = new ToDo { Title = "Test", Deadline = new DateTime(2026, 10, 20) };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);

        ClickSave(form);

        Assert.That(fake.RemoveDeadlineValue, Is.False);
    }

    [Test]
    public void Save_NoDeadlineAndUnticked_DoesNotRemoveDeadline()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        FakeInputHandling fake = new FakeInputHandling();
        using TestableForm form = new TestableForm(toDo, fake);

        ClickSave(form);

        Assert.That(fake.RemoveDeadlineValue, Is.False);
    }

    [Test]
    public void Dispose_WithComponents_DisposesForm()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        TestableForm form = new TestableForm(toDo, new FakeInputHandling());
        typeof(ToDoDetailForm)
            .GetField("components", BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(form, new System.ComponentModel.Container());

        form.Dispose();

        Assert.That(form.IsDisposed, Is.True);
    }
}
