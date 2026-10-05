using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Models;

namespace ToDo_App.Input;

public interface IInputHandling
{

    List<ToDo> GetToDoList();

}
