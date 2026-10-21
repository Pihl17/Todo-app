using NUnit.Framework.Internal;
using System.Formats.Tar;

using ToDo_App.Library.Models;
using ToDo_App.Library.Data;

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
            ToDo toDo = new ToDo();
            Testlist.Add(toDo);

            TestHandler.UpdateList(Testlist);

            Assert.True(File.Exists(TestHandler.Path));

            TestHandler.LoadTodoList(out List<ToDo> UpdatedTestlist);
            Assert.AreEqual(1, UpdatedTestlist.Count);
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
            Assert.Fail();
        }

        [Test]
        public void AddToDo_SavesToDoToJsonFile()
        {
            Assert.Fail();
        }

        [Test]
        public void AddToDo_ToDoAlreadyExists_DoesNotAdd()
        {
            Assert.Fail();
        }

        [Test]
        public void RemoveToDo_RemovesToDoFromListAndJSON()
        {
            Assert.Fail();
        }

        [Test]
        public void RemoveToDo_ToDoDoesNotExist_ReturnsError()
        {
            Assert.Fail();
        }

        [Test]
        public void UpdateToDo_ReplacesToDoValuesWithNewValues()
        {
            Assert.Fail();
        }

        [Test]
        public void UpdateToDo_ToDoCannotBeFound_ReturnsError()
        {
            Assert.Fail();
        }

        public void LoadToDo_ReturnsToDo()
        {
            Assert.Fail();
        }

        public void LoadToDo_ToDoCannotBeFound_ReturnsFailure()
        {
            Assert.Fail();
        }

        //cleanup after test
        [OneTimeTearDown]
        public void Cleanup() {
            File.Delete(TestHandler.UserPreferences);
            File.Delete(TestHandler.Path);
        }
    }
}