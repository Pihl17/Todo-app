using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;
using ToDo_App.WebApp.Pages;


namespace ToDo_App.Test.Web
{
    internal class ViewTest {
        private WebApplicationFactory<Program> factory;
        private Mock<IInputHandling> mock;

        private HttpClient client;

        private ToDo FullToDo;

        private ToDo EmptyToDo;


        [SetUp]
        public void SetUp()
        {
            factory = new WebApplicationFactory<Program>();

            mock = new Mock<IInputHandling>();

            client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(_ => mock.Object);
                });
            }).CreateClient();


            ChecklistItem ChecklistItem1 = new ChecklistItem()
            {
                Description = "this is a Description for a checklist item",
                IsDone = true,
            };

            ChecklistItem ChecklistItem2 = new ChecklistItem()
            {
                Description = "this is a Description for a second checklist item",
                IsDone = false,
            };

            FullToDo = new ToDo()
            {
                Title = "Test",
                Description = "This is a test",
                CreationDate = DateTime.Now,
                CompletionDate = DateTime.Now,
                Deadline = DateTime.Now,
                Repeat = 0,
                Taglist = ["test1", "test2"],
                Checklist = [
                    ChecklistItem1, ChecklistItem2
                ]
            };

            EmptyToDo = new ToDo();
        }

        [TearDown]
        public void TearDown()
        {
            factory.Dispose();
            client.Dispose();
        }

        [TestCase("/")]
        [TestCase("/Index")]
        [TestCase("/Privacy")]
        [TestCase("/Error")]
        [TestCase("/Calendar")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForSubmitTodo() {
            
            ToDo toDo = new ToDo();
            List<ToDo> List = new List<ToDo>() { toDo };
            mock.Setup(x => x.GetToDoList()).Returns(List);
            mock.Setup(x => x.GetAllTags()).Returns([]);

            var response = await client.GetAsync("/SubmitTodo");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForSubmitTodoContent()
        {

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { FullToDo });
            mock.Setup(x => x.GetAllTags()).Returns(["test1", "test2"]);

            var response = await client.GetAsync("/SubmitTodo");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeforTodoes()
        {
            ToDo toDo = new ToDo();

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { toDo });
            mock.Setup(x => x.GetToDo(toDo.Id)).Returns(toDo);

            var response = await client.GetAsync("/Todoes");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForTodo()
        {

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { FullToDo });
            mock.Setup(x => x.GetToDo(FullToDo.Id)).Returns(FullToDo);


            var response = await client.GetAsync($"/Todoes/Todo/{FullToDo.Id}");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForTodoEmpty()
        {

            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { EmptyToDo });
            mock.Setup(x => x.GetToDo(EmptyToDo.Id)).Returns(EmptyToDo);


            var response = await client.GetAsync($"/Todoes/Todo/{EmptyToDo.Id}");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForTodoNull()
        {

            Guid TestID = new Guid();

            var response = await client.GetAsync($"/Todoes/Todo/{TestID}");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForUpdateTodo()
        {
            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { FullToDo });
            mock.Setup(x => x.GetToDo(FullToDo.Id)).Returns(FullToDo);

            var response = await client.GetAsync($"/Todoes/Todo/{FullToDo.Id}/Update");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForUpdateTodoEmpty()
        {
            mock.Setup(x => x.GetToDoList()).Returns(new List<ToDo>() { EmptyToDo });
            mock.Setup(x => x.GetToDo(EmptyToDo.Id)).Returns(EmptyToDo);

            var response = await client.GetAsync($"/Todoes/Todo/{EmptyToDo.Id}/Update");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }

        [Test]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentTypeForUpdateTodoNull()
        {
            Guid TestID = new Guid();

            var response = await client.GetAsync($"/Todoes/Todo/{TestID}/Update");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That(response.Content.Headers.ContentType.ToString(), Is.EqualTo("text/html; charset=utf-8"));
        }
    }
}
