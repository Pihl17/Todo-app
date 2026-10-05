using NUnit.Framework.Internal;
using System.Formats.Tar;
using ToDo_App.Models;

namespace ToDo_App_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadTest()
        {
            string path = "../../../Test List/lsit.json";
            Assert.False(File.Exists(path));
            DataHandler TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
            List<ToDo> Testlist = TestHandler.LoadList();
            TestHandler.Path = path;
            Assert.AreEqual(0, Testlist.Count);
        }

        [Test]
        public void UpdateTest(){
            DataHandler TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
            TestHandler.Path = "../../../Test List/test.json";
            List<ToDo> Testlist = TestHandler.LoadList();
            ToDo toDo = new ToDo();
            Testlist.Add(toDo);
            TestHandler.UpdateList(Testlist);
            Assert.True(File.Exists(TestHandler.Path));
            List<ToDo> UpdatedTestlist = TestHandler.LoadList();
            Assert.AreEqual(1, UpdatedTestlist.Count);
            File.Delete(TestHandler.Path);
            Assert.False(File.Exists(TestHandler.Path));
        }

        [Test]
        public void GetPathTest() {
            DataHandler TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
            TestHandler.GetPath();
            Assert.AreEqual("../../../Files/todoes.json", TestHandler.Path);

            TestHandler.Path = "reset";

            TestHandler.GetPath();
            Assert.AreEqual("../../../Files/todoes.json", TestHandler.Path);
            File.Delete(TestHandler.UserPreferences);
        }

        [Test]
        public void PathTest()
        {
            DataHandler TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
            TestHandler.GetPath();
            Assert.AreEqual("../../../Files/todoes.json", TestHandler.Path);

            TestHandler.SelectSavePath("../../../Test List/todoes.json");
            Assert.AreEqual("../../../Test List/todoes.json", TestHandler.Path);

            TestHandler.GetPath();
            Assert.AreEqual("../../../Test List/UserPreferences.json", TestHandler.UserPreferences);
            Assert.AreEqual("../../../Test List/todoes.json", TestHandler.Path);
            File.Delete(TestHandler.UserPreferences);
        }
    }
}