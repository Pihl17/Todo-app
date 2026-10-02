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
            string path = ".\\todo.json";
            Assert.False(File.Exists(path));
            DataHandler TestHandler = new DataHandler();
            List<ToDo> Testlist = TestHandler.LoadList(path);
            Assert.AreEqual(0, Testlist.Count);
        }

        [Test]
        public void UpdateTest()
        {
            string path = ".\\todo.json";
            DataHandler TestHandler = new DataHandler();
            List<ToDo> Testlist = TestHandler.LoadList(path);
            ToDo toDo = new ToDo();
            Testlist.Add(toDo);
            TestHandler.UpdateList(Testlist, path);
            Assert.True(File.Exists(path));
            List<ToDo> UpdatedTestlist = TestHandler.LoadList(path);
            Assert.AreEqual(1, UpdatedTestlist.Count);
            File.Delete(path);
            Assert.False(File.Exists(path));
        }
    }
}