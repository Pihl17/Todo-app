using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Input;
using ToDo_App.Models;

namespace ToDo_App.UI;

public partial class ToDoListDisplay : UserControl
{

    IInputHandling inputHandler;
    
    public ToDoListDisplay()
    {
        InitializeComponent();
    }

    public ToDoListDisplay(IInputHandling input)
    {
        inputHandler = input;
        InitializeComponent();
    }


    public ToDoListDisplay(params ToDoDisplay[] todos)
    {
        InitializeComponent();
        foreach (ToDoDisplay todo in todos)
        {
            listContainer.Controls.Add(todo);
        }
    }

    public ToDoListDisplay(IInputHandling input, params ToDoDisplay[] todos)
    {
        inputHandler = input;
        InitializeComponent();
        foreach (ToDoDisplay todo in todos)
        {
            listContainer.Controls.Add(todo);
        }
    }

    /// <summary>
    /// Grabs the list of todos from the system and updates the list, removing deleted todos and adding new todos.
    /// </summary>
    public void RefreshList()
    {
        listContainer.Controls.Clear();
        List<ToDo> toDos = inputHandler.GetToDoList();
        ToDoDisplay currentDisplay;
        for (int i = 0; i < toDos.Count; i++)
        {
            currentDisplay = new ToDoDisplay(toDos[i]);
            if (!listContainer.Controls.Contains(currentDisplay))
                AddToList(currentDisplay);
        }
    }

    /// <summary>
    /// Returns the list of todos currently on display.
    /// </summary>
    /// <returns></returns>
    public ControlCollection GetToDos()
    {
        return listContainer.Controls;
    }

    public void AddToList(ToDoDisplay todo)
    {
        listContainer.Controls.Add(todo);
    }

    public void RemoveFromList(ToDoDisplay todo)
    {
        if (listContainer.Contains(todo))
        {
            listContainer.Controls.Remove(todo);
        }
    }
}
