using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Models;

namespace ToDo_App.Input;

public interface IInputHandling
{
    /// <summary>
    /// Gets and returns the ToDo list from the system.
    /// </summary>
    /// <returns>List of the user's current ToDos</returns>
    List<ToDo> GetToDoList();

}
