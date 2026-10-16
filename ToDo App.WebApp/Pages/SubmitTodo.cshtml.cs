using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class SubmitTodoModel(IInputHandling inputHandler) : PageModel
    {
        /// <summary>
        /// when you post run the function
        /// </summary>
        public ActionResult OnPost()
        {
            inputHandler.CreateNewToDo();
            return RedirectToPage("./TodoViews");
        }
    }
}
