using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToDo_App.Library.Input;
using ToDo_App.Library.Models;

namespace ToDo_App.UI;

public partial class ToDoDisplay : UserControl
{

    public Guid ToDoId { private set; get; }
    private MainForm parentForm;
    public Label Title { get { return toDoTitle; } }

    /// <summary>
    /// Constructs a ToDoDisplay without any ToDo attatched.
    /// It will have a default Title and Description set and the ID set as the default for Guids.
    /// </summary>
    public ToDoDisplay()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Construsts a ToDoDisplay with a given ToDo, setting the Title, Description and ID to the ToDo's.
    /// </summary>
    /// <param name="todo">The ToDo that the display will describe.</param>
    public ToDoDisplay(ToDo todo)
    {
        InitializeComponent();
        ToDoId = todo.Id;
        toDoTitle.Text = todo.Title;
        toDoDescription.Text = todo.Description;
    }

    /// <summary>
    /// Construsts a ToDoDisplay with a given ToDo, setting the Title, Description and ID to the ToDo's.
    /// </summary>
    /// <param name="todo">The ToDo that the display will describe.</param>
    /// <param name="parentForm">The MainForm parent for handling displaying todo details on click</param>
    public ToDoDisplay(ToDo todo, MainForm parentForm)
    {
        InitializeComponent();
        ToDoId = todo.Id;
        toDoTitle.Text = todo.Title;
        toDoDescription.Text = todo.Description;
        this.parentForm = parentForm;
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

    /// <summary>
    /// Tells the parent form to show the detail view for the display's todo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnClick(object sender, EventArgs e)
    {
        parentForm?.ShowToDoDetailView(ToDoId);
    }

}
