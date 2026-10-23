using System.Reflection;
using System.Windows.Forms;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;
using Moq;

namespace ToDo_App_test.Frontend;

 public class ToDoDetailFormTest
{
    private Mock<IInputHandling> inputMock = null!;

    [SetUp]
    public void Setup()
    {
        inputMock = new Mock<IInputHandling>();

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Returns(new ToDo());

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

        using TestableForm form = new TestableForm(toDo, inputMock.Object);

        Assert.That(FindControl<Label>(form, "idLabel").Text, Is.EqualTo(toDo.Id.ToString()));
        Assert.That(FindControl<TextBox>(form, "titleTextBox").Text, Is.EqualTo("Buy milk"));
        Assert.That(FindControl<TextBox>(form, "descriptionTextBox").Text, Is.EqualTo("2 liters"));
        Assert.That(FindControl<NumericUpDown>(form, "priorityNumberBox").Value, Is.EqualTo(7));
    }

    [Test]
    public void Constructor_NotCompletedToDo_ShowsNotAvailableAsCompletionDate()
    {
        ToDo toDo = new ToDo { Title = "Test" };

        using TestableForm form = new TestableForm(toDo, inputMock.Object);

        Assert.That(FindControl<Label>(form, "completionDateLabel").Text, Is.EqualTo("N/A"));
    }

    [Test]
    public void Constructor_CompletedToDo_ShowsCompletionDate()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        toDo.CompletionDate = new DateTime(2026, 10, 10, 12, 0, 0);

        using TestableForm form = new TestableForm(toDo, inputMock.Object);

        Assert.That(FindControl<Label>(form, "completionDateLabel").Text,
            Is.EqualTo(toDo.CompletionDate.Value.ToString("g")));
    }

    [Test]
    public void Constructor_SelectsCurrentStatusAndRepeat()
    {
        ToDo toDo = new ToDo { Title = "Test", Repeat = RepeatInterval.Weekly };
        toDo.MarkProgress(ToDoStatus.InProgress);

        using TestableForm form = new TestableForm(toDo, inputMock.Object);

        Assert.That(FindControl<ComboBox>(form, "statusComboBox").SelectedItem, Is.EqualTo(ToDoStatus.InProgress));
        Assert.That(FindControl<ComboBox>(form, "repeatComboBox").SelectedItem, Is.EqualTo(RepeatInterval.Weekly));
    }

    // ---------- Saving ----------

    [Test]
    public void Save_EmptyTitle_ShowsMessageAndDoesNotUpdate()
    {
        ToDo toDo = new ToDo { Title = "Old" };

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        FindControl<TextBox>(form, "titleTextBox").Text = "   ";

        ClickSave(form);

        Assert.That(form.LastMessage, Does.Contain("title"));

        inputMock.Verify(x => x.UpdateToDo(
            It.IsAny<Guid>(),
            It.IsAny<string?>(),
            It.IsAny<string?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<ToDoStatus?>(),
            It.IsAny<RepeatInterval?>(),
            It.IsAny<List<ChecklistItem>?>(),
            It.IsAny<int?>(),
            It.IsAny<bool>()),
            Times.Never);

        Assert.That(form.DialogResult, Is.Not.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_ChangedTitle_SendsTitleAndAccepts()
    {
        ToDo toDo = new ToDo { Title = "Old" };

        using TestableForm form = new TestableForm(toDo, inputMock.Object);

        FindControl<TextBox>(form, "titleTextBox").Text = "New";

        ClickSave(form);

        inputMock.Verify(x => x.UpdateToDo(
            toDo.Id,
            "New",
            It.IsAny<string?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<ToDoStatus?>(),
            It.IsAny<RepeatInterval?>(),
            It.IsAny<List<ChecklistItem>?>(),
            It.IsAny<int?>(),
            It.IsAny<bool>()),
            Times.Once);

        Assert.That(form.DialogResult, Is.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_ToDoNoLongerExists_ShowsMessageAndDoesNotAccept()
    {
        ToDo toDo = new ToDo { Title = "Test" };

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Returns((ToDo?)null);

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        ClickSave(form);

        Assert.That(form.LastMessage,
            Does.Contain("no longer exists"));

        Assert.That(form.DialogResult,
            Is.Not.EqualTo(DialogResult.OK));
    }

    [Test]
    public void Save_StatusChanged_SendsStatus()
    {
        ToDoStatus? receivedStatus = null;

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Callback<Guid, string?, string?, DateTime?, ToDoStatus?,
                RepeatInterval?, List<ChecklistItem>?, int?, bool>(
                (id, title, desc, deadline, status, repeat, checklist, priority, removeDeadline) =>
                {
                    receivedStatus = status;
                })
            .Returns(new ToDo());

        ToDo toDo = new ToDo { Title = "Test" };

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        FindControl<ComboBox>(form, "statusComboBox").SelectedItem =
            ToDoStatus.InProgress;

        ClickSave(form);

        Assert.That(receivedStatus,
            Is.EqualTo(ToDoStatus.InProgress));
    }


    [Test]
    public void Save_StatusUnchanged_SendsNoStatus()
    {
        ToDoStatus? receivedStatus = ToDoStatus.Done;

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Callback<Guid, string?, string?, DateTime?, ToDoStatus?,
                RepeatInterval?, List<ChecklistItem>?, int?, bool>(
                (id, title, description, deadline, status,
                 repeat, checklist, priority, removeDeadline) =>
                {
                    receivedStatus = status;
                })
            .Returns(new ToDo());

        ToDo toDo = new ToDo { Title = "Test" };

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        ClickSave(form);

        Assert.That(receivedStatus, Is.Null);
    }

    // ---------- Deadline ----------

    [Test]
    public void Save_DeadlineUnticked_AsksToRemoveDeadline()
    {
        bool removeDeadlineValue = false;

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Callback<Guid, string?, string?, DateTime?,
                ToDoStatus?, RepeatInterval?,
                List<ChecklistItem>?, int?, bool>(
                (id, title, description, deadline, status,
                 repeat, checklist, priority, removeDeadline) =>
                {
                    removeDeadlineValue = removeDeadline;
                })
            .Returns(new ToDo());

        ToDo toDo = new ToDo
        {
            Title = "Test",
            Deadline = new DateTime(2026, 10, 20)
        };

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        FindControl<DateTimePicker>(form, "deadlinePicker").Checked = false;

        ClickSave(form);

        Assert.That(removeDeadlineValue, Is.True);
    }

    [Test]
    public void Save_NoDeadlineAndUnticked_DoesNotRemoveDeadline()
    {
        bool? removeDeadlineValue = null;

        inputMock
            .Setup(x => x.UpdateToDo(
                It.IsAny<Guid>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<ToDoStatus?>(),
                It.IsAny<RepeatInterval?>(),
                It.IsAny<List<ChecklistItem>?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>()))
            .Callback<Guid, string?, string?, DateTime?, ToDoStatus?,
                RepeatInterval?, List<ChecklistItem>?, int?, bool>(
                (id, title, desc, deadline, status, repeat, checklist, priority, removeDeadline) =>
                {
                    removeDeadlineValue = removeDeadline;
                })
            .Returns(new ToDo());

        ToDo toDo = new ToDo { Title = "Test" };

        using TestableForm form =
            new TestableForm(toDo, inputMock.Object);

        ClickSave(form);

        Assert.That(removeDeadlineValue, Is.False);
    }

    [Test]
    public void Dispose_WithComponents_DisposesForm()
    {
        ToDo toDo = new ToDo { Title = "Test" };
        TestableForm form = new TestableForm(toDo, inputMock.Object);
        typeof(ToDoDetailForm)
            .GetField("components", BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(form, new System.ComponentModel.Container());

        form.Dispose();

        Assert.That(form.IsDisposed, Is.True);
    }
}
