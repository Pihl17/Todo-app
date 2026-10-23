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

        /// <summary>
        /// OnGet get the selected Todo, if it returns null set a Errormessage
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(Guid id)
        {
            ToDo = inputHandler.GetToDo(id);
            if (ToDo != null)
            {
                ViewData["Title"] = ToDo.Title;
            }
            else {
                ErrorMessage = "No todo found";
                ViewData["Title"] = "Could not find Todo";
            }
        }
    }
}
