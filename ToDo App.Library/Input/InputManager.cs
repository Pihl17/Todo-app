using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ToDo_App.Library.Handler;
using ToDo_App.Library.Models;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Data;

namespace ToDo_App.Library.Input;

/// <summary>
/// Class for handling the input calls from the frontend.
/// </summary>
public class InputManager : IInputHandling
{

    ToDoHandler toDoHandler;
    IDataHandler dataHandler;
    ToDoListSort listSorter;
    CalendarManager calendar;
    TagFilter tagFilter;

    public List<ToDo> ToDos { get; set; } = new List<ToDo>();
    
    /// <summary>
    /// Constructs the InputManager with all the needed dependencies.
    /// </summary>
    /// <param name="toDoHandler">Object for handling creating and changing todos</param>
    /// <param name="dataHandler">Object for handling the saving and loading of todo list</param>
    /// <param name="toDoListSort">Object for handling the sorting of the todo list</param>
    /// <param name="calendar">Object for handling calendar-related operations</param>
    /// <param name="toDoListSort">Object for handling creating a sorted list of tags</param>

    public InputManager(ToDoHandler toDoHandler, IDataHandler dataHandler, ToDoListSort toDoListSort, TagFilter tagFilter, CalendarManager calendar)
    {
        this.toDoHandler = toDoHandler;
        this.dataHandler = dataHandler;
        this.calendar = calendar;
        listSorter = toDoListSort;
        this.tagFilter = tagFilter;
        DataQueryResult result = this.dataHandler.LoadTodoList(out List<ToDo> toDos);
        ToDos = toDos;
    }

    public List<string> GetAllTags(){
        tagFilter.GenerateLibrary(ToDos);
        return tagFilter.TagLibrary;
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
        dataHandler.AddTodo(createdToDo);
        return createdToDo;
    }

    public void DeleteToDo(ToDo todo)
    {
        if (ToDos.Contains(todo))
        {
            ToDos.Remove(todo);
            dataHandler.RemoveTodo(todo.Id);
        }
    }

    public void SetNewSavePath(string path)
    {
        if (dataHandler is JSONDataHandler jsonHandler)
            jsonHandler.SelectSavePath($"{path}\\todoes.json");
    }

    public ToDo? UpdateToDo(Guid toDoId, string? title = null, string? description = null, 
        DateTime? deadline = null, ToDoStatus? status = null, RepeatInterval? repeat = null, 
        List<ChecklistItem>? checklist = null, int? priority = null, bool removeDeadline = false)
    {
        ToDo? toDo = GetToDo(toDoId);
        if (toDo == null)
            return null;

        if (title != null && !title.IsWhiteSpace())
            toDoHandler.EditName(toDo, title);
        if (description != null && !description.IsWhiteSpace())
            toDoHandler.EditDescription(toDo, description);
        if (removeDeadline)
            toDoHandler.EditDeadline(toDo, null);
        else if (deadline != null)
            toDoHandler.EditDeadline(toDo, deadline);
        if (status != null)
        {
            switch (status)
            {
                case ToDoStatus.NotDone:
                    toDo.MarkProgress(ToDoStatus.NotDone);
                    break;
                case ToDoStatus.InProgress:
                    toDoHandler.MarkInProgress(toDo);
                    break;
                case ToDoStatus.Done:
                    toDoHandler.FinishTask(toDo);
                    break;
            }
        }
        if (repeat != null)
            toDoHandler.EditRepeat(toDo, (RepeatInterval)repeat);
        if (checklist != null)
            toDoHandler.EditChecklist(toDo, checklist);
        if (priority != null)
            toDoHandler.EditPriority(toDo, (int)priority);

        dataHandler.UpdateTodo(toDo);
        return toDo;
    }

    public ToDo? GetToDo(Guid toDoId)
    {
        for (int i = 0; i < ToDos.Count; i++)
        {
            if (ToDos[i].Id == toDoId)
                return ToDos[i];
        }
        return null;
    }

    public void InitializeSelectedMonth()
    {
       calendar.InitializeSelectedMonth();
    }
    public string GetSelectedMonthString()
    {
        return calendar.selectedMonthString;
    }
    public void NextMonth()
    {
        calendar.NextMonth();
    }
    public void PreviousMonth()
    {
        calendar.PreviousMonth();
    }
    public List<CalendarDay> GetToDosInGivenMonth(List<ToDo> toDos)
    {
        return calendar.GetToDosInGivenMonth(toDos);
    }

}
