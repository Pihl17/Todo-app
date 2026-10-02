using ToDo_App.Models;
using ToDo_App.UI;

namespace ToDo_App_test.Frontend;

public class ToDoListDisplayTest
{

    [Test]
    public void AddToList_ToDoDisplayBecomesChildControl()
    {
        ToDoListDisplay list = new ToDoListDisplay();
        ToDoDisplay toDo = new ToDoDisplay();

        list.AddToList(toDo);

        Assert.That(list.GetToDos().Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveFromList_RemovesOnlyGivenOneToDo()
    {
        ToDoDisplay todo = new ToDoDisplay();
        ToDoListDisplay list = new ToDoListDisplay(new ToDoDisplay(), todo, new ToDoDisplay());

        list.RemoveFromList(todo);

        Assert.That(list.GetToDos().Count, Is.EqualTo(2));
        Assert.That(list.GetToDos(), Is.Not.Contains(todo));
    }

    [Test]
    public void RemoveFromList_ToDoNotInList_ListRemainsUnchanged()
    {
        ToDoDisplay todo1 = new ToDoDisplay();
        ToDoDisplay todo2 = new ToDoDisplay();
        ToDoDisplay nonexistingToDo = new ToDoDisplay();
        ToDoListDisplay list = new ToDoListDisplay(todo1, todo2);
        var initialList = list.GetToDos();

        list.RemoveFromList(nonexistingToDo);

        Assert.That(list.GetToDos(), Is.EqualTo(initialList));
    }

}
