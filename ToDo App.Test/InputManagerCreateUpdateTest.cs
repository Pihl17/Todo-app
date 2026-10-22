using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.Test
{
    public class InputManagerCreateUpdateTest
    {
        // Copy the setup form the existing InputManager tests.
        private InputManager CreateInputManager()
        {
            throw new NotImplementedException();
        }

        // ---------- CreateNewToDo ----------

        [Test]
        public void CreateNewToDo_NoArguments_CreatesToDoWithDefaults()
        {
            InputManager manager = CreateInputManager();

            ToDo created = manager.CreateNewToDo();

            Assert.That(created.Title, Is.EqualTo("Insert title"));
            Assert.That(created.Checklist, Is.Empty);
            Assert.That(created.Repeat, Is.EqualTo(RepeatInterval.None));
            Assert.That(manager.ToDos, Does.Contain(created));
        }

        [Test]
        public void CreateNewToDo_WithValues_CreatesToDoWithThoseValues()
        {
            InputManager manager = CreateInputManager();
            DateTime deadline = new DateTime(2026, 11, 1);

            ToDo created = manager.CreateNewToDo("Buy milk", null, 7, "2 liters", deadline, RepeatInterval.Weekly);

            Assert.That(created.Title, Is.EqualTo("Buy milk"));
            Assert.That(created.Priority, Is.EqualTo(7));
            Assert.That(created.Description, Is.EqualTo("2 liters"));
            Assert.That(created.Deadline, Is.EqualTo(deadline));
            Assert.That(created.Repeat, Is.EqualTo(RepeatInterval.Weekly));
            Assert.That(manager.ToDos, Does.Contain(created));
        }

        [Test]
        public void CreateNewToDo_BlankTitle_UsesPlaceholder()
        {
            InputManager manager = CreateInputManager();

            ToDo created = manager.CreateNewToDo("  ");

            Assert.That(created.Title, Is.EqualTo("Insert title"));
        }

        [Test]
        public void CreateNewToDo_WithChecklist_KeepsChecklist()
        {
            InputManager manager = CreateInputManager();
            List<ChecklistItem> items = new List<ChecklistItem> { new ChecklistItem { Description = "Item 1" } };

            ToDo created = manager.CreateNewToDo("Test", items);

            Assert.That(created.Checklist, Has.Count.EqualTo(1));
        }

        // ---------- UpdateToDo ----------

        [Test]
        public void UpdateToDo_UnknownId_ReturnsNull()
        {
            InputManager manager = CreateInputManager();

            Assert.That(manager.UpdateToDo(Guid.NewGuid(), title: "New"), Is.Null);
        }

        [Test]
        public void UpdateToDo_NewTitle_ChangesTitle()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Old");

            manager.UpdateToDo(created.Id, title: "New");

            Assert.That(created.Title, Is.EqualTo("New"));
        }

        [Test]
        public void UpdateToDo_BlankTitle_KeepsOldTitle()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Old");

            manager.UpdateToDo(created.Id, title: "   ");

            Assert.That(created.Title, Is.EqualTo("Old"));
        }

        [Test]
        public void UpdateToDo_EmptyDescription_ClearsDescription()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test", description: "Old description");

            manager.UpdateToDo(created.Id, description: "");

            Assert.That(created.Description, Is.EqualTo(""));
        }

        [Test]
        public void UpdateToDo_NewDeadline_SetsDeadline()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test");

            manager.UpdateToDo(created.Id, deadline: new DateTime(2026, 12, 1));

            Assert.That(created.Deadline, Is.EqualTo(new DateTime(2026, 12, 1)));
        }

        [Test]
        public void UpdateToDo_RemoveDeadline_ClearsDeadline()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test", deadline: new DateTime(2026, 12, 1));

            manager.UpdateToDo(created.Id, removeDeadline: true);

            Assert.That(created.Deadline, Is.Null);
        }

        [Test]
        public void UpdateToDo_RemoveDeadlineAndDeadline_RemoveWins()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test", deadline: new DateTime(2026, 12, 1));

            manager.UpdateToDo(created.Id, deadline: new DateTime(2027, 1, 1), removeDeadline: true);

            Assert.That(created.Deadline, Is.Null);
        }

        [Test]
        public void UpdateToDo_StatusDone_MarksDoneWithCompletionDate()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test");

            manager.UpdateToDo(created.Id, status: ToDoStatus.Done);

            Assert.That(created.Status, Is.EqualTo(ToDoStatus.Done));
            Assert.That(created.CompletionDate, Is.Not.Null);
        }

        [Test]
        public void UpdateToDo_StatusInProgress_MarksInProgress()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test");

            manager.UpdateToDo(created.Id, status: ToDoStatus.InProgress);

            Assert.That(created.Status, Is.EqualTo(ToDoStatus.InProgress));
        }

        [Test]
        public void UpdateToDo_StatusNotDone_SetsDoneToDoBack()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test");
            manager.UpdateToDo(created.Id, status: ToDoStatus.Done);

            manager.UpdateToDo(created.Id, status: ToDoStatus.NotDone);

            Assert.That(created.Status, Is.EqualTo(ToDoStatus.NotDone));
        }

        [Test]
        public void UpdateToDo_RepeatChecklistAndPriority_AreApplied()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test");
            List<ChecklistItem> items = new List<ChecklistItem> { new ChecklistItem { Description = "Item 1" } };

            manager.UpdateToDo(created.Id, repeat: RepeatInterval.Daily, checklist: items, priority: 8);

            Assert.That(created.Repeat, Is.EqualTo(RepeatInterval.Daily));
            Assert.That(created.Checklist, Has.Count.EqualTo(1));
            Assert.That(created.Priority, Is.EqualTo(8));
        }

        [Test]
        public void UpdateToDo_NoArguments_ChangesNothing()
        {
            InputManager manager = CreateInputManager();
            ToDo created = manager.CreateNewToDo("Test", description: "Keep", deadline: new DateTime(2026, 12, 1));

            manager.UpdateToDo(created.Id);

            Assert.That(created.Title, Is.EqualTo("Test"));
            Assert.That(created.Description, Is.EqualTo("Keep"));
            Assert.That(created.Deadline, Is.EqualTo(new DateTime(2026, 12, 1)));
        }
    }
}
