using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App_test.Handler
{
    internal class TagFilterTest{
        private TagFilter Taglibrary;
        private List<ToDo> toDos;


        [SetUp]
        public void Setup(){
            Taglibrary = new TagFilter();
            toDos = new List<ToDo>();
            ToDo FirstToDo = new ToDo();
            FirstToDo.AddTag("FirstTag");
            FirstToDo.AddTag("SecondTag");
            ToDo SecondToDo = new ToDo();
            SecondToDo.AddTag("SecondTag");
            SecondToDo.AddTag("ThirdTag");
            toDos.Add(FirstToDo);
            toDos.Add(SecondToDo);
        }

        [Test]
        public void GenerateLibraryTest() {
            Taglibrary.GenerateLibrary(toDos);
            Assert.IsNotEmpty(Taglibrary.TagLibrary);
            Assert.AreEqual(3, Taglibrary.TagLibrary.Count);
            Assert.AreEqual("FirstTag", Taglibrary.TagLibrary[0]);
            Assert.AreEqual("SecondTag", Taglibrary.TagLibrary[1]);
            Assert.AreEqual("ThirdTag", Taglibrary.TagLibrary[2]);
        }

        [Test]
        public void AddTagToLibraryTest() {
            Taglibrary.GenerateLibrary(toDos);
            Taglibrary.AddTagToLibrary("FirstTag");
            Taglibrary.AddTagToLibrary("Apple");
            Assert.AreEqual(4,Taglibrary.TagLibrary.Count);
            Assert.AreEqual("Apple", Taglibrary.TagLibrary[0]);
        }

    }
}
