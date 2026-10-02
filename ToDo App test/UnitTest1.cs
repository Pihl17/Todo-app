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
            DataHandler TestHandler = new DataHandler();
            List<Class1> test1 = TestHandler.LoadList();
            Assert.AreEqual(0, test1.Count);
            TestHandler.AddToList();
            TestHandler.AddToList();
            TestHandler.UpdateList();
            Assert.True(File.Exists("C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json"));
            List<Class1> test2 = TestHandler.LoadList();
            Assert.AreEqual(2, test2.Count);

        }
    }
}
