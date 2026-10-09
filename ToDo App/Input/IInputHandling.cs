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
    
    /// <summary>
    /// Creates a new todo and adds it to the list.
    /// </summary>
    /// <returns>The newly created todo</returns>
    ToDo CreateNewToDo();

    /// <summary>
    /// Deletes a todo from the list.
    /// </summary>
    /// <param name="todo">The todo to delete</param>
    void DeleteToDo(ToDo todo);

    /// <summary>
    /// Sets new filepath for the system to save the todo list to.
    /// </summary>
    /// <param name="path">The new filepath for the save file</param>
    void SetNewSavePath(string path);

    /// <summary>
    /// Gets a specific ToDo from the current list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the todo to retrieve</param>
    /// <returns>The matching ToDo if found, otherwise null</returns>
    ToDo GetToDoById(Guid id);

}
