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
    /// <summary>
    /// The input handler that this object calls to connect with the backend.
    /// </summary>
    public IInputHandling inputHandler;
    
    /// <summary>
    /// Constructs an empty ToDoListDisplay. 
    /// </summary>
    public ToDoListDisplay()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Constructs an empty ToDoListDisplay and sets its InputHandler.
    /// </summary>
    /// <param name="input">The InputHandler that the list display will call</param>
    public ToDoListDisplay(IInputHandling input)
    {
        inputHandler = input;
        InitializeComponent();
    }

    /// <summary>
    /// Constructs a ToDoListDisplay with the array of ToDoDisplays added to it.
    /// </summary>
    /// <param name="todos">ToDoDisplays that will be added to the list</param>
    public ToDoListDisplay(params ToDoDisplay[] todos)
    {
        InitializeComponent();
        listContainer.Controls.AddRange(todos);
    }

    /// <summary>
    /// Constructs a ToDoListDisplay with the array of ToDoDisplays added to it, and sets its InputHandler.
    /// </summary>
    /// <param name="input">The InputHandler that the list display will call</param>
    /// <param name="todos">ToDoDisplays that will be added to the list</param>
    public ToDoListDisplay(IInputHandling input, params ToDoDisplay[] todos)
    {
        inputHandler = input;
        InitializeComponent();
        listContainer.Controls.AddRange(todos);
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
    /// <returns>A ControlCollection containing the child ToDoDisplays</returns>
    public ControlCollection GetToDos()
    {
        return listContainer.Controls;
    }

    /// <summary>
    /// Adds a single ToDoDisplay to the list
    /// </summary>
    /// <param name="todo">ToDoDisplay to add to the list</param>
    public void AddToList(ToDoDisplay todo)
    {
        listContainer.Controls.Add(todo);
    }

    /// <summary>
    /// Removes a ToDoDisplay from the list, if it is on it.
    /// </summary>
    /// <param name="todo">ToDoDisplay to be removed from the list</param>
    public void RemoveFromList(ToDoDisplay todo)
    {
        if (listContainer.Contains(todo))
        {
            listContainer.Controls.Remove(todo);
        }
    }
}
