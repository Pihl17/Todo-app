using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class TodoesModel(IInputHandling inputHandler) : PageModel {

        public List<ToDo> ToDoList { get; set; } = inputHandler.GetToDoList();


    }
}
