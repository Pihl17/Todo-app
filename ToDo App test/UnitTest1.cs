using System.Formats.Tar;

namespace ToDo_App_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateTest(){
            Assert.False(File.Exists("C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json"));
            DataHandler TestHandler = new DataHandler();
            TestHandler.CreateJSON();
            Assert.True(File.Exists("C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json"));
            Assert.AreEqual(0, TestHandler.list.Count());
            TestHandler.AddToList();
            TestHandler.AddToList();
            Assert.AreEqual(2, TestHandler.list.Count());
            TestHandler.UpdateList();
        }
    }
}
