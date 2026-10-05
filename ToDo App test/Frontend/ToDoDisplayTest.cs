using ToDo_App.Models;
using ToDo_App.UI;

namespace ToDo_App_test.Frontend;

public class ToDoDisplayTest
{

    ToDo[] testToDos = new ToDo[2];

    [SetUp]
    public void Setup()
    {
        for (int i = 0; i < testToDos.Length; i++)
        {
            testToDos[i] = new ToDo();
            testToDos[i].Title = "testTitle" + i;
        }
    }

    [Test]
    [TestCase(true, 0, 0, TestName = "Same two todos")]
    [TestCase(false, 0, 1, TestName = "Two different todos")]
    [TestCase(false, 0, -1, TestName = "compared with null")]
    public void Equals_Returns(bool expected, int firstIndex, int secondIndex)
    {
        ToDoDisplay display1 = new ToDoDisplay(testToDos[firstIndex]);
        ToDoDisplay? display2 = (secondIndex >= 0) ? new ToDoDisplay(testToDos[secondIndex]) : null;

        bool result = display1.Equals(display2);

        Assert.AreEqual(expected, result);
    }
}
