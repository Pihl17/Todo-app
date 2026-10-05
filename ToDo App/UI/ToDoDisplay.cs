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
    
    public ToDoDisplay()
    {
        InitializeComponent();
    }

    public ToDoDisplay(ToDo todo)
    {
        toDoTitle.Text = todo.Title;
        toDoDescription.Text = todo.Description; 
    }

}
