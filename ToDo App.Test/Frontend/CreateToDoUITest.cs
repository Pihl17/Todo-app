
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WindowsApp.UI;

namespace ToDo_App.Test;

[TestFixture]
public class CreateToDoUITest
{
    private Mock<IInputHandling> inputMock = null!;
    private CreateToDoUI createToDoUI = null!;

    [SetUp]
    public void Setup()
    {
        inputMock = new Mock<IInputHandling>();

        createToDoUI = new CreateToDoUI(inputMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        createToDoUI?.Dispose();
    }

    [Test]
    public void SaveButton_Click_CallsCreateNewToDo()
    {
        // Arrange
        var titleTB = FindControl<TextBox>("titleTB");
        var descriptionTB = FindControl<TextBox>("descriptionTB");
        var tagInputTB = FindControl<TextBox>("tagInputTB");
        var saveBtn = FindControl<Button>("saveBtn");

        titleTB.Text = "Test ToDo";
        descriptionTB.Text = "Test description";
        tagInputTB.Text = "school work";

        // Act
        saveBtn.PerformClick();

        // Assert
        inputMock.Verify(m => m.CreateNewToDo(
            "Test ToDo",
            It.IsAny<DateTime?>(),
            It.IsAny<RepeatInterval>(),
            It.IsAny<int>(),
            "Test description",
            It.Is<List<string>>(tags =>
                tags.SequenceEqual(new[] { "school", "work" })),
            It.IsAny<List<ToDo_App.Library.Models.ChecklistItem>>()),
            Times.Once);
    }

    private T FindControl<T>(string name) where T : Control
    {
        var control = FindControlRecursive(createToDoUI, name);

        Assert.That(control, Is.Not.Null,
            $"Could not find control named '{name}'.");

        Assert.That(control, Is.InstanceOf<T>());

        return (T)control!;
    }

    private Control? FindControlRecursive(Control parent, string name)
    {
        foreach (Control control in parent.Controls)
        {
            if (control.Name == name)
                return control;

            var child = FindControlRecursive(control, name);

            if (child != null)
                return child;
        }

        return null;
    }
    [Test]
    public void CloseBtn_Click_Test()
    {
        Assert.That(createToDoUI.IsDisposed, Is.False);
        createToDoUI.CloseButton.PerformClick();
        Assert.That(createToDoUI.IsDisposed, Is.True);
    }
    [Test]
    public void ClearDateButton_Click_Test()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);

        try
        {
            ui.ClearDateButton.PerformClick();
            Assert.That(ui.DeadlineText, Is.EqualTo("Deadline: None"));
            Assert.That(ui.CalendarVisible, Is.False);
            Assert.That(ui.ClearDateButton.Visible, Is.False);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void DeadlineSelectButton_Click_Test()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);

        try
        {
            bool initialCalendarVisibility = ui.CalendarVisible;

            ui.DeadlineSelectButton.PerformClick();

            Assert.That(
                ui.CalendarVisible,
                Is.EqualTo(!initialCalendarVisibility));
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void ChecklistItem_ItemLeave_LastItemHasText_AddsNewItem()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);

        try
        {
            int initialCount = ui.ChecklistItemCount;
            var lastItem = ui.GetChecklistItem(initialCount - 1);

            lastItem.SetTextBoxText("Buy groceries");
            lastItem.TriggerItemLeave();

            Assert.That(ui.ChecklistItemCount, Is.EqualTo(initialCount + 1));
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void ChecklistItem_ItemLeave_EmptyNonLastItem_RemovesItem()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);
        var form = new Form();

        try
        {
            form.Controls.Add(ui);
            form.Show();
            Application.DoEvents();

            var firstItem = ui.GetChecklistItem(0);
            firstItem.SetTextBoxText("Keep this item");
            firstItem.TriggerItemLeave();

            int countBefore = ui.ChecklistItemCount;
            var emptyItem = ui.GetChecklistItem(0);
            emptyItem.SetTextBoxText("");
            emptyItem.TriggerItemLeave();

            Application.DoEvents();

            Assert.That(ui.ChecklistItemCount, Is.EqualTo(countBefore - 1));
        }
        finally
        {
            form.Dispose();
        }
    }
    [Test]
    public void ChecklistItem_ItemLeave_OnlyItemEmpty_KeepsItem()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);

        try
        {
            Assert.That(ui.ChecklistItemCount, Is.EqualTo(1));

            var item = ui.GetChecklistItem(0);
            item.SetTextBoxText("");
            item.TriggerItemLeave();

            Assert.That(ui.ChecklistItemCount, Is.EqualTo(1));
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void ChecklistItemNameTB_Leave_RaisesItemLeaveEvent()
    {
        var item = new ChecklistItemUI();
        bool eventRaised = false;

        item.ItemLeave += (sender, e) => eventRaised = true;

        item.ChecklistItemNameTB_Leave(item, EventArgs.Empty);

        Assert.That(eventRaised, Is.True);

        item.Dispose();
    }
    [Test]
    public void ChecklistItemNameTB_KeyPress_Enter_RaisesItemLeaveEvent()
    {
        var item = new ChecklistItemUI();
        bool eventRaised = false;

        item.ItemLeave += (sender, e) => eventRaised = true;

        var args = new KeyPressEventArgs((char)Keys.Enter);

        item.ChecklistItemNameTB_KeyPress(item, args);

        Assert.That(eventRaised, Is.True);

        item.Dispose();
    }
    [Test]
    public void ChecklistItemNameTB_KeyPress_OtherKey_DoesNotRaiseItemLeaveEvent()
    {
        var item = new ChecklistItemUI();
        bool eventRaised = false;

        item.ItemLeave += (sender, e) => eventRaised = true;

        var args = new KeyPressEventArgs('a');

        item.ChecklistItemNameTB_KeyPress(item, args);

        Assert.That(eventRaised, Is.False);

        item.Dispose();
    }
    [Test]
    public void ClearDateBtn_Click_ClearsDeadline()
    {
        var inputMock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(inputMock.Object);

        try
        {
            DateTime date = new DateTime(2026, 10, 29);
            ui.DeadlineCalendar.SelectionStart = date;
            ui.monthCalendar_DateChanged(
                ui.DeadlineCalendar,
                new DateRangeEventArgs(date, date));

            ui.ClearDateButton.PerformClick();

            Assert.That(ui.DeadlineText, Is.EqualTo("Deadline: None"));
            Assert.That(ui.SelectedDate, Is.Null);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void SaveButton_InvalidRepeat_UsesNone()
    {
        var mock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(mock.Object);

        try
        {
            ui.RepeatComboBox.Text = "InvalidInterval";

            ui.SaveButton.PerformClick();

            mock.Verify(x => x.CreateNewToDo(
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                RepeatInterval.None,
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<List<string>>(),
                It.IsAny<List<ChecklistItem>>()),
                Times.Once);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void SaveButton_MultipleTags_PassesSeparatedTags()
    {
        var mock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(mock.Object);

        try
        {
            ui.TagTextBox.Text = "work urgent personal";

            ui.SaveButton.PerformClick();

            mock.Verify(x => x.CreateNewToDo(
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<RepeatInterval>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.Is<List<string>>(tags =>
                    tags.SequenceEqual(new[] { "work", "urgent", "personal" })),
                It.IsAny<List<ChecklistItem>>()),
                Times.Once);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void SaveButton_EmptyTags_PassesEmptyList()
    {
        var mock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(mock.Object);

        try
        {
            ui.TagTextBox.Text = "";

            ui.SaveButton.PerformClick();

            mock.Verify(x => x.CreateNewToDo(
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<RepeatInterval>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.Is<List<string>>(tags => tags.Count == 0),
                It.IsAny<List<ChecklistItem>>()),
                Times.Once);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void SaveButton_ValidPriority_PassesPriority()
    {
        var mock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(mock.Object);

        try
        {
            ui.PriorityControl.Text = "3";

            ui.SaveButton.PerformClick();

            mock.Verify(x => x.CreateNewToDo(
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<RepeatInterval>(),
                3,
                It.IsAny<string?>(),
                It.IsAny<List<string>>(),
                It.IsAny<List<ChecklistItem>>()),
                Times.Once);
        }
        finally
        {
            ui.Dispose();
        }
    }
    [Test]
    public void SaveButton_InvalidPriority_UsesOne()
    {
        var mock = new Mock<IInputHandling>();
        var ui = new CreateToDoUI(mock.Object);

        try
        {
            ui.PriorityControl.Text = "invalid";

            ui.SaveButton.PerformClick();

            mock.Verify(x => x.CreateNewToDo(
                It.IsAny<string?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<RepeatInterval>(),
                1,
                It.IsAny<string?>(),
                It.IsAny<List<string>>(),
                It.IsAny<List<ChecklistItem>>()),
                Times.Once);
        }
        finally
        {
            ui.Dispose();
        }
    }

}
