using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Models;

namespace ToDo_App.UI;

public partial class ToDoDisplay : UserControl
{

    public Guid ToDoId { private set; get; }

    public ToDoDisplay()
    {
        InitializeComponent();
    }

    public ToDoDisplay(ToDo todo)
    {
        InitializeComponent();
        ToDoId = todo.Id;
        toDoTitle.Text = todo.Title;
        toDoDescription.Text = todo.Description;
    }

    /// <summary>
    /// Checks equality of the todo ID of two ToDoDisplays.
    /// </summary>
    /// <param name="obj">Object to compare to</param>
    /// <returns>boolean indicating true if both objects are ToDoDisplays and share the same ID</returns>
    public override bool Equals(object? obj)
    {
        ToDoDisplay? display = obj as ToDoDisplay;
        if (display == null)
            return false;
        return display.ToDoId == ToDoId;
    }

}
