using Moq;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.UI;

namespace ToDo_App.Test.Frontend;

public class ToDoDisplayTest
{

    ToDo[] testToDos = new ToDo[2];

    [OneTimeSetUp]
    public void Setup()
    {
        for (int i = 0; i < testToDos.Length; i++)
        {
            testToDos[i] = new ToDo();
            testToDos[i].Title = "testTitle" + i;
        }
    }

    [TestCase(true, 0, 0)]
    [TestCase(false, 0, 1)]
    [TestCase(false, 0, -1)]
    public void Equals_Returns(bool expected, int firstIndex, int secondIndex)
    {
        ToDoDisplay display1 = new ToDoDisplay(testToDos[firstIndex]);
        ToDoDisplay? display2 = (secondIndex >= 0) ? new ToDoDisplay(testToDos[secondIndex]) : null;

        bool result = display1.Equals(display2);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void OnClick_OpensToDoDetailView()
    {
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>(testToDos));
        mock.Setup(x => x.InitializeSelectedMonth());
        mock.Setup(x => x.GetSelectedMonthString()).Returns("October 2026");
        mock.Setup(x => x.GetToDosInGivenMonth())
            .Returns(() => Enumerable.Range(1, DateTime.DaysInMonth(2026, 10))
            .Select(day => new CalendarDay
            {
                Date = new DateTime(2026, 10, day),
                ToDosOfTheDay = new List<ToDo>()
            }).ToList());
        mock.Setup(x => x.GetToDo(testToDos[0].Id)).Returns(testToDos[0]);
        MainForm mainForm = new MainForm(mock.Object);
        mainForm.Tabs.SelectTab(0);
        ToDoDisplay display = new ToDoDisplay(testToDos[0], mainForm);

        display.OnClick(null, new EventArgs());

        Assert.That(mainForm.Tabs.SelectedTab.Controls[0], Is.TypeOf<ToDoDetailForm>());
    }
}
