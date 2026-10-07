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

    private List<ToDo> toDos = new List<ToDo>();
    
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
        toDos = this.dataHandler.LoadList();
        listSorter = toDoListSort;
    }

    public List<ToDo> GetToDoList()
    {
        toDos = listSorter.SortList(toDos);
        return toDos;
    }

    public ToDo CreateNewToDo()
    {
        throw new NotImplementedException();
    }

    public void DeleteToDo(ToDo todo)
    {
        throw new NotImplementedException();
    }

    public void SetNewSavePath(string path)
    {
        throw new NotImplementedException();
    }

}
