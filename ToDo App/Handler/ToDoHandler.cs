using System;
using System.Xml.Linq;
using ToDo_App.Models;


public class ToDoHandler
{
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
    public void EditName(ToDo toDo, string name)
    {
        toDo.Title = name;
    }
    public void EditDescription(ToDo toDo, string desc)
    {
        toDo.Description = desc;
    }
    public void EditDeadline(ToDo toDo, DateTime? choosenDeadLine)
    {
        toDo.Deadline = choosenDeadLine;
    }
    public void EditRepeat(ToDo toDo, RepeatInterval repeat)
    {
        toDo.Repeat = repeat;
    }
    public void EditChecklist(ToDo toDo, List<ChecklistItem> checklist)
    {
        toDo.Checklist = checklist;
    }

    public void Delete(List<ToDo> todos, int index)
    {
        todos.RemoveAt(index);
    }

    public void FinishTask(ToDo toDo)
    {
        toDo.MarkProgress(ToDoStatus.Done);
    }
    public void MarkInProgress(ToDo toDo)
    {
        toDo.MarkProgress(ToDoStatus.InProgress);
    }


}