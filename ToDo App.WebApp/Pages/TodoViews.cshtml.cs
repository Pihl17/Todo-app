using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class TodoViewsModel : PageModel {
        private IInputHandling inputHandler;

        public List<ToDo> ToDoList { get; set; }

        public TodoViewsModel(IInputHandling inputHandler)
        {
            this.inputHandler = inputHandler;
            ToDoList = inputHandler.GetToDoList();
        }
    }
}
