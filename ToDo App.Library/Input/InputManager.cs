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
    
    /// <summary>
    /// Constructs the InputManager with all the needed dependencies.
    /// </summary>
    /// <param name="toDoHandler">Object for handling creating and changing todos</param>
    /// <param name="dataHandler">Object for handling the saving and loading of todo list</param>
    /// <param name="toDoListSort">Object for handling the sorting of the todo list</param>
    /// <param name="tagFilter">Object for handling creating a sorted list of tags</param>
    /// <param name="calendar">Object for handling calendar-related operations</param>
    

    public InputManager(ToDoHandler toDoHandler, IDataHandler dataHandler, ToDoListSort toDoListSort, TagFilter tagFilter, CalendarManager calendar)
    {
        this.toDoHandler = toDoHandler;
        this.dataHandler = dataHandler;
        this.calendar = calendar;
        listSorter = toDoListSort;
        this.tagFilter = tagFilter;
    }

    public List<string> GetAllTags(){
        dataHandler.LoadTodoList(out List<ToDo> toDos);
        tagFilter.GenerateLibrary(toDos);
        return tagFilter.TagLibrary;
    }

    public List<ToDo> GetToDoList()
    {
        dataHandler.LoadTodoList(out List<ToDo> toDos);
        toDos = listSorter.SortList(toDos);
        return toDos;
    }

    public ToDo CreateNewToDo(string? title = "Insert title", DateTime? choosenDeadLine = null, RepeatInterval repeat = RepeatInterval.None, int? priority = null, string? description ="", List<string>? tags=null, List<ChecklistItem>? checklist = null)
    {
        if (title.IsWhiteSpace())
            title = "Insert title";

        List<ChecklistItem> items = checklist ?? new List<ChecklistItem>();
        tags = tags ?? new List<string>();
        
        if(priority == null) priority = 0;
        
        ToDo createdToDo = toDoHandler.Create(title, items, priority: priority.Value,
            desc: description, choosenDeadLine: choosenDeadLine, repeat: repeat, tags: tags);

        dataHandler.AddTodo(createdToDo);
        return createdToDo;
    }

    public void DeleteToDo(ToDo todo)
    {
        dataHandler.RemoveTodo(todo.Id);
    }

    public void SetNewSavePath(string path)
    {
        if (dataHandler is JSONDataHandler jsonHandler)
            jsonHandler.SelectSavePath($"{path}\\todoes.json");
    }

    public ToDo? UpdateToDo(Guid toDoId, string? title = null, string? description = null,
        DateTime? deadline = null, ToDoStatus? status = null, RepeatInterval? repeat = null, 
        List<ChecklistItem>? checklist = null, int? priority = null, List<string>? tags = null, bool removeDeadline = false)
    {
        ToDo? toDo = GetToDo(toDoId);
        if (toDo == null)
            return null;

        if (title != null && !title.IsWhiteSpace())
            toDoHandler.EditName(toDo, title);
        if (description != null)
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
        if (tags != null)
            toDoHandler.EditTagList(toDo, tags);
        if (priority != null)
            toDoHandler.EditPriority(toDo, (int)priority);

        dataHandler.UpdateTodo(toDo);
        return toDo;
    }

    public ToDo? GetToDo(Guid toDoId)
    {
        DataQueryResult result = dataHandler.LoadTodo(toDoId, out ToDo todo);
        if (result.IsSuccess)
            return todo;

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
