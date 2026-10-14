using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDo_Web.Pages.ToDo_Pages
{
    public class CalendarModel : PageModel
    {
        public string[] Months { get; } = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public void OnGet()
        {
        }
    }
}
