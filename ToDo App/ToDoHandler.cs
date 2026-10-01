using System;
using System.Xml.Linq;

public class ToDoHandler
{

    public ToDo Edit(ToDo toDo, string newName, string newDescription, DateTime newDeadLine)
    {

        toDo.taskName = newName;
        toDo.description = newDescription;
        toDo.deadLine = newDeadLine;
        return toDo;
    }

    public ToDo Create(string name, string desc, DateTime choosenDeadLine)
    {
        var toDo = new ToDo(name, desc, choosenDeadLine);

        return toDo;
    }

    public void Delete(List<ToDo> todos, int index)
    {
        //Har vi brug for dette?
        todos.RemoveAt(index);

        //Nok kun hvis vi også gemmer?
        //Save(todos) ?
    }

    public void FinishTask(ToDo toDo)
    {
        toDo.toDoFinished = true;
    }


}

public class ToDo
{
    //Userdefined variables
    public string taskName {  get; set; }
    public string description { get; set; }
    public DateTime? deadLine { get; set; }
    public bool toDoFinished { get; set; } = false;

    //Systemdefined variables
    public DateTime creationDate { get; set; } = DateTime.Now;
    public DateTime? finishDate { get; set; } = null;


    public ToDo (string name, string desc, DateTime choosenDeadLine){
        taskName = name;
        description = desc;
        deadLine = choosenDeadLine;
        
    }


}