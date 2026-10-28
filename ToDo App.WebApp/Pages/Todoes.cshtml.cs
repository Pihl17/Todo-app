using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.WebApp.Pages
{
    public class TodoesModel(IInputHandling inputHandler) : PageModel {

        public List<ToDo> ToDoList { get; set; } = inputHandler.GetToDoList();


        /// <summary>
        /// onPost get the selcted todoe and then send it to the inputhandler to be deleted
        /// </summary>
        /// <param name="id"></param>
        public async Task<IActionResult> OnPost(Guid id) {
            ToDo toDo = inputHandler.GetToDo(id);
            inputHandler.DeleteToDo(toDo);

            return new JsonResult(new
            {
                success = true,
                message = "Successfully deleted!"
            });
        }
    }
}
