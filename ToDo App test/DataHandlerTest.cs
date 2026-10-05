using NUnit.Framework.Internal;
using System.Formats.Tar;
using ToDo_App.Models;

namespace ToDo_App_test
{
    public class Tests
    {
        private DataHandler TestHandler;

        [SetUp]
        public void Setup()
        {
            TestHandler = new DataHandler();
            TestHandler.UserPreferences = "../../../Test List/UserPreferences.json";
        }

        [Test]
        public void LoadTest()
        {
            string path = "../../../Test List/test.json";
            Assert.False(File.Exists(path));
            List<ToDo> Testlist = TestHandler.LoadList();
            Assert.AreEqual(0, Testlist.Count);
        }

        [Test]
        public void UpdateTest(){
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