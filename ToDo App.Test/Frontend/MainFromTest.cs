using Moq;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend;

public class MainFromTest
{

    [Test]
    public void createNewToDoClick_CallsCreateNewToDo()
    {
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.CreateNewToDo()).Returns(new ToDo());
        mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>());
        mock.Setup(x => x.InitializeSelectedMonth());
        mock.Setup(x => x.GetSelectedMonthString()).Returns("October 2026");
        mock.Setup(x => x.GetToDosInGivenMonth(It.IsAny<List<ToDo>>()))
    .Returns(() => Enumerable.Range(1, DateTime.DaysInMonth(2026, 10))
    .Select(day => new CalendarDay
    {
        Date = new DateTime(2026, 10, day),
        ToDosOfTheDay = new List<ToDo>()
    }).ToList());

        MainForm mainForm = new MainForm(mock.Object);

        mainForm.createNewToDotoolStripButton_Click(new object(), new EventArgs());

        mock.Verify(x => x.CreateNewToDo(), Times.Once);
    }

    [Test]
    public void CreateNewToDoClick_GetsNewToDoAndAddsToListDisplay()
    {
        int expected = 1;
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.CreateNewToDo()).Returns(new ToDo());
        mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>());
        mock.Setup(x => x.InitializeSelectedMonth());
        mock.Setup(x => x.GetSelectedMonthString()).Returns("October 2026");
        mock.Setup(x => x.GetToDosInGivenMonth(It.IsAny<List<ToDo>>()))
    .Returns(() => Enumerable.Range(1, DateTime.DaysInMonth(2026, 10))
    .Select(day => new CalendarDay
    {
        Date = new DateTime(2026, 10, day),
        ToDosOfTheDay = new List<ToDo>()
    }).ToList());
        MainForm mainForm = new MainForm(mock.Object);

        mainForm.createNewToDotoolStripButton_Click(new object(), new EventArgs());

        Assert.AreEqual(expected, mainForm.ToDoList.GetToDos().Count);
    }
}
