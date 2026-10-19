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
        /// when you post run the function
        /// </summary>
        public ActionResult OnPost()
        {
            foreach (var item in Request.Form)
            {
                Debug.WriteLine($"{item.Key}: {item.Value}");
            }
            //inputHandler.CreateNewToDo();
            return RedirectToPage("./Todoes");
        }
    }
}
