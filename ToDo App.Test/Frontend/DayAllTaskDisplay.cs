using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend;

[TestFixture]
public class DayAllTaskDisplayTest
{
    private Mock<IInputHandling> inputMock = null!;
    private DayAllTaskDisplay datDisplay = null!;
    private DateTime clickedDay;

    [SetUp]
    public void Setup()
    {
        clickedDay = new DateTime(2026, 10, 1);

        inputMock = new Mock<IInputHandling>();

        inputMock
            .Setup(x => x.GetToDoList())
            .Returns(new List<ToDo>());

        inputMock
            .Setup(x => x.GetToDosOnDate(
                It.IsAny<DateTime>()))
            .Returns(new List<ToDo>());

        datDisplay = new DayAllTaskDisplay(
            inputMock.Object,
            clickedDay);
    }


    [Test]
    public void DisplayToDosOfDay_UsesSelectedDate()
    {
        datDisplay.DisplayToDosOfDay();

        inputMock.Verify(
            x => x.GetToDosOnDate(
                clickedDay),
            Times.Exactly(2));
    }
    [Test]
    public void DayAllTaskDisplay_ConstructTest()
    {
        var testDisplay = new DayAllTaskDisplay();
        Assert.That(testDisplay, !Is.Null);
    }
    [Test]
    public void CloseBtn_Click_Test()
    {
        Assert.That(datDisplay.IsDisposed, Is.False);
        datDisplay.CloseButton.PerformClick();
        Assert.That(datDisplay.IsDisposed, Is.True);
    }

    [Test]
    public void DisplayToDosOfDay_RequestsToDosForCorrectDate()
    {
        datDisplay.DisplayToDosOfDay();

        inputMock.Verify(
            x => x.GetToDosOnDate(
                clickedDay),
            Times.Exactly(2));
    }
    [Test]
    public void DisplayToDosOfDay_DisplaysSelectedDate()
    {
        Assert.That(
            datDisplay.ToDoDisplay.Text,
            Is.EqualTo($"To dos for  {clickedDay:dd / MM / yyyy}\n"));
    }
    [Test]
    public void DisplayToDosOfDay_DisplaysTextForEachToDo()
    {
        inputMock
            .Setup(x => x.GetToDosOnDate(
                It.IsAny<DateTime>()))
            .Returns(new List<ToDo>
            {
            new ToDo { Title = "First ToDo" },
            new ToDo { Title = "Second ToDo" },
            new ToDo { Title = "Third ToDo" }
            });

        datDisplay.DisplayToDosOfDay();

        string text = datDisplay.ToDoDisplay.Text;

        Assert.That(text.Contains("First ToDo"));
        Assert.That(text.Contains("Second ToDo"));
        Assert.That(text.Contains("Third ToDo"));

    }
    [Test]
    public void DisplayToDosOfDay_DisplaysTextForEachToDo_NullTitles()
    {
        inputMock
            .Setup(x => x.GetToDosOnDate(
                It.IsAny<DateTime>()))
            .Returns(new List<ToDo>
            {
            new ToDo { Title = null },
            new ToDo { Title = null }
            });

        datDisplay.DisplayToDosOfDay();

        string text = datDisplay.ToDoDisplay.Text;

        Assert.That(
            text.Split("Untitled ToDo").Length - 1,
            Is.EqualTo(2));
    }
    [Test]
    public void DayAllTaskDisplay_Dispose_DisposesControl()
    {
        Assert.DoesNotThrow(() => datDisplay.Dispose());
    }

    [TearDown]
    public void DisposeDisplay()
    {
        datDisplay.Dispose();
    }
}
