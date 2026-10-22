using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class TodoModel(IInputHandling inputHandler) : PageModel
    {
        public ToDo? ToDo { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet(Guid id)
        {
            ToDo = inputHandler.GetToDo(id);
            if (ToDo == null) {
                ErrorMessage = "No todo found";
            }
        }
    }
}
