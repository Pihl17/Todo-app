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

            for (int i = 0; i < 2; i++){
                Random r = new Random();
                int rInt = r.Next(0, 100);
                var testobj = new Class1()
                {
                    created = DateTime.Now,
                    value = rInt
                };
                test1.Add(testobj);
            }
           
            TestHandler.UpdateList(test1);
            Assert.True(File.Exists("C:\\Users\\SPAC-B-9\\Desktop\\Projekter\\Todo-app\\Todo Lists\\todo.json"));
            List<Class1> test2 = TestHandler.LoadList();
            Assert.AreEqual(2, test2.Count);

        }
    }
}
