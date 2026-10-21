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
        public ActionResult OnPost(string Title, string Description, DateTime Deadline, int Repeat, List<string> Tags)
        {
            Debug.WriteLine($"{Title}");
            Debug.WriteLine($"{Description}");
            Debug.WriteLine($"{Deadline}");
            Debug.WriteLine($"{Repeat}");
            for (int i = 0; i < Tags.Count; i++){
                Debug.WriteLine($"Tag {i} is: {Tags[i]}");
            }

            //inputHandler.CreateNewToDo();
            return RedirectToPage("./Todoes");
        }
    }
}
