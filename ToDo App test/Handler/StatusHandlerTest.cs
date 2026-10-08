using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App_test.Handler
{
    internal class StatusHandlerTest
    {
        private ToDoHandler toDoHandler;
        private List<ToDo> toDos;
        private StatusHandler statusHandler;
        [SetUp]
        public void Setup()
        {
            toDoHandler = new ToDoHandler();
            toDos = new List<ToDo>();
            statusHandler = new StatusHandler();

            toDos.Add(toDoHandler.Create("Test1", new List<ChecklistItem>(), 4, "Task 1", new DateTime(2026, 5, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test2", priority: 6, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test3", new List<ChecklistItem>(), priority: 7, desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat: RepeatInterval.None));
            toDos.Add(toDoHandler.Create("Test4", new List<ChecklistItem>(), 5, "Task 1", new DateTime(2026, 6, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test5", priority: 8, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test6", new List<ChecklistItem>(), priority: 10, desc: "Task 3", choosenDeadLine: new DateTime(2024, 3, 1), repeat: RepeatInterval.None));
            toDos.Add(toDoHandler.Create("Test7", new List<ChecklistItem>(), 9, "Task 1", new DateTime(2026, 7, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test8", priority: 6, desc: "Task 2", repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Test9", new List<ChecklistItem>(), priority: 6, desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat: RepeatInterval.None));
            
            toDos[3].MarkProgress(ToDoStatus.Done);//Marking Test4-7 as done to test the UpdateAllRepeatables method
            toDos[4].MarkProgress(ToDoStatus.Done);
            toDos[5].MarkProgress(ToDoStatus.Done);
            toDos[6].MarkProgress(ToDoStatus.Done);
        }

        [Test]
        public void UpdateAllRepeatablesTest()
        {
            int doneRepeatablesCount = toDos.Count(toDo => toDo.Repeat != RepeatInterval.None && toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(3, doneRepeatablesCount);

            statusHandler.UpdateAllRepeatables(toDos);

            doneRepeatablesCount = toDos.Count(toDo => toDo.Repeat != RepeatInterval.None && toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(0, doneRepeatablesCount);
        }

        [TestCase(RepeatInterval.Daily, "2026-10-10")]
        [TestCase(RepeatInterval.Weekly, "2026-10-16")]
        [TestCase(RepeatInterval.Monthly, "2026-11-9")]
        [TestCase(RepeatInterval.Yearly, "2027-10-9")]
        [TestCase(RepeatInterval.Weekdays, "2026-10-12")]
        public void UpdateAllRepeatablesTestCases(RepeatInterval repeat, string newDeadline)
        {
            var testNewDeadline = DateTime.Parse(newDeadline);
            var toDo = new ToDo
            {
                Title = "Test",
                Description = "Test",
                Deadline = new DateTime(2026, 10, 9),
                Repeat = repeat
            };
            toDo.MarkProgress(ToDoStatus.Done);
            var toDos = new List<ToDo> { toDo };

            statusHandler.UpdateAllRepeatables(toDos);
            Assert.AreEqual(ToDoStatus.NotDone, toDo.Status);
            Assert.AreEqual(testNewDeadline, toDo.Deadline);

        }

        [Test]
        public void RemoveAllDoneTest()
        {
            int doneCount = toDos.Count(toDo => toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(4, doneCount);
            statusHandler.RemoveAllDone(toDos);
            doneCount = toDos.Count(toDo => toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(3, doneCount);
            Assert.AreEqual(8, toDos.Count); // One done non-repeatable ToDo should be removed
        }
        [Test]
        public void RemoveAllDoneWithDateTest()
        {
            int doneCount = toDos.Count(toDo => toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(4, doneCount);
            statusHandler.RemoveAllDone(toDos, new DateTime(2025, 12, 31));
            doneCount = toDos.Count(toDo => toDo.Status == ToDoStatus.Done);
            Assert.AreEqual(3, doneCount);
            Assert.AreEqual(8, toDos.Count); // One done non-repeatable ToDo should be removed

        }

    }
}
