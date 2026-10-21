using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class SubmitTodoModel(IInputHandling inputHandler) : PageModel
    {
        public List<String> TagLibrary = new List<String>();

        public void OnGet() {

        }

        /// <summary>
        /// on post call CreateNewToDo and then redirect back to the Todoes page
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="Deadline"></param>
        /// <param name="Repeat"></param>
        /// <param name="Tags"></param>
        /// <returns></returns>
        public ActionResult OnPost(string Title, string Description, DateTime Deadline, RepeatInterval Repeat, int Priority, List<string> Tags)
        {

            inputHandler.CreateNewToDo(Title, Description, Deadline, Repeat, Priority, Tags);
            return RedirectToPage("./Todoes");
        }
    }
}
