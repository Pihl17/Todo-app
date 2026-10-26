using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlTypes;
using System.Globalization;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class UpdateModel(IInputHandling inputHandler) : PageModel
    {
        public ToDo? ToDo { get; set; }
        public List<string> TagLibrary = new List<string>();
        public string? HtmlDateTime {  get; set; }
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Onget get the todo, if it returns null set an error message, otherwise convert datetime to datetime.local for HTML
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(Guid id)
        {
            ToDo = inputHandler.GetToDo(id);

            if(ToDo != null) {
                ViewData["Title"] = ToDo.Title;
                if(ToDo.Deadline != null) {
                    HtmlDateTime = Convert.ToDateTime(ToDo.Deadline).ToString("yyyy-MM-ddTHH:mm");
                }
            } else {
                ViewData["Title"] = "No Todo found";
                ErrorMessage = "Could not find any Todo";
            }
        }


        /// <summary>
        /// on post call UpdateToDo and then redirect back to the Todoes page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="Deadline"></param>
        /// <param name="Repeat"></param>
        /// <param name="Priority"></param>
        /// <param name="Tags"></param>
        /// <returns></returns>
        public ActionResult OnPost(Guid id, string Title, string? Description, DateTime? Deadline, RepeatInterval? Repeat, int? Priority, List<string>? Tags)
        {
            inputHandler.UpdateToDo(id, Title, Description, Deadline, null, Repeat, null, Priority, Tags);
            return RedirectToPage("./Todoes");
        }

    }
}
