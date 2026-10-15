using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDo_App.WebApp.Pages
{
    public class TodoModel : PageModel
    {
        public int Id { get; set; } = 10;
        public void OnGet()
        {
           
        }
    }
}
