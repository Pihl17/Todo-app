using Moq;
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
    public void TestRun()
    {
        ToDo todo = new ToDo();
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.GetToDo(todo.Id)).Returns(todo);
        ToDoDisplay display = new ToDoDisplay(todo, mock.Object);
        CancellationToken cancellationToken = new CancellationToken(true);
        
        Task task = display.OpenDetailForm(cancellationToken);

        task.Wait(cancellationToken);

    }
}
