using NUnit.Framework.Internal;
using System.Formats.Tar;

using ToDo_App.Library.Models;
using ToDo_App.Library.Data;
using System.Text.Json;

namespace ToDo_App.Test.Data
{
    public class JSONDataHandlerTests
    {
        private JSONDataHandler TestHandler;

        //setup Testhandler
        [OneTimeSetUp]
        public void Setup()
        {
            Directory.CreateDirectory("../../../Test Folder One");
            Directory.CreateDirectory("../../../Test Folder Two");
            TestHandler = new JSONDataHandler();
            TestHandler.UserPreferences = "../../../Test Folder One/UserPreferences.json";
            TestHandler.Path = "../../../Test Folder One/todoes.json";
            TestHandler.LoadTodoList(out List<ToDo> Testlist);
            TestHandler.UpdateList(Testlist);
        }

        [Test]
        public void Load_And_Update_List_Test(){
            TestHandler.LoadTodoList(out List<ToDo> Testlist);
            int expected = Testlist.Count + 1;
            ToDo toDo = new ToDo();
            Testlist.Add(toDo);

            TestHandler.UpdateList(Testlist);

            Assert.True(File.Exists(TestHandler.Path));

            TestHandler.LoadTodoList(out List<ToDo> UpdatedTestlist);
            Assert.AreEqual(expected, UpdatedTestlist.Count);
        }

        [Test]
        public void Get_And_Set_Path_Test()
        {
            TestHandler.GetPath();
            Assert.AreEqual("../../../Test Folder One/todoes.json", TestHandler.Path);

            TestHandler.SelectSavePath("../../../Test Folder Two/todoes.json");
            Assert.AreEqual("../../../Test Folder Two/todoes.json", TestHandler.Path);
            Assert.True(File.Exists("../../../Test Folder Two/todoes.json"));

            TestHandler.SelectSavePath("../../../Test Folder One/todoes.json");

            TestHandler.GetPath();
            Assert.AreEqual("../../../Test Folder One/UserPreferences.json", TestHandler.UserPreferences);
            Assert.AreEqual("../../../Test Folder One/todoes.json", TestHandler.Path);
        }

        [Test]
        public void AddToDo_AddsToDoToList()
        {
            TestHandler.ToDos = new List<ToDo>();
            ToDo toDo = new ToDo();
            List<ToDo> expected = new List<ToDo>() { toDo };

            TestHandler.AddTodo(toDo);

            Assert.That(TestHandler.ToDos, Is.EquivalentTo(expected));
        }

        [Test]
        public void AddToDo_SavesToDoToJsonFile()
        {
            TestHandler.ToDos = new List<ToDo>();
            ToDo toDo = new ToDo();
            string expected = $"\"Id\": \"{toDo.Id}\"";

            TestHandler.AddTodo(toDo);

            string jsonDataString = File.ReadAllText(TestHandler.Path);
            Assert.That(jsonDataString, Contains.Substring(expected));
        }

        [Test]
        public void AddToDo_ToDoAlreadyExists_DoesNotAdd()
        {
            ToDo toDo = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo };
            List<ToDo> expected = new List<ToDo>() { toDo };

            TestHandler.AddTodo(toDo);

            Assert.That(TestHandler.ToDos, Is.EquivalentTo(expected));
        }
        
        [Test]
        public void RemoveToDo_RemovesToDoFromListAndJSON()
        {
            ToDo toDo1 = new ToDo();
            ToDo toDo2 = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo1, toDo2 };
            int expectedCount = 1;
            List<ToDo> expectedList = new List<ToDo>() { toDo1 };

            TestHandler.RemoveTodo(toDo2.Id);

            Assert.That(TestHandler.ToDos.Count, Is.EqualTo(expectedCount));
            Assert.That(TestHandler.ToDos, Is.EquivalentTo(expectedList));
        }

        [Test]
        public void RemoveToDo_ToDoDoesNotExist_DoesNotRemoveAnyOtherToDos()
        {
            ToDo toDo1 = new ToDo();
            ToDo toDo2 = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo1 };
            int expectedCount = 1;
            List<ToDo> expectedList = new List<ToDo>() { toDo1 };

            TestHandler.RemoveTodo(toDo2.Id);

            Assert.That(TestHandler.ToDos.Count, Is.EqualTo(expectedCount));
            Assert.That(TestHandler.ToDos, Is.EquivalentTo(expectedList));
        }

        [Test]
        public void RemoveToDo_ToDoDoesNotExist_ReturnsError()
        {
            ToDo toDo1 = new ToDo();
            ToDo toDo2 = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo1 };

            DataQueryResult result = TestHandler.RemoveTodo(toDo2.Id);

            Assert.That(result.IsSuccess, Is.EqualTo(false));
            Assert.That(result.Exception, Is.Not.Null);
        }

        [Test]
        public void UpdateToDo_ReplacesToDoValuesWithNewValues()
        {
            string expectedPriority = $"\"Priority\": 7";
            string expectedTitle = $"\"Title\": \"Updated ToDo Title\"";
            ToDo toDo = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo };
            TestHandler.UpdateList(TestHandler.ToDos);
            ToDo updatedToDo = new ToDo()
            {
                Id = toDo.Id,
                Priority = 7,
                Title = "Updated ToDo Title"
            };

            TestHandler.UpdateTodo(updatedToDo);

            string jsonDataString = File.ReadAllText(TestHandler.Path);
            Assert.That(jsonDataString, Contains.Substring(expectedPriority));
            Assert.That(jsonDataString, Contains.Substring(expectedTitle));
        }

        [Test]
        public void UpdateToDo_ToDoCannotBeFound_ReturnsFailure()
        {
            ToDo toDo = new ToDo();
            TestHandler.ToDos = new List<ToDo>();

            DataQueryResult result = TestHandler.UpdateTodo(toDo);

            Assert.That(result.IsSuccess, Is.EqualTo(false));
            Assert.That(result.Exception, Is.Not.Null);
        }

        [Test]
        public void LoadToDo_ReturnsToDo()
        {
            ToDo toDo = new ToDo();
            TestHandler.ToDos = new List<ToDo>() { toDo };

            TestHandler.LoadTodo(toDo.Id, out ToDo actualToDo);

            Assert.That(actualToDo, Is.EqualTo(toDo));
        }

        [Test]
        public void LoadToDo_ToDoCannotBeFound_ReturnsFailure()
        {
            ToDo toDo = new ToDo();
            TestHandler.ToDos = new List<ToDo>();

            DataQueryResult result = TestHandler.LoadTodo(toDo.Id, out ToDo actualToDo);

            Assert.That(result.IsSuccess, Is.EqualTo(false));
            Assert.That(result.Exception, Is.Not.Null);
        }

        [Test]
        public void LoadToDoList_UnableToDeserializeJSONString_ReturnsException()
        {
            File.WriteAllText(TestHandler.Path, "this isn't a JSON!");

            DataQueryResult result = TestHandler.LoadTodoList(out List<ToDo> todos);

            Assert.That(result.IsSuccess, Is.EqualTo(false));
            Assert.That(result.Exception, Is.InstanceOf<JsonException>());
        }
        
        //cleanup after test
        [OneTimeTearDown]
        public void Cleanup() {
            File.Delete(TestHandler.UserPreferences);
            File.Delete(TestHandler.Path);
        }
    }
}