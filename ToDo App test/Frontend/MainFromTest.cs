using Moq;
using ToDo_App.Input;
using ToDo_App.UI;
using ToDo_App.Models;

namespace ToDo_App_test.Frontend;

public class MainFromTest
{
    
    [Test]
    public void createNewToDoClick_CallsCreateNewToDo()
    {
        Mock<IInputHandling> mock = new Mock<IInputHandling>();
        mock.Setup(x => x.CreateNewToDo()).Returns(new ToDo());
        mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>());
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
        MainForm mainForm = new MainForm(mock.Object);

        mainForm.createNewToDotoolStripButton_Click(new object(), new EventArgs());

        Assert.AreEqual(expected, mainForm.ToDoList.GetToDos().Count);
    }
}
