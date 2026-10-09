using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Handler;
using ToDo_App.Models;

namespace ToDo_App.Input;

/// <summary>
/// Class for handling the input calls from the frontend.
/// </summary>
public class InputManager : IInputHandling
{

    ToDoHandler toDoHandler;
    DataHandler dataHandler;
    ToDoListSort listSorter;

    public List<ToDo> ToDos { get; set; } = new List<ToDo>();
    
    /// <summary>
    /// Constructs the InputManager with all the needed dependencies.
    /// </summary>
    /// <param name="toDoHandler">Object for handling creating and changing todos</param>
    /// <param name="dataHandler">Object for handling the saving and loading of todo list</param>
    /// <param name="toDoListSort">Object for handling the sorting of the todo list</param>
    public InputManager(ToDoHandler toDoHandler, DataHandler dataHandler, ToDoListSort toDoListSort)
    {
        this.toDoHandler = toDoHandler;
        this.dataHandler = dataHandler;
        ToDos = this.dataHandler.LoadList();
        listSorter = toDoListSort;
    }

    public List<ToDo> GetToDoList()
    {
        ToDos = listSorter.SortList(ToDos);
        return ToDos;
    }

    public ToDo CreateNewToDo()
    {
        ToDo createdToDo = toDoHandler.Create("Insert title");
        ToDos.Add(createdToDo);
        dataHandler.UpdateList(ToDos);
        return createdToDo;
    }

    public void DeleteToDo(ToDo todo)
    {
        if (ToDos.Contains(todo))
        {
            ToDos.Remove(todo);
            dataHandler.UpdateList(ToDos);
        }
    }

    public void SetNewSavePath(string path)
    {
        throw new NotImplementedException();
    }

    public ToDo? UpdateToDo(Guid toDoId, string? title = null, string? description = null, 
        DateTime? deadline = null, ToDoStatus? status = null, RepeatInterval? repeat = null, 
        List<ChecklistItem>? checklist = null, int? priority = null)
    {
        ToDo? editedToDo = GetToDo(toDoId);
        if (editedToDo == null)
        {
            return editedToDo;
        }
        if (title != null && !title.IsWhiteSpace())
        {
            toDoHandler.EditName(editedToDo, title);
        }
        if (description != null && !description.IsWhiteSpace())
        {
            toDoHandler.EditDescription(editedToDo, description);
        }
        return editedToDo;
    }

    public ToDo? GetToDo(Guid toDoId)
    {
        throw new NotImplementedException();
    }

}
