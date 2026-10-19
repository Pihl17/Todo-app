using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Calendar;
using ToDo_App.Library.Models;
namespace ToDo_App.Library.Input;

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

    /// Updates various variables on the ToDo with the given ID.
    /// </summary>
    /// <param name="toDoId">ID of the todo to update</param>
    /// <param name="title">Sets new title</param>
    /// <param name="description">Sets new description</param>
    /// <param name="deadline">Sets new deadline</param>
    /// <param name="status">Changes the status</param>
    /// <param name="repeat">Sets new RepeatInterval</param>
    /// <param name="checklist">Sets new checklist</param>
    /// <param name="priority">Sets new priority</param>
    /// <returns>The updated ToDo</returns>
    /// <param name="removeDeadline">If true, removes the deadline. The deadline parameter is then ignored</param>
    ToDo? UpdateToDo(Guid toDoId, string? title = null, string? description = null,
        DateTime? deadline = null, ToDoStatus? status = null, RepeatInterval? repeat = null,
        List<ChecklistItem>? checklist = null, int? priority = null, bool removeDeadline = false);

    /// <summary>
    /// Returns the first todo note with the given ID or null if no such todo exists.
    /// </summary>
    /// <param name="toDoId">ID to find the todo with</param>
    /// <returns>The first todo with the given ID or null if none were found</returns>
    ToDo? GetToDo(Guid toDoId);

    /// <summary>
    /// Initializes the selected month to the current month and updates the selectedMonthString accordingly.
    /// </summary>
    public void InitializeSelectedMonth();
    /// <summary>
    /// Returns the selected month as a string.
    /// </summary>
    /// <returns></returns>
    public string GetSelectedMonthString();
    /// <summary>
    /// Updates the selected month to the previous month and updates the selectedMonth accordingly.
    /// </summary>
    public void NextMonth();
    /// <summary>
    /// Updates the selected month to the next month and updates the selectedMonth accordingly.
    /// </summary>
    public void PreviousMonth();
    /// <summary>
    /// Returns a list of CalendarDay objects for the given month and year, each containing the date and the ToDos that have a deadline or repeat on that date.
    /// </summary>
    /// <param name="toDos"></param>
    /// <returns></returns>
    public List<CalendarDay> GetToDosInGivenMonth(List<ToDo> toDos);

}
