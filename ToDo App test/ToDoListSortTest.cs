using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Models;
using ToDo_App.Handler;

namespace ToDo_App_test
{
    public class ToDoListSortTest
    {
        private ToDoHandler toDoHandler;
        private List<ToDo> toDos;
        private ToDoListSort toDoListSort;

        [SetUp]
        public void Setup()
        {
            toDoHandler = new ToDoHandler();
            toDos = new List<ToDo>();
            toDoListSort = new ToDoListSort();
            toDos.Add(toDoHandler.Create("Test1", new List<ChecklistItem>(), 4, "Task 1", new DateTime(2026, 5, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test2", priority: 6, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test3", new List<ChecklistItem>(), priority: 7, desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat: RepeatInterval.None));
            toDos.Add(toDoHandler.Create("Test4", new List<ChecklistItem>(), 5, "Task 1", new DateTime(2026, 6, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test5", priority: 8, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test6", new List<ChecklistItem>(), priority: 10, desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat: RepeatInterval.None));
            toDos.Add(toDoHandler.Create("Test7", new List<ChecklistItem>(), 9, "Task 1", new DateTime(2026, 7, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test8", priority: 6, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test9", new List<ChecklistItem>(), priority: 6, desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat: RepeatInterval.None));

        }
        [Test]
        public void SortListTest()
        {

            toDos =toDoListSort.SortList(toDos);
            Assert.AreEqual("Test6", toDos[0].Title); // Highest priority
            Assert.AreEqual("Test3", toDos[1].Title); // Next highest priority
            Assert.AreEqual("Test9", toDos[2].Title); // Next highest priority
            Assert.AreEqual("Test1", toDos[3].Title); // Next highest priority
            Assert.AreEqual("Test4", toDos[4].Title); // Next highest priority
            Assert.AreEqual("Test7", toDos[5].Title); // Next highest priority
            Assert.AreEqual("Test5", toDos[6].Title); // Next highest priority
            Assert.AreEqual("Test2", toDos[7].Title); // Next highest priority
            Assert.AreEqual("Test8", toDos[8].Title); // Lowest priority
        }


    }
}
