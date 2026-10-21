using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class UpdateModel(IInputHandling inputHandler) : PageModel
    {
        public ToDo? ToDo { get; set; }
        public List<string> TagLibrary = new List<String>();

        public void OnGet(Guid id)
        {
            ToDo = inputHandler.GetToDo(id);
            TagLibrary = ToDo.Taglist;
        }

        public ActionResult OnPost(Guid id, string Title, string Description, DateTime Deadline, RepeatInterval Repeat, int Priority, List<string> Tags)
        {
            inputHandler.UpdateToDo(id, Title, Description, Deadline, null, Repeat, null, Priority, Tags);
            return RedirectToPage("./Todoes");
        }

    }
}
