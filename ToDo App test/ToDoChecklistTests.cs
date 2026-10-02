using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToDo_App.Models;

namespace ToDo_App_test
{
    [TestFixture]
    public class ToDoChecklistTests
    {
        [Test]
        public void Constructor_CrestesEmptyChecklist()
        {
            ToDo toDo = new ToDo();

            Assert.That(toDo.Checklist, Is.Empty);
        }

        [Test]
        public void Checklist_AddItem_ItemIsStored()
        {
            ToDo toDo = new ToDo();
            ChecklistItem item = new ChecklistItem { Description = "Buy Buy New Item" };

            toDo.Checklist.Add(item);

            Assert.That(toDo.Checklist, Has.Count.EqualTo(1));
        }

        [Test]
        public void Checklist_NewItem_IsNotDone()
        {
            ChecklistItem item = new ChecklistItem();

            Assert.That(item.IsDone, Is.False);
        }
    }
}
