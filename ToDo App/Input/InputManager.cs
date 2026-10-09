using System;
using System.Collections.Generic;
using System.Globalization;
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
    Calendar calendar;

    public List<ToDo> ToDos { get; private set; } = new List<ToDo>();
    
    /// <summary>
    /// Constructs the InputManager with all the needed dependencies.
    /// </summary>
    /// <param name="toDoHandler">Object for handling creating and changing todos</param>
    /// <param name="dataHandler">Object for handling the saving and loading of todo list</param>
    /// <param name="toDoListSort">Object for handling the sorting of the todo list</param>
    /// <param name="calendar">Object for handling calendar-related operations</param>
    public InputManager(ToDoHandler toDoHandler, DataHandler dataHandler, ToDoListSort toDoListSort, Calendar calendar)
    {
        this.toDoHandler = toDoHandler;
        this.dataHandler = dataHandler;
        this.calendar = calendar;
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
        return createdToDo;
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
