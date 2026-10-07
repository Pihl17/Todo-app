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
            if (!Directory.Exists("../../../Test Folder One"))
            {
                Directory.CreateDirectory("../../../Test Folder One");
            }
            if (!Directory.Exists("../../../Test Folder Two"))
            {
                Directory.CreateDirectory("../../../Test Folder Two");
            }
            TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test Folder One/UserPreferences.json";
            TestHandler.Path = "../../../Test Folder One/todoes.json";
            List<ToDo> Testlist = TestHandler.LoadList();
            TestHandler.UpdateList(Testlist);
        }

        [Test]
        public void Load_And_Update_List_Test(){
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
            Assert.AreEqual("../../../Test Folder One/todoes.json", TestHandler.Path);

            TestHandler.SelectSavePath("../../../Test Folder Two/todoes.json");
            Assert.AreEqual("../../../Test Folder Two/todoes.json", TestHandler.Path);
            Assert.True(File.Exists("../../../Test Folder two/todoes.json"));

            TestHandler.SelectSavePath("../../../Test Folder One/todoes.json");

            TestHandler.GetPath();
            Assert.AreEqual("../../../Test Folder One/UserPreferences.json", TestHandler.UserPreferences);
            Assert.AreEqual("../../../Test Folder One/todoes.json", TestHandler.Path);
        }

        //cleanup after test
        [OneTimeTearDown]
        public void Cleanup() {
            File.Delete(TestHandler.UserPreferences);
            File.Delete(TestHandler.Path);
        }
    }
}