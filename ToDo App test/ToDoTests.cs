using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToDo_App.Models;

namespace ToDo_App_test
{
    [TestFixture]
    public class ToDoTests
    {
        [Test]
        public void Contructor_SetsCreationDateAndDefaultStatus()
        {
            ToDo toDo = new ToDo();

            Assert.That(toDo.Status, Is.EqualTo(ToDoStatus.NotDone));
            Assert.That(toDo.CreationDate, Is.Not.EqualTo(default(DateTime)));
        }

        [Test]
        public void MarkAsDone_SetsStatusAndCompletionDate()
        {
            ToDo toDo = new ToDo();

            toDo.MarkAsDone();

            Assert.That(toDo.Status, Is.EqualTo(ToDoStatus.Done));
            Assert.That(toDo.CompletionDate, Is.Not.Null);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Priority_InsideRange_IsStored(int validPriority)
        {
            ToDo toDo = new ToDo();

            toDo.Priority = validPriority;

            Assert.That(toDo.Priority, Is.EqualTo(validPriority));
        }

        [TestCase(-1, 0)]
        [TestCase(11, 10)]
        public void Priority_OutsideRange_ClampsToRange(int invalidPriority, int expectedPriority)
        {
            ToDo toDo = new ToDo();

            toDo.Priority = invalidPriority;

            Assert.That(toDo.Priority, Is.EqualTo(expectedPriority));
        }

        [Test]
        public void Constructor_SetsIdThatIsNotEmpty()
        {
            ToDo toDo = new ToDo();

            Assert.That(toDo.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Constructor_TwoDos_HaveDifferentIds()
        {
            ToDo first = new ToDo();
            ToDo second = new ToDo();

            Assert.That(first.Id, Is.Not.EqualTo(second.Id));
        }
    }
}
