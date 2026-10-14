using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;


namespace ToDo_App.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private IInputHandling inputHandler;

        public List<ToDo> ToDoList { get; set;}

        public IndexModel(IInputHandling inputHandler)
        {
            this.inputHandler = inputHandler;
            ToDoList = inputHandler.GetToDoList();
        }
    }
}
