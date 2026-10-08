using ToDo_App.Models;

namespace ToDo_App_test.Handler
{
    public class ToDoHandlerTests
    {
        private ToDoHandler toDoHandler;
        private List<ToDo> toDos;

        [SetUp]
        public void Setup()
        {
            toDoHandler = new ToDoHandler();
            toDos = new List<ToDo>();
            toDos.Add(toDoHandler.Create("Test1", new List<ChecklistItem>(), 4, "Task 1", new DateTime(2026, 1, 1), RepeatInterval.Daily));
            toDos.Add(toDoHandler.Create("Test2", desc: "Task 2", choosenDeadLine: new DateTime(2026, 2, 1), repeat: RepeatInterval.Weekly));
            toDos.Add(toDoHandler.Create("Delete this", new List<ChecklistItem>(), desc: "Task 3", choosenDeadLine: new DateTime(2026, 3, 1), repeat:  RepeatInterval.None));



        }

        [Test]
        public void CreateTest()
        {
            toDos.Add(toDoHandler.Create("Test ToDo", new List<ChecklistItem>(), 7, "Test Description", new DateTime(2026, 1, 1), RepeatInterval.Daily));
            
            Assert.AreEqual(4, toDos.Count);
            Assert.AreEqual("Test ToDo", toDos[3].Title);
            Assert.AreEqual("Test Description", toDos[3].Description);
            Assert.AreEqual(RepeatInterval.Daily, toDos[3].Repeat);
            Assert.AreEqual(new DateTime(2026, 1, 1), toDos[3].Deadline);
            Assert.AreEqual(0, toDos[3].Checklist.Count);

        }

        [Test]
        public void EditNameTest()
        {
            Assert.AreEqual("Test2", toDos[1].Title);
            toDoHandler.EditName(toDos[1], "Edited Test2");
            Assert.AreEqual("Edited Test2", toDos[1].Title);
        }

        [Test]
        public void EditDescriptionTest()
        {
            Assert.AreEqual("Task 2", toDos[1].Description);
            toDoHandler.EditDescription(toDos[1], "Edited Task 2 Description");
            Assert.AreEqual("Edited Task 2 Description", toDos[1].Description);
        }
        [Test]
        public void EditDeadlineTest()
        {
            Assert.AreEqual(new DateTime(2026, 2, 1), toDos[1].Deadline);
            toDoHandler.EditDeadline(toDos[1], new DateTime(2027, 3, 15));
            Assert.AreEqual(new DateTime(2027, 3, 15), toDos[1].Deadline);
        }
        [Test]
        public void EditRepeatTest()
        {
            Assert.AreEqual(RepeatInterval.Weekly, toDos[1].Repeat);
            toDoHandler.EditRepeat(toDos[1], RepeatInterval.Monthly);
            Assert.AreEqual(RepeatInterval.Monthly, toDos[1].Repeat);
        }
        [Test]
        public void SetStatusTest()
        {
            Assert.AreEqual(ToDoStatus.NotDone, toDos[1].Status);
            toDoHandler.MarkInProgress(toDos[1]);
            Assert.AreEqual(ToDoStatus.InProgress, toDos[1].Status);
        }
        [Test]
        public void MarkAsDoneTest()
        {
            Assert.AreEqual(ToDoStatus.NotDone, toDos[1].Status);
            Assert.IsNull(toDos[1].CompletionDate);
            toDoHandler.FinishTask(toDos[1]);
            Assert.AreEqual(ToDoStatus.Done, toDos[1].Status);
            Assert.IsNotNull(toDos[1].CompletionDate);
        }
        [Test]
        public void EditChecklistTest()
        {
            Assert.AreEqual(0, toDos[1].Checklist.Count);
            var newChecklist = new List<ChecklistItem>
            {
                new ChecklistItem { Description = "Item 1", IsDone = false },
                new ChecklistItem { Description = "Item 2", IsDone = true }
            };
            toDoHandler.EditChecklist(toDos[1], newChecklist);
            Assert.AreEqual(2, toDos[1].Checklist.Count);
            Assert.AreEqual("Item 1", toDos[1].Checklist[0].Description);
            Assert.IsFalse(toDos[1].Checklist[0].IsDone);
            Assert.AreEqual("Item 2", toDos[1].Checklist[1].Description);
            Assert.IsTrue(toDos[1].Checklist[1].IsDone);
            Assert.AreEqual(toDos[1].Checklist.Count, 2);
        }

        [Test]
        public void DeleteTest()
        {
            Assert.AreEqual(3, toDos.Count);
            Assert.AreEqual("Delete this", toDos[2].Title);
            toDoHandler.Delete(toDos, 2);
            Assert.AreEqual(2, toDos.Count);
            foreach (var toDo in toDos)
            {
                Assert.AreNotEqual("Delete this", toDo.Title);
            }
        }

        public void CheckPriorityTest()
        {
            Assert.AreEqual(4, toDos[0].Priority);
        }

        [TestCase(5, 5)]
        [TestCase(10, 15)]
        [TestCase(0, -5)]
        public void EditPriorityTest(int testPriority, int newPriority)
        {
            toDoHandler.EditPriority(toDos[1], newPriority);
            Assert.AreEqual(testPriority, toDos[1].Priority);
        }
    }
}
