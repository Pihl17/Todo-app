using NUnit.Framework.Internal;
using System.Formats.Tar;
using ToDo_App.Models;

namespace ToDo_App_test
{
    public class DataHandlerTests
    {
        private DataHandler TestHandler;

        //setup Testhandler
        [OneTimeSetUp]
        public void Setup()
        {
            TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
            TestHandler.Path = "../../../Test List/todoes.json";
        }

        [Test]
        public void Load_And_Update_List_Test(){
            Assert.False(File.Exists(TestHandler.Path));

            List<ToDo> Testlist = TestHandler.LoadList();
            ToDo toDo = new ToDo();
            Testlist.Add(toDo);

            TestHandler.UpdateList(Testlist);

            Assert.True(File.Exists(TestHandler.Path));

            List<ToDo> UpdatedTestlist = TestHandler.LoadList();
            Assert.AreEqual(1, UpdatedTestlist.Count);
        }

        [Test]
        public void Get_And_Set_Path_Test()
        {
            TestHandler.GetPath();
            Assert.AreEqual("../../../Test List/todoes.json", TestHandler.Path);

            TestHandler.SelectSavePath("Test");
            Assert.AreEqual("Test", TestHandler.Path);

            TestHandler.SelectSavePath("../../../Test List/todoes.json");

            TestHandler.GetPath();
            Assert.AreEqual("../../../Test List/UserPreferences.json", TestHandler.UserPreferences);
            Assert.AreEqual("../../../Test List/todoes.json", TestHandler.Path);
        }

        //cleanup after test
        [OneTimeTearDown]
        public void Cleanup() {
            File.Delete(TestHandler.UserPreferences);
            File.Delete(TestHandler.Path);
        }
    }
}