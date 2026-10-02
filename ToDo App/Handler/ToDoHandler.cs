using System;
using System.Xml.Linq;
using ToDo_App.Models;


public class ToDoHandler
{
    ///<summary>
    /// Creates a new ToDo item.
    /// </summary>
    /// <param name="name">The name of the ToDo item.</param>
    /// <param name="checklist">The checklist for the ToDo item.</param>
    /// <param name="desc">The description of the ToDo item.</param>
    /// <param name="choosenDeadLine">The chosen deadline for the ToDo item.</param>
    /// <param name="repeat">The repeat interval for the ToDo item.</param>
    /// <returns>The created ToDo item.</returns>
    public ToDo Create(string name, List<ChecklistItem> checklist = null, string desc = "", DateTime? choosenDeadLine = null, RepeatInterval repeat = RepeatInterval.None)
    {
        var toDo = new ToDo();
        toDo.Title = name;
        toDo.Description = desc;
        toDo.Deadline = choosenDeadLine;
        toDo.Repeat = repeat;
        if (checklist != null)
        {
            toDo.Checklist = checklist;
        }

        return toDo;
    }

    ///<summary>
    /// Edits the name of a ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to edit.</param>
    /// <param name="name">The new name for the ToDo item.</param>
    public void EditName(ToDo toDo, string name)
    {
        toDo.Title = name;
    }
    ///<summary>
    /// Edits the description of a ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to edit.</param>
    /// <param name="desc">The new description for the ToDo item.</param>
    public void EditDescription(ToDo toDo, string desc)
    {
        toDo.Description = desc;
    }
    ///<summary>
    /// Edits the deadline of a ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to edit.</param>
    /// <param name="choosenDeadLine">The new deadline for the ToDo item.</param>
    public void EditDeadline(ToDo toDo, DateTime? choosenDeadLine)
    {
        toDo.Deadline = choosenDeadLine;
    }
    ///<summary>
    /// Edits the repeat interval of a ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to edit.</param>
    /// <param name="repeat">The new repeat interval for the ToDo item.</param>
    public void EditRepeat(ToDo toDo, RepeatInterval repeat)
    {
        toDo.Repeat = repeat;
    }

    /// <summary>
    /// Edits the checklist of a ToDo item. 
    /// </summary>
    /// <param name="toDo">The ToDo item to edit.</param>   
    /// <param name="checklist">The new checklist for the ToDo item.</param>
    public void EditChecklist(ToDo toDo, List<ChecklistItem> checklist)
    {
        toDo.Checklist = checklist;
    }

    /// <summary>
    /// Deletes a ToDo item from the list of ToDo items.
    /// </summary>
    /// <param name="todos">The list of ToDo items.</param>
    /// <param name="index">The index of the ToDo item to delete.</param>
    public void Delete(List<ToDo> todos, Guid id)
    {
        var toDoToRemove = todos.FirstOrDefault(t => t.Id == id);
        if (toDoToRemove != null)
        {
            todos.Remove(toDoToRemove);
        }
    }
    /// <summary>
    /// Marks a ToDo item as done.
    /// </summary>
    /// <param name="toDo">The ToDo item to mark as done.</param>
    public void FinishTask(ToDo toDo)
    {
        toDo.MarkProgress(ToDoStatus.Done);
    }
    /// <summary>
    /// Marks a ToDo item as in progress.
    /// </summary>
    /// <param name="toDo">The ToDo item to mark as in progress.</param>
    public void MarkInProgress(ToDo toDo)
    {
        toDo.MarkProgress(ToDoStatus.InProgress);
    }


}