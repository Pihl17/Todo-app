using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WebApp.Pages;


namespace ToDo_App.Test.Web
{
    internal class ViewTest {
        private WebApplicationFactory<Program> _factory;
        private Mock<IInputHandling> mock;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();

            mock = new Mock<IInputHandling>();

        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }

        [TestCase("/")]
        [TestCase("/Index")]
        [TestCase("/Privacy")]
        [TestCase("/Error")]
        [TestCase("/Todoes")]
        [TestCase("/SubmitTodo")]
        [TestCase("/Calendar")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForTodo()
        {
            ToDo toDo = new ToDo()
            {
                Title = "Test",
            };

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { toDo });
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/Todoes/Todo/{toDo.Id}");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForUpdateTodo()
        {
            ToDo toDo = new ToDo()
            {
                Title = "Test",
            };

            var client = _factory.CreateClient();


            var response = await client.GetAsync($"/Todoes/Todo/{toDo.Id}/Update");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }
    }
}
